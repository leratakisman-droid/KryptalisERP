using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CustomerActivityEditForm : Form
    {
        private readonly int _customerId;
        private readonly int _activityId;
        private readonly string _connStr;

        public CustomerActivityEditForm(int customerId, int activityId = 0)
        {
            InitializeComponent();
            _customerId = customerId;
            _activityId = activityId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += CustomerActivityEditForm_Load;
        }

        private void CustomerActivityEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _activityId > 0 ? "️ Επεξεργασία Δραστηριότητας" : "➕ Νέα Δραστηριότητα";
            cmbStatus.Items.AddRange(new object[] { "Εκκρεμής", "Σε Εξέλιξη", "Ολοκληρωμένη", "Ακυρώθηκε" });
            cmbType.Items.AddRange(new object[] { "Τηλέφωνο", "Email", "Συνάντηση", "Επίσκεψη", "Άλλο" });
            cmbStatus.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            dtpActivityDate.Value = DateTime.Today;
            if (_activityId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var activity = conn.QueryFirstOrDefault<dynamic>(
                    "SELECT * FROM CustomerActivities WHERE Id = @Id", new { Id = _activityId });
                if (activity == null) return;

                txtSubject.Text = activity.Subject ?? "";
                txtDescription.Text = activity.Description ?? "";
                dtpActivityDate.Value = activity.ActivityDate;
                cmbStatus.Text = activity.Status ?? "Εκκρεμής";
                cmbType.Text = activity.ActivityType ?? "";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("⚠️ Το Θέμα είναι υποχρεωτικό!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_activityId > 0)
                {
                    conn.Execute(@"UPDATE CustomerActivities SET 
                        CustomerId=@CustomerId, Subject=@Subject, Description=@Description, 
                        ActivityDate=@ActivityDate, Status=@Status, ActivityType=@ActivityType, UpdatedAt=GETDATE()
                        WHERE Id=@Id", new
                    {
                        Id = _activityId,
                        CustomerId = _customerId,
                        Subject = txtSubject.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        ActivityDate = dtpActivityDate.Value,
                        Status = cmbStatus.Text.Trim(),
                        ActivityType = cmbType.Text.Trim()
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO CustomerActivities 
                        (CustomerId, Subject, Description, ActivityDate, Status, ActivityType, CreatedAt) 
                        VALUES (@CustomerId, @Subject, @Description, @ActivityDate, @Status, @ActivityType, GETDATE())", new
                    {
                        CustomerId = _customerId,
                        Subject = txtSubject.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        ActivityDate = dtpActivityDate.Value,
                        Status = cmbStatus.Text.Trim(),
                        ActivityType = cmbType.Text.Trim()
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