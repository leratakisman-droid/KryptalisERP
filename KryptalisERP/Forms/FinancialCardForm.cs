using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class FinancialCardForm : Form
    {
        private int _clientId;
        private string _clientName;

        public FinancialCardForm(int clientId, string clientName)
        {
            InitializeComponent();
            _clientId = clientId;
            _clientName = clientName;
            this.Text = "💰 Καρτέλα Πελάτη: " + clientName;
            LoadFinancials();
        }

        private void LoadFinancials()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetClientFinancials", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientId", _clientId);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        // Table[0] = Ιστορικό Κινήσεων
                        dgvHistory.DataSource = ds.Tables[0];

                        // Table[1] = Σύνοψη (με ελληνικά ονόματα)
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            DataRow summary = ds.Tables[1].Rows[0];

                            // ΧΡΗΣΙΜΟΠΟΙΟΥΜΕ ΤΑ ΕΛΛΗΝΙΚΑ ΟΝΟΜΑΤΑ
                            lblTotalSales.Text = "Σύνολο Πωλήσεων: " + Convert.ToDecimal(summary["Σύνολο Πωλήσεων"]).ToString("N2") + " €";
                            lblTotalPayments.Text = "Σύνολο Εισπράξεων: " + Convert.ToDecimal(summary["Σύνολο Εισπράξεων"]).ToString("N2") + " €";

                            decimal balance = Convert.ToDecimal(summary["Υπόλοιπο"]);
                            lblBalance.Text = "ΤΡΕΧΟΝ ΥΠΟΛΟΙΠΟ: " + balance.ToString("N2") + " €";

                            // Χρωματισμός Υπολοίπου
                            if (balance > 0)
                            {
                                lblBalance.ForeColor = Color.Red;
                                lblBalance.Text += " (Χρέος Πελάτη)";
                            }
                            else if (balance < 0)
                            {
                                lblBalance.ForeColor = Color.Green;
                                lblBalance.Text += " (Πλεόνασμα/Προκαταβολή)";
                            }
                            else
                            {
                                lblBalance.ForeColor = Color.Gray;
                                lblBalance.Text += " (Εξοφλημένος)";
                            }
                        }

                        // Μορφοποίηση Grid
                        FormatGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης καρτέλας: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvHistory.Columns["Ημερομηνία"] != null) dgvHistory.Columns["Ημερομηνία"].HeaderText = "Ημερομηνία";
            if (dgvHistory.Columns["Αριθμός"] != null) dgvHistory.Columns["Αριθμός"].HeaderText = "Αριθμός";
            if (dgvHistory.Columns["Τύπος"] != null) dgvHistory.Columns["Τύπος"].HeaderText = "Τύπος";
            if (dgvHistory.Columns["Ποσό"] != null) dgvHistory.Columns["Ποσό"].HeaderText = "Ποσό";
            if (dgvHistory.Columns["Κίνηση"] != null) dgvHistory.Columns["Κίνηση"].HeaderText = "Κίνηση";
            if (dgvHistory.Columns["Σημειώσεις"] != null) dgvHistory.Columns["Σημειώσεις"].HeaderText = "Σημειώσεις";

            if (dgvHistory.Columns["Ποσό"] != null)
            {
                dgvHistory.Columns["Ποσό"].DefaultCellStyle.Format = "N2";
                dgvHistory.Columns["Ποσό"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.RowsDefaultCellStyle.BackColor = Color.White;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}