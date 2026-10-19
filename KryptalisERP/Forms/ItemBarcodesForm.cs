using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemBarcodesForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemBarcodesForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemBarcodesForm_Load;
        }

        private void ItemBarcodesForm_Load(object sender, EventArgs e)
        {
            LoadBarcodes();
        }

        private void LoadBarcodes()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                // Join με UnitsOfMeasure για να φαίνεται το όνομα της μονάδας
                var barcodes = conn.Query(@"
                    SELECT b.Id, b.Barcode AS [Barcode], b.BarcodeType AS [Τύπος], 
                           u.Name AS [Μονάδα], b.Quantity AS [Ποσότητα], 
                           CASE WHEN b.IsPrimary = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Κύριο],
                           b.Description AS [Περιγραφή]
                    FROM ItemBarcodes b
                    LEFT JOIN UnitsOfMeasure u ON b.UomId = u.Id
                    WHERE b.ItemId = @Id
                    ORDER BY b.IsPrimary DESC, b.Barcode",
                    new { Id = _itemId }).ToList();

                dgvBarcodes.DataSource = barcodes;
                if (dgvBarcodes.Columns.Contains("Id")) dgvBarcodes.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var form = new ItemBarcodeEditForm(_itemId, 0);
            if (form.ShowDialog() == DialogResult.OK) LoadBarcodes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBarcodes.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα barcode!", "Προσοχή"); return; }
            var id = dgvBarcodes.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            using var form = new ItemBarcodeEditForm(_itemId, Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadBarcodes();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBarcodes.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα barcode!", "Προσοχή"); return; }
            var id = dgvBarcodes.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            if (MessageBox.Show("Είσαι σίγουρος ότι θέλεις να διαγράψεις αυτό το barcode;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Execute("DELETE FROM ItemBarcodes WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε επιτυχώς!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBarcodes();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα διαγραφής: " + ex.Message, "Σφάλμα"); }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}