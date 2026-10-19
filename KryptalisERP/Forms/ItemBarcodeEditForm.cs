using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemBarcodeEditForm : Form
    {
        private readonly int _itemId;
        private readonly int _barcodeId;
        private readonly string _connStr;

        public ItemBarcodeEditForm(int itemId, int barcodeId = 0)
        {
            InitializeComponent();
            _itemId = itemId;
            _barcodeId = barcodeId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemBarcodeEditForm_Load;
        }

        private void ItemBarcodeEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _barcodeId > 0 ? "✏️ Επεξεργασία Barcode" : "➕ Νέο Barcode";

            // Γέμισμα ComboBox Τύπου
            cmbBarcodeType.Items.AddRange(new object[] { "EAN-13", "EAN-8", "UPC-A", "UPC-E", "Code128", "Code39", "QR", "Εσωτερικός" });
            cmbBarcodeType.SelectedIndex = 0;

            // Γέμισμα ComboBox Μονάδων Μέτρησης
            try
            {
                using var conn = new SqlConnection(_connStr);
                var units = conn.Query("SELECT Id, Name FROM UnitsOfMeasure ORDER BY Name").ToList();
                cmbUom.DataSource = units;
                cmbUom.DisplayMember = "Name";
                cmbUom.ValueMember = "Id";
            }
            catch { }

            if (_barcodeId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var bc = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM ItemBarcodes WHERE Id = @Id", new { Id = _barcodeId });
                if (bc == null) return;

                txtBarcode.Text = bc.Barcode ?? "";
                cmbBarcodeType.Text = bc.BarcodeType ?? "EAN-13";

                if (bc.UomId != null) cmbUom.SelectedValue = bc.UomId;
                numQuantity.Value = bc.Quantity ?? 1;

                chkIsPrimary.Checked = bc.IsPrimary ?? false;
                txtDescription.Text = bc.Description ?? "";
                chkIsActive.Checked = bc.IsActive ?? true;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("⚠️ Το πεδίο Barcode είναι υποχρεωτικό!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcode.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                int? uomId = cmbUom.SelectedValue is int id ? (int?)id : null;

                if (_barcodeId > 0)
                {
                    conn.Execute(@"UPDATE ItemBarcodes SET 
                        Barcode=@Barcode, BarcodeType=@Type, UomId=@UomId, Quantity=@Qty, 
                        IsPrimary=@IsPrimary, Description=@Desc, IsActive=@Active, UpdatedAt=GETDATE()
                        WHERE Id=@Id AND ItemId=@ItemId", new
                    {
                        Id = _barcodeId,
                        ItemId = _itemId,
                        Barcode = txtBarcode.Text.Trim(),
                        Type = cmbBarcodeType.Text.Trim(),
                        UomId = uomId,
                        Qty = numQuantity.Value,
                        IsPrimary = chkIsPrimary.Checked,
                        Desc = txtDescription.Text.Trim(),
                        Active = chkIsActive.Checked
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO ItemBarcodes (ItemId, Barcode, BarcodeType, UomId, Quantity, IsPrimary, Description, IsActive, CreatedAt, UpdatedAt) 
                        VALUES (@ItemId, @Barcode, @Type, @UomId, @Qty, @IsPrimary, @Desc, @Active, GETDATE(), GETDATE())", new
                    {
                        ItemId = _itemId,
                        Barcode = txtBarcode.Text.Trim(),
                        Type = cmbBarcodeType.Text.Trim(),
                        UomId = uomId,
                        Qty = numQuantity.Value,
                        IsPrimary = chkIsPrimary.Checked,
                        Desc = txtDescription.Text.Trim(),
                        Active = chkIsActive.Checked
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε επιτυχώς!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα αποθήκευσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}