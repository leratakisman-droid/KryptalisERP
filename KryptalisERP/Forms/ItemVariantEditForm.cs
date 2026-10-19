using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemVariantEditForm : Form
    {
        private readonly int _itemId;
        private readonly int _variantId;
        private readonly string _connStr;

        public ItemVariantEditForm(int itemId, int variantId = 0)
        {
            InitializeComponent();
            _itemId = itemId;
            _variantId = variantId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemVariantEditForm_Load;
        }

        private void ItemVariantEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _variantId > 0 ? "✏️ Επεξεργασία Παραλλαγής" : " Νέα Παραλλαγή";
            chkIsActive.Checked = true;
            if (_variantId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var variant = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM ItemVariants WHERE Id = @Id", new { Id = _variantId });
                if (variant == null) return;

                txtVariantCode.Text = variant.VariantCode ?? "";
                txtSku.Text = variant.Sku ?? "";
                txtBarcode.Text = variant.Barcode ?? "";
                numPurchasePrice.Value = variant.PurchasePrice ?? 0;
                numSalesPrice.Value = variant.SalesPrice ?? 0;
                numWeight.Value = variant.Weight ?? 0;
                txtNotes.Text = variant.Notes ?? "";
                chkIsActive.Checked = variant.IsActive ?? true;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVariantCode.Text))
            {
                MessageBox.Show("️ Ο Κωδικός Παραλλαγής είναι υποχρεωτικός!");
                txtVariantCode.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_variantId > 0)
                {
                    conn.Execute(@"UPDATE ItemVariants SET VariantCode=@VariantCode, Sku=@Sku, Barcode=@Barcode,
                        PurchasePrice=@PurchasePrice, SalesPrice=@SalesPrice, Weight=@Weight, Notes=@Notes, 
                        IsActive=@IsActive, UpdatedAt=GETDATE()
                        WHERE Id=@Id AND ItemId=@ItemId", new
                    {
                        Id = _variantId,
                        ItemId = _itemId,
                        VariantCode = txtVariantCode.Text.Trim(),
                        Sku = txtSku.Text.Trim(),
                        Barcode = txtBarcode.Text.Trim(),
                        PurchasePrice = numPurchasePrice.Value,
                        SalesPrice = numSalesPrice.Value,
                        Weight = numWeight.Value,
                        Notes = txtNotes.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO ItemVariants (ItemId, VariantCode, Sku, Barcode, PurchasePrice, SalesPrice, Weight, Notes, IsActive, CreatedAt) 
                        VALUES (@ItemId, @VariantCode, @Sku, @Barcode, @PurchasePrice, @SalesPrice, @Weight, @Notes, @IsActive, GETDATE())", new
                    {
                        ItemId = _itemId,
                        VariantCode = txtVariantCode.Text.Trim(),
                        Sku = txtSku.Text.Trim(),
                        Barcode = txtBarcode.Text.Trim(),
                        PurchasePrice = numPurchasePrice.Value,
                        SalesPrice = numSalesPrice.Value,
                        Weight = numWeight.Value,
                        Notes = txtNotes.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}