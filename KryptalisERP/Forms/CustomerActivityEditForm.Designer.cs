namespace KryptalisERP
{
    partial class CustomerActivityEditForm
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
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpActivityDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            this.lblSubject.AutoSize = true; this.lblSubject.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblSubject.Text = "Θέμα:"; this.lblSubject.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtSubject.Location = new System.Drawing.Point(controlX, y); this.txtSubject.Size = new System.Drawing.Size(400, 30);
            this.txtSubject.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblDescription.AutoSize = true; this.lblDescription.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblDescription.Text = "Περιγραφή:"; this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtDescription.Location = new System.Drawing.Point(controlX, y); this.txtDescription.Size = new System.Drawing.Size(400, 120);
            this.txtDescription.Multiline = true; this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F); y += 140;

            this.lblDate.AutoSize = true; this.lblDate.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblDate.Text = "Ημερομηνία:"; this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtpActivityDate.Location = new System.Drawing.Point(controlX, y); this.dtpActivityDate.Size = new System.Drawing.Size(200, 30);
            this.dtpActivityDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpActivityDate.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblStatus.AutoSize = true; this.lblStatus.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblStatus.Text = "Κατάσταση:"; this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbStatus.Location = new System.Drawing.Point(controlX, y); this.cmbStatus.Size = new System.Drawing.Size(200, 30);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblType.AutoSize = true; this.lblType.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblType.Text = "Τύπος:"; this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbType.Location = new System.Drawing.Point(controlX, y); this.cmbType.Size = new System.Drawing.Size(200, 30);
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10F); y += 60;

            this.btnSave.Location = new System.Drawing.Point(350, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(490, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = " Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSubject, this.txtSubject, this.lblDescription, this.txtDescription,
                this.lblDate, this.dtpActivityDate, this.lblStatus, this.cmbStatus,
                this.lblType, this.cmbType, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "CustomerActivityEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "➕ Νέα Δραστηριότητα";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblSubject, lblDescription, lblDate, lblStatus, lblType;
        private System.Windows.Forms.TextBox txtSubject, txtDescription;
        private System.Windows.Forms.DateTimePicker dtpActivityDate;
        private System.Windows.Forms.ComboBox cmbStatus, cmbType;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}