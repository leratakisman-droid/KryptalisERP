namespace KryptalisERP
{
    partial class EmployeeEditForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAFM = new System.Windows.Forms.Label();
            this.txtAFM = new System.Windows.Forms.TextBox();
            this.lblCommission = new System.Windows.Forms.Label();
            this.numCommission = new System.Windows.Forms.NumericUpDown();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.lblBank = new System.Windows.Forms.Label();
            this.txtBankAccount = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numCommission)).BeginInit();
            this.SuspendLayout();

            // ═══════════════════════════════════════════════════════════
            // ΧΡΩΜΑΤΑ THEME
            // ═══════════════════════════════════════════════════════════
            System.Drawing.Color formBg = System.Drawing.Color.FromArgb(241, 245, 249);
            System.Drawing.Color titleColor = System.Drawing.Color.FromArgb(212, 175, 55);
            System.Drawing.Color labelColor = System.Drawing.Color.FromArgb(44, 62, 80);
            System.Drawing.Color inputBg = System.Drawing.Color.White;

            // === TITLE ===
            this.lblTitle.Text = "👤 Στοιχεία Πωλητή / Εισπράκτορα";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = titleColor;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.AutoSize = true;

            // ══════════════════════════════════════════════════════════
            // ΣΤΑΘΕΡΕΣ ΣΤΗΛΕΣ
            // ═══════════════════════════════════════════════════════════
            int lblX1 = 20, inpX1 = 170;   // Στήλη 1
            int lblX2 = 340, inpX2 = 440;  // Στήλη 2

            // === ROW 1: Κωδικός, Ρόλος (y=65) ===
            int y = 65;
            this.lblCode.Text = "Κωδικός:";
            this.lblCode.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = labelColor;

            this.txtCode.Location = new System.Drawing.Point(inpX1, y);
            this.txtCode.Size = new System.Drawing.Size(140, 25);
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.BackColor = inputBg;

            this.lblRole.Text = "Ρόλος:";
            this.lblRole.Location = new System.Drawing.Point(lblX2, y + 3);
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = labelColor;

            this.cmbRole.Location = new System.Drawing.Point(inpX2, y);
            this.cmbRole.Size = new System.Drawing.Size(180, 25);
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRole.BackColor = inputBg;
            this.cmbRole.Items.AddRange(new object[] { "Πωλητής", "Εισπράκτορας", "Και τα δύο" });
            this.cmbRole.SelectedIndex = 0;

            // === ROW 2: Ονοματεπώνυμο (y=105) ===
            y += 40;
            this.lblFullName.Text = "Ονοματεπώνυμο:";
            this.lblFullName.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = labelColor;

            this.txtFullName.Location = new System.Drawing.Point(inpX1, y);
            this.txtFullName.Size = new System.Drawing.Size(450, 25);
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.BackColor = inputBg;

            // === ROW 3: Τηλέφωνο, Κινητό (y=145) ===
            y += 40;
            this.lblPhone.Text = "Τηλέφωνο:";
            this.lblPhone.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = labelColor;

            this.txtPhone.Location = new System.Drawing.Point(inpX1, y);
            this.txtPhone.Size = new System.Drawing.Size(140, 25);
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.BackColor = inputBg;

            this.lblMobile.Text = "Κινητό:";
            this.lblMobile.Location = new System.Drawing.Point(lblX2, y + 3);
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMobile.ForeColor = labelColor;

            this.txtMobile.Location = new System.Drawing.Point(inpX2, y);
            this.txtMobile.Size = new System.Drawing.Size(180, 25);
            this.txtMobile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMobile.BackColor = inputBg;

            // === ROW 4: Email (y=185) ===
            y += 40;
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = labelColor;

            this.txtEmail.Location = new System.Drawing.Point(inpX1, y);
            this.txtEmail.Size = new System.Drawing.Size(450, 25);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.BackColor = inputBg;

            // === ROW 5: ΑΦΜ, Προμήθεια % (y=225) ===
            y += 40;
            this.lblAFM.Text = "ΑΦΜ:";
            this.lblAFM.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblAFM.AutoSize = true;
            this.lblAFM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAFM.ForeColor = labelColor;

            this.txtAFM.Location = new System.Drawing.Point(inpX1, y);
            this.txtAFM.Size = new System.Drawing.Size(140, 25);
            this.txtAFM.MaxLength = 9;
            this.txtAFM.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAFM.BackColor = inputBg;

            this.lblCommission.Text = "Προμήθεια %:";
            this.lblCommission.Location = new System.Drawing.Point(lblX2, y + 3);
            this.lblCommission.AutoSize = true;
            this.lblCommission.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCommission.ForeColor = labelColor;

            this.numCommission.Location = new System.Drawing.Point(inpX2, y);
            this.numCommission.Size = new System.Drawing.Size(100, 25);
            this.numCommission.DecimalPlaces = 2;
            this.numCommission.Maximum = 100;
            this.numCommission.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCommission.BackColor = inputBg;

            // === ROW 6: Περιοχή (y=265) ===
            y += 40;
            this.lblRegion.Text = "Περιοχή:";
            this.lblRegion.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblRegion.AutoSize = true;
            this.lblRegion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRegion.ForeColor = labelColor;

            this.txtRegion.Location = new System.Drawing.Point(inpX1, y);
            this.txtRegion.Size = new System.Drawing.Size(450, 25);
            this.txtRegion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRegion.BackColor = inputBg;

            // === ROW 7: Τραπεζικός Λογαριασμός (y=305) ===
            y += 40;
            this.lblBank.Text = "Τρ. Λογαριασμός:";
            this.lblBank.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblBank.AutoSize = true;
            this.lblBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBank.ForeColor = labelColor;

            this.txtBankAccount.Location = new System.Drawing.Point(inpX1, y);
            this.txtBankAccount.Size = new System.Drawing.Size(450, 25);
            this.txtBankAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBankAccount.BackColor = inputBg;

            // === ROW 8: Σημειώσεις (y=345) ===
            y += 40;
            this.lblNotes.Text = "Σημειώσεις:";
            this.lblNotes.Location = new System.Drawing.Point(lblX1, y + 3);
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = labelColor;

            this.txtNotes.Location = new System.Drawing.Point(inpX1, y);
            this.txtNotes.Size = new System.Drawing.Size(450, 80);
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.BackColor = inputBg;

            // === ROW 9: Ενεργός + Κουμπιά (y=440) ===
            y += 95;
            this.chkActive.Text = "✅ Ενεργός";
            this.chkActive.Location = new System.Drawing.Point(lblX1, y + 10);
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkActive.ForeColor = labelColor;

            this.btnSave.Text = "💾 Αποθήκευση";
            this.btnSave.Location = new System.Drawing.Point(340, y);
            this.btnSave.Size = new System.Drawing.Size(140, 40);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // ✅ ΕΔΩ ΕΙΝΑΙ Η ΔΙΟΡΘΩΣΗ ΓΙΑ ΤΟ ΔΙΠΛΟ ΚΛΙΚ
            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Location = new System.Drawing.Point(490, y);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;

            // ✅ Η ΜΑΓΙΚΗ ΓΡΑΜΜΗ: ΑΓΝΟΕΙ ΟΠΟΙΟΔΗΠΟΤΕ VALIDATION ΚΑΙ ΚΛΕΙΝΕΙ ΑΜΕΣΑ
            this.btnCancel.CausesValidation = false;

            // ✅ ΜΟΝΟ ΜΙΑ ΦΟΡΑ ΣΥΝΔΕΣΗ ΤΟΥ EVENT
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // === FORM ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = formBg;
            this.ClientSize = new System.Drawing.Size(650, y + 60);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCode); this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblRole); this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.lblFullName); this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblPhone); this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblMobile); this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.lblEmail); this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblAFM); this.Controls.Add(this.txtAFM);
            this.Controls.Add(this.lblCommission); this.Controls.Add(this.numCommission);
            this.Controls.Add(this.lblRegion); this.Controls.Add(this.txtRegion);
            this.Controls.Add(this.lblBank); this.Controls.Add(this.txtBankAccount);
            this.Controls.Add(this.lblNotes); this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Πωλητής / Εισπράκτορας";

            ((System.ComponentModel.ISupportInitialize)(this.numCommission)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode, lblFullName, lblRole, lblPhone, lblMobile;
        private System.Windows.Forms.Label lblEmail, lblAFM, lblCommission, lblRegion, lblBank, lblNotes;
        private System.Windows.Forms.TextBox txtCode, txtFullName, txtPhone, txtMobile;
        private System.Windows.Forms.TextBox txtEmail, txtAFM, txtRegion, txtBankAccount, txtNotes;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.NumericUpDown numCommission;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}