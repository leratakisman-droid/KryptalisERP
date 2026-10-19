namespace KryptalisERP
{
    partial class ItemColorsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvColors = new System.Windows.Forms.DataGridView();
            this.txtColorCode = new System.Windows.Forms.TextBox();
            this.txtColorName = new System.Windows.Forms.TextBox();
            this.txtColorHex = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "🎨 Χρώματα";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.txtColorCode.Location = new System.Drawing.Point(20, 60); this.txtColorCode.Size = new System.Drawing.Size(150, 30);
            this.txtColorName.Location = new System.Drawing.Point(180, 60); this.txtColorName.Size = new System.Drawing.Size(250, 30);
            this.txtColorHex.Location = new System.Drawing.Point(440, 60); this.txtColorHex.Size = new System.Drawing.Size(120, 30);

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 35); this.btnAdd.Location = new System.Drawing.Point(20, 100);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 35); this.btnDelete.Location = new System.Drawing.Point(170, 100);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Location = new System.Drawing.Point(750, 100);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvColors.Location = new System.Drawing.Point(20, 150); this.dgvColors.Size = new System.Drawing.Size(860, 380);
            this.dgvColors.AllowUserToAddRows = false; this.dgvColors.AllowUserToDeleteRows = false;
            this.dgvColors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColors.ReadOnly = true; this.dgvColors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvColors.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.txtColorCode); this.Controls.Add(this.txtColorName);
            this.Controls.Add(this.txtColorHex);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvColors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemColorsForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🎨 Χρώματα";

            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvColors;
        private System.Windows.Forms.TextBox txtColorCode, txtColorName, txtColorHex;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}