using KryptalisERP.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ChargesForm : Form
    {
        public DataTable SelectedCharges { get; private set; }

        public ChargesForm()
        {
            InitializeComponent();
            SelectedCharges = new DataTable();
            SelectedCharges.Columns.Add("Id", typeof(int));
            SelectedCharges.Columns.Add("ChargeName", typeof(string));
            SelectedCharges.Columns.Add("ChargeType", typeof(string));
            SelectedCharges.Columns.Add("Rate", typeof(decimal));
            LoadCharges();
        }

        private void LoadCharges()
        {
            try
            {
                // Φόρτωση Κρατήσεων
                DataTable withholdings = DatabaseHelper.ExecuteQuery("SELECT Id, Name, Rate FROM TaxWithholdingRates WHERE IsActive = 1 ORDER BY Name");
                foreach (DataRow row in withholdings.Rows)
                {
                    dgvCharges.Rows.Add(false, row["Name"], "Παρακράτηση", row["Rate"]);
                }

                // Φόρτωση Επιβαρύνσεων
                DataTable surcharges = DatabaseHelper.ExecuteQuery("SELECT Id, SurchargeName, SurchargeRate FROM Surcharges WHERE IsActive = 1 ORDER BY SurchargeName");
                foreach (DataRow row in surcharges.Rows)
                {
                    dgvCharges.Rows.Add(false, row["SurchargeName"], "Επιβάρυνση", row["SurchargeRate"]);
                }

                // Fallback αν οι πίνακες είναι άδειοι
                if (dgvCharges.Rows.Count == 0)
                {
                    dgvCharges.Rows.Add(false, "Παρακράτηση Φόρου 3%", "Παρακράτηση", 3.00);
                    dgvCharges.Rows.Add(false, "Παρακράτηση Φόρου 6%", "Παρακράτηση", 6.00);
                    dgvCharges.Rows.Add(false, "Επιβάρυνση 2%", "Επιβάρυνση", 2.00);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης χρεώσεων: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SelectedCharges.Rows.Clear();
            foreach (DataGridViewRow row in dgvCharges.Rows)
            {
                if (row.Cells["colSelect"].Value != null && Convert.ToBoolean(row.Cells["colSelect"].Value))
                {
                    SelectedCharges.Rows.Add(0, row.Cells["colName"].Value?.ToString(), row.Cells["colType"].Value?.ToString(), Convert.ToDecimal(row.Cells["colRate"].Value ?? 0));
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}