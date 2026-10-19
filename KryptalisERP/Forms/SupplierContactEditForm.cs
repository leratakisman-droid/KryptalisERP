using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SupplierContactEditForm : Form
    {
        private readonly int _supplierId;
        private readonly int _contactId;
        private readonly string _connStr;

        public SupplierContactEditForm(int supplierId, int contactId = 0)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _contactId = contactId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SupplierContactEditForm_Load;
        }

        private void SupplierContactEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _contactId > 0 ? "✏️ Επεξεργασία Επαφής" : "➕ Νέα Επαφή";
            if (_contactId > 0) LoadData();
        }

        private void LoadData()
        {
            using var conn = new SqlConnection(_connStr);
            var c = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM SupplierContacts WHERE Id = @Id", new { Id = _contactId });
            if (c == null) return;

            txtFirstName.Text = c.FirstName;
            txtLastName.Text = c.LastName;
            txtPosition.Text = c.Position;
            txtDepartment.Text = c.Department;
            txtPhone.Text = c.Phone;
            txtMobile.Text = c.Mobile;
            txtEmail.Text = c.Email;
            txtNotes.Text = c.Notes;
            chkIsPrimary.Checked = c.IsPrimary;
            chkIsActive.Checked = c.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text)) { MessageBox.Show("Το Όνομα είναι υποχρεωτικό."); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();

                var p = new
                {
                    SupplierId = _supplierId,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Position = txtPosition.Text.Trim(),
                    Department = txtDepartment.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Mobile = txtMobile.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Notes = txtNotes.Text.Trim(),
                    IsPrimary = chkIsPrimary.Checked,
                    IsActive = chkIsActive.Checked
                };

                if (_contactId > 0)
                {
                    conn.Execute(@"UPDATE SupplierContacts SET FirstName=@FirstName, LastName=@LastName, Position=@Position, Department=@Department, Phone=@Phone, Mobile=@Mobile, Email=@Email, Notes=@Notes, IsPrimary=@IsPrimary, IsActive=@IsActive WHERE Id=@Id AND SupplierId=@SupplierId",
                        new { Id = _contactId, p.SupplierId, p.FirstName, p.LastName, p.Position, p.Department, p.Phone, p.Mobile, p.Email, p.Notes, p.IsPrimary, p.IsActive }, tran);
                }
                else
                {
                    conn.Execute(@"INSERT INTO SupplierContacts (SupplierId, FirstName, LastName, Position, Department, Phone, Mobile, Email, Notes, IsPrimary, IsActive) VALUES (@SupplierId, @FirstName, @LastName, @Position, @Department, @Phone, @Mobile, @Email, @Notes, @IsPrimary, @IsActive)", p, tran);
                }
                tran.Commit();
                this.DialogResult = DialogResult.OK; this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}