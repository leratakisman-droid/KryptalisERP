using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SupplierBranchesForm : Form
    {
        private int _supplierId;
        private string _supplierName;

        public SupplierBranchesForm(int supplierId, string supplierName)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _supplierName = supplierName;
            this.Text = $"🏢 Υποκαταστήματα - {supplierName}";
            LoadBranches();
        }

        private void LoadBranches()
        {
            try
            {
                string sql = @"SELECT 
                    Id,
                    BranchName AS [Όνομα Υποκαταστήματος],
                    BranchCode AS [Κωδικός],
                    Phone1 AS [Τηλέφωνο],
                    Email AS [Email],
                    City AS [Πόλη],
                    DOY AS [ΔΟΥ],
                    CAST(ISNULL(IsActive, 1) AS BIT) AS [Ενεργό]
                FROM SupplierBranches 
                WHERE SupplierId = @SupplierId
                ORDER BY BranchName";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    new SqlParameter("@SupplierId", _supplierId));

                dgvBranches.DataSource = dt;
                if (dgvBranches.Columns.Contains("Id")) dgvBranches.Columns["Id"].Visible = false;

                dgvBranches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvBranches.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 175, 55);
                dgvBranches.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dgvBranches.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);

                lblCount.Text = $"Συνολικά: {dt.Rows.Count} υποκαταστήματα";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenBranchEditForm(0);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBranches.SelectedRows.Count == 0)
            {
                MessageBox.Show("Επίλεξε ένα υποκατάστημα!", "Προσοχή");
                return;
            }
            int id = Convert.ToInt32(dgvBranches.SelectedRows[0].Cells["Id"].Value);
            OpenBranchEditForm(id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBranches.SelectedRows.Count == 0)
            {
                MessageBox.Show("Επίλεξε ένα υποκατάστημα!", "Προσοχή");
                return;
            }
            if (MessageBox.Show("Θέλεις σίγουρα να διαγράψεις;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvBranches.SelectedRows[0].Cells["Id"].Value);
                    DatabaseHelper.ExecuteQuery("DELETE FROM SupplierBranches WHERE Id = @Id",
                        new SqlParameter("@Id", id));
                    MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία");
                    LoadBranches();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Σφάλμα: " + ex.Message);
                }
            }
        }

        private void OpenBranchEditForm(int branchId)
        {
            SupplierBranchEditForm editForm = new SupplierBranchEditForm(_supplierId, branchId);
            editForm.FormClosed += (s, args) => LoadBranches();
            editForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}