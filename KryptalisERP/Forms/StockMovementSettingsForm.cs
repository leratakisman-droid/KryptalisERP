using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Runtime;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class StockMovementSettingsForm : Form
    {
        public StockMovementSettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            string sql = @"SELECT Id, 
                MovementName AS [Τύπος Κίνησης],
                IncreaseQuantity AS [↑ Ποσότητα],
                DecreaseQuantity AS [↓ Ποσότητα],
                IncreaseValue AS [↑ Αξία],
                DecreaseValue AS [↓ Αξία],
                RequireSourceWarehouse AS [Απ. Αποθ. Προέλευσης],
                RequireDestWarehouse AS [Απ. Αποθ. Προορισμού],
                RequireQuantity AS [Απ. Ποσότητα],
                RequirePrice AS [Απ. Τιμή]
                FROM StockMovementTypeSettings 
                WHERE IsActive = 1 
                ORDER BY Id";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);
            dgvSettings.DataSource = dt;

            // Κάνουμε τα checkboxes editable
            foreach (DataGridViewColumn col in dgvSettings.Columns)
            {
                if (col.Name != "Id" && col.Name != "Τύπος Κίνησης")
                {
                    col.ReadOnly = false;
                    if (col is DataGridViewCheckBoxColumn cbCol)
                    {
                        cbCol.TrueValue = true;
                        cbCol.FalseValue = false;
                    }
                }
            }

            dgvSettings.Columns["Id"].Visible = false;
            dgvSettings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvSettings.Rows)
                {
                    if (row.IsNewRow) continue;

                    int id = Convert.ToInt32(row.Cells["Id"].Value);
                    bool incQty = Convert.ToBoolean(row.Cells["↑ Ποσότητα"].Value);
                    bool decQty = Convert.ToBoolean(row.Cells["↓ Ποσότητα"].Value);
                    bool incVal = Convert.ToBoolean(row.Cells["↑ Αξία"].Value);
                    bool decVal = Convert.ToBoolean(row.Cells["↓ Αξία"].Value);
                    bool reqSrc = Convert.ToBoolean(row.Cells["Απ. Αποθ. Προέλευσης"].Value);
                    bool reqDst = Convert.ToBoolean(row.Cells["Απ. Αποθ. Προορισμού"].Value);
                    bool reqQty = Convert.ToBoolean(row.Cells["Απ. Ποσότητα"].Value);
                    bool reqPrc = Convert.ToBoolean(row.Cells["Απ. Τιμή"].Value);

                    DatabaseHelper.ExecuteQuery(@"
                        UPDATE StockMovementTypeSettings SET 
                        IncreaseQuantity=@IncQty, DecreaseQuantity=@DecQty,
                        IncreaseValue=@IncVal, DecreaseValue=@DecVal,
                        RequireSourceWarehouse=@ReqSrc, RequireDestWarehouse=@ReqDst,
                        RequireQuantity=@ReqQty, RequirePrice=@ReqPrc
                        WHERE Id=@Id",
                        new SqlParameter("@Id", id),
                        new SqlParameter("@IncQty", incQty),
                        new SqlParameter("@DecQty", decQty),
                        new SqlParameter("@IncVal", incVal),
                        new SqlParameter("@DecVal", decVal),
                        new SqlParameter("@ReqSrc", reqSrc),
                        new SqlParameter("@ReqDst", reqDst),
                        new SqlParameter("@ReqQty", reqQty),
                        new SqlParameter("@ReqPrc", reqPrc));
                }
                MessageBox.Show("✅ Οι ρυθμίσεις αποθηκεύτηκαν!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
