namespace KryptalisERP
{
    partial class ItemSuppliersForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.numMinQty = new System.Windows.Forms.NumericUpDown();
            this.numLeadTime = new System.Windows.Forms.NumericUpDown();
            this.chkIsDefault = new System.Windows.Forms.CheckBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeadTime)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "🚚 Προμηθευτές Είδους";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.cmbSupplier.Location = new System.Drawing.Point(20, 60); this.cmbSupplier.Size = new System.Drawing.Size(200, 30); this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSupplierCode.Location = new System.Drawing.Point(230, 60); this.txtSupplierCode.Size = new System.Drawing.Size(120, 30);
            this.numPrice.Location = new System.Drawing.Point(360, 60); this.numPrice.Size = new System.Drawing.Size(100, 30); this.numPrice.DecimalPlaces = 2;
            this.cmbCurrency.Location = new System.Drawing.Point(470, 60); this.cmbCurrency.Size = new System.Drawing.Size(80, 30);
            this.cmbCurrency.Items.AddRange(new object[] { "€", "$", "£" }); this.cmbCurrency.SelectedIndex = 0;
            this.numMinQty.Location = new System.Drawing.Point(560, 60); this.numMinQty.Size = new System.Drawing.Size(80, 30); this.numMinQty.DecimalPlaces = 2;
            this.numLeadTime.Location = new System.Drawing.Point(650, 60); this.numLeadTime.Size = new System.Drawing.Size(60, 30);
            this.chkIsDefault.AutoSize = true; this.chkIsDefault.Location = new System.Drawing.Point(720, 65); this.chkIsDefault.Text = "Βασικός";

            this.txtNotes.Location = new System.Drawing.Point(20, 100); this.txtNotes.Size = new System.Drawing.Size(600, 30);

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 35); this.btnAdd.Location = new System.Drawing.Point(20, 140);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 35); this.btnDelete.Location = new System.Drawing.Point(170, 140);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Location = new System.Drawing.Point(750, 140);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvSuppliers.Location = new System.Drawing.Point(20, 190); this.dgvSuppliers.Size = new System.Drawing.Size(860, 340);
            this.dgvSuppliers.AllowUserToAddRows = false; this.dgvSuppliers.AllowUserToDeleteRows = false;
            this.dgvSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuppliers.ReadOnly = true; this.dgvSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliers.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.cmbSupplier); this.Controls.Add(this.txtSupplierCode);
            this.Controls.Add(this.numPrice); this.Controls.Add(this.cmbCurrency); this.Controls.Add(this.numMinQty);
            this.Controls.Add(this.numLeadTime); this.Controls.Add(this.chkIsDefault); this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvSuppliers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemSuppliersForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🚚 Προμηθευτές";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeadTime)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.ComboBox cmbSupplier, cmbCurrency;
        private System.Windows.Forms.TextBox txtSupplierCode, txtNotes;
        private System.Windows.Forms.NumericUpDown numPrice, numMinQty, numLeadTime;
        private System.Windows.Forms.CheckBox chkIsDefault;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}