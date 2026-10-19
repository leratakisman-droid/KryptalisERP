namespace KryptalisERP
{
    partial class TransformSalesForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSourceInfo = new System.Windows.Forms.Label();
            this.lblTargetInfo = new System.Windows.Forms.Label();
            this.lblSeries = new System.Windows.Forms.Label();
            this.cmbSeries = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();

            // ═══════════════════════════════════════════════════════════
            // ΧΡΩΜΑΤΑ THEME
            // ═══════════════════════════════════════════════════════════
            System.Drawing.Color formBg = System.Drawing.Color.FromArgb(241, 245, 249);
            System.Drawing.Color panelBg = System.Drawing.Color.White;
            System.Drawing.Color titleColor = System.Drawing.Color.FromArgb(212, 175, 55);
            System.Drawing.Color labelColor = System.Drawing.Color.FromArgb(44, 62, 80);

            // === ΤΙΤΛΟΣ ===
            this.lblTitle.Text = "🔄 Μετασχηματισμός Πώλησης";
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = titleColor;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.AutoSize = true;

            // === PANEL ΠΛΗΡΟΦΟΡΙΩΝ ===
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Location = new System.Drawing.Point(20, 60);
            this.pnlInfo.Size = new System.Drawing.Size(460, 80);
            this.pnlInfo.BackColor = panelBg;
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblSourceInfo.Text = "📄 Πηγή: -";
            this.lblSourceInfo.Name = "lblSourceInfo";
            this.lblSourceInfo.Location = new System.Drawing.Point(12, 12);
            this.lblSourceInfo.AutoSize = true;
            this.lblSourceInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSourceInfo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

            this.lblTargetInfo.Text = "🎯 Προορισμός: -";
            this.lblTargetInfo.Name = "lblTargetInfo";
            this.lblTargetInfo.Location = new System.Drawing.Point(12, 42);
            this.lblTargetInfo.AutoSize = true;
            this.lblTargetInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTargetInfo.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);

            this.pnlInfo.Controls.Add(this.lblSourceInfo);
            this.pnlInfo.Controls.Add(this.lblTargetInfo);

            // === ΕΠΙΛΟΓΗ ΣΕΙΡΑΣ ===
            this.lblSeries.Text = "Επίλεξε σειρά προορισμού:";
            this.lblSeries.Name = "lblSeries";
            this.lblSeries.Location = new System.Drawing.Point(20, 155);
            this.lblSeries.AutoSize = true;
            this.lblSeries.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeries.ForeColor = labelColor;

            this.cmbSeries.Name = "cmbSeries";
            this.cmbSeries.Location = new System.Drawing.Point(20, 180);
            this.cmbSeries.Size = new System.Drawing.Size(460, 25);
            this.cmbSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeries.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSeries.BackColor = panelBg;

            // === ΚΟΥΜΠΙΑ ===
            this.btnConfirm.Text = "✅ Συνέχεια (Άνοιγμα Παραστατικού)";
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Location = new System.Drawing.Point(20, 230);
            this.btnConfirm.Size = new System.Drawing.Size(300, 45);
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);

            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(330, 230);
            this.btnCancel.Size = new System.Drawing.Size(150, 45);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // === FORM ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = formBg;
            this.ClientSize = new System.Drawing.Size(500, 295);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.lblSeries);
            this.Controls.Add(this.cmbSeries);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransformSalesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Μετασχηματισμός Πώλησης";

            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSourceInfo;
        private System.Windows.Forms.Label lblTargetInfo;
        private System.Windows.Forms.Label lblSeries;
        private System.Windows.Forms.ComboBox cmbSeries;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlInfo;
    }
}