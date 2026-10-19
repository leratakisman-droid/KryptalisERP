using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KryptalisERP
{
    public partial class ItemCategoryEditForm : Form
    {
        private readonly int? _categoryId;
        private readonly string _connStr;

        public ItemCategoryEditForm(int? categoryId = null)
        {
            InitializeComponent();
            _categoryId = categoryId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemCategoryEditForm_Load;
        }

        private void ItemCategoryEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _categoryId.HasValue ? "✏️ Επεξεργασία Κατηγορίας" : "➕ Νέα Κατηγορία";
            if (_categoryId.HasValue) LoadCategoryData(_categoryId.Value);
        }

        private void LoadCategoryData(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var cat = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM ItemCategories WHERE Id = @Id", new { Id = id });
                if (cat == null) return;

                txtCode.Text = cat.Code ?? "";
                txtName.Text = cat.Name ?? "";
                txtDescription.Text = cat.Description ?? "";
                chkIsActive.Checked = cat.IsActive ?? true;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("⚠️ Το Όνομα είναι υποχρεωτικό!", "Προσοχή");
                txtName.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_categoryId.HasValue)
                {
                    conn.Execute(@"UPDATE ItemCategories SET 
                        Code=@Code, Name=@Name, Description=@Description, IsActive=@IsActive, UpdatedAt=GETDATE()
                        WHERE Id=@Id", new
                    {
                        Id = _categoryId.Value,
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO ItemCategories (Code, Name, Description, IsActive, CreatedAt) 
                        VALUES (@Code, @Name, @Description, @IsActive, GETDATE())", new
                    {
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία");
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