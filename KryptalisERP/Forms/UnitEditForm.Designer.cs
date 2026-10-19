namespace KryptalisERP
{
    partial class UnitEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSymbol = new System.Windows.Forms.Label();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 30, labelX = 30, controlX = 180;

            this.lblCode.AutoSize = true; this.lblCode.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblCode.Text = "Κωδικός:"; this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCode.Location = new System.Drawing.Point(controlX, y); this.txtCode.Size = new System.Drawing.Size(150, 30);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F); y += 50;

            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblName.Text = "Όνομα*:"; this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtName.Location = new System.Drawing.Point(controlX, y); this.txtName.Size = new System.Drawing.Size(300, 30);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F); y += 50;

            this.lblSymbol.AutoSize = true; this.lblSymbol.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblSymbol.Text = "Σύμβολο:"; this.lblSymbol.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtSymbol.Location = new System.Drawing.Point(controlX, y); this.txtSymbol.Size = new System.Drawing.Size(100, 30);
            this.txtSymbol.Font = new System.Drawing.Font("Segoe UI", 10F); y += 50;

            this.lblType.AutoSize = true; this.lblType.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblType.Text = "Τύπος:"; this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbType.Location = new System.Drawing.Point(controlX, y); this.cmbType.Size = new System.Drawing.Size(200, 30);
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10F); y += 60;

            this.chkIsActive.AutoSize = true; this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "✅ Ενεργή"; this.chkIsActive.Checked = true; this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 60;

            this.btnSave.Location = new System.Drawing.Point(300, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(440, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = " Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCode, this.txtCode, this.lblName, this.txtName, this.lblSymbol, this.txtSymbol,
                this.lblType, this.cmbType, this.chkIsActive, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 320);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "UnitEditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Νέα Μονάδα Μέτρησης";
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblCode, lblName, lblSymbol, lblType;
        private System.Windows.Forms.TextBox txtCode, txtName, txtSymbol;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}