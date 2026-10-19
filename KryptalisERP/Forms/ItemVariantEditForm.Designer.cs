namespace KryptalisERP
{
    partial class ItemVariantEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblVariantCode = new System.Windows.Forms.Label();
            this.txtVariantCode = new System.Windows.Forms.TextBox();
            this.lblSku = new System.Windows.Forms.Label();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.numPurchasePrice = new System.Windows.Forms.NumericUpDown();
            this.lblSalesPrice = new System.Windows.Forms.Label();
            this.numSalesPrice = new System.Windows.Forms.NumericUpDown();
            this.lblWeight = new System.Windows.Forms.Label();
            this.numWeight = new System.Windows.Forms.NumericUpDown();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            this.lblVariantCode.AutoSize = true; this.lblVariantCode.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblVariantCode.Text = "Κωδικός Παραλλαγής*:"; this.lblVariantCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtVariantCode.Location = new System.Drawing.Point(controlX, y); this.txtVariantCode.Size = new System.Drawing.Size(200, 30);
            this.txtVariantCode.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblSku.AutoSize = true; this.lblSku.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblSku.Text = "SKU:"; this.lblSku.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtSku.Location = new System.Drawing.Point(controlX, y); this.txtSku.Size = new System.Drawing.Size(200, 30);
            this.txtSku.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblBarcode.AutoSize = true; this.lblBarcode.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBarcode.Text = "Barcode:"; this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBarcode.Location = new System.Drawing.Point(controlX, y); this.txtBarcode.Size = new System.Drawing.Size(200, 30);
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblPurchasePrice.AutoSize = true; this.lblPurchasePrice.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblPurchasePrice.Text = "Τιμή Αγοράς (€):"; this.lblPurchasePrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numPurchasePrice.Location = new System.Drawing.Point(controlX, y); this.numPurchasePrice.Size = new System.Drawing.Size(150, 30);
            this.numPurchasePrice.DecimalPlaces = 2; this.numPurchasePrice.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblSalesPrice.AutoSize = true; this.lblSalesPrice.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblSalesPrice.Text = "Τιμή Πώλησης (€):"; this.lblSalesPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numSalesPrice.Location = new System.Drawing.Point(controlX, y); this.numSalesPrice.Size = new System.Drawing.Size(150, 30);
            this.numSalesPrice.DecimalPlaces = 2; this.numSalesPrice.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblWeight.AutoSize = true; this.lblWeight.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblWeight.Text = "Βάρος (kg):"; this.lblWeight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numWeight.Location = new System.Drawing.Point(controlX, y); this.numWeight.Size = new System.Drawing.Size(150, 30);
            this.numWeight.DecimalPlaces = 2; this.numWeight.Font = new System.Drawing.Font("Segoe UI", 10F); y += rowHeight;

            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblNotes.Text = "Σημειώσεις:"; this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y); this.txtNotes.Size = new System.Drawing.Size(400, 80);
            this.txtNotes.Multiline = true; this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F); y += 100;

            this.chkIsActive.AutoSize = true; this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "✅ Ενεργή"; this.chkIsActive.Checked = true; this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
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
                this.lblVariantCode, this.txtVariantCode, this.lblSku, this.txtSku,
                this.lblBarcode, this.txtBarcode, this.lblPurchasePrice, this.numPurchasePrice,
                this.lblSalesPrice, this.numSalesPrice, this.lblWeight, this.numWeight,
                this.lblNotes, this.txtNotes, this.chkIsActive, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemVariantEditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Νέα Παραλλαγή";
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblVariantCode, lblSku, lblBarcode, lblPurchasePrice, lblSalesPrice, lblWeight, lblNotes;
        private System.Windows.Forms.TextBox txtVariantCode, txtSku, txtBarcode, txtNotes;
        private System.Windows.Forms.NumericUpDown numPurchasePrice, numSalesPrice, numWeight;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}