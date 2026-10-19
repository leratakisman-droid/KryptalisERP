namespace KryptalisERP
{
    partial class ItemUnitRelationsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvRelations = new System.Windows.Forms.DataGridView();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.cmbRelationType = new System.Windows.Forms.ComboBox();
            this.numCoefficient = new System.Windows.Forms.NumericUpDown();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblCoeff = new System.Windows.Forms.Label();
            this.lblFormula = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRelations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefficient)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "🔄 Μετατροπές Μονάδων";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.lblUnit.AutoSize = true; this.lblUnit.Location = new System.Drawing.Point(20, 60); this.lblUnit.Text = "Μονάδα:"; this.lblUnit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbUnit.Location = new System.Drawing.Point(100, 57); this.cmbUnit.Size = new System.Drawing.Size(150, 30); this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblType.AutoSize = true; this.lblType.Location = new System.Drawing.Point(270, 60); this.lblType.Text = "Τύπος:"; this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbRelationType.Location = new System.Drawing.Point(330, 57); this.cmbRelationType.Size = new System.Drawing.Size(150, 30); this.cmbRelationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblCoeff.AutoSize = true; this.lblCoeff.Location = new System.Drawing.Point(500, 60); this.lblCoeff.Text = "Συντελεστής:"; this.lblCoeff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numCoefficient.Location = new System.Drawing.Point(610, 57); this.numCoefficient.Size = new System.Drawing.Size(100, 30); this.numCoefficient.DecimalPlaces = 4; this.numCoefficient.Value = 1;

            this.lblFormula.AutoSize = true; this.lblFormula.Location = new System.Drawing.Point(20, 100); this.lblFormula.Text = "Τύπος:"; this.lblFormula.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtFormula.Location = new System.Drawing.Point(100, 97); this.txtFormula.Size = new System.Drawing.Size(300, 30);

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 35); this.btnAdd.Location = new System.Drawing.Point(420, 95);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 35); this.btnDelete.Location = new System.Drawing.Point(570, 95);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Location = new System.Drawing.Point(720, 95);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvRelations.Location = new System.Drawing.Point(20, 140); this.dgvRelations.Size = new System.Drawing.Size(860, 380);
            this.dgvRelations.AllowUserToAddRows = false; this.dgvRelations.AllowUserToDeleteRows = false;
            this.dgvRelations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRelations.ReadOnly = true; this.dgvRelations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRelations.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.lblUnit); this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblType); this.Controls.Add(this.cmbRelationType); this.Controls.Add(this.lblCoeff); this.Controls.Add(this.numCoefficient);
            this.Controls.Add(this.lblFormula); this.Controls.Add(this.txtFormula);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvRelations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemUnitRelationsForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🔄 Μετατροπές Μονάδων";

            ((System.ComponentModel.ISupportInitialize)(this.dgvRelations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoefficient)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvRelations;
        private System.Windows.Forms.ComboBox cmbUnit, cmbRelationType;
        private System.Windows.Forms.NumericUpDown numCoefficient;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle, lblUnit, lblType, lblCoeff, lblFormula;
    }
}