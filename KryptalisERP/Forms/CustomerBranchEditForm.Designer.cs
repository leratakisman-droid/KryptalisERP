namespace KryptalisERP
{
    partial class CustomerBranchEditForm
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
            this.lblBranchName = new System.Windows.Forms.Label();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.lblBranchCode = new System.Windows.Forms.Label();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblManagerName = new System.Windows.Forms.Label();
            this.txtManagerName = new System.Windows.Forms.TextBox();
            this.lblVatNumber = new System.Windows.Forms.Label();
            this.txtVatNumber = new System.Windows.Forms.TextBox();
            this.lblDoy = new System.Windows.Forms.Label();
            this.txtDoy = new System.Windows.Forms.TextBox();
            this.lblBankName = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblBankIban = new System.Windows.Forms.Label();
            this.txtBankIban = new System.Windows.Forms.TextBox();
            this.lblBankIban2 = new System.Windows.Forms.Label();
            this.txtBankIban2 = new System.Windows.Forms.TextBox();
            this.lblBankIban3 = new System.Windows.Forms.Label();
            this.txtBankIban3 = new System.Windows.Forms.TextBox();
            this.lblBankIban4 = new System.Windows.Forms.Label();
            this.txtBankIban4 = new System.Windows.Forms.TextBox();
            this.lblBankIban5 = new System.Windows.Forms.Label();
            this.txtBankIban5 = new System.Windows.Forms.TextBox();
            this.lblBankIban6 = new System.Windows.Forms.Label();
            this.txtBankIban6 = new System.Windows.Forms.TextBox();
            this.chkIsMain = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20;
            int labelX = 20;
            int controlX = 180;
            int rowHeight = 42;

            // Όνομα Υποκαταστήματος
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBranchName.Text = "Όνομα Υποκ.:";
            this.lblBranchName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBranchName.Location = new System.Drawing.Point(controlX, y);
            this.txtBranchName.Size = new System.Drawing.Size(400, 30);
            this.txtBranchName.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Κωδικός
            this.lblBranchCode.AutoSize = true;
            this.lblBranchCode.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBranchCode.Text = "Κωδικός:";
            this.lblBranchCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBranchCode.Location = new System.Drawing.Point(controlX, y);
            this.txtBranchCode.Size = new System.Drawing.Size(150, 30);
            this.txtBranchCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Διεύθυνση
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblAddress.Text = "Διεύθυνση:";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtAddress.Location = new System.Drawing.Point(controlX, y);
            this.txtAddress.Size = new System.Drawing.Size(400, 30);
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Πόλη + ΤΚ
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblCity.Text = "Πόλη:";
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCity.Location = new System.Drawing.Point(controlX, y);
            this.txtCity.Size = new System.Drawing.Size(180, 30);
            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblPostalCode.AutoSize = true;
            this.lblPostalCode.Location = new System.Drawing.Point(controlX + 200, y + 12);
            this.lblPostalCode.Text = "ΤΚ:";
            this.lblPostalCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPostalCode.Location = new System.Drawing.Point(controlX + 240, y);
            this.txtPostalCode.Size = new System.Drawing.Size(100, 30);
            this.txtPostalCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Τηλέφωνο + Κινητό
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblPhone.Text = "Τηλέφωνο:";
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone.Location = new System.Drawing.Point(controlX, y);
            this.txtPhone.Size = new System.Drawing.Size(180, 30);
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(controlX + 200, y + 12);
            this.lblMobile.Text = "Κινητό:";
            this.lblMobile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtMobile.Location = new System.Drawing.Point(controlX + 270, y);
            this.txtMobile.Size = new System.Drawing.Size(180, 30);
            this.txtMobile.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(controlX, y);
            this.txtEmail.Size = new System.Drawing.Size(400, 30);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Υπεύθυνος
            this.lblManagerName.AutoSize = true;
            this.lblManagerName.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblManagerName.Text = "Υπεύθυνος:";
            this.lblManagerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtManagerName.Location = new System.Drawing.Point(controlX, y);
            this.txtManagerName.Size = new System.Drawing.Size(400, 30);
            this.txtManagerName.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // ΑΦΜ + ΔΟΥ
            this.lblVatNumber.AutoSize = true;
            this.lblVatNumber.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblVatNumber.Text = "ΑΦΜ:";
            this.lblVatNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtVatNumber.Location = new System.Drawing.Point(controlX, y);
            this.txtVatNumber.Size = new System.Drawing.Size(150, 30);
            this.txtVatNumber.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblDoy.AutoSize = true;
            this.lblDoy.Location = new System.Drawing.Point(controlX + 170, y + 12);
            this.lblDoy.Text = "ΔΟΥ:";
            this.lblDoy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtDoy.Location = new System.Drawing.Point(controlX + 220, y);
            this.txtDoy.Size = new System.Drawing.Size(200, 30);
            this.txtDoy.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // Τράπεζα + IBAN 1
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBankName.Text = "Τράπεζα:";
            this.lblBankName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankName.Location = new System.Drawing.Point(controlX, y);
            this.txtBankName.Size = new System.Drawing.Size(200, 30);
            this.txtBankName.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblBankIban.AutoSize = true;
            this.lblBankIban.Location = new System.Drawing.Point(controlX + 220, y + 12);
            this.lblBankIban.Text = "IBAN 1:";
            this.lblBankIban.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankIban.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtBankIban.Size = new System.Drawing.Size(280, 30);
            this.txtBankIban.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // IBAN 2
            this.lblBankIban2.AutoSize = true;
            this.lblBankIban2.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBankIban2.Text = "IBAN 2:";
            this.lblBankIban2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankIban2.Location = new System.Drawing.Point(controlX, y);
            this.txtBankIban2.Size = new System.Drawing.Size(570, 30);
            this.txtBankIban2.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // IBAN 3
            this.lblBankIban3.AutoSize = true;
            this.lblBankIban3.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBankIban3.Text = "IBAN 3:";
            this.lblBankIban3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankIban3.Location = new System.Drawing.Point(controlX, y);
            this.txtBankIban3.Size = new System.Drawing.Size(570, 30);
            this.txtBankIban3.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // IBAN 4
            this.lblBankIban4.AutoSize = true;
            this.lblBankIban4.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBankIban4.Text = "IBAN 4:";
            this.lblBankIban4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankIban4.Location = new System.Drawing.Point(controlX, y);
            this.txtBankIban4.Size = new System.Drawing.Size(570, 30);
            this.txtBankIban4.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // IBAN 5
            this.lblBankIban5.AutoSize = true;
            this.lblBankIban5.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBankIban5.Text = "IBAN 5:";
            this.lblBankIban5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankIban5.Location = new System.Drawing.Point(controlX, y);
            this.txtBankIban5.Size = new System.Drawing.Size(570, 30);
            this.txtBankIban5.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // IBAN 6
            this.lblBankIban6.AutoSize = true;
            this.lblBankIban6.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBankIban6.Text = "IBAN 6:";
            this.lblBankIban6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBankIban6.Location = new System.Drawing.Point(controlX, y);
            this.txtBankIban6.Size = new System.Drawing.Size(570, 30);
            this.txtBankIban6.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight + 10;

            // Κύριο Υποκατάστημα
            this.chkIsMain.AutoSize = true;
            this.chkIsMain.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsMain.Text = "🏆 Κύριο Υποκατάστημα";
            this.chkIsMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            y += 50;

            // Κουμπιά
            this.btnSave.Location = new System.Drawing.Point(400, y);
            this.btnSave.Size = new System.Drawing.Size(160, 45);
            this.btnSave.Text = "💾 Αποθήκευση";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(570, y);
            this.btnCancel.Size = new System.Drawing.Size(140, 45);
            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBranchName, this.txtBranchName,
                this.lblBranchCode, this.txtBranchCode,
                this.lblAddress, this.txtAddress,
                this.lblCity, this.txtCity,
                this.lblPostalCode, this.txtPostalCode,
                this.lblPhone, this.txtPhone,
                this.lblMobile, this.txtMobile,
                this.lblEmail, this.txtEmail,
                this.lblManagerName, this.txtManagerName,
                this.lblVatNumber, this.txtVatNumber,
                this.lblDoy, this.txtDoy,
                this.lblBankName, this.txtBankName,
                this.lblBankIban, this.txtBankIban,
                this.lblBankIban2, this.txtBankIban2,
                this.lblBankIban3, this.txtBankIban3,
                this.lblBankIban4, this.txtBankIban4,
                this.lblBankIban5, this.txtBankIban5,
                this.lblBankIban6, this.txtBankIban6,
                this.chkIsMain, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerBranchEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "✏️ Επεξεργασία Υποκαταστήματος";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblBranchName, lblBranchCode, lblAddress, lblCity, lblPostalCode;
        private System.Windows.Forms.Label lblPhone, lblMobile, lblEmail, lblManagerName, lblVatNumber, lblDoy;
        private System.Windows.Forms.Label lblBankName, lblBankIban, lblBankIban2, lblBankIban3, lblBankIban4, lblBankIban5, lblBankIban6;
        private System.Windows.Forms.TextBox txtBranchName, txtBranchCode, txtAddress, txtCity, txtPostalCode;
        private System.Windows.Forms.TextBox txtPhone, txtMobile, txtEmail, txtManagerName, txtVatNumber, txtDoy;
        private System.Windows.Forms.TextBox txtBankName, txtBankIban, txtBankIban2, txtBankIban3, txtBankIban4, txtBankIban5, txtBankIban6;
        private System.Windows.Forms.CheckBox chkIsMain;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}