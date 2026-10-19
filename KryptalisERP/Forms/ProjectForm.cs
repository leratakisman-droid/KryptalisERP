using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class ProjectForm : Form
    {
        private TextBox txtCode, txtName, txtDescription, txtNotes;
        private ComboBox cmbCustomer, cmbManager, cmbStatus, cmbPriority;
        private DateTimePicker dtpStart, dtpEnd, dtpActualStart, dtpActualEnd;
        private NumericUpDown numBudget, numActualCost, numActualRev, numProgress;
        private Label lblProfitDisplay, lblColorPreview;
        private Button btnSave, btnClose;

        private int? _projectId;
        private bool _isLoading = false;

        // Status Map based on your DB values (assuming NVARCHAR strings like 'ACTIVE', 'COMPLETED')
        public static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "PLANNING", "📝 Σχεδιασμός" },
            { "ACTIVE", "🚀 Εν Εξελίξει" },
            { "ON_HOLD", "⏸️ Σε Αναμονή" },
            { "COMPLETED", "✅ Ολοκληρώθηκε" },
            { "CANCELLED", "❌ Ακυρώθηκε" }
        };

        public ProjectForm(int? projectId = null)
        {
            _projectId = projectId;
            InitializeUI();
            this.Load += ProjectForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📂 Διαχείριση Έργου";
            this.Size = new Size(950, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(850, 650);

            // --- PANEL 1: BASIC INFO ---
            var pnlBasic = CreatePanel("📋 ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ", 100, Color.FromArgb(52, 73, 94));
            int y = 22, lx = 10, lw = 90, gap = 10;

            txtCode = AddText(pnlBasic, "Κωδικός:", lx, y, lw, 120, true); // Read-only usually generated
            lx += lw + 120 + gap;
            txtName = AddText(pnlBasic, "Όνομα Έργου:", lx, y, lw + 10, 300, false);

            y = 52; lx = 10;
            cmbStatus = AddCombo(pnlBasic, "Κατάσταση:", lx, y, lw, 150);
            lx += lw + 150 + gap;
            cmbPriority = AddCombo(pnlBasic, "Προτεραιότητα:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            numProgress = AddNumeric(pnlBasic, "Πρόοδος %:", lx, y, lw, 80);
            numProgress.Maximum = 100;
            numProgress.ValueChanged += (s, e) => UpdateVisuals();

            this.Controls.Add(pnlBasic);

            // --- PANEL 2: PEOPLE & CUSTOMER ---
            var pnlPeople = CreatePanel("👥 ΠΕΛΑΤΗΣ & ΥΠΕΥΘΥΝΟΣ", 80, Color.FromArgb(52, 73, 94));
            y = 22; lx = 10;
            cmbCustomer = AddCombo(pnlPeople, "Πελάτης:", lx, y, lw, 250);
            lx += lw + 250 + gap;
            cmbManager = AddCombo(pnlPeople, "Υπεύθυνος:", lx, y, lw, 200);

            this.Controls.Add(pnlPeople);

            // --- PANEL 3: DATES ---
            var pnlDates = CreatePanel("📅 ΧΡΟΝΟΔΙΑΓΡΑΜΜΑ", 80, Color.FromArgb(52, 73, 94));
            y = 22; lx = 10;
            dtpStart = AddDate(pnlDates, "Έναρξη:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            dtpEnd = AddDate(pnlDates, "Λήξη:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            dtpActualStart = AddDate(pnlDates, "Πραγ. Έναρξη:", lx, y, lw + 10, 120);
            lx += lw + 10 + 120 + gap;
            dtpActualEnd = AddDate(pnlDates, "Πραγ. Λήξη:", lx, y, lw + 10, 120);

            this.Controls.Add(pnlDates);

            // --- PANEL 4: FINANCIALS ---
            var pnlFin = CreatePanel("💰 ΟΙΚΟΝΟΜΙΚΑ ΣΤΟΙΧΕΙΑ", 100, Color.FromArgb(46, 204, 113));
            y = 22; lx = 10;
            numBudget = AddNumeric(pnlFin, "Προϋπολ. €:", lx, y, lw, 120);
            lx += lw + 120 + gap;
            numActualCost = AddNumeric(pnlFin, "Πραγ. Κόστος €:", lx, y, lw + 10, 120);
            lx += lw + 10 + 120 + gap;
            numActualRev = AddNumeric(pnlFin, "Πραγ. Έσοδα €:", lx, y, lw + 10, 120);

            y = 52; lx = 10;
            lblProfitDisplay = new Label
            {
                Text = "Κέρδος/Ζημία: 0,00 €",
                Location = new Point(lx, y),
                AutoSize = false,
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray
            };
            pnlFin.Controls.Add(lblProfitDisplay);

            // Bind calculations
            numBudget.ValueChanged += (s, e) => CalcFinancials();
            numActualCost.ValueChanged += (s, e) => CalcFinancials();
            numActualRev.ValueChanged += (s, e) => CalcFinancials();

            this.Controls.Add(pnlFin);

            // --- PANEL 5: DESCRIPTION & NOTES ---
            var pnlDesc = CreatePanel("📝 ΠΕΡΙΓΡΑΦΗ & ΣΗΜΕΙΩΣΕΙΣ", 150, Color.FromArgb(108, 117, 125));
            txtDescription = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Top, Height = 60, Margin = new Padding(10, 5, 10, 5), Font = new Font("Segoe UI", 9.5F) };
            txtNotes = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Top, Height = 60, Margin = new Padding(10, 5, 10, 5), Font = new Font("Segoe UI", 9.5F) };
            pnlDesc.Controls.Add(txtNotes);
            pnlDesc.Controls.Add(txtDescription);
            this.Controls.Add(pnlDesc);

            // --- FOOTER ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(10) };
            btnSave = CreateButton("💾 Αποθήκευση", 10, 10, 130, 40, Color.FromArgb(13, 110, 253));
            btnSave.Click += (s, e) => SaveProject();
            pnlFooter.Controls.Add(btnSave);
            btnClose = CreateButton("❌ Κλείσιμο", 150, 10, 110, 40, Color.FromArgb(108, 117, 125));
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);
            this.Controls.Add(pnlFooter);
        }

        // ============================================
        // HELPERS UI
        // ============================================
        private Panel CreatePanel(string title, int height, Color color)
        {
            var p = new Panel { Dock = DockStyle.Top, Height = height, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8), Margin = new Padding(0, 0, 0, 5) };
            p.Controls.Add(new Label { Text = title, Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = color });
            return p;
        }
        private ComboBox AddCombo(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var c = new ComboBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(c); return c;
        }
        private TextBox AddText(Panel p, string lbl, int x, int y, int lw, int cw, bool ro = false)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var t = new TextBox { Location = new Point(x + lw, y), Size = new Size(cw, 26), ReadOnly = ro, Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, BackColor = ro ? Color.LightYellow : Color.White };
            p.Controls.Add(t); return t;
        }
        private DateTimePicker AddDate(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var d = new DateTimePicker { Location = new Point(x + lw, y), Size = new Size(cw, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(d); return d;
        }
        private NumericUpDown AddNumeric(Panel p, string lbl, int x, int y, int lw, int cw)
        {
            p.Controls.Add(new Label { Text = lbl, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            var n = new NumericUpDown { Location = new Point(x + lw, y), Size = new Size(cw, 26), DecimalPlaces = 2, ThousandsSeparator = true, Maximum = 999999999, Font = new Font("Segoe UI", 9.5F) };
            p.Controls.Add(n); return n;
        }
        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        // ============================================
        // LOAD LOGIC
        // ============================================
        private void ProjectForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            LoadLists();
            if (!_projectId.HasValue) GenerateCode();
            else LoadProject(_projectId.Value);
            _isLoading = false;
            UpdateVisuals();
        }

        private void LoadLists()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // Customers
                var custs = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo=1 ORDER BY Epwnymia").ToList();
                cmbCustomer.DataSource = custs;
                cmbCustomer.DisplayMember = "Epwnymia";
                cmbCustomer.ValueMember = "Id";
                cmbCustomer.SelectedIndex = -1;

                // Managers (Assuming Users table has FullName or Name)
                try
                {
                    var users = conn.Query("SELECT Id, Name AS DisplayName FROM Users ORDER BY Name").ToList(); // Adjust column name if needed
                    if (users.Count == 0) users = conn.Query("SELECT Id, Epwnymia AS DisplayName FROM Employees WHERE Active=1 ORDER BY Epwnymia").ToList();

                    cmbManager.DataSource = users;
                    cmbManager.DisplayMember = "DisplayName";
                    cmbManager.ValueMember = "Id";
                }
                catch { cmbManager.Items.Add("Admin"); }

                // Status & Priority
                cmbStatus.DataSource = new BindingList<KeyValuePair<string, string>>(StatusMap.ToList());
                cmbStatus.DisplayMember = "Value";
                cmbStatus.ValueMember = "Key";
                cmbStatus.SelectedValue = "PLANNING";

                cmbPriority.Items.AddRange(new object[] { "LOW", "NORMAL", "HIGH", "URGENT" });
                cmbPriority.SelectedItem = "NORMAL";
            }
            catch { }
        }

        private void GenerateCode()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                int next = conn.ExecuteScalar<int>("SELECT ISNULL(MAX(Id),0)+1 FROM Projects");
                txtCode.Text = $"PRJ-{DateTime.Now.Year}-{next:D4}";
            }
            catch { txtCode.Text = "NEW"; }
        }

        private void CalcFinancials()
        {
            decimal profit = numActualRev.Value - numActualCost.Value;
            lblProfitDisplay.Text = $"Κέρδος/Ζημία: {profit:N2} €";
            lblProfitDisplay.ForeColor = profit >= 0 ? Color.Green : Color.Red;
        }

        private void UpdateVisuals()
        {
            // Optional: Change form border color based on status/priority
            string status = cmbStatus.SelectedValue?.ToString();
            if (status == "ACTIVE") this.BackColor = Color.FromArgb(240, 255, 240); // Light green tint
            else if (status == "ON_HOLD") this.BackColor = Color.FromArgb(255, 255, 240); // Light yellow tint
            else this.BackColor = Color.FromArgb(240, 242, 245);
        }

        // ============================================
        // SAVE LOGIC
        // ============================================
        private void SaveProject()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Βάλε Όνομα Έργου!"); return; }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                conn.Open();
                using var tran = conn.BeginTransaction();

                // ✅ FIX: Χρησιμοποιούμε object ή nullable types σωστά για τα parameters
                // Το Dapper χειρίζεται το DBNull.Value αυτόματα αν περάσουμε null
                object customerIdParam = cmbCustomer.SelectedValue != null ? Convert.ToInt32(cmbCustomer.SelectedValue) : (object)null;
                object managerIdParam = cmbManager.SelectedValue != null ? Convert.ToInt32(cmbManager.SelectedValue) : (object)null;

                string status = cmbStatus.SelectedValue?.ToString() ?? "PLANNING";
                string priority = cmbPriority.SelectedItem?.ToString() ?? "NORMAL";

                decimal calculatedProfit = numActualRev.Value - numActualCost.Value;

                if (_projectId.HasValue)
                {
                    conn.Execute(@"UPDATE Projects SET 
                        Name=@Name, Description=@Desc, CustomerId=@Cust, ProjectManagerId=@Mgr, 
                        StartDate=@Start, EndDate=@End, ActualStartDate=@ActStart, ActualEndDate=@ActEnd, 
                        BudgetAmount=@Bud, ActualCost=@Spent, ActualRevenue=@Rev, Profit=@Prof, 
                        ProgressPercentage=@Prog, Status=@St, Priority=@Pri, Notes=@Notes 
                        WHERE Id=@Id",
                        new
                        {
                            Id = _projectId,
                            Name = txtName.Text,
                            Desc = txtDescription.Text,
                            Cust = customerIdParam, // << Εδώ περνάμε το object/null
                            Mgr = managerIdParam,    // << Εδώ περνάμε το object/null
                            Start = dtpStart.Value.Date,
                            End = dtpEnd.Value.Date,
                            ActStart = dtpActualStart.Value.Date,
                            ActEnd = dtpActualEnd.Value.Date,
                            Bud = numBudget.Value,
                            Spent = numActualCost.Value,
                            Rev = numActualRev.Value,
                            Prof = calculatedProfit,
                            Prog = numProgress.Value,
                            St = status,
                            Pri = priority,
                            Notes = txtNotes.Text
                        }, tran);
                }
                else
                {
                    _projectId = conn.ExecuteScalar<int>(@"INSERT INTO Projects (CompanyId, Code, Name, Description, CustomerId, ProjectManagerId, StartDate, EndDate, ActualStartDate, ActualEndDate, BudgetAmount, ActualCost, ActualRevenue, Profit, ProgressPercentage, Status, Priority, Notes, CreatedBy, CreatedAt) VALUES (1, @Code, @Name, @Desc, @Cust, @Mgr, @Start, @End, @ActStart, @ActEnd, @Bud, @Spent, @Rev, @Prof, @Prog, @St, @Pri, @Notes, @Uid, GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new
                        {
                            Code = txtCode.Text,
                            Name = txtName.Text,
                            Desc = txtDescription.Text,
                            Cust = customerIdParam, // << Object/Null
                            Mgr = managerIdParam,    // << Object/Null
                            Start = dtpStart.Value.Date,
                            End = dtpEnd.Value.Date,
                            ActStart = dtpActualStart.Value.Date,
                            ActEnd = dtpActualEnd.Value.Date,
                            Bud = numBudget.Value,
                            Spent = numActualCost.Value,
                            Rev = numActualRev.Value,
                            Prof = calculatedProfit,
                            Prog = numProgress.Value,
                            St = status,
                            Pri = priority,
                            Notes = txtNotes.Text,
                            Uid = Program.CurrentUserId
                        }, tran);
                }

                tran.Commit();
                MessageBox.Show("✅ Το έργο αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void LoadProject(int id)
        {
            _isLoading = true;
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var p = conn.QueryFirstOrDefault("SELECT * FROM Projects WHERE Id=@Id", new { Id = id });
                if (p == null) return;

                txtCode.Text = p.Code;
                txtName.Text = p.Name;
                txtDescription.Text = p.Description ?? "";
                txtNotes.Text = p.Notes ?? "";

                if (p.CustomerId != null) cmbCustomer.SelectedValue = p.CustomerId;
                if (p.ProjectManagerId != null) cmbManager.SelectedValue = p.ProjectManagerId;

                cmbStatus.SelectedValue = p.Status ?? "PLANNING";
                cmbPriority.SelectedItem = p.Priority ?? "NORMAL";

                dtpStart.Value = p.StartDate ?? DateTime.Today;
                if (p.EndDate != null) dtpEnd.Value = p.EndDate;
                if (p.ActualStartDate != null) dtpActualStart.Value = p.ActualStartDate;
                if (p.ActualEndDate != null) dtpActualEnd.Value = p.ActualEndDate;

                numBudget.Value = p.BudgetAmount ?? 0;
                numActualCost.Value = p.ActualCost ?? 0;
                numActualRev.Value = p.ActualRevenue ?? 0;
                numProgress.Value = p.ProgressPercentage ?? 0;

                CalcFinancials();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            _isLoading = false;
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}
