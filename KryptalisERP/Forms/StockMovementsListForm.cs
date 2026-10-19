using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class StockMovementsListForm : Form
    {
        private DataGridView dgvList;
        private Button btnNew, btnEdit, btnRefresh;

        public StockMovementsListForm()
        {
            CreateUI();
            this.Load += StockMovementsListForm_Load;
        }

        private void CreateUI()
        {
            this.Text = "Ιστορικό Κινήσεων Αποθήκης";
            this.Size = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(10) };

            btnNew = new Button { Text = "➕ Νέα Κίνηση", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(120, 40), BackColor = System.Drawing.Color.FromArgb(46, 204, 113), ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat };
            btnNew.Click += (s, e) => OpenNewMovement();
            pnlTop.Controls.Add(btnNew);

            btnEdit = new Button { Text = "✏️ Επεξεργασία", Location = new System.Drawing.Point(140, 10), Size = new System.Drawing.Size(120, 40), BackColor = System.Drawing.Color.FromArgb(52, 152, 219), ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat };
            btnEdit.Click += (s, e) => OpenEditMovement();
            pnlTop.Controls.Add(btnEdit);

            btnRefresh = new Button { Text = "🔄 Ανανέωση", Location = new System.Drawing.Point(270, 10), Size = new System.Drawing.Size(100, 40), BackColor = System.Drawing.Color.Gray, ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat };
            btnRefresh.Click += (s, e) => LoadMovements();
            pnlTop.Controls.Add(btnRefresh);

            this.Controls.Add(pnlTop);

            dgvList = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, BackgroundColor = System.Drawing.Color.White };
            dgvList.CellDoubleClick += (s, e) => OpenEditMovement();
            this.Controls.Add(dgvList);
        }

        private void StockMovementsListForm_Load(object sender, EventArgs e) { LoadMovements(); }

        private void LoadMovements()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // Εδώ τραβάμε τη λίστα των παραστατικών (Headers)
                var data = conn.Query(@"
                    SELECT Id, TransferNumber AS [Αριθμός], TransferDate AS [Ημερομηνία], 
                           Status AS [Κατάσταση], Notes AS [Παρατηρήσεις]
                    FROM StockTransfers ORDER BY TransferDate DESC").ToList();
                dgvList.DataSource = data;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void OpenNewMovement()
        {
            using var frm = new StockMovementForm(null);
            frm.ShowDialog();
            LoadMovements(); // Refresh λίστας
        }

        private void OpenEditMovement()
        {
            if (dgvList.CurrentRow == null) return;
            var id = dgvList.CurrentRow.Cells["Id"].Value;
            if (id != null)
            {
                using var frm = new StockMovementForm(Convert.ToInt32(id));
                frm.ShowDialog();
                LoadMovements();
            }
        }

        private string GetConnectionString()
        {
            string connStr = Program.Configuration?.GetConnectionString("DefaultConnection");
            return string.IsNullOrEmpty(connStr) ? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;" : connStr;
        }
    }
}