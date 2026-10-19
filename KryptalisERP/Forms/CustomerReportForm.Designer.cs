namespace KryptalisERP
{
    partial class ClientReportsForm
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
            this.grpFilters = new System.Windows.Forms.GroupBox();
            this.lblReportType = new System.Windows.Forms.Label();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();

            this.grpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Text = "📊 Αναφορές Πελατών";

            // grpFilters
            this.grpFilters.Controls.Add(this.lblReportType);
            this.grpFilters.Controls.Add(this.cmbReportType);
            this.grpFilters.Controls.Add(this.lblClient);
            this.grpFilters.Controls.Add(this.cmbClient);
            this.grpFilters.Controls.Add(this.lblBranch);
            this.grpFilters.Controls.Add(this.cmbBranch);
            this.grpFilters.Controls.Add(this.lblDateFrom);
            this.grpFilters.Controls.Add(this.dtpFrom);
            this.grpFilters.Controls.Add(this.lblDateTo);
            this.grpFilters.Controls.Add(this.dtpTo);
            this.grpFilters.Controls.Add(this.btnGenerate);
            this.grpFilters.Location = new System.Drawing.Point(20, 60);
            this.grpFilters.Name = "grpFilters";
            this.grpFilters.Size = new System.Drawing.Size(1100, 100);
            this.grpFilters.Text = "🔍 Φίλτρα Αναφοράς";

            // lblReportType
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(15, 25);
            this.lblReportType.Text = "Τύπος:";
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.Location = new System.Drawing.Point(70, 22);
            this.cmbReportType.Size = new System.Drawing.Size(200, 23);

            // lblClient
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(290, 25);
            this.lblClient.Text = "Πελάτης:";
            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.Location = new System.Drawing.Point(350, 22);
            this.cmbClient.Size = new System.Drawing.Size(200, 23);

            // lblBranch
            this.lblBranch.AutoSize = true;
            this.lblBranch.Location = new System.Drawing.Point(570, 25);
            this.lblBranch.Text = "Υποκατάστημα:";
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.Location = new System.Drawing.Point(680, 22);
            this.cmbBranch.Size = new System.Drawing.Size(200, 23);

            // lblDateFrom / dtpFrom
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(15, 60);
            this.lblDateFrom.Text = "Από:";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(50, 57);
            this.dtpFrom.Size = new System.Drawing.Size(100, 23);

            // lblDateTo / dtpTo
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(165, 60);
            this.lblDateTo.Text = "Έως:";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(200, 57);
            this.dtpTo.Size = new System.Drawing.Size(100, 23);

            // btnGenerate
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(320, 55);
            this.btnGenerate.Size = new System.Drawing.Size(180, 30);
            this.btnGenerate.Text = "🔍 Δημιουργία";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            // dgvReport
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.Location = new System.Drawing.Point(20, 175);
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1100, 350);

            // lblCount & lblTotalAmount
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.Gray;
            this.lblCount.Location = new System.Drawing.Point(20, 540);
            this.lblCount.Text = "Συνολικά: 0 εγγραφές";

            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblTotalAmount.Location = new System.Drawing.Point(200, 540);
            this.lblTotalAmount.Text = "Συνολικό Ποσό: 0,00 €";

            // Κουμπιά Ενέργειας (✅ ΔΙΟΡΘΩΜΕΝΗ ΣΤΟΙΧΙΣΗ ΓΙΑ ΝΑ ΧΩΡΑΝΕ ΟΛΑ)
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(500, 535);
            this.btnPrint.Size = new System.Drawing.Size(120, 30);
            this.btnPrint.Text = "🖨️ Εκτύπωση";
            this.btnPrint.Enabled = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            this.btnPrintPreview.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrintPreview.ForeColor = System.Drawing.Color.White;
            this.btnPrintPreview.Location = new System.Drawing.Point(630, 535);
            this.btnPrintPreview.Size = new System.Drawing.Size(130, 30);
            this.btnPrintPreview.Text = "👁️ Προεπισκόπηση";
            this.btnPrintPreview.Enabled = false;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);

            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(770, 535);
            this.btnExportExcel.Size = new System.Drawing.Size(110, 30);
            this.btnExportExcel.Text = "📥 Excel";
            this.btnExportExcel.Enabled = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnExportPdf.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportPdf.Location = new System.Drawing.Point(890, 535);
            this.btnExportPdf.Size = new System.Drawing.Size(110, 30);
            this.btnExportPdf.Text = "📄 PDF";
            this.btnExportPdf.Enabled = false;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);

            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1010, 535); // ✅ ΔΙΟΡΘΩΜΕΝΟ: Τώρα χωράει άνετα
            this.btnClose.Size = new System.Drawing.Size(110, 30);
            this.btnClose.Text = "❌ Κλείσιμο";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1140, 580);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpFilters);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Αναφορές Πελατών";

            this.grpFilters.ResumeLayout(false);
            this.grpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFilters;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnClose;
    }
}