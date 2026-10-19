using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class TransformSalesForm : Form
    {
        public int SourceInvoiceId { get; private set; }
        public int SelectedSeriesId { get; private set; }
        public string SourceType { get; private set; }
        public string TargetType { get; private set; }
        public bool Confirmed { get; private set; }

        public TransformSalesForm(int sourceInvoiceId)
        {
            InitializeComponent();
            SourceInvoiceId = sourceInvoiceId;
            Confirmed = false;

            LoadSourceInfo();
            LoadTargetSeries();
        }

        private void LoadSourceInfo()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    @"SELECT si.DocNumber, s.InvoiceSubType, s.SeriesName
                      FROM SalesInvoices si
                      INNER JOIN Series s ON s.Id = si.SeriesId
                      WHERE si.Id = @Id",
                    new SqlParameter("@Id", SourceInvoiceId));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Το παραστατικό δεν βρέθηκε!", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                DataRow r = dt.Rows[0];
                SourceType = r["InvoiceSubType"]?.ToString() ?? "Invoice";
                TargetType = (SourceType == "Invoice") ? "Credit" : "Invoice";

                lblSourceInfo.Text = $"Πηγή: {r["SeriesName"]} - {r["DocNumber"]} ({SourceType})";
                lblTargetInfo.Text = $"Προορισμός: Σειρά {TargetType} (Πιστωτικό/Τιμολόγιο)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void LoadTargetSeries()
        {
            try
            {
                DataTable series = DatabaseHelper.ExecuteQuery(
                    @"SELECT Id, SeriesName, Prefix, NextNumber 
                      FROM Series 
                      WHERE IsActive = 1 
                        AND SeriesType = 'SALES'
                        AND InvoiceSubType = @Type
                      ORDER BY SeriesName",
                    new SqlParameter("@Type", TargetType));

                if (series.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"Δεν βρέθηκαν σειρές τύπου '{TargetType}'!\n\n" +
                        $"Πήγαινε στις Ρυθμίσεις → Σειρές Παραστατικών και δημιούργησε μία σειρά με:\n" +
                        $"- Κατηγορία (Type) = SALES\n" +
                        $"- Υπο-Τύπος = {TargetType}",
                        "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                cmbSeries.DataSource = series;
                cmbSeries.DisplayMember = "SeriesName";
                cmbSeries.ValueMember = "Id";
                cmbSeries.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης σειρών: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbSeries.SelectedValue == null)
            {
                MessageBox.Show("Επίλεξε σειρά προορισμού!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedSeriesId = Convert.ToInt32(cmbSeries.SelectedValue);
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