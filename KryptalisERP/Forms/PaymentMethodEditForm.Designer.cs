namespace KryptalisERP
{
    partial class PaymentMethodEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblMyData = new System.Windows.Forms.Label();
            this.txtMyData = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ═══════════════════════════════════════════════════════════
            // ΧΡΩΜΑΤΑ THEME
            // ═══════════════════════════════════════════════════════════
            System.Drawing.Color formBg = System.Drawing.Color.FromArgb(241, 245, 249);
            System.Drawing.Color titleColor = System.Drawing.Color.FromArgb(212, 175, 55);
            System.Drawing.Color labelColor = System.Drawing.Color.FromArgb(44, 62, 80);
            System.Drawing.Color inputBg = System.Drawing.Color.White;

            // ═══════════════════════════════════════════════════════════
            // ΤΙΤΛΟΣ
            // ═══════════════════════════════════════════════════════════
            this.lblTitle.Text = "💳 Τρόπος Πληρωμής";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = titleColor;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Name = "lblTitle";

            // ═══════════════════════════════════════════════════════════
            // ΣΤΑΘΕΡΕΣ ΣΤΗΛΕΣ
            // ═══════════════════════════════════════════════════════════
            int lblX = 20, inpX = 140;
            int y = 60;

            // Κωδικός
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(lblX, y + 3);
            this.lblCode.Text = "Κωδικός:";
            this.lblCode.Name = "lblCode";
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = labelColor;

            this.txtCode.Location = new System.Drawing.Point(inpX, y);
            this.txtCode.Size = new System.Drawing.Size(250, 25);
            this.txtCode.Name = "txtCode";
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.BackColor = inputBg;

            // Όνομα
            y += 40;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(lblX, y + 3);
            this.lblName.Text = "Όνομα:";
            this.lblName.Name = "lblName";
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = labelColor;

            this.txtName.Location = new System.Drawing.Point(inpX, y);
            this.txtName.Size = new System.Drawing.Size(250, 25);
            this.txtName.Name = "txtName";
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.BackColor = inputBg;

            // Κωδ. myDATA
            y += 40;
            this.lblMyData.AutoSize = true;
            this.lblMyData.Location = new System.Drawing.Point(lblX, y + 3);
            this.lblMyData.Text = "Κωδ. myDATA:";
            this.lblMyData.Name = "lblMyData";
            this.lblMyData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMyData.ForeColor = labelColor;

            this.txtMyData.Location = new System.Drawing.Point(inpX, y);
            this.txtMyData.Size = new System.Drawing.Size(120, 25);
            this.txtMyData.Name = "txtMyData";
            this.txtMyData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMyData.BackColor = inputBg;

            // Περιγραφή
            y += 40;
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(lblX, y + 3);
            this.lblDesc.Text = "Περιγραφή:";
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDesc.ForeColor = labelColor;

            this.txtDesc.Location = new System.Drawing.Point(inpX, y);
            this.txtDesc.Size = new System.Drawing.Size(250, 25);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDesc.BackColor = inputBg;

            // Ενεργός
            y += 45;
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.Location = new System.Drawing.Point(inpX, y);
            this.chkActive.Name = "chkActive";
            this.chkActive.Text = "✅ Ενεργός";
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkActive.ForeColor = labelColor;

            // Κουμπιά
            y += 50;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(80, y);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 40);
            this.btnSave.Text = "💾 Αποθήκευση";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(230, y);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ═══════════════════════════════════════════════════════════
            // FORM
            // ═══════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = formBg;
            this.ClientSize = new System.Drawing.Size(420, y + 60);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblMyData);
            this.Controls.Add(this.txtMyData);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentMethodEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Τρόπος Πληρωμής";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode, lblName, lblMyData, lblDesc;
        private System.Windows.Forms.TextBox txtCode, txtName, txtMyData, txtDesc;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}