namespace KryptalisERP
{
    partial class SupplierContactEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsPrimary = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            int y = 10;
            this.txtFirstName.Location = new System.Drawing.Point(10, y); this.txtFirstName.Size = new System.Drawing.Size(200, 23); this.txtFirstName.PlaceholderText = "Όνομα"; y += 30;
            this.txtLastName.Location = new System.Drawing.Point(10, y); this.txtLastName.Size = new System.Drawing.Size(200, 23); this.txtLastName.PlaceholderText = "Επώνυμο"; y += 30;
            this.txtPosition.Location = new System.Drawing.Point(10, y); this.txtPosition.Size = new System.Drawing.Size(200, 23); this.txtPosition.PlaceholderText = "Θέση"; y += 30;
            this.txtDepartment.Location = new System.Drawing.Point(10, y); this.txtDepartment.Size = new System.Drawing.Size(200, 23); this.txtDepartment.PlaceholderText = "Τμήμα"; y += 30;
            this.txtPhone.Location = new System.Drawing.Point(10, y); this.txtPhone.Size = new System.Drawing.Size(200, 23); this.txtPhone.PlaceholderText = "Τηλέφων"; y += 30;
            this.txtMobile.Location = new System.Drawing.Point(10, y); this.txtMobile.Size = new System.Drawing.Size(200, 23); this.txtMobile.PlaceholderText = "Κινητό"; y += 30;
            this.txtEmail.Location = new System.Drawing.Point(10, y); this.txtEmail.Size = new System.Drawing.Size(200, 23); this.txtEmail.PlaceholderText = "Email"; y += 30;
            this.txtNotes.Location = new System.Drawing.Point(10, y); this.txtNotes.Size = new System.Drawing.Size(200, 60); this.txtNotes.Multiline = true; this.txtNotes.PlaceholderText = "Σημειώσεις"; y += 70;
            this.chkIsPrimary.Location = new System.Drawing.Point(10, y); this.chkIsPrimary.Text = "Κύρια Επαφή"; y += 30;
            this.chkIsActive.Location = new System.Drawing.Point(10, y); this.chkIsActive.Text = "Ενεργή"; this.chkIsActive.Checked = true; y += 30;
            this.btnSave.Location = new System.Drawing.Point(10, y); this.btnSave.Size = new System.Drawing.Size(100, 30); this.btnSave.Text = "Αποθήκευση"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Location = new System.Drawing.Point(120, y); this.btnCancel.Size = new System.Drawing.Size(100, 30); this.btnCancel.Text = "Ακύρωση"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 400);
            this.Controls.Add(this.txtFirstName); this.Controls.Add(this.txtLastName); this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.txtDepartment); this.Controls.Add(this.txtPhone); this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.txtEmail); this.Controls.Add(this.txtNotes); this.Controls.Add(this.chkIsPrimary);
            this.Controls.Add(this.chkIsActive); this.Controls.Add(this.btnSave); this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "SupplierContactEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Επεξεργασία Επαφής";
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.TextBox txtFirstName, txtLastName, txtPosition, txtDepartment, txtPhone, txtMobile, txtEmail, txtNotes;
        private System.Windows.Forms.CheckBox chkIsPrimary, chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}