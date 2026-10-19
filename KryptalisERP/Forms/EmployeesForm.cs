using KryptalisERP.Helpers;
using KryptalisERP.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class EmployeesForm : Form
    {
        private bool _showInactive = false;

        public EmployeesForm()
        {
            InitializeComponent();
            ThemeManager.ApplyToCurrentForm(this);

            // ✅ ΣΥΝΔΕΣΗ ΟΛΩΝ ΤΩΝ EVENTS
            btnSearch.Click += btnSearch_Click;
            btnNew.Click += btnNew_Click;
            btnEdit.Click += btnEdit_Click;
            btnDeactivate.Click += btnDeactivate_Click;
            btnBack.Click += btnBack_Click;
            chkShowInactive.CheckedChanged += chkShowInactive_CheckedChanged;
            dgvEmployees.CellDoubleClick += dgvEmployees_CellDoubleClick;

            LoadEmployees();
            LoadStats();
        }

        // ==========================================
        // ✅ ΦΟΡΤΩΣΗ ΠΩΛΗΤΩΝ/ΕΙΣΠΡΑΚΤΟΡΩΝ
        // ==========================================
        private void LoadEmployees(string searchTerm = "")
        {
            try
            {
                string activeFilter = _showInactive ? "" : "WHERE e.IsActive = 1";
                string searchFilter = "";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchFilter = activeFilter == "" ? "WHERE" : "AND";
                    searchFilter += $" (e.FullName LIKE '%{searchTerm}%' OR e.EmployeeCode LIKE '%{searchTerm}%' OR e.Region LIKE '%{searchTerm}%')";
                }

                string query = $@"
                    SELECT 
                        e.Id,
                        e.EmployeeCode AS [Κωδικός],
                        e.FullName AS [Ονοματεπώνυμο],
                        e.Role AS [Ρόλος],
                        e.Phone AS [Τηλέφωνο],
                        e.Mobile AS [Κινητό],
                        e.Email AS [Email],
                        e.Region AS [Περιοχή],
                        e.CommissionPercent AS [Προμήθεια %],
                        CASE WHEN e.IsActive = 1 THEN 'Ενεργός' ELSE 'Ανενεργός' END AS [Κατάσταση]
                    FROM Employees e
                    {activeFilter} {searchFilter}
                    ORDER BY e.FullName";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvEmployees.DataSource = dt;
                if (dgvEmployees.Columns.Contains("Id")) dgvEmployees.Columns["Id"].Visible = false;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // ✅ ΣΤΑΤΙΣΤΙΚΑ
        // ==========================================
        private void LoadStats()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(@"
                    SELECT 
                        COUNT(*) AS Total,
                        SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) AS Active,
                        SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END) AS Inactive
                    FROM Employees");

                if (dt.Rows.Count > 0)
                    lblSubtitle.Text = $"Συνολικοί: {dt.Rows[0]["Total"]} | Ενεργοί: {dt.Rows[0]["Active"]} | Ανενεργοί: {dt.Rows[0]["Inactive"]}";
            }
            catch { }
        }

        // ==========================================
        // ✅ ΜΟΡΦΟΠΟΙΗΣΗ GRID
        // ==========================================
        private void FormatGrid()
        {
            dgvEmployees.EnableHeadersVisualStyles = false;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.AllowUserToDeleteRows = false;
            dgvEmployees.ReadOnly = true;
            dgvEmployees.RowTemplate.Height = 35;

            dgvEmployees.ColumnHeadersDefaultCellStyle.BackColor = StandardFormLayout.GridHeaderBg;
            dgvEmployees.ColumnHeadersDefaultCellStyle.ForeColor = StandardFormLayout.GridHeaderFg;
            dgvEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvEmployees.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEmployees.ColumnHeadersHeight = 40;

            dgvEmployees.DefaultCellStyle.BackColor = Color.White;
            dgvEmployees.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvEmployees.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvEmployees.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvEmployees.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvEmployees.GridColor = Color.FromArgb(220, 230, 240);
            dgvEmployees.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Μορφοποίηση αριθμητικών στηλών
            foreach (DataGridViewColumn col in dgvEmployees.Columns)
            {
                if (col.HeaderText.Contains("Προμήθεια"))
                {
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Χρωματισμός κατάστασης
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                string status = row.Cells["Κατάσταση"].Value?.ToString() ?? "";
                if (status == "Ανενεργός")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                    row.Cells["Κατάσταση"].Style.ForeColor = Color.Red;
                }
                else
                {
                    row.Cells["Κατάσταση"].Style.ForeColor = Color.Green;
                }
            }
        }

        // ==========================================
        // ✅ ΚΟΥΜΠΙ: ΑΝΑΖΗΤΗΣΗ
        // ==========================================
        private void btnSearch_Click(object sender, EventArgs e) => LoadEmployees(txtSearch.Text.Trim());

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        // ==========================================
        // ✅ ΚΟΥΜΠΙ: ΕΜΦΑΝΙΣΗ ΑΝΕΝΕΡΓΩΝ
        // ==========================================
        private void chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            _showInactive = chkShowInactive.Checked;
            LoadEmployees(txtSearch.Text.Trim());
        }

        // ==========================================
        // ✅ ΚΟΥΜΠΙ: ΝΕΟΣ ΠΩΛΗΤΗΣ/ΕΙΣΠΡΑΚΤΟΡΑΣ
        // ==========================================
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeEditForm editForm = new EmployeeEditForm(0);
                editForm.FormClosed += (s, args) =>
                {
                    LoadEmployees(txtSearch.Text.Trim());
                    LoadStats();
                };
                editForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα ανοίγματος φόρμας: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // ✅ ΚΟΥΜΠΙ: ΕΠΕΞΕΡΓΑΣΙΑ
        // ==========================================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("⚠️ Επίλεξε εγγραφή!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);
                EmployeeEditForm editForm = new EmployeeEditForm(id);
                editForm.FormClosed += (s, args) =>
                {
                    LoadEmployees(txtSearch.Text.Trim());
                    LoadStats();
                };
                editForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα επεξεργασίας: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // ✅ ΚΟΥΜΠΙ: ΑΠΕΝΕΡΓΟΠΟΙΗΣΗ
        // ==========================================
        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("⚠️ Επίλεξε εγγραφή!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = dgvEmployees.SelectedRows[0].Cells["Ονοματεπώνυμο"].Value?.ToString() ?? "";
            string status = dgvEmployees.SelectedRows[0].Cells["Κατάσταση"].Value?.ToString() ?? "";

            if (status == "Ανενεργός")
            {
                // Αν είναι ανενεργός, πρόταση επανενεργοποίησης
                if (MessageBox.Show($"Ο/Η {name} είναι ΑΝΕΝΕΡΓΟΣ.\nΘέλεις να τον/την ΕΠΑΝΕΝΕΡΓΟΠΟΙΗΣΕΙΣ;",
                    "Επανενεργοποίηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);
                        DatabaseHelper.ExecuteQuery("UPDATE Employees SET IsActive = 1 WHERE Id = @Id",
                            new SqlParameter("@Id", id));
                        MessageBox.Show($"✅ Ο/Η {name} επανενεργοποιήθηκε!", "Επιτυχία");
                        LoadEmployees(txtSearch.Text.Trim());
                        LoadStats();
                    }
                    catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
                }
                return;
            }

            if (MessageBox.Show($"Θέλεις να απενεργοποιήσεις τον/την {name};",
                "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);
                    DatabaseHelper.ExecuteQuery("UPDATE Employees SET IsActive = 0 WHERE Id = @Id",
                        new SqlParameter("@Id", id));
                    MessageBox.Show($"✅ Ο/Η {name} απενεργοποιήθηκε!", "Επιτυχία");
                    LoadEmployees(txtSearch.Text.Trim());
                    LoadStats();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        // ==========================================
        // ✅ ΚΟΥΜΠΙ: ΚΛΕΙΣΙΜΟ
        // ==========================================
        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        // ==========================================
        // ✅ DOUBLE CLICK ΣΤΟ GRID
        // ==========================================
        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit.PerformClick();
        }
    }
}