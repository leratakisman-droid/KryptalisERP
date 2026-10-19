namespace KryptalisERP
{
    partial class ItemBarcodeEditForm
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
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbBarcodeType = new System.Windows.Forms.ComboBox();
            this.lblUom = new System.Windows.Forms.Label();
            this.cmbUom = new System.Windows.Forms.ComboBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.chkIsPrimary = new System.Windows.Forms.CheckBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            this.lblBarcode.AutoSize = true; this.lblBarcode.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBarcode.Text = "Barcode*:"; this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBarcode.Location = new System.Drawing.Point(controlX, y); this.txtBarcode.Size = new System.Drawing.Size(300, 30);
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblType.AutoSize = true; this.lblType.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblType.Text = "Τύπος:"; this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBarcodeType.Location = new System.Drawing.Point(controlX, y); this.cmbBarcodeType.Size = new System.Drawing.Size(200, 30);
            this.cmbBarcodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodeType.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblUom.AutoSize = true; this.lblUom.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblUom.Text = "Μονάδα Μέτρησης:"; this.lblUom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbUom.Location = new System.Drawing.Point(controlX, y); this.cmbUom.Size = new System.Drawing.Size(200, 30);
            this.cmbUom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUom.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblQty.AutoSize = true; this.lblQty.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblQty.Text = "Ποσότητα:"; this.lblQty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numQuantity.Location = new System.Drawing.Point(controlX, y); this.numQuantity.Size = new System.Drawing.Size(100, 30);
            this.numQuantity.DecimalPlaces = 4; this.numQuantity.Value = 1;
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.chkIsPrimary.AutoSize = true; this.chkIsPrimary.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsPrimary.Text = "👑 Κύριο Barcode"; this.chkIsPrimary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += rowHeight;

            this.lblDesc.AutoSize = true; this.lblDesc.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblDesc.Text = "Περιγραφή:"; this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtDescription.Location = new System.Drawing.Point(controlX, y); this.txtDescription.Size = new System.Drawing.Size(400, 80);
            this.txtDescription.Multiline = true; this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F); y += 100;

            this.chkIsActive.AutoSize = true; this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "✅ Ενεργό"; this.chkIsActive.Checked = true; this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 60;

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
                this.lblBarcode, this.txtBarcode, this.lblType, this.cmbBarcodeType,
                this.lblUom, this.cmbUom, this.lblQty, this.numQuantity, this.chkIsPrimary,
                this.lblDesc, this.txtDescription, this.chkIsActive, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemBarcodeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Νέο Barcode";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblBarcode, lblType, lblUom, lblQty, lblDesc;
        private System.Windows.Forms.TextBox txtBarcode, txtDescription;
        private System.Windows.Forms.ComboBox cmbBarcodeType, cmbUom;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.CheckBox chkIsPrimary, chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}