namespace KryptalisERP
{
    partial class UserEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblUsername = new System.Windows.Forms.Label(); this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label(); this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label(); this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label(); this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label(); this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label(); this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblRights = new System.Windows.Forms.Label();
            this.chkClients = new System.Windows.Forms.CheckBox(); this.chkSuppliers = new System.Windows.Forms.CheckBox();
            this.chkSales = new System.Windows.Forms.CheckBox(); this.chkPurchases = new System.Windows.Forms.CheckBox();
            this.chkSettings = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button(); this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblUsername.AutoSize = true; this.lblUsername.Location = new System.Drawing.Point(20, 20); this.lblUsername.Text = "Username:";
            this.txtUsername.Location = new System.Drawing.Point(150, 17); this.txtUsername.Size = new System.Drawing.Size(200, 23);

            this.lblFullName.AutoSize = true; this.lblFullName.Location = new System.Drawing.Point(20, 55); this.lblFullName.Text = "Ονοματεπώνυμο:";
            this.txtFullName.Location = new System.Drawing.Point(150, 52); this.txtFullName.Size = new System.Drawing.Size(200, 23);

            this.lblEmail.AutoSize = true; this.lblEmail.Location = new System.Drawing.Point(20, 90); this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(150, 87); this.txtEmail.Size = new System.Drawing.Size(200, 23);

            this.lblPhone.AutoSize = true; this.lblPhone.Location = new System.Drawing.Point(20, 125); this.lblPhone.Text = "Τηλέφωνο:";
            this.txtPhone.Location = new System.Drawing.Point(150, 122); this.txtPhone.Size = new System.Drawing.Size(200, 23);

            this.lblPassword.AutoSize = true; this.lblPassword.Location = new System.Drawing.Point(20, 160); this.lblPassword.Text = "Κωδικός:";
            this.txtPassword.Location = new System.Drawing.Point(150, 157); this.txtPassword.Size = new System.Drawing.Size(200, 23); this.txtPassword.PasswordChar = '*';

            this.lblRole.AutoSize = true; this.lblRole.Location = new System.Drawing.Point(20, 195); this.lblRole.Text = "Ρόλος:";
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Admin", "User", "Viewer" });
            this.cmbRole.Location = new System.Drawing.Point(150, 192); this.cmbRole.Size = new System.Drawing.Size(200, 23);

            this.lblRights.AutoSize = true; this.lblRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblRights.Location = new System.Drawing.Point(20, 235); this.lblRights.Text = "Δικαιώματα Πρόσβασης:";

            this.chkClients.AutoSize = true; this.chkClients.Location = new System.Drawing.Point(20, 265); this.chkClients.Name = "chkClients"; this.chkClients.Text = "Πελάτες";
            this.chkSuppliers.AutoSize = true; this.chkSuppliers.Location = new System.Drawing.Point(150, 265); this.chkSuppliers.Name = "chkSuppliers"; this.chkSuppliers.Text = "Προμηθευτές";
            this.chkSales.AutoSize = true; this.chkSales.Location = new System.Drawing.Point(20, 290); this.chkSales.Name = "chkSales"; this.chkSales.Text = "Πωλήσεις";
            this.chkPurchases.AutoSize = true; this.chkPurchases.Location = new System.Drawing.Point(150, 290); this.chkPurchases.Name = "chkPurchases"; this.chkPurchases.Text = "Αγορές";
            this.chkSettings.AutoSize = true; this.chkSettings.Location = new System.Drawing.Point(20, 315); this.chkSettings.Name = "chkSettings"; this.chkSettings.Text = "Ρυθμίσεις";

            this.chkActive.AutoSize = true; this.chkActive.Checked = true; this.chkActive.Location = new System.Drawing.Point(20, 350); this.chkActive.Name = "chkActive"; this.chkActive.Text = "Ενεργός Χρήστης";

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(80, 390); this.btnSave.Name = "btnSave"; this.btnSave.Size = new System.Drawing.Size(100, 35); this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(190, 390); this.btnCancel.Name = "btnCancel"; this.btnCancel.Size = new System.Drawing.Size(100, 35); this.btnCancel.Text = "❌ Ακύρωση"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 450);
            this.Controls.Add(this.lblUsername); this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblFullName); this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblEmail); this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPhone); this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPassword); this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblRole); this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.lblRights);
            this.Controls.Add(this.chkClients); this.Controls.Add(this.chkSuppliers);
            this.Controls.Add(this.chkSales); this.Controls.Add(this.chkPurchases);
            this.Controls.Add(this.chkSettings);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "UserEditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Χρήστης";
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblUsername, lblFullName, lblEmail, lblPhone, lblPassword, lblRole, lblRights;
        private System.Windows.Forms.TextBox txtUsername, txtFullName, txtEmail, txtPhone, txtPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.CheckBox chkClients, chkSuppliers, chkSales, chkPurchases, chkSettings, chkActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}