namespace KryptalisERP
{
    partial class CustomerContactEditForm
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
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsPrimary = new System.Windows.Forms.CheckBox();
            this.chkIsDecisionMaker = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            this.lblFirstName.AutoSize = true; this.lblFirstName.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblFirstName.Text = "Όνομα:"; this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtFirstName.Location = new System.Drawing.Point(controlX, y); this.txtFirstName.Size = new System.Drawing.Size(250, 30);
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblLastName.AutoSize = true; this.lblLastName.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblLastName.Text = "Επώνυμο:"; this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtLastName.Location = new System.Drawing.Point(controlX, y); this.txtLastName.Size = new System.Drawing.Size(250, 30);
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblPosition.AutoSize = true; this.lblPosition.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblPosition.Text = "Θέση:"; this.lblPosition.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPosition.Location = new System.Drawing.Point(controlX, y); this.txtPosition.Size = new System.Drawing.Size(300, 30);
            this.txtPosition.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblDepartment.AutoSize = true; this.lblDepartment.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblDepartment.Text = "Τμήμα:"; this.lblDepartment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtDepartment.Location = new System.Drawing.Point(controlX, y); this.txtDepartment.Size = new System.Drawing.Size(300, 30);
            this.txtDepartment.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblPhone.AutoSize = true; this.lblPhone.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblPhone.Text = "Τηλέφωνο:"; this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone.Location = new System.Drawing.Point(controlX, y); this.txtPhone.Size = new System.Drawing.Size(200, 30);
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblMobile.AutoSize = true; this.lblMobile.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblMobile.Text = "Κινητό:"; this.lblMobile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtMobile.Location = new System.Drawing.Point(controlX, y); this.txtMobile.Size = new System.Drawing.Size(200, 30);
            this.txtMobile.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblEmail.AutoSize = true; this.lblEmail.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblEmail.Text = "Email:"; this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(controlX, y); this.txtEmail.Size = new System.Drawing.Size(300, 30);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblNotes.Text = "Σημειώσεις:"; this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y); this.txtNotes.Size = new System.Drawing.Size(400, 80);
            this.txtNotes.Multiline = true; this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F); y += 100;

            this.chkIsPrimary.AutoSize = true; this.chkIsPrimary.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsPrimary.Text = "⭐ Κύρια Επαφή"; this.chkIsPrimary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 35;

            this.chkIsDecisionMaker.AutoSize = true; this.chkIsDecisionMaker.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsDecisionMaker.Text = " Υπεύθυνος Απόφασης"; this.chkIsDecisionMaker.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 50;

            this.btnSave.Location = new System.Drawing.Point(350, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(490, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = "❌ Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFirstName, this.txtFirstName, this.lblLastName, this.txtLastName,
                this.lblPosition, this.txtPosition, this.lblDepartment, this.txtDepartment,
                this.lblPhone, this.txtPhone, this.lblMobile, this.txtMobile,
                this.lblEmail, this.txtEmail, this.lblNotes, this.txtNotes,
                this.chkIsPrimary, this.chkIsDecisionMaker, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 550);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "CustomerContactEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Νέα Επαφή";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblFirstName, lblLastName, lblPosition, lblDepartment, lblPhone, lblMobile, lblEmail, lblNotes;
        private System.Windows.Forms.TextBox txtFirstName, txtLastName, txtPosition, txtDepartment, txtPhone, txtMobile, txtEmail, txtNotes;
        private System.Windows.Forms.CheckBox chkIsPrimary, chkIsDecisionMaker;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}