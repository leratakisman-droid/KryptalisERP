namespace KryptalisERP
{
    partial class CustomerCreditLimitEditForm
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
            this.lblLimitAmount = new System.Windows.Forms.Label();
            this.numLimitAmount = new System.Windows.Forms.NumericUpDown();
            this.lblCurrentBalance = new System.Windows.Forms.Label();
            this.numCurrentBalance = new System.Windows.Forms.NumericUpDown();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.dtpValidFrom = new System.Windows.Forms.DateTimePicker();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.dtpValidTo = new System.Windows.Forms.DateTimePicker();
            this.lblApprovalDate = new System.Windows.Forms.Label();
            this.dtpApprovalDate = new System.Windows.Forms.DateTimePicker();
            this.lblApprovedBy = new System.Windows.Forms.Label();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.chkIsBlocked = new System.Windows.Forms.CheckBox();
            this.lblBlockReason = new System.Windows.Forms.Label();
            this.txtBlockReason = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 200, rowHeight = 42;

            this.lblLimitAmount.AutoSize = true; this.lblLimitAmount.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblLimitAmount.Text = "Πιστωτικό Όριο (€):"; this.lblLimitAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numLimitAmount.Location = new System.Drawing.Point(controlX, y); this.numLimitAmount.Size = new System.Drawing.Size(200, 30);
            this.numLimitAmount.DecimalPlaces = 2; this.numLimitAmount.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numLimitAmount.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblCurrentBalance.AutoSize = true; this.lblCurrentBalance.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblCurrentBalance.Text = "Τρέχον Υπόλοιπο (€):"; this.lblCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numCurrentBalance.Location = new System.Drawing.Point(controlX, y); this.numCurrentBalance.Size = new System.Drawing.Size(200, 30);
            this.numCurrentBalance.DecimalPlaces = 2; this.numCurrentBalance.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numCurrentBalance.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblValidFrom.AutoSize = true; this.lblValidFrom.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblValidFrom.Text = "Ισχύει από:"; this.lblValidFrom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtpValidFrom.Location = new System.Drawing.Point(controlX, y); this.dtpValidFrom.Size = new System.Drawing.Size(200, 30);
            this.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValidFrom.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblValidTo.AutoSize = true; this.lblValidTo.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblValidTo.Text = "Ισχύει έως:"; this.lblValidTo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtpValidTo.Location = new System.Drawing.Point(controlX, y); this.dtpValidTo.Size = new System.Drawing.Size(200, 30);
            this.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValidTo.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblApprovalDate.AutoSize = true; this.lblApprovalDate.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblApprovalDate.Text = "Ημ/νία Έγκρισης:"; this.lblApprovalDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtpApprovalDate.Location = new System.Drawing.Point(controlX, y); this.dtpApprovalDate.Size = new System.Drawing.Size(200, 30);
            this.dtpApprovalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpApprovalDate.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblApprovedBy.AutoSize = true; this.lblApprovedBy.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblApprovedBy.Text = "Εγκρίθηκε από:"; this.lblApprovedBy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtApprovedBy.Location = new System.Drawing.Point(controlX, y); this.txtApprovedBy.Size = new System.Drawing.Size(300, 30);
            this.txtApprovedBy.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.chkIsBlocked.AutoSize = true; this.chkIsBlocked.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsBlocked.Text = "🚫 Μπλοκαρισμένο"; this.chkIsBlocked.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkIsBlocked.CheckedChanged += new System.EventHandler(this.chkIsBlocked_CheckedChanged); y += 40;

            this.lblBlockReason.AutoSize = true; this.lblBlockReason.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBlockReason.Text = "Αιτία Μπλοκαρίσματος:"; this.lblBlockReason.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBlockReason.Location = new System.Drawing.Point(controlX, y); this.txtBlockReason.Size = new System.Drawing.Size(400, 30);
            this.txtBlockReason.Enabled = false; this.txtBlockReason.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblNotes.Text = "Σημειώσεις:"; this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y); this.txtNotes.Size = new System.Drawing.Size(400, 80);
            this.txtNotes.Multiline = true; this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F); y += 110;

            this.btnSave.Location = new System.Drawing.Point(370, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(510, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = "❌ Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLimitAmount, this.numLimitAmount, this.lblCurrentBalance, this.numCurrentBalance,
                this.lblValidFrom, this.dtpValidFrom, this.lblValidTo, this.dtpValidTo,
                this.lblApprovalDate, this.dtpApprovalDate, this.lblApprovedBy, this.txtApprovedBy,
                this.chkIsBlocked, this.lblBlockReason, this.txtBlockReason, this.lblNotes, this.txtNotes,
                this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 550);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "CustomerCreditLimitEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "➕ Νέο Πιστωτικό Όριο";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblLimitAmount, lblCurrentBalance, lblValidFrom, lblValidTo, lblApprovalDate, lblApprovedBy, lblBlockReason, lblNotes;
        private System.Windows.Forms.NumericUpDown numLimitAmount, numCurrentBalance;
        private System.Windows.Forms.DateTimePicker dtpValidFrom, dtpValidTo, dtpApprovalDate;
        private System.Windows.Forms.TextBox txtApprovedBy, txtBlockReason, txtNotes;
        private System.Windows.Forms.CheckBox chkIsBlocked;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}