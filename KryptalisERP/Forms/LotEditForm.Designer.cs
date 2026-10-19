namespace KryptalisERP
{
    partial class LotEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblLotNumber = new System.Windows.Forms.Label();
            this.txtLotNumber = new System.Windows.Forms.TextBox();
            this.lblManufacture = new System.Windows.Forms.Label();
            this.dtpManufacture = new System.Windows.Forms.DateTimePicker();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            this.lblLotNumber.AutoSize = true; this.lblLotNumber.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblLotNumber.Text = "Κωδικός Παρτίδας*:"; this.lblLotNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtLotNumber.Location = new System.Drawing.Point(controlX, y); this.txtLotNumber.Size = new System.Drawing.Size(200, 30);
            this.txtLotNumber.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblManufacture.AutoSize = true; this.lblManufacture.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblManufacture.Text = "Ημ/νία Παραγωγής:"; this.lblManufacture.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtpManufacture.Location = new System.Drawing.Point(controlX, y); this.dtpManufacture.Size = new System.Drawing.Size(200, 30);
            this.dtpManufacture.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpManufacture.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblExpiry.AutoSize = true; this.lblExpiry.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblExpiry.Text = "Ημ/νία Λήξης:"; this.lblExpiry.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtpExpiry.Location = new System.Drawing.Point(controlX, y); this.dtpExpiry.Size = new System.Drawing.Size(200, 30);
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblCountry.AutoSize = true; this.lblCountry.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblCountry.Text = "Χώρα Προέλευσης:"; this.lblCountry.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCountry.Location = new System.Drawing.Point(controlX, y); this.txtCountry.Size = new System.Drawing.Size(200, 30);
            this.txtCountry.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblNotes.Text = "Σημειώσεις:"; this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y); this.txtNotes.Size = new System.Drawing.Size(400, 80);
            this.txtNotes.Multiline = true; this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F); y += 100;

            this.btnSave.Location = new System.Drawing.Point(350, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(490, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = "❌ Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLotNumber, this.txtLotNumber, this.lblManufacture, this.dtpManufacture,
                this.lblExpiry, this.dtpExpiry, this.lblCountry, this.txtCountry,
                this.lblNotes, this.txtNotes, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "LotEditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Νέα Παρτίδα";
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblLotNumber, lblManufacture, lblExpiry, lblCountry, lblNotes;
        private System.Windows.Forms.TextBox txtLotNumber, txtCountry, txtNotes;
        private System.Windows.Forms.DateTimePicker dtpManufacture, dtpExpiry;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}