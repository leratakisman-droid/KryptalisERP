using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class SeriesEditForm : Form
    {
        // --- State Variables ---
        private int _seriesId;
        private int _companyId;
        private int? _branchId;

        // --- UI Controls Declarations ---
        private TextBox txtCode, txtName, txtPrefix;
        private NumericUpDown numNextNumber, numNumberLength;
        private ComboBox cmbDocType;
        private CheckBox chkIsActive, chkIsDefault, chkAnnualReset, chkManualNumbering;
        private Button btnSave, btnCancel;
        private Label lblNextNumber, lblNumLength; // For dynamic styling

        // --- Colors ---
        private readonly Color COLOR_PRIMARY = Color.FromArgb(46, 204, 113); // Green for Save
        private readonly Color COLOR_DANGER = Color.FromArgb(231, 76, 60);   // Red for Cancel/Delete
        private readonly Color COLOR_GRAY = Color.Gray;

        public SeriesEditForm(int companyId, int? branchId, int? seriesId = null)
        {
            _companyId = companyId;
            _branchId = branchId;
            _seriesId = seriesId ?? 0;

            InitializeUI(); // Build the form visually first

            this.Text = _seriesId > 0 ? "✏️ Επεξεργασία Σειράς Παραστατικού" : "➕ Νέα Σειρά Παραστατικού";

            // Load Data after UI is built
            LoadDocumentTypes();

            if (_seriesId > 0)
            {
                LoadExistingSeries(_seriesId);
            }
            else
            {
                PrepareForNewInsert();
            }
        }

        private void InitializeUI()
        {
            this.Size = new Size(550, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var pnlMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), AutoScroll = true };

            int y = 20;
            int lx = 10;      // Label X
            int lw = 130;     // Label Width
            int cx = lx + lw; // Control X
            int cw = 350;     // Control Width

            // 1. Code
            AddLabel(pnlMain, "Κωδικός Σειράς:", lx, ref y);
            txtCode = AddTextBox(pnlMain, cx, y, cw);
            y += 35;

            // 2. Name
            AddLabel(pnlMain, "Όνομα Σειράς:", lx, ref y);
            txtName = AddTextBox(pnlMain, cx, y, cw);
            y += 35;

            // 3. Document Type (Dropdown)
            AddLabel(pnlMain, "Τύπος Παραστατικού:", lx, ref y);
            cmbDocType = new ComboBox
            {
                Location = new Point(cx, y),
                Size = new Size(cw, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            pnlMain.Controls.Add(cmbDocType);
            y += 35;

            // 4. Prefix
            AddLabel(pnlMain, "Πρόθεμα (Prefix):", lx, ref y);
            txtPrefix = AddTextBox(pnlMain, cx, y, 100);
            txtPrefix.MaxLength = 10;
            y += 35;

            // 5. Next Number & Length (Side by side)
            lblNextNumber = new Label { Text = "Επόμενος Αρ.:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            pnlMain.Controls.Add(lblNextNumber);
            numNextNumber = new NumericUpDown { Location = new Point(cx, y), Size = new Size(100, 26), Minimum = 1, Maximum = 99999999 };
            pnlMain.Controls.Add(numNextNumber);

            lblNumLength = new Label { Text = "Μήκος:", Location = new Point(cx + 120, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            pnlMain.Controls.Add(lblNumLength);
            numNumberLength = new NumericUpDown { Location = new Point(cx + 170, y), Size = new Size(80, 26), Minimum = 1, Maximum = 10 };
            numNumberLength.Value = 6;
            pnlMain.Controls.Add(numNumberLength);
            y += 35;

            // 6. Checkboxes Section
            var grpOptions = new GroupBox { Text = "⚙️ Επιλογές Αρίθμησης", Location = new Point(lx, y), Size = new Size(cw + 100, 120) };

            chkManualNumbering = new CheckBox { Text = "Χειροκίνητη Αρίθμηση (No Auto)", Location = new Point(15, 25), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            chkManualNumbering.CheckedChanged += ChkManualNumbering_CheckedChanged;
            grpOptions.Controls.Add(chkManualNumbering);

            chkAnnualReset = new CheckBox { Text = "Μηδενισμός κάθε χρόνο", Location = new Point(15, 55), AutoSize = true, Checked = true };
            grpOptions.Controls.Add(chkAnnualReset);

            chkIsDefault = new CheckBox { Text = "Προεπιλεγμένη Σειρά για αυτόν τον τύπο", Location = new Point(15, 85), AutoSize = true };
            grpOptions.Controls.Add(chkIsDefault);

            chkIsActive = new CheckBox { Text = "Ενεργή Σειρά", Location = new Point(200, 25), AutoSize = true, Checked = true, ForeColor = COLOR_PRIMARY };
            grpOptions.Controls.Add(chkIsActive);

            pnlMain.Controls.Add(grpOptions);
            y += 130;

            // Footer Buttons
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.LightGray, Padding = new Padding(10) };

            btnSave = CreateButton("💾 Αποθήκευση", 10, 10, 120, 40, COLOR_PRIMARY);
            btnSave.Click += BtnSave_Click;
            pnlFooter.Controls.Add(btnSave);

            btnCancel = CreateButton("❌ Άκυρο", 140, 10, 100, 40, COLOR_DANGER);
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            pnlFooter.Controls.Add(btnCancel);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
        }

        // ==========================================
        // LOGIC METHODS
        // ==========================================

        private void LoadDocumentTypes()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                var types = conn.Query<DocumentTypeDto>(@"
                    SELECT Id, DocTypeCode AS Code, DocTypeName AS Name, DocTypeCategory AS Category
                    FROM DocumentTypes 
                    WHERE IsActive = 1 
                    ORDER BY Category, Code").ToList();

                cmbDocType.DataSource = types;
                cmbDocType.DisplayMember = "Name";
                cmbDocType.ValueMember = "Id";

                if (types.Any()) cmbDocType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης Τύπων: " + ex.Message);
            }
        }

        private void LoadExistingSeries(int id)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                var data = conn.QueryFirstOrDefault<SeriesDataDto>(@"
                    SELECT ds.Id, ds.Code, ds.Name, ds.Prefix, ds.NextNumber, ds.NumberLength,
                           ds.DocumentTypeId, dt.DocTypeCode AS AadeCode, dt.DocTypeName AS TypeName,
                           ds.IsAnnualReset, ds.IsActive, ds.IsDefault, ds.IsManualNumbering
                    FROM DocumentSeries ds
                    INNER JOIN DocumentTypes dt ON ds.DocumentTypeId = dt.Id
                    WHERE ds.Id = @Id", new { Id = id });

                if (data == null) return;

                txtCode.Text = data.Code;
                txtName.Text = data.Name;
                txtPrefix.Text = data.Prefix ?? "";
                numNextNumber.Value = data.NextNumber ?? 1;
                numNumberLength.Value = data.NumberLength ?? 6;

                chkIsActive.Checked = data.IsActive ?? true;
                chkIsDefault.Checked = data.IsDefault ?? false;
                chkAnnualReset.Checked = data.IsAnnualReset ?? false;
                chkManualNumbering.Checked = data.IsManualNumbering ?? false;

                // Set Selected Type in Combo safely
                if (cmbDocType.Items.Count > 0 && data.DocumentTypeId > 0)
                {
                    foreach (var item in cmbDocType.Items.Cast<DocumentTypeDto>())
                    {
                        if (item.Id == data.DocumentTypeId)
                        {
                            cmbDocType.SelectedItem = item;
                            break;
                        }
                    }
                }

                UpdateUiState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης Σειράς: " + ex.Message);
            }
        }

        private void PrepareForNewInsert()
        {
            txtCode.Text = $"SER-{DateTime.Now.Year}-{new Random().Next(100, 999)}";
            txtName.Text = "Νέα Σειρά";
            txtPrefix.Text = "T-";
            numNextNumber.Value = 1;
            numNumberLength.Value = 6;
            chkIsActive.Checked = true;
            chkAnnualReset.Checked = true;
            chkManualNumbering.Checked = false;
            chkIsDefault.Checked = false;

            UpdateUiState();
        }

        private void ChkManualNumbering_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUiState();
        }

        private void UpdateUiState()
        {
            bool isManual = chkManualNumbering.Checked;

            // Disable auto-numbering fields if manual mode is on
            numNextNumber.Enabled = !isManual;
            numNumberLength.Enabled = !isManual;
            chkAnnualReset.Enabled = !isManual;

            // Visual feedback
            lblNextNumber.ForeColor = isManual ? COLOR_GRAY : Color.Black;
            lblNumLength.ForeColor = isManual ? COLOR_GRAY : Color.Black;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Συμπλήρωσε Κωδικό και Όνομα!", "Προσοχή");
                return;
            }

            if (cmbDocType.SelectedValue == null)
            {
                MessageBox.Show("Επίλεξε Τύπο Παραστατικού!", "Προσοχή");
                return;
            }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                int docTypeId = Convert.ToInt32(cmbDocType.SelectedValue);
                string aadeCode = ((DocumentTypeDto)cmbDocType.SelectedItem)?.Code ?? "";

                if (_seriesId > 0)
                {
                    conn.Execute(@"
                        UPDATE DocumentSeries SET 
                            Code=@Code, Name=@Name, Prefix=@Prefix, 
                            NextNumber=@Next, NumberLength=@Len,
                            DocumentTypeId=@TypeId, AadeDocTypeCode=@Aade,
                            IsAnnualReset=@Reset, IsActive=@Active, 
                            IsDefault=@Default, IsManualNumbering=@Manual,
                            UpdatedAt=GETDATE()
                        WHERE Id=@Id",
                        new
                        {
                            Id = _seriesId,
                            Code = txtCode.Text.Trim(),
                            Name = txtName.Text.Trim(),
                            Prefix = txtPrefix.Text.Trim(),
                            Next = (int)numNextNumber.Value,
                            Len = (int)numNumberLength.Value,
                            TypeId = docTypeId,
                            Aade = aadeCode,
                            Reset = chkAnnualReset.Checked,
                            Active = chkIsActive.Checked,
                            Default = chkIsDefault.Checked,
                            Manual = chkManualNumbering.Checked
                        }, tran);
                }
                else
                {
                    conn.Execute(@"
                        INSERT INTO DocumentSeries (
                            CompanyId, BranchId, DocumentTypeId, Code, Name, Prefix, 
                            NextNumber, NumberLength, IsActive, AadeDocTypeCode, 
                            IsAnnualReset, IsDefault, IsManualNumbering, CreatedAt
                        ) VALUES (
                            @Cid, @Bid, @TypeId, @Code, @Name, @Prefix, 
                            @Next, @Len, @Active, @Aade, 
                            @Reset, @Default, @Manual, GETDATE()
                        ); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new
                        {
                            Cid = _companyId,
                            Bid = _branchId,
                            TypeId = docTypeId,
                            Code = txtCode.Text.Trim(),
                            Name = txtName.Text.Trim(),
                            Prefix = txtPrefix.Text.Trim(),
                            Next = (int)numNextNumber.Value,
                            Len = (int)numNumberLength.Value,
                            Active = chkIsActive.Checked,
                            Aade = aadeCode,
                            Reset = chkAnnualReset.Checked,
                            Default = chkIsDefault.Checked,
                            Manual = chkManualNumbering.Checked
                        }, tran);
                }

                tran.Commit();
                MessageBox.Show("✅ Η σειρά αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποθήκευσης:\n" + ex.Message, "Λάθος");
            }
        }

        // ==========================================
        // HELPERS & DTOs
        // ==========================================

        private void AddLabel(Control parent, string text, int x, ref int y)
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

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP; User Id=sa; Password=epsilonsa; TrustServerCertificate=True;";

        // Internal Classes for Mapping
        public class DocumentTypeDto
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
        }

        public class SeriesDataDto
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Prefix { get; set; }
            public int? NextNumber { get; set; }
            public int? NumberLength { get; set; }
            public int DocumentTypeId { get; set; }
            public string AadeCode { get; set; }
            public string TypeName { get; set; }
            public bool? IsAnnualReset { get; set; }
            public bool? IsActive { get; set; }
            public bool? IsDefault { get; set; }
            public bool? IsManualNumbering { get; set; }
        }
    }
}