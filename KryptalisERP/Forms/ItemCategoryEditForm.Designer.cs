namespace KryptalisERP
{
    partial class ItemCategoryEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
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
            this.txtName.Location = new System.Drawing.Point(controlX, y); this.txtName.Size = new System.Drawing.Size(400, 30);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F); y += 50;

            this.lblDescription.AutoSize = true; this.lblDescription.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblDescription.Text = "Περιγραφή:"; this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtDescription.Location = new System.Drawing.Point(controlX, y); this.txtDescription.Size = new System.Drawing.Size(400, 80);
            this.txtDescription.Multiline = true; this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F); y += 100;

            this.chkIsActive.AutoSize = true; this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "✅ Ενεργή"; this.chkIsActive.Checked = true; this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 60;

            this.btnSave.Location = new System.Drawing.Point(350, y); this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.Text = "💾 Αποθήκευση"; this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(490, y); this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.Text = "❌ Ακύρωση"; this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCode, this.txtCode, this.lblName, this.txtName,
                this.lblDescription, this.txtDescription, this.chkIsActive,
                this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 350);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemCategoryEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Νέα Κατηγορία";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblCode, lblName, lblDescription;
        private System.Windows.Forms.TextBox txtCode, txtName, txtDescription;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}