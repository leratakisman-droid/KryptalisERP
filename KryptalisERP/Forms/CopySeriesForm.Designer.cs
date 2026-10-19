namespace KryptalisERP
{
    partial class CopySeriesForm
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
            this.lblSource = new System.Windows.Forms.Label();
            this.cmbSourceCompany = new System.Windows.Forms.ComboBox();
            this.lblSourceBranch = new System.Windows.Forms.Label();
            this.cmbSourceBranch = new System.Windows.Forms.ComboBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.cmbDestCompany = new System.Windows.Forms.ComboBox();
            this.lblDestBranch = new System.Windows.Forms.Label();
            this.cmbDestBranch = new System.Windows.Forms.ComboBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtCustomPrefix = new System.Windows.Forms.TextBox();
            this.dgvSeries = new System.Windows.Forms.DataGridView();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSeries)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text = "📋 Αντιγραφή Σειρών μεταξύ Εταιρειών/Υποκαταστημάτων";
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.AutoSize = true;

            // ΠΗΓΗ - Εταιρεία
            this.lblSource.Text = " ΠΗΓΗ - Εταιρεία:";
            this.lblSource.Location = new System.Drawing.Point(20, 60);
            this.lblSource.AutoSize = true;
            this.lblSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.cmbSourceCompany.Location = new System.Drawing.Point(180, 57);
            this.cmbSourceCompany.Size = new System.Drawing.Size(250, 23);
            this.cmbSourceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ΠΗΓΗ - Υποκατάστημα
            this.lblSourceBranch.Text = "Υποκατάστημα:";
            this.lblSourceBranch.Location = new System.Drawing.Point(450, 60);
            this.lblSourceBranch.AutoSize = true;
            this.lblSourceBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.cmbSourceBranch.Location = new System.Drawing.Point(560, 57);
            this.cmbSourceBranch.Size = new System.Drawing.Size(250, 23);
            this.cmbSourceBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ΠΡΟΟΡΙΣΜΟΣ - Εταιρεία
            this.lblDest.Text = "📥 ΠΡΟΟΡΙΣΜΟΣ - Εταιρεία:";
            this.lblDest.Location = new System.Drawing.Point(20, 100);
            this.lblDest.AutoSize = true;
            this.lblDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.cmbDestCompany.Location = new System.Drawing.Point(200, 97);
            this.cmbDestCompany.Size = new System.Drawing.Size(250, 23);
            this.cmbDestCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ΠΡΟΟΡΙΣΜΟΣ - Υποκατάστημα
            this.lblDestBranch.Text = "Υποκατάστημα:";
            this.lblDestBranch.Location = new System.Drawing.Point(470, 100);
            this.lblDestBranch.AutoSize = true;
            this.lblDestBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.cmbDestBranch.Location = new System.Drawing.Point(580, 97);
            this.cmbDestBranch.Size = new System.Drawing.Size(250, 23);
            this.cmbDestBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Custom Prefix
            this.lblPrefix.Text = "🔤 Νέο Prefix:";
            this.lblPrefix.Location = new System.Drawing.Point(20, 140);
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.txtCustomPrefix.Location = new System.Drawing.Point(130, 137);
            this.txtCustomPrefix.Size = new System.Drawing.Size(100, 23);
            this.txtCustomPrefix.Text = "";
            this.txtCustomPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // Info Label
            this.lblInfo.Text = "💡 Επιλέξεις σειρές από την πηγή (Ctrl+Click για πολλαπλές) → Πάτα 'Αντιγραφή' → Θα δημιουργηθούν στον προορισμό με νέο Prefix (π.χ. ΤΠ → ΤΠ-B)";
            this.lblInfo.Location = new System.Drawing.Point(20, 170);
            this.lblInfo.Size = new System.Drawing.Size(800, 20);
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);

            // DataGridView
            this.dgvSeries.Location = new System.Drawing.Point(20, 200);
            this.dgvSeries.Size = new System.Drawing.Size(810, 400);
            this.dgvSeries.BackgroundColor = System.Drawing.Color.White;
            this.dgvSeries.RowHeadersVisible = false;
            this.dgvSeries.AllowUserToAddRows = false;
            this.dgvSeries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSeries.MultiSelect = true;
            this.dgvSeries.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.dgvSeries.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            // btnCopy
            this.btnCopy.Text = " Αντιγραφή Επιλεγμένων";
            this.btnCopy.Location = new System.Drawing.Point(20, 620);
            this.btnCopy.Size = new System.Drawing.Size(220, 45);
            this.btnCopy.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);

            // btnClose
            this.btnClose.Text = "❌ Κλείσιμο";
            this.btnClose.Location = new System.Drawing.Point(710, 620);
            this.btnClose.Size = new System.Drawing.Size(120, 45);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(850, 680);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSource); this.Controls.Add(this.cmbSourceCompany);
            this.Controls.Add(this.lblSourceBranch); this.Controls.Add(this.cmbSourceBranch);
            this.Controls.Add(this.lblDest); this.Controls.Add(this.cmbDestCompany);
            this.Controls.Add(this.lblDestBranch); this.Controls.Add(this.cmbDestBranch);
            this.Controls.Add(this.lblPrefix); this.Controls.Add(this.txtCustomPrefix);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvSeries);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CopySeriesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Αντιγραφή Σειρών";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSeries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSource, lblSourceBranch, lblDest, lblDestBranch, lblPrefix, lblInfo;
        private System.Windows.Forms.ComboBox cmbSourceCompany, cmbSourceBranch, cmbDestCompany, cmbDestBranch;
        private System.Windows.Forms.TextBox txtCustomPrefix;
        private System.Windows.Forms.DataGridView dgvSeries;
        private System.Windows.Forms.Button btnCopy, btnClose;
    }
}