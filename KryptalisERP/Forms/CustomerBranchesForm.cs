using System;
using System.Data;
using System.Windows.Forms;
using Dapper;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class CustomerBranchesForm : Form
    {
        private readonly int _clientId;
        private readonly string _clientName;

        // =========================================================
        // ΣΥΝΔΕΣΗ ΜΕ ΤΗ ΒΑΣΗ
        // =========================================================
        private string GetConnectionString()
        {
            return "Server=.;Database=KryptalisERP;Integrated Security=True;TrustServerCertificate=True;";
        }

        public CustomerBranchesForm(int clientId, string clientName)
        {
            InitializeComponent();

            _clientId = clientId;
            _clientName = clientName ?? string.Empty;

            Text = "🏢 Υποκαταστήματα - " + _clientName;

            LoadBranches();
        }

        private void LoadBranches()
        {
            try
            {
                using var connection = new SqlConnection(GetConnectionString());

                string sql = @"SELECT Id, CustomerId, IsMain, BranchCode, BranchName, Address, City, PostalCode, Phone, Mobile, Email, ManagerName, IsActive
                               FROM CustomerBranches 
                               WHERE CustomerId = @CustomerId
                               ORDER BY IsMain DESC, BranchName";

                var branches = connection.Query<dynamic>(sql, new { CustomerId = _clientId });

                dgvBranches.DataSource = branches.ToList();

                // Απόκρυψη τεχνικών στηλών
                if (dgvBranches.Columns.Contains("Id")) dgvBranches.Columns["Id"].Visible = false;
                if (dgvBranches.Columns.Contains("CustomerId")) dgvBranches.Columns["CustomerId"].Visible = false;

                // Ενημέρωση Label αν υπάρχει
                if (lblRecordCount != null)
                    lblRecordCount.Text = "Εγγραφές: " + dgvBranches.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Σφάλμα φόρτωσης υποκαταστημάτων:\n\n" + ex.Message,
                    "Σφάλμα",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int GetSelectedBranchId()
        {
            if (dgvBranches.CurrentRow == null)
                return 0;

            object value = dgvBranches.CurrentRow.Cells["Id"].Value;

            if (value == null || value == DBNull.Value)
                return 0;

            return Convert.ToInt32(value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenBranchEditForm(0);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int branchId = GetSelectedBranchId();

            if (branchId <= 0)
            {
                MessageBox.Show(
                    "Επίλεξε πρώτα ένα υποκατάστημα.",
                    "Προσοχή",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            OpenBranchEditForm(branchId);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int branchId = GetSelectedBranchId();

            if (branchId <= 0)
            {
                MessageBox.Show(
                    "Επίλεξε πρώτα ένα υποκατάστημα.",
                    "Προσοχή",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult answer = MessageBox.Show(
                "Θέλεις να απενεργοποιήσεις το επιλεγμένο υποκατάστημα;",
                "Επιβεβαίωση",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
                return;

            try
            {
                using var connection = new SqlConnection(GetConnectionString());

                string sql = "UPDATE CustomerBranches SET IsActive = 0 WHERE Id = @Id";

                connection.Execute(sql, new { Id = branchId });

                LoadBranches();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Σφάλμα απενεργοποίησης υποκαταστήματος:\n\n" + ex.Message,
                    "Σφάλμα",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenBranchEditForm(int branchId)
        {
            // Χρησιμοποιούμε τον constructor που φτιάξαμε πριν (customerId, branchId)
            using var dialog = new CustomerBranchEditForm(_clientId, branchId);

            if (dialog.ShowDialog(this) == DialogResult.OK)
                LoadBranches();
        }

        private void dgvBranches_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int branchId = GetSelectedBranchId();

            if (branchId > 0)
                OpenBranchEditForm(branchId);
        }
    }
}