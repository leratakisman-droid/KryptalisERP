using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class WarehouseEditForm : Form
    {
        private readonly int? _warehouseId;
        private readonly string _connStr;

        public WarehouseEditForm(int? warehouseId = null)
        {
            InitializeComponent();
            _warehouseId = warehouseId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += WarehouseEditForm_Load;
        }

        private void WarehouseEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _warehouseId.HasValue ? "✏️ Επεξεργασία Αποθήκης" : "➕ Νέα Αποθήκη";
            if (_warehouseId.HasValue) LoadWarehouseData(_warehouseId.Value);
        }

        private void LoadWarehouseData(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var wh = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Warehouses WHERE Id = @Id", new { Id = id });
                if (wh == null) return;

                txtCode.Text = wh.Code ?? "";
                txtName.Text = wh.Name ?? "";
                txtLocation.Text = wh.Location ?? "";
                txtManager.Text = wh.Manager ?? "";
                txtPhone.Text = wh.Phone ?? "";
                txtEmail.Text = wh.Email ?? "";
                txtAddress.Text = wh.Address ?? "";
                txtNotes.Text = wh.Notes ?? "";
                chkIsActive.Checked = wh.IsActive ?? true;
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

                if (_warehouseId.HasValue)
                {
                    conn.Execute(@"UPDATE Warehouses SET Code=@Code, Name=@Name, Location=@Location, Manager=@Manager, 
                        Phone=@Phone, Email=@Email, Address=@Address, Notes=@Notes, IsActive=@IsActive, UpdatedAt=GETDATE() WHERE Id=@Id", new
                    {
                        Id = _warehouseId.Value,
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Location = txtLocation.Text.Trim(),
                        Manager = txtManager.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Notes = txtNotes.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO Warehouses (Code, Name, Location, Manager, Phone, Email, Address, Notes, IsActive, CreatedAt) 
                        VALUES (@Code, @Name, @Location, @Manager, @Phone, @Email, @Address, @Notes, @IsActive, GETDATE())", new
                    {
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Location = txtLocation.Text.Trim(),
                        Manager = txtManager.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Notes = txtNotes.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Cancel; this.Close(); }
    }
}