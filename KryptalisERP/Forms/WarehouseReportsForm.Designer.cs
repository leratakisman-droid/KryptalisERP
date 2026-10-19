namespace KryptalisERP
{
    partial class WarehouseReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpFilters = new System.Windows.Forms.GroupBox();
            this.lblReportType = new System.Windows.Forms.Label();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblItemType = new System.Windows.Forms.Label();
            this.cmbItemType = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.grpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "📊 Αναφορές Αποθήκης";

            this.grpFilters.Text = "  Φίλτρα Αναφοράς";
            this.grpFilters.Location = new System.Drawing.Point(20, 50);
            this.grpFilters.Size = new System.Drawing.Size(1100, 100);

            this.lblReportType.AutoSize = true; this.lblReportType.Location = new System.Drawing.Point(15, 25); this.lblReportType.Name = "lblReportType"; this.lblReportType.Text = "Τύπος:";
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbReportType.Location = new System.Drawing.Point(70, 22); this.cmbReportType.Name = "cmbReportType"; this.cmbReportType.Size = new System.Drawing.Size(200, 23);

            this.lblWarehouse.AutoSize = true; this.lblWarehouse.Location = new System.Drawing.Point(290, 25); this.lblWarehouse.Name = "lblWarehouse"; this.lblWarehouse.Text = "Αποθήκη:";
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbWarehouse.Location = new System.Drawing.Point(360, 22); this.cmbWarehouse.Name = "cmbWarehouse"; this.cmbWarehouse.Size = new System.Drawing.Size(180, 23);

            this.lblCategory.AutoSize = true; this.lblCategory.Location = new System.Drawing.Point(560, 25); this.lblCategory.Name = "lblCategory"; this.lblCategory.Text = "Κατηγορία:";
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbCategory.Location = new System.Drawing.Point(640, 22); this.cmbCategory.Name = "cmbCategory"; this.cmbCategory.Size = new System.Drawing.Size(150, 23);

            this.lblSupplier.AutoSize = true; this.lblSupplier.Location = new System.Drawing.Point(810, 25); this.lblSupplier.Name = "lblSupplier"; this.lblSupplier.Text = "Προμηθευτής:";
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbSupplier.Location = new System.Drawing.Point(910, 22); this.cmbSupplier.Name = "cmbSupplier"; this.cmbSupplier.Size = new System.Drawing.Size(170, 23);

            this.lblItemType.AutoSize = true; this.lblItemType.Location = new System.Drawing.Point(15, 60); this.lblItemType.Name = "lblItemType"; this.lblItemType.Text = "Τύπος:";
            this.cmbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbItemType.Location = new System.Drawing.Point(70, 57); this.cmbItemType.Name = "cmbItemType"; this.cmbItemType.Size = new System.Drawing.Size(120, 23);

            this.lblFrom.AutoSize = true; this.lblFrom.Location = new System.Drawing.Point(210, 60); this.lblFrom.Name = "lblFrom"; this.lblFrom.Text = "Από:";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpFrom.Location = new System.Drawing.Point(250, 57); this.dtpFrom.Name = "dtpFrom"; this.dtpFrom.Size = new System.Drawing.Size(110, 23);

            this.lblTo.AutoSize = true; this.lblTo.Location = new System.Drawing.Point(380, 60); this.lblTo.Name = "lblTo"; this.lblTo.Text = "Έως:";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpTo.Location = new System.Drawing.Point(420, 57); this.dtpTo.Name = "dtpTo"; this.dtpTo.Size = new System.Drawing.Size(110, 23);

            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold); this.btnGenerate.ForeColor = System.Drawing.Color.White; this.btnGenerate.Location = new System.Drawing.Point(550, 50); this.btnGenerate.Name = "btnGenerate"; this.btnGenerate.Size = new System.Drawing.Size(150, 35); this.btnGenerate.Text = "🔍 Δημιουργία"; this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            // ✅ ΕΔΩ ΠΡΟΣΤΙΘΕΝΤΑΙ ΟΛΑ ΣΤΟ GROUPBOX
            this.grpFilters.Controls.Add(this.lblReportType);
            this.grpFilters.Controls.Add(this.cmbReportType);
            this.grpFilters.Controls.Add(this.lblWarehouse);
            this.grpFilters.Controls.Add(this.cmbWarehouse);
            this.grpFilters.Controls.Add(this.lblCategory);
            this.grpFilters.Controls.Add(this.cmbCategory);
            this.grpFilters.Controls.Add(this.lblSupplier);
            this.grpFilters.Controls.Add(this.cmbSupplier);
            this.grpFilters.Controls.Add(this.lblItemType);
            this.grpFilters.Controls.Add(this.cmbItemType);
            this.grpFilters.Controls.Add(this.lblFrom);
            this.grpFilters.Controls.Add(this.dtpFrom);
            this.grpFilters.Controls.Add(this.lblTo);
            this.grpFilters.Controls.Add(this.dtpTo);
            this.grpFilters.Controls.Add(this.btnGenerate);

            this.dgvReport.Location = new System.Drawing.Point(20, 160); this.dgvReport.Name = "dgvReport"; this.dgvReport.Size = new System.Drawing.Size(1100, 400);
            this.dgvReport.AllowUserToAddRows = false; this.dgvReport.AllowUserToDeleteRows = false; this.dgvReport.ReadOnly = true;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.lblTotalRecords.AutoSize = true; this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.lblTotalRecords.Location = new System.Drawing.Point(20, 575); this.lblTotalRecords.Name = "lblTotalRecords"; this.lblTotalRecords.Text = "Συνολικά: 0 εγγραφές";
            this.lblTotalValue.AutoSize = true; this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold); this.lblTotalValue.ForeColor = System.Drawing.Color.Green; this.lblTotalValue.Location = new System.Drawing.Point(300, 575); this.lblTotalValue.Name = "lblTotalValue"; this.lblTotalValue.Text = "Συνολική Αξία: 0,00 €";

            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnExportCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnExportCsv.ForeColor = System.Drawing.Color.White; this.btnExportCsv.Location = new System.Drawing.Point(600, 570); this.btnExportCsv.Name = "btnExportCsv"; this.btnExportCsv.Size = new System.Drawing.Size(130, 35); this.btnExportCsv.Text = "📊 Excel"; this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnPrint.ForeColor = System.Drawing.Color.White; this.btnPrint.Location = new System.Drawing.Point(740, 570); this.btnPrint.Name = "btnPrint"; this.btnPrint.Size = new System.Drawing.Size(130, 35); this.btnPrint.Text = "🖨️ Εκτύπωση"; this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnClose.ForeColor = System.Drawing.Color.White; this.btnClose.Location = new System.Drawing.Point(880, 570); this.btnClose.Name = "btnClose"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.White; this.ClientSize = new System.Drawing.Size(1140, 620);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.grpFilters); this.Controls.Add(this.dgvReport); this.Controls.Add(this.lblTotalRecords); this.Controls.Add(this.lblTotalValue); this.Controls.Add(this.btnExportCsv); this.Controls.Add(this.btnPrint); this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable; this.MaximizeBox = false; this.MinimizeBox = false; this.Name = "WarehouseReportsForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Αναφορές Αποθήκης";

            this.grpFilters.ResumeLayout(false); this.grpFilters.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFilters;
        private System.Windows.Forms.Label lblReportType; private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Label lblWarehouse; private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label lblCategory; private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblSupplier; private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblItemType; private System.Windows.Forms.ComboBox cmbItemType;
        private System.Windows.Forms.Label lblFrom; private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo; private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblTotalRecords; private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Button btnExportCsv; private System.Windows.Forms.Button btnPrint; private System.Windows.Forms.Button btnClose;
    }
}