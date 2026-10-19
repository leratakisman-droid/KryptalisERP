namespace KryptalisERP
{
    partial class ItemCPVCodesForm
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
            this.dgvCPVs = new System.Windows.Forms.DataGridView();
            this.cmbCPVCode = new System.Windows.Forms.ComboBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsPrimary = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCPV = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCPVs)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30);
            this.lblTitle.Text = "🏷️ CPV Codes Είδους";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.lblCPV.AutoSize = true;
            this.lblCPV.Location = new System.Drawing.Point(20, 60);
            this.lblCPV.Text = "CPV Code:";
            this.lblCPV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.cmbCPVCode.Location = new System.Drawing.Point(120, 57);
            this.cmbCPVCode.Size = new System.Drawing.Size(400, 30);
            this.cmbCPVCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPVCode.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.chkIsPrimary.AutoSize = true;
            this.chkIsPrimary.Location = new System.Drawing.Point(540, 62);
            this.chkIsPrimary.Text = "👑 Κύριο";
            this.chkIsPrimary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(20, 100);
            this.lblNotes.Text = "Σημειώσεις:";
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.txtNotes.Location = new System.Drawing.Point(120, 97);
            this.txtNotes.Size = new System.Drawing.Size(500, 30);
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.btnAdd.Text = "➕ Προσθήκη";
            this.btnAdd.Size = new System.Drawing.Size(140, 35);
            this.btnAdd.Location = new System.Drawing.Point(20, 140);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή";
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.Location = new System.Drawing.Point(170, 140);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο";
            this.btnClose.Size = new System.Drawing.Size(130, 35);
            this.btnClose.Location = new System.Drawing.Point(750, 140);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvCPVs.Location = new System.Drawing.Point(20, 190);
            this.dgvCPVs.Size = new System.Drawing.Size(860, 340);
            this.dgvCPVs.AllowUserToAddRows = false;
            this.dgvCPVs.AllowUserToDeleteRows = false;
            this.dgvCPVs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCPVs.ReadOnly = true;
            this.dgvCPVs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCPVs.BackgroundColor = System.Drawing.Color.White;
            this.dgvCPVs.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvCPVs.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCPVs.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvCPVs.ColumnHeadersHeight = 40;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCPV);
            this.Controls.Add(this.cmbCPVCode);
            this.Controls.Add(this.chkIsPrimary);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvCPVs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemCPVCodesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🏷️ CPV Codes Είδους";

            ((System.ComponentModel.ISupportInitialize)(this.dgvCPVs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvCPVs;
        private System.Windows.Forms.ComboBox cmbCPVCode;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.CheckBox chkIsPrimary;
        private System.Windows.Forms.Button btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle, lblCPV, lblNotes;
    }
}