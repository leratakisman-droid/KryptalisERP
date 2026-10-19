using KryptalisERP.Helpers;
using KryptalisERP.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class TransformPurchasesForm : Form
    {
        public int SelectedSeriesId { get; private set; }
        public bool Confirmed { get; private set; }
        private int _sourceId;

        public TransformPurchasesForm(int sourceId)
        {
            _sourceId = sourceId;
            InitializeComponent();
            ThemeManager.ApplyToCurrentForm(this);
            LoadPurchaseSeries();
        }

        private void LoadPurchaseSeries()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, SeriesName, Prefix FROM PurchaseSeries WHERE IsActive = 1 ORDER BY SeriesName");

                cmbTargetSeries.DataSource = dt;
                cmbTargetSeries.DisplayMember = "SeriesName";
                cmbTargetSeries.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης σειρών: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbTargetSeries.SelectedValue == null)
            {
                MessageBox.Show("⚠️ Επίλεξε μια σειρά προορισμού!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedSeriesId = Convert.ToInt32(cmbTargetSeries.SelectedValue);
            Confirmed = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Confirmed = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}