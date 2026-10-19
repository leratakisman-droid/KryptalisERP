using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace KryptalisERP
{
    public partial class ItemStockForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemStockForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemStockForm_Load;
        }

        private void ItemStockForm_Load(object sender, EventArgs e)
        {
            LoadStock();
            LoadWarehouses();
            LoadVariants();
        }

        private void LoadWarehouses()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var warehouses = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList();
                cmbWarehouse.DataSource = warehouses; cmbWarehouse.DisplayMember = "Name"; cmbWarehouse.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadVariants()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var variants = conn.Query("SELECT Id, VariantCode FROM ItemVariants WHERE ItemId = @Id ORDER BY VariantCode", new { Id = _itemId }).ToList();
                cmbVariant.DataSource = variants; cmbVariant.DisplayMember = "VariantCode"; cmbVariant.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadStock()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvStock.DataSource = conn.Query(@"
                    SELECT sv.Id, w.Name AS [Αποθήκη], iv.VariantCode AS [Παραλλαγή],
                           sv.Quantity AS [Ποσότητα], sv.ReservedQty AS [Δεσμευμένο], 
                           sv.AvailableQty AS [Διαθέσιμο], sv.MinStock AS [Ελάχιστο], 
                           sv.MaxStock AS [Μέγιστο], sv.Notes AS [Σημειώσεις]
                    FROM StockVariants sv
                    LEFT JOIN Warehouses w ON sv.WarehouseId = w.Id
                    LEFT JOIN ItemVariants iv ON sv.ItemVariantId = iv.Id
                    WHERE iv.ItemId = @Id",
                    new { Id = _itemId }).ToList();
                if (dgvStock.Columns.Contains("Id")) dgvStock.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbWarehouse.SelectedValue == null || cmbVariant.SelectedValue == null)
            { MessageBox.Show("Επίλεξε αποθήκη και παραλλαγή!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                // AvailableQty είναι COMPUTED column - δεν το βάζουμε στο INSERT
                conn.Execute(@"INSERT INTO StockVariants (WarehouseId, ItemVariantId, Quantity, ReservedQty, MinStock, MaxStock, Notes) 
            VALUES (@Warehouse, @Variant, @Qty, 0, @Min, @Max, @Notes)", new
                {
                    Warehouse = cmbWarehouse.SelectedValue,
                    Variant = cmbVariant.SelectedValue,
                    Qty = numQuantity.Value,
                    Min = numMinStock.Value,
                    Max = numMaxStock.Value,
                    Notes = txtNotes.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadStock();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStock.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvStock.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM StockVariants WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadStock();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}