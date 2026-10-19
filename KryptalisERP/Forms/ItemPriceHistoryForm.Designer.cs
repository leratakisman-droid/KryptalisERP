namespace KryptalisERP
{
    partial class ItemPriceHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "📊 Ιστορικό Τιμών";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.dgvHistory.Location = new System.Drawing.Point(20, 60); this.dgvHistory.Size = new System.Drawing.Size(760, 440);
            this.dgvHistory.AllowUserToAddRows = false; this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.ReadOnly = true; this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;

            this.btnClose.Text = " Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 40); this.btnClose.Location = new System.Drawing.Point(650, 510);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 570);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.dgvHistory); this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemPriceHistoryForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📊 Ιστορικό Τιμών";

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}