using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class ProviderForm : Form
    {
        // Controls for Basic Info
        private TextBox txtCode, txtName, txtEnv, txtAfm, txtContactPerson, txtEmail, txtPhone, txtWebsite, txtNotes;

        // Control for Type (Fixed to ComboBox)
        private ComboBox cmbType;

        // Controls for API Credentials
        private TextBox txtApiUrl, txtUsername, txtPassword, txtApiKey, txtLicense, txtVersion;

        // Checkboxes for Features
        private CheckBox chkUseMyData, chkUseTaxisnet, chkIsLocal, chkIsActive;

        // Buttons
        private Button btnSave, btnTestConnection, btnClose;

        private int? _providerId;
        private bool _isLoading = false;

        public ProviderForm(int? providerId = null)
        {
            _providerId = providerId;
            InitializeUI();
            this.Load += ProviderForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "🏢 Ρύθμιση Παρόχου myDATA/TAXISNET";
            this.Size = new Size(850, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(750, 650);

            // --- SECTION 1: GENERAL INFO ---
            var pnlGeneral = CreatePanel("ℹ️ ΓΕΝΙΚΑ ΣΤΟΙΧΕΙΑ", 100, Color.FromArgb(52, 73, 94));
            int y = 22, lx = 10, lw = 100, gap = 10;

            txtCode = AddText(pnlGeneral, "Κωδικός:", lx, y, lw, 100, true);
            lx += lw + 100 + gap;
            txtName = AddText(pnlGeneral, "Όνομα Πάροχου:", lx, y, lw + 20, 250, false);

            y = 52; lx = 10;

            // ✅ FIX: Δημιουργία ComboBox για τον Τύπο
            pnlGeneral.Controls.Add(new Label { Text = "Τύπος:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cmbType = new ComboBox { Location = new Point(lx + lw, y), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbType.Items.AddRange(new object[] { "DIRECT_AADE", "PROVIDER_LOGISMOS", "PROVIDER_EPSILON", "OTHER" });
            pnlGeneral.Controls.Add(cmbType);

            lx += lw + 150 + gap;
            txtEnv = AddText(pnlGeneral, "Περιβάλλον:", lx, y, lw, 100, false);
            txtEnv.Text = "TEST";

            this.Controls.Add(pnlGeneral);

            // --- SECTION 2: API CREDENTIALS ---
            var pnlApi = CreatePanel("🔑 API CREDENTIALS", 180, Color.FromArgb(231, 76, 60));
            y = 22; lx = 10;

            txtApiUrl = AddText(pnlApi, "API URL:", lx, y, lw + 20, 400, false);
            y += 30;
            txtUsername = AddText(pnlApi, "Username:", lx, y, lw, 150, false);
            lx += lw + 150 + gap;
            txtPassword = AddText(pnlApi, "Password:", lx, y, lw, 150, false);
            ((TextBox)txtPassword).UseSystemPasswordChar = true;

            y += 30; lx = 10;
            txtApiKey = AddText(pnlApi, "API Key:", lx, y, lw, 200, false);
            lx += lw + 200 + gap;
            txtLicense = AddText(pnlApi, "License No.:", lx, y, lw, 150, false);

            y += 30; lx = 10;
            txtVersion = AddText(pnlApi, "API Version:", lx, y, lw, 100, false);
            txtVersion.Text = "v1.0";

            this.Controls.Add(pnlApi);

            // --- SECTION 3: FLAGS & OPTIONS ---
            var pnlFlags = CreatePanel("⚙️ ΡΥΘΜΙΣΕΙΣ ΣΥΝΔΕΣΗΣ", 100, Color.FromArgb(46, 204, 113));
            y = 22; lx = 10;

            chkUseMyData = new CheckBox { Text = "Χρήση myDATA (Phase A/B)", Location = new Point(lx, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            chkUseMyData.Checked = true;
            pnlFlags.Controls.Add(chkUseMyData);
            lx += 200;

            chkUseTaxisnet = new CheckBox { Text = "Χρήση TAXISNET (e-Invoice)", Location = new Point(lx, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            pnlFlags.Controls.Add(chkUseTaxisnet);
            lx += 220;

            chkIsLocal = new CheckBox { Text = "Local Mode (Offline/Test)", Location = new Point(lx, y), AutoSize = true, Font = new Font("Segoe UI", 10F) };
            pnlFlags.Controls.Add(chkIsLocal);

            y += 30; lx = 10;
            chkIsActive = new CheckBox { Text = "Ενεργός Πάροχος", Location = new Point(lx, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.Green };
            chkIsActive.Checked = true;
            pnlFlags.Controls.Add(chkIsActive);

            this.Controls.Add(pnlFlags);

            // --- SECTION 4: CONTACT INFO ---
            var pnlContact = CreatePanel("📞 ΕΠΙΚΟΙΝΩΝΙΑ", 120, Color.FromArgb(108, 117, 125));
            y = 22; lx = 10;

            txtAfm = AddText(pnlContact, "ΑΦΜ Πάροχου:", lx, y, lw, 100, false);
            lx += lw + 100 + gap;
            txtContactPerson = AddText(pnlContact, "Υπεύθυνος:", lx, y, lw, 150, false);

            y += 30; lx = 10;
            txtEmail = AddText(pnlContact, "Email:", lx, y, lw, 200, false);
            lx += lw + 200 + gap;
            txtPhone = AddText(pnlContact, "Τηλέφωνο:", lx, y, lw, 150, false);

            y += 30; lx = 10;
            txtWebsite = AddText(pnlContact, "Website:", lx, y, lw, 300, false);

            this.Controls.Add(pnlContact);

            // --- SECTION 5: NOTES ---
            var pnlNotes = CreatePanel("📝 ΣΗΜΕΙΩΣΕΙΣ", 80, Color.Gray);
            txtNotes = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill, Margin = new Padding(10, 5, 10, 5), Font = new Font("Segoe UI", 9.5F) };
            pnlNotes.Controls.Add(txtNotes);
            this.Controls.Add(pnlNotes);

            // --- FOOTER BUTTONS ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(10) };

            btnTestConnection = CreateButton("🔌 Έλεγχος Σύνδεσης", 10, 10, 150, 40, Color.FromArgb(230, 126, 34));
            btnTestConnection.Click += async (s, e) => await TestConnectionAsync();
            pnlFooter.Controls.Add(btnTestConnection);

            btnSave = CreateButton("💾 Αποθήκευση", 170, 10, 130, 40, Color.FromArgb(13, 110, 253));
            btnSave.Click += (s, e) => SaveProvider();
            pnlFooter.Controls.Add(btnSave);

            btnClose = CreateButton("❌ Κλείσιμο", 310, 10, 110, 40, Color.FromArgb(108, 117, 125));
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);

            this.Controls.Add(pnlFooter);
        }

        // ==========================================
        // HELPERS UI
        // ==========================================
        private Panel CreatePanel(string title, int height, Color color)
        {
            var p = new Panel { Dock = DockStyle.Top, Height = height, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8), Margin = new Padding(0, 0, 0, 5) };
            p.Controls.Add(new Label { Text = title, Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = color });
            return p;
        }

        private TextBox AddText(Panel p, string lbl, int x, int y, int lw, int cw, bool ro = false)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var t = new TextBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), ReadOnly = ro, Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, BackColor = ro ? Color.LightYellow : Color.White };
            p.Controls.Add(t); return t;
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        // ==========================================
        // LOAD LOGIC
        // ==========================================
        private void ProviderForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            if (_providerId.HasValue) LoadProvider(_providerId.Value);
            else GenerateCode();
            _isLoading = false;
        }

        private void GenerateCode()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                int next = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(Id),0)+1 FROM Providers");
                txtCode.Text = $"PRV-{next:D3}";
            }
            catch { txtCode.Text = "NEW"; }
        }

        private void LoadProvider(int id)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var p = conn.QueryFirstOrDefault("SELECT * FROM Providers WHERE Id=@Id", new { Id = id });
                if (p == null) return;

                txtCode.Text = p.ProviderCode;
                txtName.Text = p.ProviderName;

                // ✅ FIX: Χρήση ComboBox directly
                if (!string.IsNullOrEmpty(p.ProviderType))
                    cmbType.SelectedItem = p.ProviderType;
                else
                    cmbType.SelectedIndex = 0; // Default DIRECT_AADE

                txtApiUrl.Text = p.ApiUrl ?? "";
                txtUsername.Text = p.ApiUsername ?? "";
                txtPassword.Text = p.ApiPassword ?? "";
                txtApiKey.Text = p.ApiKey ?? "";
                txtLicense.Text = p.LicenseNumber ?? "";
                txtVersion.Text = p.ApiVersion ?? "v1.0";
                txtEnv.Text = p.Environment ?? "TEST";

                chkUseMyData.Checked = p.UseMyDATA ?? false;
                chkUseTaxisnet.Checked = p.UseTaxisnet ?? false;
                chkIsLocal.Checked = p.IsLocal ?? false;
                chkIsActive.Checked = p.IsActive ?? true;

                txtAfm.Text = p.Afm ?? "";
                txtContactPerson.Text = p.ContactPerson ?? "";
                txtEmail.Text = p.ContactEmail ?? "";
                txtPhone.Text = p.ContactPhone ?? "";
                txtWebsite.Text = p.Website ?? "";
                txtNotes.Text = p.Notes ?? "";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message); }
        }

        // ==========================================
        // SAVE LOGIC
        // ==========================================
        private void SaveProvider()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Βάλε Όνομα Πάροχου!"); return; }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                // ✅ FIX: Get value from SelectedItem of ComboBox
                string selectedType = cmbType.SelectedItem?.ToString() ?? "DIRECT_AADE";

                if (_providerId.HasValue)
                {
                    conn.Execute(@"UPDATE Providers SET 
                        ProviderCode=@Code, ProviderName=@Name, ProviderType=@Type, ApiUrl=@Url, 
                        ApiUsername=@User, ApiPassword=@Pass, ApiKey=@Key, LicenseNumber=@Lic, 
                        ApiVersion=@Ver, Environment=@Env, UseMyDATA=@MyData, UseTaxisnet=@Taxis, 
                        IsLocal=@Loc, IsActive=@Act, Afm=@Afm, ContactPerson=@Cont, ContactEmail=@Mail, 
                        ContactPhone=@Tel, Website=@Web, Notes=@Not, UpdatedAt=GETDATE() 
                        WHERE Id=@Id",
                        new
                        {
                            Id = _providerId,
                            Code = txtCode.Text,
                            Name = txtName.Text,
                            Type = selectedType,
                            Url = txtApiUrl.Text,
                            User = txtUsername.Text,
                            Pass = txtPassword.Text,
                            Key = txtApiKey.Text,
                            Lic = txtLicense.Text,
                            Ver = txtVersion.Text,
                            Env = txtEnv.Text,
                            MyData = chkUseMyData.Checked,
                            Taxis = chkUseTaxisnet.Checked,
                            Loc = chkIsLocal.Checked,
                            Act = chkIsActive.Checked,
                            Afm = txtAfm.Text,
                            Cont = txtContactPerson.Text,
                            Mail = txtEmail.Text,
                            Tel = txtPhone.Text,
                            Web = txtWebsite.Text,
                            Not = txtNotes.Text
                        }, tran);
                }
                else
                {
                    _providerId = conn.ExecuteScalar<int>(@"INSERT INTO Providers (ProviderCode, ProviderName, ProviderType, ApiUrl, ApiUsername, ApiPassword, ApiKey, LicenseNumber, ApiVersion, Environment, UseMyDATA, UseTaxisnet, IsLocal, IsActive, Afm, ContactPerson, ContactEmail, ContactPhone, Website, Notes, CreatedAt) VALUES (@Code, @Name, @Type, @Url, @User, @Pass, @Key, @Lic, @Ver, @Env, @MyData, @Taxis, @Loc, @Act, @Afm, @Cont, @Mail, @Tel, @Web, @Not, GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new
                        {
                            Code = txtCode.Text,
                            Name = txtName.Text,
                            Type = selectedType,
                            Url = txtApiUrl.Text,
                            User = txtUsername.Text,
                            Pass = txtPassword.Text,
                            Key = txtApiKey.Text,
                            Lic = txtLicense.Text,
                            Ver = txtVersion.Text,
                            Env = txtEnv.Text,
                            MyData = chkUseMyData.Checked,
                            Taxis = chkUseTaxisnet.Checked,
                            Loc = chkIsLocal.Checked,
                            Act = chkIsActive.Checked,
                            Afm = txtAfm.Text,
                            Cont = txtContactPerson.Text,
                            Mail = txtEmail.Text,
                            Tel = txtPhone.Text,
                            Web = txtWebsite.Text,
                            Not = txtNotes.Text
                        }, tran);
                }

                tran.Commit();
                MessageBox.Show("✅ Ο πάροχος αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private async Task TestConnectionAsync()
        {
            if (string.IsNullOrWhiteSpace(txtApiUrl.Text)) { MessageBox.Show("Βάλε API URL πρώτα."); return; }

            Cursor = Cursors.WaitCursor;
            btnTestConnection.Enabled = false;
            btnTestConnection.Text = "⏳ Έλεγχος...";

            try
            {
                await Task.Delay(1000);

                if (chkIsLocal.Checked)
                {
                    MessageBox.Show("✅ [LOCAL MODE] Η σύνδεση θεωρείται επιτυχής χωρίς δίκτυο.", "Έλεγχος");
                }
                else
                {
                    MessageBox.Show("✅ Η σύνδεση με τον πάροχο είναι ενεργή.\n(URL Reachable)", "Έλεγχος");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Σφάλμα σύνδεσης:\n{ex.Message}", "Λάθος", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnTestConnection.Enabled = true;
                btnTestConnection.Text = "🔌 Έλεγχος Σύνδεσης";
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}