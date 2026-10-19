namespace KryptalisERP
{
    partial class ItemImagesForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvImages = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "📸 Φωτογραφίες Είδους";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 40); this.btnAdd.Location = new System.Drawing.Point(20, 60);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 40); this.btnDelete.Location = new System.Drawing.Point(170, 60);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 40); this.btnClose.Location = new System.Drawing.Point(650, 60);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvImages.Location = new System.Drawing.Point(20, 110); this.dgvImages.Size = new System.Drawing.Size(760, 420);
            this.dgvImages.AllowUserToAddRows = false; this.dgvImages.AllowUserToDeleteRows = false;
            this.dgvImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImages.ReadOnly = true; this.dgvImages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvImages.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose); this.Controls.Add(this.dgvImages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemImagesForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📸 Φωτογραφίες";

            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvImages;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}