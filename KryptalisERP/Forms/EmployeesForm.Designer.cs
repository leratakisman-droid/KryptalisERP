namespace KryptalisERP
{
    partial class EmployeesForm
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
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkShowInactive = new System.Windows.Forms.CheckBox();
            this.dgvEmployees = StandardFormLayout.CreateStyledDataGridView();

            this.btnNew = StandardFormLayout.CreateButton("➕ Νέος", StandardFormLayout.BtnGreen);
            this.btnEdit = StandardFormLayout.CreateButton("✏️ Επεξεργασία", StandardFormLayout.BtnBlue);
            this.btnDeactivate = StandardFormLayout.CreateButton("️ Απενεργοποίηση", StandardFormLayout.BtnRed);
            this.btnBack = StandardFormLayout.CreateButton("◀ Κλείσιμο", StandardFormLayout.BtnGold);

            this.SuspendLayout();

            // === HEADER ===
            this.pnlHeader = StandardFormLayout.CreateHeaderPanel(this.lblTitle, this.lblSubtitle);
            this.lblTitle.Text = "👔 ΠΩΛΗΤΕΣ / ΕΙΣΠΡΑΚΤΟΡΕΣ";
            this.lblSubtitle.Text = "Συνολικοί: 0 | Ενεργοί: 0 | Ανενεργοί: 0";

            // === SEARCH ===
            this.pnlSearch = StandardFormLayout.CreateSearchPanel(this.txtSearch, this.btnSearch, this.chkShowInactive);
            this.txtSearch.Text = ""; // ✅ ΚΕΝΟ - χωρίς placeholder
            this.chkShowInactive.Text = "👁️ Εμφάνιση ανενεργών";

            // === BUTTONS ===
            this.pnlButtons = StandardFormLayout.CreateButtonsPanel(
                this.btnNew, this.btnEdit, this.btnDeactivate, this.btnBack);

            // ✅ Η ΜΑΓΙΚΗ ΓΡΑΜΜΗ: ΤΟ ΚΟΥΜΠΙ ΚΛΕΙΣΙΜΟΥ ΑΓΝΟΕΙ VALIDATION
            this.btnBack.CausesValidation = false;

            // === FORM ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1200, 700);

            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "EmployeesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KRYPTALIS ERP - Πωλητές & Εισπράκτορες";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkShowInactive;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnBack;
    }
}