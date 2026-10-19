using Dapper;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KryptalisERP
{
    // ✅ Βεβαιώσου ότι υπάρχει το ": Form" εδώ κάτω
    public partial class SupplierItemEditForm : Form
    {
        private readonly int _supplierId;
        private readonly int _itemId;
        private readonly string _connStr;

        public SupplierItemEditForm(int supplierId, int itemId = 0)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SupplierItemEditForm_Load;
        }

        private void SupplierItemEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _itemId > 0 ? "✏️ Επεξεργασία Είδους" : "➕ Νέο Είδος";
            if (_itemId > 0) LoadData();
        }

        private void LoadData()
        {
            using var conn = new SqlConnection(_connStr);
            var item = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM SupplierItems WHERE Id = @Id", new { Id = _itemId });
            if (item == null) return;

            txtCode.Text = item.SupplierCode;
            txtDescription.Text = item.SupplierDescription;
            numPrice.Value = item.PurchasePrice ?? 0;
            txtCurrency.Text = item.Currency ?? "EUR";
            chkActive.Checked = item.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();

                if (_itemId > 0)
                {
                    conn.Execute(@"UPDATE SupplierItems SET SupplierCode=@SupplierCode, SupplierDescription=@SupplierDescription, 
                        PurchasePrice=@PurchasePrice, Currency=@Currency, IsActive=@IsActive WHERE Id=@Id AND SupplierId=@SupplierId",
                        new
                        {
                            Id = _itemId,
                            SupplierId = _supplierId,
                            SupplierCode = txtCode.Text.Trim(),
                            SupplierDescription = txtDescription.Text.Trim(),
                            PurchasePrice = numPrice.Value,
                            Currency = txtCurrency.Text.Trim(),
                            IsActive = chkActive.Checked
                        }, tran);
                }
                else
                {
                    conn.Execute(@"INSERT INTO SupplierItems (SupplierId, SupplierCode, SupplierDescription, PurchasePrice, Currency, IsActive) 
                        VALUES (@SupplierId, @SupplierCode, @SupplierDescription, @PurchasePrice, @Currency, @IsActive)",
                        new
                        {
                            SupplierId = _supplierId,
                            SupplierCode = txtCode.Text.Trim(),
                            SupplierDescription = txtDescription.Text.Trim(),
                            PurchasePrice = numPrice.Value,
                            Currency = txtCurrency.Text.Trim(),
                            IsActive = chkActive.Checked
                        }, tran);
                }

                tran.Commit();
                this.DialogResult = DialogResult.OK; this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}