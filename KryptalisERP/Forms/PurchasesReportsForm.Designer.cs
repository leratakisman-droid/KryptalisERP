namespace KryptalisERP
{
    partial class PurchasesReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnPurchasesByDate = new System.Windows.Forms.Button();
            this.btnPurchasesBySupplier = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "📊 Αναφορές Αγορών";

            this.lblFrom.AutoSize = true; this.lblFrom.Location = new System.Drawing.Point(20, 70); this.lblFrom.Text = "Από:";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(60, 67); this.dtpFrom.Name = "dtpFrom"; this.dtpFrom.Size = new System.Drawing.Size(120, 23);
            this.dtpFrom.Value = DateTime.Now.AddMonths(-1);

            this.lblTo.AutoSize = true; this.lblTo.Location = new System.Drawing.Point(200, 70); this.lblTo.Text = "Έως:";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(240, 67); this.dtpTo.Name = "dtpTo"; this.dtpTo.Size = new System.Drawing.Size(120, 23);
            this.dtpTo.Value = DateTime.Now;

            this.btnPurchasesByDate.BackColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.btnPurchasesByDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchasesByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPurchasesByDate.ForeColor = System.Drawing.Color.Black;
            this.btnPurchasesByDate.Location = new System.Drawing.Point(380, 65);
            this.btnPurchasesByDate.Name = "btnPurchasesByDate";
            this.btnPurchasesByDate.Size = new System.Drawing.Size(150, 27);
            this.btnPurchasesByDate.Text = "📅 Ανά Ημερομηνία";
            this.btnPurchasesByDate.Click += new System.EventHandler(this.btnPurchasesByDate_Click);

            this.btnPurchasesBySupplier.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnPurchasesBySupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchasesBySupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPurchasesBySupplier.ForeColor = System.Drawing.Color.White;
            this.btnPurchasesBySupplier.Location = new System.Drawing.Point(540, 65);
            this.btnPurchasesBySupplier.Name = "btnPurchasesBySupplier";
            this.btnPurchasesBySupplier.Size = new System.Drawing.Size(150, 27);
            this.btnPurchasesBySupplier.Text = "🏭 Ανά Προμηθευτή";
            this.btnPurchasesBySupplier.Click += new System.EventHandler(this.btnPurchasesBySupplier_Click);

            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(33, 150, 83);
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportCsv.ForeColor = System.Drawing.Color.White;
            this.btnExportCsv.Location = new System.Drawing.Point(700, 65);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(80, 27);
            this.btnExportCsv.Text = "📊 CSV";
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);

            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(790, 65);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 27);
            this.btnPrint.Text = "🖨️ Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(880, 65);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 27);
            this.btnClose.Text = "◀ Κλείσιμο";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.Location = new System.Drawing.Point(20, 110);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(940, 450);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(980, 580);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFrom); this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblTo); this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.btnPurchasesByDate);
            this.Controls.Add(this.btnPurchasesBySupplier);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PurchasesReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Αναφορές Αγορών";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnPurchasesByDate;
        private System.Windows.Forms.Button btnPurchasesBySupplier;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
    }
}