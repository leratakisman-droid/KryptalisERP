namespace KryptalisERP
{
    partial class SupplierBankAccountEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblBank = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.lblIBAN = new System.Windows.Forms.Label();
            this.txtIBAN = new System.Windows.Forms.TextBox();
            this.lblSWIFT = new System.Windows.Forms.Label();
            this.txtSWIFT = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.chkIsDefault = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(12, 15);
            this.lblBank.Text = "Τράπεζα:";

            this.cmbBank.Location = new System.Drawing.Point(150, 12);
            this.cmbBank.Size = new System.Drawing.Size(180, 23);
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;

            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Location = new System.Drawing.Point(12, 45);
            this.lblAccountNumber.Text = "Αριθμός Λογαριασμού:";

            this.txtAccountNumber.Location = new System.Drawing.Point(150, 42);
            this.txtAccountNumber.Size = new System.Drawing.Size(180, 23);

            this.lblIBAN.AutoSize = true;
            this.lblIBAN.Location = new System.Drawing.Point(12, 75);
            this.lblIBAN.Text = "IBAN:";

            this.txtIBAN.Location = new System.Drawing.Point(150, 72);
            this.txtIBAN.Size = new System.Drawing.Size(180, 23);

            this.lblSWIFT.AutoSize = true;
            this.lblSWIFT.Location = new System.Drawing.Point(12, 105);
            this.lblSWIFT.Text = "SWIFT / BIC:";

            this.txtSWIFT.Location = new System.Drawing.Point(150, 102);
            this.txtSWIFT.Size = new System.Drawing.Size(180, 23);

            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(12, 135);
            this.lblCurrency.Text = "Νόμισμα:";

            this.txtCurrency.Location = new System.Drawing.Point(150, 132);
            this.txtCurrency.Size = new System.Drawing.Size(80, 23);
            this.txtCurrency.Text = "EUR";

            this.chkIsDefault.AutoSize = true;
            this.chkIsDefault.Location = new System.Drawing.Point(150, 165);
            this.chkIsDefault.Text = "Προεπιλεγμένος";

            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(12, 195);
            this.lblNotes.Text = "Σημειώσεις:";

            this.txtNotes.Location = new System.Drawing.Point(150, 192);
            this.txtNotes.Multiline = true;
            this.txtNotes.Size = new System.Drawing.Size(180, 60);

            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.Location = new System.Drawing.Point(150, 265);
            this.chkIsActive.Text = "Ενεργός";

            this.btnSave.Location = new System.Drawing.Point(150, 300);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Αποθήκευση";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(250, 300);
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.Text = "Ακύρωση";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 350);
            this.Controls.Add(this.lblBank);
            this.Controls.Add(this.cmbBank);
            this.Controls.Add(this.lblAccountNumber);
            this.Controls.Add(this.txtAccountNumber);
            this.Controls.Add(this.lblIBAN);
            this.Controls.Add(this.txtIBAN);
            this.Controls.Add(this.lblSWIFT);
            this.Controls.Add(this.txtSWIFT);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.chkIsDefault);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupplierBankAccountEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Επεξεργασία Τραπεζικού Λογαριασμού";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label lblIBAN;
        private System.Windows.Forms.TextBox txtIBAN;
        private System.Windows.Forms.Label lblSWIFT;
        private System.Windows.Forms.TextBox txtSWIFT;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.TextBox txtCurrency;
        private System.Windows.Forms.CheckBox chkIsDefault;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}