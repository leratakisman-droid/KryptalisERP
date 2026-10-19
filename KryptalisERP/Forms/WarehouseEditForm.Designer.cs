namespace KryptalisERP
{
    partial class WarehouseEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblCode = new System.Windows.Forms.Label(); this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label(); this.txtName = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label(); this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblManager = new System.Windows.Forms.Label(); this.txtManager = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label(); this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label(); this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label(); this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label(); this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button(); this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            this.lblCode.AutoSize = true; this.lblCode.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblCode.Text = "Κωδικός:"; this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCode.Location = new System.Drawing.Point(controlX, y); this.txtCode.Size = new System.Drawing.Size(150, 30);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblName.Text = "Όνομα*:"; this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtName.Location = new System.Drawing.Point(controlX, y); this.txtName.Size = new System.Drawing.Size(400, 30);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblLocation.AutoSize = true; this.lblLocation.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblLocation.Text = "Τοποθεσία:"; this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtLocation.Location = new System.Drawing.Point(controlX, y); this.txtLocation.Size = new System.Drawing.Size(400, 30);
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblManager.AutoSize = true; this.lblManager.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblManager.Text = "Υπεύθυνος:"; this.lblManager.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtManager.Location = new System.Drawing.Point(controlX, y); this.txtManager.Size = new System.Drawing.Size(300, 30);
            this.txtManager.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblPhone.AutoSize = true; this.lblPhone.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblPhone.Text = "Τηλέφωνο:"; this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone.Location = new System.Drawing.Point(controlX, y); this.txtPhone.Size = new System.Drawing.Size(200, 30);
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblEmail.AutoSize = true; this.lblEmail.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblEmail.Text = "Email:"; this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(controlX, y); this.txtEmail.Size = new System.Drawing.Size(300, 30);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblAddress.AutoSize = true; this.lblAddress.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblAddress.Text = "Διεύθυνση:"; this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtAddress.Location = new System.Drawing.Point(controlX, y); this.txtAddress.Size = new System.Drawing.Size(400, 30);
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblNotes.Text = "Σημειώσεις:"; this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y); this.txtNotes.Size = new System.Drawing.Size(400, 80);
            this.txtNotes.Multiline = true; this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F); y += 100;

            this.chkIsActive.AutoSize = true; this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "✅ Ενεργή"; this.chkIsActive.Checked = true; this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 60;

            this.btnSave.Location = new System.Drawing.Point(370, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(510, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = " Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCode, this.txtCode, this.lblName, this.txtName, this.lblLocation, this.txtLocation,
                this.lblManager, this.txtManager, this.lblPhone, this.txtPhone, this.lblEmail, this.txtEmail,
                this.lblAddress, this.txtAddress, this.lblNotes, this.txtNotes, this.chkIsActive,
                this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 550);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "WarehouseEditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Νέα Αποθήκη";
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblCode, lblName, lblLocation, lblManager, lblPhone, lblEmail, lblAddress, lblNotes;
        private System.Windows.Forms.TextBox txtCode, txtName, txtLocation, txtManager, txtPhone, txtEmail, txtAddress, txtNotes;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}