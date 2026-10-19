using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class ProjectListForm : Form
    {
        private DataGridView dgvProjects;
        private TextBox txtSearch;
        private ComboBox cmbStatus, cmbPriority;
        private Button btnNew, btnEdit, btnDelete, btnRefresh, btnClose;
        private Label lblStats;

        public ProjectListForm()
        {
            InitializeUI();
            this.Load += (s, e) => LoadData();
        }

        private void InitializeUI()
        {
            this.Text = "📂 Λίστα Έργων (Projects)";
            this.Size = new Size(1300, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // ============================================
            // 1. TOOLBAR (Buttons) - ΠΡΕΠΕΙ ΝΑ ΕΙΝΑΙ ΟΡΙΖΟΝΤΙΑ ΣΤΗΝ ΚΟΡΥΦΗ
            // ============================================
            var pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60, // Αυξήσαμε λίγο το ύψος για ευκολία
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle // Για να ξεχωρίζει
            };

            btnNew = CreateBtn("➕ Νέο Έργο", 10, 10, 120, 38, Color.FromArgb(46, 204, 113));
            btnNew.Click += (s, e) => OpenForm(null);
            pnlToolbar.Controls.Add(btnNew);

            btnEdit = CreateBtn("✏️ Επεξεργασία", 140, 10, 120, 38, Color.FromArgb(52, 152, 219));
            btnEdit.Click += (s, e) => EditSel();
            pnlToolbar.Controls.Add(btnEdit);

            btnDelete = CreateBtn("🗑️ Ακύρωση", 270, 10, 100, 38, Color.FromArgb(231, 76, 60));
            btnDelete.Click += (s, e) => CancelSel();
            pnlToolbar.Controls.Add(btnDelete);

            btnRefresh = CreateBtn("🔄 Ανανέωση", 380, 10, 100, 38, Color.FromArgb(108, 117, 125));
            btnRefresh.Click += (s, e) => LoadData();
            pnlToolbar.Controls.Add(btnRefresh);

            btnClose = CreateBtn("❌ Κλείσιμο", 490, 10, 100, 38, Color.FromArgb(149, 165, 166));
            btnClose.Click += (s, e) => this.Close();
            pnlToolbar.Controls.Add(btnClose);

            // ============================================
            // 2. FILTERS BAR - ΑΜΕΣΩΣ ΑΠΟ ΚΑΤΩ ΤΟ TOOLBAR
            // ============================================
            var pnlFilters = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(10, 5, 10, 5)
            };

            int fx = 10;
            pnlFilters.Controls.Add(new Label { Text = "Κατάσταση:", Location = new Point(fx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); fx += 85;

            cmbStatus = new ComboBox { Location = new Point(fx, 8), Size = new Size(130, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Όλα", "PLANNING", "ACTIVE", "ON_HOLD", "COMPLETED", "CANCELLED" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(cmbStatus); fx += 140;

            pnlFilters.Controls.Add(new Label { Text = "Προτεραιότητα:", Location = new Point(fx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); fx += 100;

            cmbPriority = new ComboBox { Location = new Point(fx, 8), Size = new Size(100, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPriority.Items.AddRange(new object[] { "Όλες", "LOW", "NORMAL", "HIGH", "URGENT" });
            cmbPriority.SelectedIndex = 0;
            cmbPriority.SelectedIndexChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(cmbPriority); fx += 110;

            txtSearch = new TextBox { Location = new Point(fx, 8), Size = new Size(250, 26), PlaceholderText = "Αναζήτηση σε Κωδικό/Όνομα..." };
            txtSearch.TextChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(txtSearch);

            // ============================================
            // 3. DATA GRID - ΓΕΜΙΖΕΙ ΤΟΝ ΥΠΟΛΟΙΠΟ ΧΩΡΟ
            // ============================================
            dgvProjects = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 10F),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false
            };

            // Header Style
            dgvProjects.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Columns Definition
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "Code", HeaderText = "Κωδικός", Width = 100, DataPropertyName = "Code" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Όνομα Έργου", Width = 250, DataPropertyName = "Name" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "Πελάτης", Width = 200, DataPropertyName = "CustomerName" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartDate", HeaderText = "Έναρξη", Width = 100, DataPropertyName = "StartDate" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "EndDate", HeaderText = "Λήξη", Width = 100, DataPropertyName = "EndDate" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = "Κατάσταση", Width = 120, DataPropertyName = "StatusDisplay" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "Priority", HeaderText = "Προτ.", Width = 80, DataPropertyName = "Priority" });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "BudgetAmount", HeaderText = "Προϋπ/μός €", Width = 100, DataPropertyName = "BudgetAmount", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "ActualCost", HeaderText = "Κόστος €", Width = 100, DataPropertyName = "ActualCost", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "Profit", HeaderText = "Κέρδος €", Width = 100, DataPropertyName = "Profit", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold) } });
            dgvProjects.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProgressPercentage", HeaderText = "%", Width = 50, DataPropertyName = "ProgressPercentage", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "N0" } });

            dgvProjects.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSel(); };
            dgvProjects.CellFormatting += Dgv_CellFmt;

            // ============================================
            // 4. STATUS BAR (Bottom)
            // ============================================
            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.LightGray, Padding = new Padding(10, 5, 10, 5) };
            lblStats = new Label { Text = "", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            pnlStatus.Controls.Add(lblStats);

            // ============================================
            // ⚠️ ΚΡΙΣΙΜΟ: ΣΕΙΡΑ ΠΡΟΣΘΗΚΗΣ CONTROLS
            // Το Fill (Grid) πρέπει να προστεθεί ΠΡΩΤΟ, μετά τα Top panels, μετά το Bottom panel.
            // Ή καλύτερα: Add Fill first, then docked items in reverse visual order?
            // Standard WinForms practice for mixed docking:
            // 1. Add the control that fills the space (dgvProjects) LAST among top-docked ones? No.
            // Correct Order for Controls.Add when using Dock.Top and Dock.Fill:
            // Add Dock.Fill FIRST? NO.
            // Rule: The last control added with Dock.Top is at the very top.
            // So we add: Status(Bottom), Filters(Top), Toolbar(Top), Grid(Fill)?
            // Actually, simplest robust way:
            // 1. Add Grid (Fill)
            // 2. Add Filters (Top) -> It pushes Grid down
            // 3. Add Toolbar (Top) -> It pushes Filters down
            // 4. Add Status (Bottom)

            this.Controls.Add(dgvProjects); // Fill takes remaining space
            this.Controls.Add(pnlFilters);  // Top
            this.Controls.Add(pnlToolbar);  // Top (Above Filters)
            this.Controls.Add(pnlStatus);   // Bottom

            // Bring to front ensures visibility if overlapping occurs due to z-order issues
            dgvProjects.BringToFront();
        }

        private Button CreateBtn(string t, int x, int y, int w, int h, Color c)
            => new Button { Text = t, Location = new Point(x, y), Size = new Size(w, h), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(GetConnStr());
                string sql = @"
                    SELECT p.Id, p.Code, p.Name, c.Epwnymia AS CustomerName, 
                           p.StartDate, p.EndDate, p.Status, p.Priority, 
                           p.BudgetAmount, p.ActualCost, p.Profit, p.ProgressPercentage,
                           CASE p.Status WHEN 'PLANNING' THEN '📝 Σχεδιασμός' WHEN 'ACTIVE' THEN '🚀 Εν Εξελίξει' WHEN 'ON_HOLD' THEN '⏸️ Αναμονή' WHEN 'COMPLETED' THEN '✅ Ολοκλ.' WHEN 'CANCELLED' THEN '❌ Ακυρώθ.' ELSE p.Status END AS StatusDisplay
                    FROM Projects p LEFT JOIN Customers c ON p.CustomerId=c.Id WHERE 1=1";

                if (cmbStatus.SelectedIndex > 0) sql += " AND p.Status=@St";
                if (cmbPriority.SelectedIndex > 0) sql += " AND p.Priority=@Pri";
                string s = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(s)) sql += " AND (p.Name LIKE @S OR p.Code LIKE @S OR c.Epwnymia LIKE @S)";
                sql += " ORDER BY p.CreatedAt DESC";

                var data = conn.Query(sql, new
                {
                    St = cmbStatus.SelectedIndex > 0 ? cmbStatus.SelectedItem.ToString() : null,
                    Pri = cmbPriority.SelectedIndex > 0 ? cmbPriority.SelectedItem.ToString() : null,
                    S = $"%{s}%"
                }).ToList();

                dgvProjects.DataSource = data;
                lblStats.Text = $"Σύνολο: {data.Count} | Εν Εξελίξει: {data.Count(x => x.Status == "ACTIVE")} | Ολοκληρωμένα: {data.Count(x => x.Status == "COMPLETED")}";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Dgv_CellFmt(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Colorize Profit
            if (e.ColumnIndex == dgvProjects.Columns["Profit"].Index)
            {
                decimal val = 0;
                if (decimal.TryParse(e.Value?.ToString(), out val))
                {
                    e.CellStyle.ForeColor = val >= 0 ? Color.Green : Color.Red;
                }
            }

            // Colorize Status Badge
            if (e.ColumnIndex == dgvProjects.Columns["StatusDisplay"].Index)
            {
                string st = e.Value?.ToString();
                if (st != null)
                {
                    if (st.Contains("Εν Εξελίξει")) e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    else if (st.Contains("Σχεδιασμός")) e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    else if (st.Contains("Ολοκλ.")) e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    else if (st.Contains("Ακυρώθ.")) e.CellStyle.BackColor = Color.FromArgb(248, 215, 218);
                }
            }
        }

        private void OpenForm(int? id)
        {
            try
            {
                using (var f = new ProjectForm(id))
                {
                    if (f.ShowDialog() == DialogResult.OK) LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditSel()
        {
            if (dgvProjects.CurrentRow == null) return;
            OpenForm(Convert.ToInt32(dgvProjects.CurrentRow.Cells["Id"].Value));
        }

        private void CancelSel()
        {
            if (dgvProjects.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvProjects.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Ακύρωση έργου;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnStr());
                    conn.Execute("UPDATE Projects SET Status='CANCELLED' WHERE Id=@Id", new { Id = id });
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private string GetConnStr() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}