using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KryptalisERP
{
    // ✅ Βεβαιώσου ότι υπάρχει το ": Form" εδώ κάτω
    public partial class SupplierPriceListEditForm : Form
    {
        private readonly int _supplierId;
        private readonly int _listId;
        private readonly string _connStr;

        public SupplierPriceListEditForm(int supplierId, int listId = 0)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _listId = listId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SupplierPriceListEditForm_Load;
        }

        private void SupplierPriceListEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _listId > 0 ? "✏️ Επεξεργασία Τιμοκαταλόγου" : "➕ Νέος Τιμοκατάλογος";
            if (_listId > 0) LoadData();
        }

        private void LoadData()
        {
            using var conn = new SqlConnection(_connStr);
            var list = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM SupplierPriceLists WHERE Id = @Id", new { Id = _listId });
            if (list == null) return;

            txtCode.Text = list.Code;
            txtName.Text = list.Name;
            txtDescription.Text = list.Description;
            cmbCurrency.Text = list.Currency ?? "EUR";
            dtpFrom.Value = list.ValidFrom ?? DateTime.Today;
            dtpTo.Value = list.ValidTo ?? DateTime.Today.AddYears(1);
            chkActive.Checked = list.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();

                if (_listId > 0)
                {
                    conn.Execute(@"UPDATE SupplierPriceLists SET Code=@Code, Name=@Name, Description=@Description, 
                        Currency=@Currency, ValidFrom=@ValidFrom, ValidTo=@ValidTo, IsActive=@IsActive WHERE Id=@Id AND SupplierId=@SupplierId",
                        new
                        {
                            Id = _listId,
                            SupplierId = _supplierId,
                            Code = txtCode.Text.Trim(),
                            Name = txtName.Text.Trim(),
                            Description = txtDescription.Text.Trim(),
                            Currency = cmbCurrency.Text.Trim(),
                            ValidFrom = dtpFrom.Value,
                            ValidTo = dtpTo.Value,
                            IsActive = chkActive.Checked
                        }, tran);
                }
                else
                {
                    conn.Execute(@"INSERT INTO SupplierPriceLists (SupplierId, Code, Name, Description, Currency, ValidFrom, ValidTo, IsActive) 
                        VALUES (@SupplierId, @Code, @Name, @Description, @Currency, @ValidFrom, @ValidTo, @IsActive)",
                        new
                        {
                            SupplierId = _supplierId,
                            Code = txtCode.Text.Trim(),
                            Name = txtName.Text.Trim(),
                            Description = txtDescription.Text.Trim(),
                            Currency = cmbCurrency.Text.Trim(),
                            ValidFrom = dtpFrom.Value,
                            ValidTo = dtpTo.Value,
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