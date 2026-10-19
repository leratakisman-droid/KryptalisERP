namespace KryptalisERP
{
    partial class ItemAttributesForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvAttributes = new System.Windows.Forms.DataGridView();
            this.txtAttributeName = new System.Windows.Forms.TextBox();
            this.txtAttributeValue = new System.Windows.Forms.TextBox();
            this.txtAttributeCode = new System.Windows.Forms.TextBox();
            this.cmbAttributeType = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "️ Χαρακτηριστικά Είδους";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.txtAttributeName.Location = new System.Drawing.Point(20, 60); this.txtAttributeName.Size = new System.Drawing.Size(150, 30);
            this.txtAttributeValue.Location = new System.Drawing.Point(180, 60); this.txtAttributeValue.Size = new System.Drawing.Size(200, 30);
            this.txtAttributeCode.Location = new System.Drawing.Point(390, 60); this.txtAttributeCode.Size = new System.Drawing.Size(120, 30);
            this.cmbAttributeType.Location = new System.Drawing.Point(520, 60); this.cmbAttributeType.Size = new System.Drawing.Size(120, 30);
            this.cmbAttributeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 35); this.btnAdd.Location = new System.Drawing.Point(20, 100);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 35); this.btnDelete.Location = new System.Drawing.Point(170, 100);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Location = new System.Drawing.Point(750, 100);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvAttributes.Location = new System.Drawing.Point(20, 150); this.dgvAttributes.Size = new System.Drawing.Size(860, 380);
            this.dgvAttributes.AllowUserToAddRows = false; this.dgvAttributes.AllowUserToDeleteRows = false;
            this.dgvAttributes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttributes.ReadOnly = true; this.dgvAttributes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttributes.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.txtAttributeName); this.Controls.Add(this.txtAttributeValue);
            this.Controls.Add(this.txtAttributeCode); this.Controls.Add(this.cmbAttributeType);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvAttributes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemAttributesForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "⚙️ Χαρακτηριστικά";

            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvAttributes;
        private System.Windows.Forms.TextBox txtAttributeName, txtAttributeValue, txtAttributeCode;
        private System.Windows.Forms.ComboBox cmbAttributeType;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}