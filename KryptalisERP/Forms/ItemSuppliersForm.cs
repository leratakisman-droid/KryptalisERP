using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemSuppliersForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemSuppliersForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemSuppliersForm_Load;
        }

        private void ItemSuppliersForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            LoadAllSuppliers();
        }

        private void LoadAllSuppliers()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                // ✅ ΧΡΗΣΗ SupplierName αντί για Epwnymia
                var suppliers = conn.Query("SELECT Id, ISNULL(SupplierName, '') AS Name FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList();
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "Name";
                cmbSupplier.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadSuppliers()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                // ✅ ΧΡΗΣΗ SupplierName αντί για Epwnymia, χωρίς IsActive filter
                dgvSuppliers.DataSource = conn.Query(@"
                    SELECT si.Id, ISNULL(s.SupplierName, '') AS [Προμηθευτής], si.SupplierCode AS [Κωδικός], 
                           si.PurchasePrice AS [Τιμή Αγοράς], si.Currency AS [Νόμισμα],
                           si.LeadTime AS [Ημέρες], 
                           CASE WHEN si.IsDefaultSupplier = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Βασικός],
                           si.LastPurchaseDate AS [Τελευταία Αγορά]
                    FROM SupplierItems si
                    LEFT JOIN Suppliers s ON si.SupplierId = s.Id
                    WHERE si.ItemId = @Id",
                    new { Id = _itemId }).ToList();
                if (dgvSuppliers.Columns.Contains("Id")) dgvSuppliers.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedValue == null) { MessageBox.Show("Επίλεξε προμηθευτή!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO SupplierItems (SupplierId, ItemId, SupplierCode, PurchasePrice, Currency, MinOrderQty, LeadTime, IsDefaultSupplier, Notes, IsActive) 
                    VALUES (@Supplier, @Item, @Code, @Price, @Currency, @MinQty, @Lead, @Default, @Notes, 1)", new
                {
                    Supplier = cmbSupplier.SelectedValue,
                    Item = _itemId,
                    Code = txtSupplierCode.Text.Trim(),
                    Price = numPrice.Value,
                    Currency = cmbCurrency.Text.Trim(),
                    MinQty = numMinQty.Value,
                    Lead = (int)numLeadTime.Value,
                    Default = chkIsDefault.Checked,
                    Notes = txtNotes.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadSuppliers();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvSuppliers.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM SupplierItems WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadSuppliers();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}