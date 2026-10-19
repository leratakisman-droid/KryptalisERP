using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemPriceHistoryForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemPriceHistoryForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemPriceHistoryForm_Load;
        }

        private void ItemPriceHistoryForm_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void LoadHistory()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvHistory.DataSource = conn.Query(@"
                    SELECT Id,
                           ValidFrom AS [Ημερομηνία Από], 
                           ValidTo AS [Ημερομηνία Έως],
                           OldPrice AS [Παλιά Τιμή], 
                           NewPrice AS [Νέα Τιμή], 
                           Currency AS [Νόμισμα],
                           ChangeReason AS [Λόγος], 
                           ChangeType AS [Τύπος],
                           Notes AS [Σημειώσεις]
                    FROM ItemPriceHistory 
                    WHERE ItemId = @Id 
                    ORDER BY ValidFrom DESC",
                    new { Id = _itemId }).ToList();
                if (dgvHistory.Columns.Contains("Id")) dgvHistory.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}