namespace KryptalisERP
{
    partial class TaxWithholdingEditForm
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
            this.lblSector = new System.Windows.Forms.Label();
            this.cmbSector = new System.Windows.Forms.ComboBox();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.cmbSubCategory = new System.Windows.Forms.ComboBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.numRate = new System.Windows.Forms.NumericUpDown();
            this.lblMyData = new System.Windows.Forms.Label();
            this.txtMyData = new System.Windows.Forms.TextBox();
            this.chkAppliesSales = new System.Windows.Forms.CheckBox();
            this.chkAppliesPurchases = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numRate)).BeginInit();
            this.SuspendLayout();

            // Τομέας
            this.lblSector.AutoSize = true;
            this.lblSector.Location = new System.Drawing.Point(20, 20);
            this.lblSector.Text = "Τομέας:";
            this.cmbSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSector.Location = new System.Drawing.Point(180, 17);
            this.cmbSector.Size = new System.Drawing.Size(300, 23);
            this.cmbSector.SelectedIndexChanged += new System.EventHandler(this.cmbSector_SelectedIndexChanged);

            // Κατηγορία
            this.lblSubCategory.AutoSize = true;
            this.lblSubCategory.Location = new System.Drawing.Point(20, 55);
            this.lblSubCategory.Text = "Κατηγορία:";
            this.cmbSubCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCategory.Location = new System.Drawing.Point(180, 52);
            this.cmbSubCategory.Size = new System.Drawing.Size(300, 23);
            this.cmbSubCategory.SelectedIndexChanged += new System.EventHandler(this.cmbSubCategory_SelectedIndexChanged);

            // Κωδικός
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(20, 90);
            this.lblCode.Text = "Κωδικός:";
            this.txtCode.Location = new System.Drawing.Point(180, 87);
            this.txtCode.Size = new System.Drawing.Size(200, 23);

            // Όνομα
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 125);
            this.lblName.Text = "Όνομα:";
            this.txtName.Location = new System.Drawing.Point(180, 122);
            this.txtName.Size = new System.Drawing.Size(300, 23);

            // Ποσοστό
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(20, 160);
            this.lblRate.Text = "Ποσοστό %:";
            this.numRate.Location = new System.Drawing.Point(180, 157);
            this.numRate.Size = new System.Drawing.Size(100, 23);
            this.numRate.DecimalPlaces = 2;
            this.numRate.Maximum = 100;
            this.numRate.Value = 0;

            // Κωδ. myDATA
            this.lblMyData.AutoSize = true;
            this.lblMyData.Location = new System.Drawing.Point(20, 195);
            this.lblMyData.Text = "Κωδ. myDATA:";
            this.txtMyData.Location = new System.Drawing.Point(180, 192);
            this.txtMyData.Size = new System.Drawing.Size(200, 23);
            this.txtMyData.ReadOnly = true;
            this.txtMyData.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Checkboxes
            this.chkAppliesSales.AutoSize = true;
            this.chkAppliesSales.Location = new System.Drawing.Point(180, 230);
            this.chkAppliesSales.Name = "chkAppliesSales";
            this.chkAppliesSales.Text = "Εφαρμόζεται σε Πωλήσεις";

            this.chkAppliesPurchases.AutoSize = true;
            this.chkAppliesPurchases.Location = new System.Drawing.Point(180, 255);
            this.chkAppliesPurchases.Name = "chkAppliesPurchases";
            this.chkAppliesPurchases.Text = "Εφαρμόζεται σε Αγορές";

            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.Location = new System.Drawing.Point(180, 280);
            this.chkActive.Name = "chkActive";
            this.chkActive.Text = "Ενεργή";

            // Buttons
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(200, 325);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "💾 Αποθήκευση";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(330, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 380);
            this.Controls.Add(this.lblSector); this.Controls.Add(this.cmbSector);
            this.Controls.Add(this.lblSubCategory); this.Controls.Add(this.cmbSubCategory);
            this.Controls.Add(this.lblCode); this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblRate); this.Controls.Add(this.numRate);
            this.Controls.Add(this.lblMyData); this.Controls.Add(this.txtMyData);
            this.Controls.Add(this.chkAppliesSales);
            this.Controls.Add(this.chkAppliesPurchases);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "TaxWithholdingEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Κράτηση/Φόρος/Επιβάρυνση";

            ((System.ComponentModel.ISupportInitialize)(this.numRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSector, lblSubCategory, lblCode, lblName, lblRate, lblMyData;
        private System.Windows.Forms.ComboBox cmbSector, cmbSubCategory;
        private System.Windows.Forms.TextBox txtCode, txtName, txtMyData;
        private System.Windows.Forms.NumericUpDown numRate;
        private System.Windows.Forms.CheckBox chkAppliesSales, chkAppliesPurchases, chkActive;
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}