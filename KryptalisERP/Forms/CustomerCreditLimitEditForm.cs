using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class CustomerCreditLimitEditForm : Form
    {
        private readonly int _customerId;
        private readonly int _creditId;
        private readonly string _connStr;

        public CustomerCreditLimitEditForm(int customerId, int creditId = 0)
        {
            InitializeComponent();
            _customerId = customerId;
            _creditId = creditId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += CustomerCreditLimitEditForm_Load;
        }

        private void CustomerCreditLimitEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _creditId > 0 ? "️ Επεξεργασία Πιστωτικού Ορίου" : "➕ Νέο Πιστωτικό Όριο";
            dtpValidFrom.Value = DateTime.Today;
            dtpValidTo.Value = DateTime.Today.AddYears(1);
            dtpApprovalDate.Value = DateTime.Today;
            if (_creditId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var credit = conn.QueryFirstOrDefault<dynamic>(
                    "SELECT * FROM CustomerCreditLimits WHERE Id = @Id", new { Id = _creditId });
                if (credit == null) return;

                numLimitAmount.Value = credit.LimitAmount ?? 0;
                numCurrentBalance.Value = credit.CurrentBalance ?? 0;
                dtpValidFrom.Value = credit.ValidFrom ?? DateTime.Today;
                dtpValidTo.Value = credit.ValidTo ?? DateTime.Today.AddYears(1);
                dtpApprovalDate.Value = credit.ApprovalDate ?? DateTime.Today;
                txtApprovedBy.Text = credit.ApprovedBy ?? "";
                chkIsBlocked.Checked = credit.IsBlocked;
                txtBlockReason.Text = credit.BlockReason ?? "";
                txtNotes.Text = credit.Notes ?? "";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_creditId > 0)
                {
                    conn.Execute(@"UPDATE CustomerCreditLimits SET 
                        CustomerId=@CustomerId, LimitAmount=@LimitAmount, CurrentBalance=@CurrentBalance, 
                        ValidFrom=@ValidFrom, ValidTo=@ValidTo, ApprovalDate=@ApprovalDate, 
                        ApprovedBy=@ApprovedBy, IsBlocked=@IsBlocked, BlockReason=@BlockReason, Notes=@Notes, UpdatedAt=GETDATE()
                        WHERE Id=@Id", new
                    {
                        Id = _creditId,
                        CustomerId = _customerId,
                        LimitAmount = numLimitAmount.Value,
                        CurrentBalance = numCurrentBalance.Value,
                        ValidFrom = dtpValidFrom.Value,
                        ValidTo = dtpValidTo.Value,
                        ApprovalDate = dtpApprovalDate.Value,
                        ApprovedBy = string.IsNullOrWhiteSpace(txtApprovedBy.Text) ? (object)DBNull.Value : txtApprovedBy.Text.Trim(),
                        IsBlocked = chkIsBlocked.Checked,
                        BlockReason = string.IsNullOrWhiteSpace(txtBlockReason.Text) ? (object)DBNull.Value : txtBlockReason.Text.Trim(),
                        Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim()
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO CustomerCreditLimits 
                        (CustomerId, LimitAmount, CurrentBalance, ValidFrom, ValidTo, ApprovalDate, ApprovedBy, IsBlocked, BlockReason, Notes, CreatedAt) 
                        VALUES (@CustomerId, @LimitAmount, @CurrentBalance, @ValidFrom, @ValidTo, @ApprovalDate, @ApprovedBy, @IsBlocked, @BlockReason, @Notes, GETDATE())", new
                    {
                        CustomerId = _customerId,
                        LimitAmount = numLimitAmount.Value,
                        CurrentBalance = numCurrentBalance.Value,
                        ValidFrom = dtpValidFrom.Value,
                        ValidTo = dtpValidTo.Value,
                        ApprovalDate = dtpApprovalDate.Value,
                        ApprovedBy = string.IsNullOrWhiteSpace(txtApprovedBy.Text) ? (object)DBNull.Value : txtApprovedBy.Text.Trim(),
                        IsBlocked = chkIsBlocked.Checked,
                        BlockReason = string.IsNullOrWhiteSpace(txtBlockReason.Text) ? (object)DBNull.Value : txtBlockReason.Text.Trim(),
                        Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim()
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

        private void chkIsBlocked_CheckedChanged(object sender, EventArgs e)
        {
            txtBlockReason.Enabled = chkIsBlocked.Checked;
        }
    }
}