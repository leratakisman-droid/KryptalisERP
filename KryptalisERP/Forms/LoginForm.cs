using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class LoginForm : Form
    {
        private bool _isFormLoading = true;

        public LoginForm()
        {
            InitializeComponent();

            // ✅ ΚΡΙΣΙΜΗ ΔΙΟΡΘΩΣΗ: Αποτρέπει το διπλό κλικ στο Χ
            this.CancelButton = null;
            this.FormClosing += LoginForm_FormClosing;

            LoadCompanies();
            SetLogo();

            _isFormLoading = false;
            txtUsername.Focus();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void SetLogo()
        {
            try
            {
                picLogo.Image = Properties.Resources.KryptalisLogo;
                picLogo.SizeMode = PictureBoxSizeMode.Zoom;
                picLogo.Visible = true;
                lblTitle.Visible = false;
            }
            catch
            {
                picLogo.Visible = false;
                lblTitle.Visible = true;
            }
        }

        private void LoadCompanies()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, CompanyName FROM Companies WHERE IsActive = 1 ORDER BY CompanyName");

                if (dt.Rows.Count == 0)
                {
                    lblError.Text = "⚠️ Δεν βρέθηκαν εταιρείες!";
                    return;
                }

                cmbCompany.SelectedIndexChanged -= CmbCompany_SelectedIndexChanged;
                cmbCompany.DataSource = dt;
                cmbCompany.DisplayMember = "CompanyName";
                cmbCompany.ValueMember = "Id";
                cmbCompany.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbCompany.SelectedIndex = -1;
                cmbCompany.SelectedIndexChanged += CmbCompany_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Σφάλμα φόρτωσης εταιρειών: " + ex.Message;
            }
        }

        private void CmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFormLoading) return;

            try
            {
                int companyId = GetSelectedId(cmbCompany);

                if (companyId == 0)
                {
                    cmbBranch.DataSource = null;
                    cmbBranch.Items.Clear();
                    return;
                }

                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, BranchName, IsMain FROM CompanyBranches WHERE CompanyId = @Id AND IsActive = 1 ORDER BY IsMain DESC, BranchName",
                    new SqlParameter("@Id", companyId));

                cmbBranch.SelectedIndexChanged -= CmbBranch_SelectedIndexChanged;
                cmbBranch.DataSource = dt;
                cmbBranch.DisplayMember = "BranchName";
                cmbBranch.ValueMember = "Id";
                cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBranch.SelectedIndex = -1;
                cmbBranch.SelectedIndexChanged += CmbBranch_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Σφάλμα: " + ex.Message;
            }
        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFormLoading) return;
        }

        private int GetSelectedId(ComboBox cmb)
        {
            if (cmb.SelectedValue == null) return 0;
            if (cmb.SelectedValue is DataRowView drv)
            {
                return Convert.ToInt32(drv["Id"]);
            }
            return Convert.ToInt32(cmb.SelectedValue);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            int companyId = GetSelectedId(cmbCompany);
            int branchId = GetSelectedId(cmbBranch);

            if (companyId == 0) { lblError.Text = "⚠️ Επίλεξε εταιρεία!"; return; }
            if (branchId == 0) { lblError.Text = "⚠️ Επίλεξε υποκατάστημα!"; return; }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { lblError.Text = "⚠️ Συμπλήρωσε username και password!"; return; }

            try
            {
                DataTable result = DatabaseHelper.ExecuteQuery(
                    "EXEC sp_AuthenticateUser @Username, @Password",
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                );

                if (result.Rows.Count > 0)
                {
                    DataRow user = result.Rows[0];

                    Program.CurrentUserId = Convert.ToInt32(user["Id"]);
                    Program.CurrentUserName = user["FullName"]?.ToString() ?? "";
                    Program.CurrentUserRole = user["Role"]?.ToString() ?? "";
                    Program.CanSettings = Convert.ToBoolean(user["CanSettings"]);
                    Program.CanUsers = Convert.ToBoolean(user["CanUsers"]);
                    Program.CanSales = Convert.ToBoolean(user["CanSales"]);
                    Program.CanWarehouse = Convert.ToBoolean(user["CanWarehouse"]);
                    Program.CanClients = Convert.ToBoolean(user["CanClients"]);

                    Program.CurrentCompanyId = companyId;
                    Program.CurrentCompanyName = cmbCompany.Text;
                    Program.CurrentBranchId = branchId;
                    Program.CurrentBranchName = cmbBranch.Text;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblError.Text = "❌ Λάθος username ή password!";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Σφάλμα: " + ex.Message;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true; // Αποτρέπει τον ήχο "beep"
            }
        }
    }
}