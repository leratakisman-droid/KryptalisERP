using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CustomerContactEditForm : Form
    {
        private readonly int _customerId;
        private readonly int _contactId;
        private readonly string _connStr;

        public CustomerContactEditForm(int customerId, int contactId = 0)
        {
            InitializeComponent();
            _customerId = customerId;
            _contactId = contactId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += CustomerContactEditForm_Load;
        }

        private void CustomerContactEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _contactId > 0 ? "✏️ Επεξεργασία Επαφής" : "➕ Νέα Επαφή";
            if (_contactId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var contact = conn.QueryFirstOrDefault<dynamic>(
                    "SELECT * FROM CustomerContacts WHERE Id = @Id", new { Id = _contactId });
                if (contact == null) return;

                txtFirstName.Text = contact.FirstName ?? "";
                txtLastName.Text = contact.LastName ?? "";
                txtPosition.Text = contact.Position ?? "";
                txtDepartment.Text = contact.Department ?? "";
                txtPhone.Text = contact.Phone ?? "";
                txtMobile.Text = contact.Mobile ?? "";
                txtEmail.Text = contact.Email ?? "";
                txtNotes.Text = contact.Notes ?? "";
                chkIsPrimary.Checked = contact.IsPrimary;
                chkIsDecisionMaker.Checked = contact.IsDecisionMaker;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) && string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε τουλάχιστον Όνομα ή Επώνυμο!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_contactId > 0)
                {
                    conn.Execute(@"UPDATE CustomerContacts SET 
                        CustomerId=@CustomerId, FirstName=@FirstName, LastName=@LastName, Position=@Position, 
                        Department=@Department, Phone=@Phone, Mobile=@Mobile, Email=@Email, Notes=@Notes, 
                        IsPrimary=@IsPrimary, IsDecisionMaker=@IsDecisionMaker, UpdatedAt=GETDATE()
                        WHERE Id=@Id", new
                    {
                        Id = _contactId,
                        CustomerId = _customerId,
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Position = txtPosition.Text.Trim(),
                        Department = txtDepartment.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Mobile = txtMobile.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Notes = txtNotes.Text.Trim(),
                        IsPrimary = chkIsPrimary.Checked,
                        IsDecisionMaker = chkIsDecisionMaker.Checked
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO CustomerContacts 
                        (CustomerId, FirstName, LastName, Position, Department, Phone, Mobile, Email, Notes, IsPrimary, IsDecisionMaker, CreatedAt) 
                        VALUES (@CustomerId, @FirstName, @LastName, @Position, @Department, @Phone, @Mobile, @Email, @Notes, @IsPrimary, @IsDecisionMaker, GETDATE())", new
                    {
                        CustomerId = _customerId,
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Position = txtPosition.Text.Trim(),
                        Department = txtDepartment.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Mobile = txtMobile.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Notes = txtNotes.Text.Trim(),
                        IsPrimary = chkIsPrimary.Checked,
                        IsDecisionMaker = chkIsDecisionMaker.Checked
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}