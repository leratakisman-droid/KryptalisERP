namespace KryptalisERP
{
    partial class SupplierBranchEditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

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
            this.lblArea = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblDoy = new System.Windows.Forms.Label();
            this.cmbDoy = new System.Windows.Forms.ComboBox();
            this.lblPhone1 = new System.Windows.Forms.Label();
            this.txtPhone1 = new System.Windows.Forms.TextBox();
            this.lblPhone2 = new System.Windows.Forms.Label();
            this.txtPhone2 = new System.Windows.Forms.TextBox();
            this.lblPhone3 = new System.Windows.Forms.Label();
            this.txtPhone3 = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblBank = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.lblIban1 = new System.Windows.Forms.Label();
            this.txtIban1 = new System.Windows.Forms.TextBox();
            this.lblBank2 = new System.Windows.Forms.Label();
            this.cmbBank2 = new System.Windows.Forms.ComboBox();
            this.lblIban2 = new System.Windows.Forms.Label();
            this.txtIban2 = new System.Windows.Forms.TextBox();
            this.lblBank3 = new System.Windows.Forms.Label();
            this.cmbBank3 = new System.Windows.Forms.ComboBox();
            this.lblIban3 = new System.Windows.Forms.Label();
            this.txtIban3 = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            int y = 20;
            int labelX = 20;
            int controlX = 150;
            int rowHeight = 40;

            this.lblBranchName.AutoSize = true;
            this.lblBranchName.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBranchName.Text = "Όνομα:";
            this.lblBranchName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBranchName.Location = new System.Drawing.Point(controlX, y);
            this.txtBranchName.Size = new System.Drawing.Size(400, 30);
            this.txtBranchName.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblBranchCode.AutoSize = true;
            this.lblBranchCode.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBranchCode.Text = "Κωδικός:";
            this.lblBranchCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBranchCode.Location = new System.Drawing.Point(controlX, y);
            this.txtBranchCode.Size = new System.Drawing.Size(150, 30);
            this.txtBranchCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblAddress.Text = "Διεύθυνση:";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtAddress.Location = new System.Drawing.Point(controlX, y);
            this.txtAddress.Size = new System.Drawing.Size(400, 30);
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblCity.Text = "Πόλη:";
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCity.Location = new System.Drawing.Point(controlX, y);
            this.txtCity.Size = new System.Drawing.Size(150, 30);
            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(controlX + 170, y + 10);
            this.lblArea.Text = "Περιοχή:";
            this.lblArea.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtArea.Location = new System.Drawing.Point(controlX + 240, y);
            this.txtArea.Size = new System.Drawing.Size(150, 30);
            this.txtArea.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblPostalCode.AutoSize = true;
            this.lblPostalCode.Location = new System.Drawing.Point(controlX + 410, y + 10);
            this.lblPostalCode.Text = "ΤΚ:";
            this.lblPostalCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPostalCode.Location = new System.Drawing.Point(controlX + 450, y);
            this.txtPostalCode.Size = new System.Drawing.Size(100, 30);
            this.txtPostalCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblDoy.AutoSize = true;
            this.lblDoy.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblDoy.Text = "ΔΟΥ:";
            this.lblDoy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbDoy.Location = new System.Drawing.Point(controlX, y);
            this.cmbDoy.Size = new System.Drawing.Size(300, 30);
            this.cmbDoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbDoy.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblPhone1.AutoSize = true;
            this.lblPhone1.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblPhone1.Text = "Τηλέφωνο 1:";
            this.lblPhone1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone1.Location = new System.Drawing.Point(controlX, y);
            this.txtPhone1.Size = new System.Drawing.Size(150, 30);
            this.txtPhone1.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblPhone2.AutoSize = true;
            this.lblPhone2.Location = new System.Drawing.Point(controlX + 170, y + 10);
            this.lblPhone2.Text = "Τηλέφωνο 2:";
            this.lblPhone2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone2.Location = new System.Drawing.Point(controlX + 270, y);
            this.txtPhone2.Size = new System.Drawing.Size(150, 30);
            this.txtPhone2.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblPhone3.AutoSize = true;
            this.lblPhone3.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblPhone3.Text = "Τηλέφωνο 3:";
            this.lblPhone3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone3.Location = new System.Drawing.Point(controlX, y);
            this.txtPhone3.Size = new System.Drawing.Size(150, 30);
            this.txtPhone3.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(controlX + 170, y + 10);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(controlX + 240, y);
            this.txtEmail.Size = new System.Drawing.Size(250, 30);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // ΓΡΑΜΜΗ 1: Τράπεζα + IBAN 1
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBank.Text = "Τράπεζα:";
            this.lblBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBank.Location = new System.Drawing.Point(controlX, y);
            this.cmbBank.Size = new System.Drawing.Size(200, 30);
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbBank.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblIban1.AutoSize = true;
            this.lblIban1.Location = new System.Drawing.Point(controlX + 220, y + 10);
            this.lblIban1.Text = "IBAN 1:";
            this.lblIban1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtIban1.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtIban1.Size = new System.Drawing.Size(280, 30);
            this.txtIban1.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // ΓΡΑΜΜΗ 2: Τράπεζα + IBAN 2
            this.lblBank2.AutoSize = true;
            this.lblBank2.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBank2.Text = "Τράπεζα:";
            this.lblBank2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBank2.Location = new System.Drawing.Point(controlX, y);
            this.cmbBank2.Size = new System.Drawing.Size(200, 30);
            this.cmbBank2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbBank2.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblIban2.AutoSize = true;
            this.lblIban2.Location = new System.Drawing.Point(controlX + 220, y + 10);
            this.lblIban2.Text = "IBAN 2:";
            this.lblIban2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtIban2.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtIban2.Size = new System.Drawing.Size(280, 30);
            this.txtIban2.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            // ΓΡΑΜΜΗ 3: Τράπεζα + IBAN 3
            this.lblBank3.AutoSize = true;
            this.lblBank3.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblBank3.Text = "Τράπεζα:";
            this.lblBank3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBank3.Location = new System.Drawing.Point(controlX, y);
            this.cmbBank3.Size = new System.Drawing.Size(200, 30);
            this.cmbBank3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbBank3.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblIban3.AutoSize = true;
            this.lblIban3.Location = new System.Drawing.Point(controlX + 220, y + 10);
            this.lblIban3.Text = "IBAN 3:";
            this.lblIban3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtIban3.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtIban3.Size = new System.Drawing.Size(280, 30);
            this.txtIban3.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblContactPerson.Text = "Υπεύθυνος:";
            this.lblContactPerson.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtContactPerson.Location = new System.Drawing.Point(controlX, y);
            this.txtContactPerson.Size = new System.Drawing.Size(300, 30);
            this.txtContactPerson.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += rowHeight;

            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(labelX, y + 10);
            this.lblNotes.Text = "Σημειώσεις:";
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y);
            this.txtNotes.Size = new System.Drawing.Size(570, 80);
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 90;

            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "Ενεργό";
            this.chkIsActive.Checked = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.btnSave.Location = new System.Drawing.Point(350, y);
            this.btnSave.Size = new System.Drawing.Size(140, 40);
            this.btnSave.Text = "💾 Αποθήκευση";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(500, y);
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
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
                this.lblArea, this.txtArea,
                this.lblPostalCode, this.txtPostalCode,
                this.lblDoy, this.cmbDoy,
                this.lblPhone1, this.txtPhone1,
                this.lblPhone2, this.txtPhone2,
                this.lblPhone3, this.txtPhone3,
                this.lblEmail, this.txtEmail,
                this.lblBank, this.cmbBank,
                this.lblIban1, this.txtIban1,
                this.lblBank2, this.cmbBank2,
                this.lblIban2, this.txtIban2,
                this.lblBank3, this.cmbBank3,
                this.lblIban3, this.txtIban3,
                this.lblContactPerson, this.txtContactPerson,
                this.lblNotes, this.txtNotes,
                this.chkIsActive, this.btnSave, this.btnCancel
            });

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplierBranchEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "✏️ Επεξεργασία Υποκαταστήματος";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblBranchName, lblBranchCode, lblAddress, lblCity, lblArea, lblPostalCode, lblDoy;
        private System.Windows.Forms.Label lblPhone1, lblPhone2, lblPhone3, lblEmail;
        private System.Windows.Forms.Label lblBank, lblBank2, lblBank3, lblIban1, lblIban2, lblIban3;
        private System.Windows.Forms.Label lblContactPerson, lblNotes;
        private System.Windows.Forms.TextBox txtBranchName, txtBranchCode, txtAddress, txtCity, txtArea, txtPostalCode;
        private System.Windows.Forms.TextBox txtPhone1, txtPhone2, txtPhone3, txtEmail;
        private System.Windows.Forms.TextBox txtIban1, txtIban2, txtIban3, txtContactPerson, txtNotes;
        private System.Windows.Forms.ComboBox cmbDoy, cmbBank, cmbBank2, cmbBank3;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}