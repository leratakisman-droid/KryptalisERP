namespace KryptalisERP
{
    partial class WithholdingEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblCode = new System.Windows.Forms.Label(); this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label(); this.txtName = new System.Windows.Forms.TextBox();
            this.lblRate = new System.Windows.Forms.Label(); this.numRate = new System.Windows.Forms.NumericUpDown();
            this.lblCat = new System.Windows.Forms.Label(); this.txtCat = new System.Windows.Forms.TextBox();
            this.lblMyData = new System.Windows.Forms.Label(); this.txtMyData = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label(); this.txtDesc = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button(); this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numRate)).BeginInit();
            this.SuspendLayout();

            this.lblCode.AutoSize = true; this.lblCode.Location = new System.Drawing.Point(20, 20); this.lblCode.Text = "Κωδικός:"; this.txtCode.Location = new System.Drawing.Point(120, 17); this.txtCode.Size = new System.Drawing.Size(200, 23);
            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(20, 55); this.lblName.Text = "Όνομα:"; this.txtName.Location = new System.Drawing.Point(120, 52); this.txtName.Size = new System.Drawing.Size(200, 23);
            this.lblRate.AutoSize = true; this.lblRate.Location = new System.Drawing.Point(20, 90); this.lblRate.Text = "Ποσοστό %:"; this.numRate.Location = new System.Drawing.Point(120, 87); this.numRate.Size = new System.Drawing.Size(80, 23); this.numRate.DecimalPlaces = 2; this.numRate.Maximum = 100;
            this.lblCat.AutoSize = true; this.lblCat.Location = new System.Drawing.Point(20, 125); this.lblCat.Text = "Κατηγορία:"; this.txtCat.Location = new System.Drawing.Point(120, 122); this.txtCat.Size = new System.Drawing.Size(200, 23);
            this.lblMyData.AutoSize = true; this.lblMyData.Location = new System.Drawing.Point(20, 160); this.lblMyData.Text = "Κωδ. myDATA:"; this.txtMyData.Location = new System.Drawing.Point(120, 157); this.txtMyData.Size = new System.Drawing.Size(100, 23);
            this.lblDesc.AutoSize = true; this.lblDesc.Location = new System.Drawing.Point(20, 195); this.lblDesc.Text = "Περιγραφή:"; this.txtDesc.Location = new System.Drawing.Point(120, 192); this.txtDesc.Size = new System.Drawing.Size(200, 23);
            this.chkActive.AutoSize = true; this.chkActive.Checked = true; this.chkActive.Location = new System.Drawing.Point(120, 230); this.chkActive.Name = "chkActive"; this.chkActive.Text = "Ενεργός";

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(80, 270); this.btnSave.Name = "btnSave"; this.btnSave.Size = new System.Drawing.Size(100, 35); this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(190, 270); this.btnCancel.Name = "btnCancel"; this.btnCancel.Size = new System.Drawing.Size(100, 35); this.btnCancel.Text = "❌ Ακύρωση"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 330);
            this.Controls.Add(this.lblCode); this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblRate); this.Controls.Add(this.numRate);
            this.Controls.Add(this.lblCat); this.Controls.Add(this.txtCat);
            this.Controls.Add(this.lblMyData); this.Controls.Add(this.txtMyData);
            this.Controls.Add(this.lblDesc); this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "WithholdingEditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Κράτηση / Φόρος";
            ((System.ComponentModel.ISupportInitialize)(this.numRate)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblCode, lblName, lblRate, lblCat, lblMyData, lblDesc;
        private System.Windows.Forms.TextBox txtCode, txtName, txtCat, txtMyData, txtDesc;
        private System.Windows.Forms.NumericUpDown numRate;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}