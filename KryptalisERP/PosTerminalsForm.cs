using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class PosTerminalsForm : Form
    {
        // --- Controls ---
        private TextBox txtTerminalName, txtTid, txtIpAddress, txtSerialNumber;
        private ComboBox cmbConnectionType, cmbStatus, cmbProvider;
        private TextBox txtApiKey, txtApiSecret; // For Provider Credentials
        private CheckBox chkIsActive;
        private Button btnSave, btnDelete, btnClose, btnTestPing;
        private Label lblContextInfo, lblProviderHint;

        // --- State ---
        private int _branchId;
        private int _companyId;
        private int? _terminalId;
        private bool _isNewRecord;

        // --- Colors ---
        private readonly Color COLOR_PRIMARY = Color.FromArgb(155, 89, 182); // Violet
        private readonly Color COLOR_SUCCESS = Color.FromArgb(46, 204, 113);
        private readonly Color COLOR_DANGER = Color.FromArgb(231, 76, 60);
        private readonly Color COLOR_INFO = Color.FromArgb(52, 152, 219);
        private readonly Color COLOR_WARNING = Color.FromArgb(243, 156, 18);

        /// <summary>
        /// Constructor called from NewSettingsForm
        /// </summary>
        public PosTerminalsForm(int branchId, int companyId, int? terminalId = null)
        {
            _branchId = branchId;
            _companyId = companyId;
            _terminalId = terminalId;
            _isNewRecord = !terminalId.HasValue;

            InitializeUI();

            this.Load += async (s, e) =>
            {
                await LoadContextAsync();
                if (_isNewRecord) PrepareForNewInsert();
                else LoadExistingTerminal(_terminalId.Value);
            };
        }

        private void InitializeUI()
        {
            this.Text = "💳 Ρύθμιση POS Τερματικού";
            this.Size = new Size(700, 650); // Increased size for more fields
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var pnlMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), AutoScroll = true };

            // --- SECTION 1: CONTEXT INFO ---
            var grpContext = new GroupBox
            {
                Text = "📍 Τοποθεσία Εγκατάστασης",
                Location = new Point(10, 10),
                Size = new Size(640, 60),
                ForeColor = COLOR_INFO
            };
            lblContextInfo = new Label
            {
                Text = "Φόρτωση...",
                Location = new Point(15, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = COLOR_PRIMARY
            };
            grpContext.Controls.Add(lblContextInfo);
            pnlMain.Controls.Add(grpContext);

            // --- SECTION 2: PROVIDER SELECTION (NEW!) ---
            var grpProvider = new GroupBox
            {
                Text = "🔗 Πάροχος Πληρωμών (Acquirer)",
                Location = new Point(10, 80),
                Size = new Size(640, 120),
                ForeColor = COLOR_WARNING
            };

            int py = 25;
            AddLabel(grpProvider, "Πάροχος:", 15, py);
            cmbProvider = new ComboBox { Location = new Point(120, py - 2), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbProvider.Items.AddRange(new object[] {
                "MYPOS",
                "VIVA_COM",
                "CARDLINK",
                "EDPS",
                "EVERYPAY",
                "WORLDLINE",
                "EPAY",
                "NBG_PAY"
            });
            cmbProvider.SelectedIndexChanged += CmbProvider_SelectedIndexChanged;
            grpProvider.Controls.Add(cmbProvider);

            // Dynamic Fields based on provider (Hidden by default, shown via logic)
            AddLabel(grpProvider, "API Key / Merchant ID:", 15, py + 35);
            txtApiKey = new TextBox { Location = new Point(180, py + 32), Size = new Size(200, 26), Font = new Font("Segoe UI", 9.5F) };
            grpProvider.Controls.Add(txtApiKey);

            AddLabel(grpProvider, "API Secret / Password:", 15, py + 65);
            txtApiSecret = new TextBox { Location = new Point(180, py + 62), Size = new Size(200, 26), UseSystemPasswordChar = true, Font = new Font("Segoe UI", 9.5F) };
            grpProvider.Controls.Add(txtApiSecret);

            lblProviderHint = new Label
            {
                Text = "💡 Συμβουλή: Βρες τα credentials στο Portal του παρόχου σου.",
                Location = new Point(400, py + 32),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
            grpProvider.Controls.Add(lblProviderHint);

            pnlMain.Controls.Add(grpProvider);

            // --- SECTION 3: TERMINAL DETAILS ---
            var grpDetails = new GroupBox
            {
                Text = "⚙️ Στοιχεία Συσκευής & TID",
                Location = new Point(10, 210),
                Size = new Size(640, 200)
            };

            int y = 25;
            int lx = 15;
            int lw = 140;
            int cw = 450;

            // 1. Terminal Name
            AddLabel(grpDetails, "Όνομα Τερματικού:", lx, y);
            txtTerminalName = AddTextBox(grpDetails, lx + lw, y, cw);
            y += 35;

            // 2. TID (Critical)
            AddLabel(grpDetails, "Terminal ID (TID):", lx, y);
            txtTid = AddTextBox(grpDetails, lx + lw, y, cw);
            txtTid.PlaceholderText = "Π.χ. 12345678 (Μοναδικό ID από τράπεζα)";
            y += 35;

            // 3. Connection Type
            AddLabel(grpDetails, "Τύπος Σύνδεσης:", lx, y);
            cmbConnectionType = new ComboBox { Location = new Point(lx + lw, y), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbConnectionType.Items.AddRange(new object[] { "IP", "USB", "BLUETOOTH", "SERIAL" });
            grpDetails.Controls.Add(cmbConnectionType);

            // IP Address next to it
            AddLabel(grpDetails, "IP Address:", lx + 170, y);
            txtIpAddress = AddTextBox(grpDetails, lx + 260, y, 150);
            y += 35;

            // 4. Serial Number
            AddLabel(grpDetails, "Serial No.:", lx, y);
            txtSerialNumber = AddTextBox(grpDetails, lx + lw, y, cw);

            pnlMain.Controls.Add(grpDetails);

            // --- SECTION 4: STATUS & ACTIONS ---
            var grpStatus = new GroupBox
            {
                Text = "🔌 Κατάσταση Λειτουργίας",
                Location = new Point(10, 420),
                Size = new Size(640, 100)
            };

            AddLabel(grpStatus, "Κατάσταση:", lx, 25);
            cmbStatus = new ComboBox { Location = new Point(lx + lw, 22), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F) };
            cmbStatus.Items.AddRange(new object[] { "ACTIVE", "INACTIVE", "MAINTENANCE" });
            grpStatus.Controls.Add(cmbStatus);

            chkIsActive = new CheckBox
            {
                Text = "Ενεργό για Πωλήσεις",
                Location = new Point(lx + 180, 25),
                AutoSize = true,
                Checked = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = COLOR_SUCCESS
            };
            grpStatus.Controls.Add(chkIsActive);

            btnTestPing = CreateButton("📡 Test Ping / Reachability", lx, 60, 180, 30, Color.Gray);
            btnTestPing.Click += BtnTestPing_Click;
            grpStatus.Controls.Add(btnTestPing);

            pnlMain.Controls.Add(grpStatus);

            // --- FOOTER BUTTONS ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.LightGray, Padding = new Padding(10) };

            btnSave = CreateButton("💾 Αποθήκευση", 10, 10, 130, 40, COLOR_PRIMARY);
            btnSave.Click += SaveTerminal;
            pnlFooter.Controls.Add(btnSave);

            btnDelete = CreateButton("🗑️ Διαγραφή", 150, 10, 110, 40, COLOR_DANGER);
            btnDelete.Visible = !_isNewRecord;
            btnDelete.Click += DeleteTerminal;
            pnlFooter.Controls.Add(btnDelete);

            btnClose = CreateButton("❌ Κλείσιμο", 270, 10, 100, 40, Color.DarkGray);
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
        }

        // ==========================================
        // LOGIC METHODS
        // ==========================================

        private void CmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            string provider = cmbProvider.SelectedItem?.ToString();

            // Show/hide hints or change labels based on provider requirements
            if (provider == "MYPOS")
            {
                lblProviderHint.Text = "💡 myPOS: Χρησιμοποίησε το 'Merchant ID' και το 'API Key' από το dashboard.";
            }
            else if (provider == "VIVA_COM")
            {
                lblProviderHint.Text = "💡 Viva.com: Συνήθως χρειάζεσαι 'Store ID' και 'Secure Key'.";
            }
            else if (provider == "CARDLINK")
            {
                lblProviderHint.Text = "💡 Cardlink: Έλεγξε για 'Terminal ID' και 'Activation Code'.";
            }
            else
            {
                lblProviderHint.Text = "💡 Γενική συμβουλή: Βρες τα credentials στο Portal του παρόχου σου.";
            }
        }

        private async Task LoadContextAsync()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var company = await conn.QueryFirstOrDefaultAsync<dynamic>("SELECT CompanyName FROM Companies WHERE Id = @Id", new { Id = _companyId });
                var branch = await conn.QueryFirstOrDefaultAsync<dynamic>("SELECT BranchName FROM CompanyBranches WHERE Id = @Id", new { Id = _branchId });

                string companyName = company?.CompanyName ?? "Άγνωστη Εταιρεία";
                string branchName = branch?.BranchName ?? "Άγνωστο Υποκατάστημα";

                lblContextInfo.Text = $"🏢 {companyName} > 🏬 {branchName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης πλαισίου: " + ex.Message);
            }
        }

        private void PrepareForNewInsert()
        {
            txtTerminalName.Focus();
            cmbConnectionType.SelectedIndex = 0; // Default IP
            cmbStatus.SelectedIndex = 0;         // Default ACTIVE
            cmbProvider.SelectedIndex = 0;       // Default MYPOS
            txtIpAddress.Text = "192.168.1.";    // Hint
            txtTid.Text = "";                    // Empty TID

            // Clear sensitive fields
            txtApiKey.Text = "";
            txtApiSecret.Text = "";
        }

        private void LoadExistingTerminal(int id)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // Note: Ensure your DB table has columns for Provider, ApiKey, etc.
                // If not, you might need to alter the table first.
                var term = conn.QueryFirstOrDefault<PostTerminalDto>(
                    @"SELECT Id, CompanyId, BranchId, TerminalName, Tid, ConnectionType, IpAddress, SerialNumber, Status, IsActive, ProviderCode, ApiKeyEncrypted 
                      FROM POSTerminals WHERE Id = @Id",
                    new { Id = id });

                if (term == null) return;

                txtTerminalName.Text = term.TerminalName;
                txtTid.Text = term.Tid;
                txtIpAddress.Text = term.IpAddress ?? "";
                txtSerialNumber.Text = term.SerialNumber ?? "";

                // Set Combos
                SafeSetCombo(cmbConnectionType, term.ConnectionType);
                SafeSetCombo(cmbStatus, term.Status);
                SafeSetCombo(cmbProvider, term.ProviderCode ?? "MYPOS");

                // Decrypt and show keys (Mock decryption for now)
                if (!string.IsNullOrEmpty(term.ApiKeyEncrypted))
                {
                    // TODO: Implement actual decryption here before setting text
                    txtApiKey.Text = "[Encrypted Data Loaded]";
                    txtApiSecret.Text = "********";
                }

                chkIsActive.Checked = term.IsActive;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης τερματικού: " + ex.Message);
            }
        }

        private void SafeSetCombo(ComboBox combo, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (combo.Items.Contains(value))
                    combo.SelectedItem = value;
                else
                    combo.Items.Insert(0, value);
            }
        }

        private void SaveTerminal(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtTerminalName.Text)) { ShowError("Βάλε Όνομα Τερματικού!"); return; }
            if (string.IsNullOrWhiteSpace(txtTid.Text)) { ShowError("Το TID (Terminal ID) είναι υποχρεωτικό!"); return; }
            if (cmbProvider.SelectedItem == null) { ShowError("Επίλεξε Πάροχο!"); return; }

            // Check Unique TID
            using (var checkConn = new SqlConnection(GetConnectionString()))
            {
                var exists = checkConn.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM POSTerminals WHERE Tid = @Tid AND (@Id IS NULL OR Id != @Id)",
                    new { Tid = txtTid.Text.Trim(), Id = _terminalId });

                if (exists > 0)
                {
                    ShowError($"Το TID '{txtTid.Text}' υπάρχει ήδη σε άλλο τερματικό!");
                    return;
                }
            }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                string providerCode = cmbProvider.SelectedItem.ToString();
                string connType = cmbConnectionType.SelectedItem?.ToString() ?? "IP";
                string status = cmbStatus.SelectedItem?.ToString() ?? "ACTIVE";
                bool isActive = chkIsActive.Checked && status == "ACTIVE";

                // Encrypt API Keys before saving (Placeholder for real encryption)
                string encryptedKey = EncryptString(txtApiKey.Text);
                string encryptedSecret = EncryptString(txtApiSecret.Text);

                if (_isNewRecord)
                {
                    conn.Execute(@"INSERT INTO POSTerminals (
                                        CompanyId, BranchId, TerminalName, Tid, 
                                        ConnectionType, IpAddress, SerialNumber, 
                                        Status, IsActive, ProviderCode, ApiKeyEncrypted, CreatedAt, UpdatedAt
                                    ) VALUES (
                                        @Cid, @Bid, @Name, @Tid, 
                                        @ConnType, @Ip, @Sn, 
                                        @Status, @Active, @Prov, @Key, GETDATE(), GETDATE()
                                    ); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new
                        {
                            Cid = _companyId,
                            Bid = _branchId,
                            Name = txtTerminalName.Text.Trim(),
                            Tid = txtTid.Text.Trim(),
                            ConnType = connType,
                            Ip = txtIpAddress.Text.Trim(),
                            Sn = txtSerialNumber.Text.Trim(),
                            Status = status,
                            Active = isActive,
                            Prov = providerCode,
                            Key = encryptedKey // Storing combined or just key depending on schema
                        }, tran);
                }
                else
                {
                    conn.Execute(@"UPDATE POSTerminals SET 
                                        TerminalName=@Name, Tid=@Tid, 
                                        ConnectionType=@ConnType, IpAddress=@Ip, 
                                        SerialNumber=@Sn, Status=@Status, 
                                        IsActive=@Active, ProviderCode=@Prov, ApiKeyEncrypted=@Key,
                                        UpdatedAt=GETDATE() 
                                    WHERE Id=@Id",
                        new
                        {
                            Id = _terminalId,
                            Name = txtTerminalName.Text.Trim(),
                            Tid = txtTid.Text.Trim(),
                            ConnType = connType,
                            Ip = txtIpAddress.Text.Trim(),
                            Sn = txtSerialNumber.Text.Trim(),
                            Status = status,
                            Active = isActive,
                            Prov = providerCode,
                            Key = encryptedKey
                        }, tran);
                }

                tran.Commit();
                MessageBox.Show("✅ Το τερματικό αποθηκεύτηκε επιτυχώς!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποθήκευσης:\n" + ex.Message, "Λάθος", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteTerminal(object sender, EventArgs e)
        {
            if (!_terminalId.HasValue) return;

            if (MessageBox.Show("Σίγουρα θέλεις να διαγράψεις αυτό το τερματικό;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());

                    // Safety Check: Transactions
                    var txCount = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM POSTransactions WHERE TerminalId = @Id", new { Id = _terminalId });
                    if (txCount > 0)
                    {
                        MessageBox.Show($"⚠️ Δεν μπορείτε να διαγράψετε το τερματικό γιατί έχει {txCount} συναλλαγές.\n\nΑλλάξτε την κατάσταση σε INACTIVE.", "Απαγόρευση", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    conn.Execute("DELETE FROM POSTerminals WHERE Id = @Id", new { Id = _terminalId });
                    MessageBox.Show("✅ Διαγράφηκε.", "Επιτυχία");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Σφάλμα διαγραφής: " + ex.Message);
                }
            }
        }

        private void BtnTestPing_Click(object sender, EventArgs e)
        {
            string ip = txtIpAddress.Text.Trim();
            if (string.IsNullOrEmpty(ip) || ip == "192.168.1.")
            {
                MessageBox.Show("Βάλε σωστό IP Address πρώτα.");
                return;
            }

            Cursor = Cursors.WaitCursor;
            btnTestPing.Enabled = false;
            btnTestPing.Text = "⏳ Έλεγχος...";

            try
            {
                var ping = new Ping();
                var reply = ping.Send(ip, 2000);

                if (reply.Status == IPStatus.Success)
                {
                    MessageBox.Show($"✅ Επιτυχής σύνδεση!\nLatency: {reply.RoundtripTime}ms", "Success");
                }
                else
                {
                    MessageBox.Show($"❌ Δεν ανταποκρίνεται.\nStatus: {reply.Status}", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα Ping: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnTestPing.Enabled = true;
                btnTestPing.Text = "📡 Test Ping / Reachability";
            }
        }

        // ==========================================
        // HELPERS & SECURITY MOCK
        // ==========================================

        private string EncryptString(string plainText)
        {
            // TODO: Replace with real AES Encryption using a secure key stored in appsettings.json or DPAPI
            // For now, returning Base64 encoded string as a placeholder
            if (string.IsNullOrEmpty(plainText)) return null;
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }

        private void AddLabel(Control parent, string text, int x, int y)
        {
            parent.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
        }

        private TextBox AddTextBox(Control parent, int x, int y, int width)
        {
            var tb = new TextBox { Location = new Point(x, y), Size = new Size(width, 26), Font = new Font("Segoe UI", 10F) };
            parent.Controls.Add(tb);
            return tb;
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP; User Id=sa; Password=epsilonsa; TrustServerCertificate=True;";

        // DTO updated with new fields
        public class PostTerminalDto
        {
            public int Id { get; set; }
            public int CompanyId { get; set; }
            public int BranchId { get; set; }
            public string TerminalName { get; set; }
            public string Tid { get; set; }
            public string ConnectionType { get; set; }
            public string IpAddress { get; set; }
            public string SerialNumber { get; set; }
            public string Status { get; set; }
            public bool IsActive { get; set; }
            public string ProviderCode { get; set; } // NEW
            public string ApiKeyEncrypted { get; set; } // NEW
        }
    }
}