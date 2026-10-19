using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP.Forms
{
    public class SeriesConfigForm : Form
    {
        private int _seriesId;
        private string _connStr;

        // Controls
        private TextBox txtName, txtPrefix, txtAadeCode, txtTemplatePath;
        private NumericUpDown numNext, numLength, numCopies;
        private ComboBox cmbDocType, cmbIncomeClass, cmbExpenseClass, cmbCategory, cmbModule;
        private CheckBox chkActive, chkYearReset, chkBranchReset, chkManual;
        private CheckedListBox clbRoles;
        private Button btnSave, btnCancel, btnBrowseTemplate;

        public SeriesConfigForm(int seriesId)
        {
            _seriesId = seriesId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=...;Database=KryptalisERP;...";

            InitializeUI();
            this.Load += SeriesConfigForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "⚙️ Ρύθμιση Σειράς Παραστατικού";
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var pnlMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), AutoScroll = true };
            int y = 10; int lx = 10; int lw = 140; int cx = lx + lw + 10; int cw = 480;

            // --- GROUP 1: BASIC INFO ---
            var grpBasic = new GroupBox { Text = "📝 Βασικά Στοιχεία", Location = new Point(lx, y), Size = new Size(cw + lw + 20, 130) };

            AddLabel(grpBasic, "Όνομα Σειράς:", 10, 25);
            txtName = AddTextBox(grpBasic, 120, 22, 300);

            AddLabel(grpBasic, "Κατηγορία:", 10, 55);
            cmbCategory = new ComboBox { Location = new Point(120, 52), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.Items.AddRange(new[] { "SALES", "PURCHASE", "RECEIPT", "PAYMENT", "INVENTORY", "OTHER" });
            grpBasic.Controls.Add(cmbCategory);

            AddLabel(grpBasic, "Module:", 280, 55);
            cmbModule = new ComboBox { Location = new Point(340, 52), Size = new Size(100, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbModule.Items.AddRange(new[] { "ERP", "POS", "WEB" });
            grpBasic.Controls.Add(cmbModule);

            AddLabel(grpBasic, "Πρόθεμα (Prefix):", 10, 85);
            txtPrefix = AddTextBox(grpBasic, 120, 82, 100);

            chkActive = new CheckBox { Text = "Ενεργή", Location = new Point(240, 85), AutoSize = true, Checked = true };
            grpBasic.Controls.Add(chkActive);

            pnlMain.Controls.Add(grpBasic);
            y += 140;

            // --- GROUP 2: NUMBERING ---
            var grpNum = new GroupBox { Text = "🔢 Αρίθμηση", Location = new Point(lx, y), Size = new Size(cw + lw + 20, 100) };

            AddLabel(grpNum, "Επόμενος Αριθμός:", 10, 25);
            numNext = new NumericUpDown { Location = new Point(120, 22), Size = new Size(100, 26), Minimum = 1 };
            grpNum.Controls.Add(numNext);

            AddLabel(grpNum, "Μήκος Ψηφίων:", 240, 25);
            numLength = new NumericUpDown { Location = new Point(340, 22), Size = new Size(80, 26), Minimum = 1, Maximum = 10, Value = 6 };
            grpNum.Controls.Add(numLength);

            chkYearReset = new CheckBox { Text = "Reset κάθε Χρόνο", Location = new Point(10, 55), AutoSize = true, Checked = true };
            grpNum.Controls.Add(chkYearReset);

            chkBranchReset = new CheckBox { Text = "Reset ανά Υποκατάστημα", Location = new Point(180, 55), AutoSize = true };
            grpNum.Controls.Add(chkBranchReset);

            chkManual = new CheckBox { Text = "Χειροκίνητη Αρίθμηση", Location = new Point(380, 55), AutoSize = true };
            grpNum.Controls.Add(chkManual);

            pnlMain.Controls.Add(grpNum);
            y += 110;

            // --- GROUP 3: MYDATA & TAXES ---
            var grpMyData = new GroupBox { Text = "🇬🇷 myDATA & Φορολογικά", Location = new Point(lx, y), Size = new Size(cw + lw + 20, 130) };

            AddLabel(grpMyData, "Κωδικός ΑΑΔΕ:", 10, 25);
            txtAadeCode = AddTextBox(grpMyData, 120, 22, 100);
            txtAadeCode.PlaceholderText = "π.χ. 1.1";

            AddLabel(grpMyData, "Χαρακ. Εσόδου (E):", 10, 55);
            cmbIncomeClass = new ComboBox { Location = new Point(120, 52), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            grpMyData.Controls.Add(cmbIncomeClass);

            AddLabel(grpMyData, "Χαρακ. Εξόδου (V):", 10, 85);
            cmbExpenseClass = new ComboBox { Location = new Point(120, 82), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            grpMyData.Controls.Add(cmbExpenseClass);

            pnlMain.Controls.Add(grpMyData);
            y += 140;

            // --- GROUP 4: PRINTING ---
            var grpPrint = new GroupBox { Text = "🖨️ Εκτύπωση", Location = new Point(lx, y), Size = new Size(cw + lw + 20, 100) };

            AddLabel(grpPrint, "Template Αρχείο:", 10, 25);
            txtTemplatePath = AddTextBox(grpPrint, 120, 22, 300);
            btnBrowseTemplate = new Button { Text = "...", Location = new Point(430, 21), Size = new Size(30, 26) };
            btnBrowseTemplate.Click += (s, e) => {
                using (var ofd = new OpenFileDialog { Filter = "Report Files|*.rdlc;*.html;*.pdf" })
                    if (ofd.ShowDialog() == DialogResult.OK) txtTemplatePath.Text = ofd.FileName;
            };
            grpPrint.Controls.Add(btnBrowseTemplate);

            AddLabel(grpPrint, "Αντίγραφα:", 10, 55);
            numCopies = new NumericUpDown { Location = new Point(120, 52), Size = new Size(60, 26), Minimum = 1, Value = 1 };
            grpPrint.Controls.Add(numCopies);

            pnlMain.Controls.Add(grpPrint);
            y += 110;

            // --- GROUP 5: ROLES ---
            var grpRoles = new GroupBox { Text = "👥 Δικαιώματα Πρόσβασης (Ρόλοι)", Location = new Point(lx, y), Size = new Size(cw + lw + 20, 120) };
            clbRoles = new CheckedListBox { Location = new Point(10, 20), Size = new Size(450, 90), CheckOnClick = true };
            grpRoles.Controls.Add(clbRoles);
            pnlMain.Controls.Add(grpRoles);
            y += 130;

            // --- FOOTER ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.LightGray };
            btnSave = new Button { Text = "💾 Αποθήκευση", Location = new Point(10, 10), Size = new Size(120, 40), BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += BtnSave_Click;
            pnlFooter.Controls.Add(btnSave);

            btnCancel = new Button { Text = "❌ Άκυρο", Location = new Point(140, 10), Size = new Size(100, 40), DialogResult = DialogResult.Cancel };
            pnlFooter.Controls.Add(btnCancel);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
        }

        private async void SeriesConfigForm_Load(object sender, EventArgs e)
        {
            await LoadLookups();
            LoadSeriesData();
        }

        private async System.Threading.Tasks.Task LoadLookups()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);

                // Load Tax Classifications (E3, V8 etc)
                var taxes = await conn.QueryAsync<TaxDto>("SELECT Id, Code, NameGR FROM TaxClassificationCodes ORDER BY Category, Code");

                var incomeTaxes = taxes.Where(t => t.Category == "E").ToList();
                incomeTaxes.Insert(0, new TaxDto { Id = 0, Code = "-", NameGR = "-- Κανένα --" });
                cmbIncomeClass.DataSource = incomeTaxes;
                cmbIncomeClass.DisplayMember = "NameGR";
                cmbIncomeClass.ValueMember = "Id";

                var expenseTaxes = taxes.Where(t => t.Category == "V").ToList();
                expenseTaxes.Insert(0, new TaxDto { Id = 0, Code = "-", NameGR = "-- Κανένα --" });
                cmbExpenseClass.DataSource = expenseTaxes;
                cmbExpenseClass.DisplayMember = "NameGR";
                cmbExpenseClass.ValueMember = "Id";

                // Load Roles (Mocked or from DB)
                // Ideally fetch from a Roles table
                clbRoles.Items.AddRange(new[] { "Admin", "SalesManager", "Accountant", "StoreKeeper", "Cashier", "PurchasingManager" });
            }
            catch (Exception ex) { MessageBox.Show("Error loading lookups: " + ex.Message); }
        }

        private void LoadSeriesData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var s = conn.QueryFirstOrDefault<dynamic>(@"
                    SELECT * FROM DocumentSeries WHERE Id = @Id", new { Id = _seriesId });

                if (s == null) return;

                txtName.Text = s.Name;
                txtPrefix.Text = s.Prefix;
                cmbCategory.SelectedItem = s.Category ?? "OTHER";
                cmbModule.SelectedItem = s.Module ?? "ERP";
                numNext.Value = s.NextNumber ?? 1;
                numLength.Value = s.NumberLength ?? 6;
                chkActive.Checked = s.IsActive ?? true;
                chkYearReset.Checked = s.IncludeYearInNumber ?? true;
                chkBranchReset.Checked = s.IncludeBranchInNumber ?? false;
                chkManual.Checked = s.IsManualNumbering ?? false;

                txtAadeCode.Text = s.MyDataDocTypeCode ?? "";
                txtTemplatePath.Text = s.PrintTemplatePath ?? "";
                numCopies.Value = s.PrintCopies ?? 1;

                if (s.IncomeClassificationId != null && s.IncomeClassificationId > 0)
                    cmbIncomeClass.SelectedValue = s.IncomeClassificationId;

                if (s.ExpenseClassificationId != null && s.ExpenseClassificationId > 0)
                    cmbExpenseClass.SelectedValue = s.ExpenseClassificationId;

                // Parse Roles CSV
                if (!string.IsNullOrEmpty(s.AllowedRoles))
                {
                    var roles = s.AllowedRoles.ToString().Split(',');
                    for (int i = 0; i < clbRoles.Items.Count; i++)
                        if (roles.Contains(clbRoles.Items[i].ToString()))
                            clbRoles.SetItemChecked(i, true);
                }
            }
            catch (Exception ex) { MessageBox.Show("Load Error: " + ex.Message); }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);

                // Build Roles CSV
                var selectedRoles = clbRoles.CheckedItems.Cast<string>().ToList();
                string rolesCsv = selectedRoles.Any() ? string.Join(",", selectedRoles) : null;

                conn.Execute(@"
                    UPDATE DocumentSeries SET
                        Name = @Name, Prefix = @Prefix, Category = @Cat, Module = @Mod,
                        NextNumber = @Next, NumberLength = @Len, IsActive = @Act,
                        IncludeYearInNumber = @YearRes, IncludeBranchInNumber = @BrRes, IsManualNumbering = @Man,
                        MyDataDocTypeCode = @Aade, 
                        IncomeClassificationId = @IncId, ExpenseClassificationId = @ExpId,
                        PrintTemplatePath = @Tpl, PrintCopies = @Copies,
                        AllowedRoles = @Roles
                    WHERE Id = @Id",
                    new
                    {
                        Id = _seriesId,
                        Name = txtName.Text.Trim(),
                        Prefix = txtPrefix.Text.Trim(),
                        Cat = cmbCategory.SelectedItem.ToString(),
                        Mod = cmbModule.SelectedItem.ToString(),
                        Next = (int)numNext.Value,
                        Len = (int)numLength.Value,
                        Act = chkActive.Checked,
                        YearRes = chkYearReset.Checked,
                        BrRes = chkBranchReset.Checked,
                        Man = chkManual.Checked,
                        Aade = txtAadeCode.Text.Trim(),
                        IncId = cmbIncomeClass.SelectedValue is int iid && iid > 0 ? (int?)iid : null,
                        ExpId = cmbExpenseClass.SelectedValue is int eid && eid > 0 ? (int?)eid : null,
                        Tpl = txtTemplatePath.Text.Trim(),
                        Copies = (int)numCopies.Value,
                        Roles = rolesCsv
                    });

                MessageBox.Show("✅ Η σειρά ενημερώθηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Save Error: " + ex.Message); }
        }

        // Helpers
        private void AddLabel(Control p, string t, int x, int y) => p.Controls.Add(new Label { Text = t, Location = new Point(x, y + 3), AutoSize = true });
        private TextBox AddTextBox(Control p, int x, int y, int w) { var tb = new TextBox { Location = new Point(x, y), Size = new Size(w, 26) }; p.Controls.Add(tb); return tb; }

        public class TaxDto { public int Id { get; set; } public string Code { get; set; } public string NameGR { get; set; } public string Category { get; set; } }
    }
}