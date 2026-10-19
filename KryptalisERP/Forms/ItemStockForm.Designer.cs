namespace KryptalisERP
{
    partial class ItemStockForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.cmbVariant = new System.Windows.Forms.ComboBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.numMinStock = new System.Windows.Forms.NumericUpDown();
            this.numMaxStock = new System.Windows.Forms.NumericUpDown();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.lblVariant = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxStock)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "📦 Stock ανά Αποθήκη";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.lblWarehouse.AutoSize = true; this.lblWarehouse.Location = new System.Drawing.Point(20, 60); this.lblWarehouse.Text = "Αποθήκη:"; this.lblWarehouse.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbWarehouse.Location = new System.Drawing.Point(100, 57); this.cmbWarehouse.Size = new System.Drawing.Size(150, 30); this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblVariant.AutoSize = true; this.lblVariant.Location = new System.Drawing.Point(270, 60); this.lblVariant.Text = "Παραλλαγή:"; this.lblVariant.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbVariant.Location = new System.Drawing.Point(380, 57); this.cmbVariant.Size = new System.Drawing.Size(150, 30); this.cmbVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblQty.AutoSize = true; this.lblQty.Location = new System.Drawing.Point(550, 60); this.lblQty.Text = "Ποσότητα:"; this.lblQty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numQuantity.Location = new System.Drawing.Point(640, 57); this.numQuantity.Size = new System.Drawing.Size(100, 30); this.numQuantity.DecimalPlaces = 2; this.numQuantity.Value = 0;

            this.lblMin.AutoSize = true; this.lblMin.Location = new System.Drawing.Point(20, 100); this.lblMin.Text = "Ελάχιστο:"; this.lblMin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numMinStock.Location = new System.Drawing.Point(100, 97); this.numMinStock.Size = new System.Drawing.Size(100, 30); this.numMinStock.DecimalPlaces = 2;

            this.lblMax.AutoSize = true; this.lblMax.Location = new System.Drawing.Point(220, 100); this.lblMax.Text = "Μέγιστο:"; this.lblMax.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numMaxStock.Location = new System.Drawing.Point(300, 97); this.numMaxStock.Size = new System.Drawing.Size(100, 30); this.numMaxStock.DecimalPlaces = 2;

            this.txtNotes.Location = new System.Drawing.Point(420, 97); this.txtNotes.Size = new System.Drawing.Size(320, 30);

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 35); this.btnAdd.Location = new System.Drawing.Point(20, 140);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 35); this.btnDelete.Location = new System.Drawing.Point(170, 140);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = " Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Location = new System.Drawing.Point(750, 140);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvStock.Location = new System.Drawing.Point(20, 190); this.dgvStock.Size = new System.Drawing.Size(860, 340);
            this.dgvStock.AllowUserToAddRows = false; this.dgvStock.AllowUserToDeleteRows = false;
            this.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStock.ReadOnly = true; this.dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStock.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.lblWarehouse); this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.lblVariant); this.Controls.Add(this.cmbVariant); this.Controls.Add(this.lblQty); this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblMin); this.Controls.Add(this.numMinStock); this.Controls.Add(this.lblMax); this.Controls.Add(this.numMaxStock);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvStock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemStockForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "📦 Stock Management";

            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxStock)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.ComboBox cmbWarehouse, cmbVariant;
        private System.Windows.Forms.NumericUpDown numQuantity, numMinStock, numMaxStock;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle, lblWarehouse, lblVariant, lblQty, lblMin, lblMax;
    }
}