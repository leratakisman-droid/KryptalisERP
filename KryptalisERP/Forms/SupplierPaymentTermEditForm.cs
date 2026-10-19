using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    // ✅ Βεβαιώσου ότι υπάρχει το ": Form" εδώ κάτω
    public partial class SupplierPaymentTermEditForm : Form
    {
        private readonly int _supplierId;
        private readonly int _termId;
        private readonly string _connStr;

        public SupplierPaymentTermEditForm(int supplierId, int termId = 0)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _termId = termId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SupplierPaymentTermEditForm_Load;
        }

        private void SupplierPaymentTermEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _termId > 0 ? "✏️ Επεξεργασία Όρου" : "➕ Νέος Όρος";
            if (_termId > 0) LoadData();
        }

        private void LoadData()
        {
            using var conn = new SqlConnection(_connStr);
            var term = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM SupplierPaymentTerms WHERE Id = @Id", new { Id = _termId });
            if (term == null) return;

            txtPaymentTermName.Text = term.PaymentTermName;
            chkDefault.Checked = term.IsDefault;
            txtNotes.Text = term.Notes;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();

                if (_termId > 0)
                {
                    conn.Execute(@"UPDATE SupplierPaymentTerms SET PaymentTermName=@PaymentTermName, IsDefault=@IsDefault, Notes=@Notes 
                        WHERE Id=@Id AND SupplierId=@SupplierId",
                        new
                        {
                            Id = _termId,
                            SupplierId = _supplierId,
                            PaymentTermName = txtPaymentTermName.Text.Trim(),
                            IsDefault = chkDefault.Checked,
                            Notes = txtNotes.Text.Trim()
                        }, tran);
                }
                else
                {
                    conn.Execute(@"INSERT INTO SupplierPaymentTerms (SupplierId, PaymentTermName, IsDefault, Notes) 
                        VALUES (@SupplierId, @PaymentTermName, @IsDefault, @Notes)",
                        new
                        {
                            SupplierId = _supplierId,
                            PaymentTermName = txtPaymentTermName.Text.Trim(),
                            IsDefault = chkDefault.Checked,
                            Notes = txtNotes.Text.Trim()
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