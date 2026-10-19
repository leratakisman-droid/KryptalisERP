namespace KryptalisERP
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // === LOGO ===
            this.picLogo.Location = new System.Drawing.Point(150, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 120);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // === TITLE ===
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(100, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "KRYPTALIS ERP";
            this.lblTitle.Visible = false;

            // === ΕΤΑΙΡΕΙΑ ===
            int y = 160;
            int labelX = 30;
            int inputX = 180;
            int inputWidth = 290;

            this.lblCompany.AutoSize = true;
            this.lblCompany.ForeColor = System.Drawing.Color.White;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompany.Location = new System.Drawing.Point(labelX, y + 3);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Text = "🏢 Εταιρεία:";

            this.cmbCompany.Location = new System.Drawing.Point(inputX, y);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(inputWidth, 23);
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbCompany.BackColor = System.Drawing.Color.FromArgb(30, 40, 60);
            this.cmbCompany.ForeColor = System.Drawing.Color.White;
            this.cmbCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // === ΥΠΟΚΑΤΑΣΤΗΜΑ ===
            y += 45;
            this.lblBranch.AutoSize = true;
            this.lblBranch.ForeColor = System.Drawing.Color.White;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblBranch.Location = new System.Drawing.Point(labelX, y + 3);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Text = "🏭 Υποκ/μα:";

            this.cmbBranch.Location = new System.Drawing.Point(inputX, y);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(inputWidth, 23);
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbBranch.BackColor = System.Drawing.Color.FromArgb(30, 40, 60);
            this.cmbBranch.ForeColor = System.Drawing.Color.White;
            this.cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // === USERNAME ===
            y += 45;
            this.lblUsername.AutoSize = true;
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsername.Location = new System.Drawing.Point(labelX, y + 3);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "👤 Username:";

            this.txtUsername.Location = new System.Drawing.Point(inputX, y);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(inputWidth, 23);
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(30, 40, 60);
            this.txtUsername.ForeColor = System.Drawing.Color.White;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // === PASSWORD ===
            y += 45;
            this.lblPassword.AutoSize = true;
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.Location = new System.Drawing.Point(labelX, y + 3);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = " Password:";

            this.txtPassword.Location = new System.Drawing.Point(inputX, y);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(inputWidth, 23);
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(30, 40, 60);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);

            // === BUTTON LOGIN ===
            y += 60;
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(inputX, y);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(inputWidth, 45);
            this.btnLogin.Text = "🚀 Σύνδεση";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += (s, ev) => this.btnLogin.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnLogin.MouseLeave += (s, ev) => this.btnLogin.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);

            // === ERROR LABEL ===
            y += 55;
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblError.Location = new System.Drawing.Point(inputX, y);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 15);
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // === FOOTER ===
            this.lblFooter.AutoSize = true;
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(100, 150, 200);
            this.lblFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblFooter.Location = new System.Drawing.Point(150, 580);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Text = "© 2026 KRYPTALIS TECH & SECURITY";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // === FORM ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(15, 30, 50);
            this.ClientSize = new System.Drawing.Size(500, 620);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCompany); this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.lblBranch); this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblUsername); this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword); this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KRYPTALIS ERP - Σύνδεση";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblFooter;
    }
}