namespace KryptalisERP
{
    partial class ReceiptsReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnByDate = new System.Windows.Forms.Button();
            this.btnByParty = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55); this.lblTitle.Location = new System.Drawing.Point(20, 20); this.lblTitle.Name = "lblTitle"; this.lblTitle.Text = "📊 Αναφορές Εισπράξεων/Πληρωμών";

            this.lblFrom.AutoSize = true; this.lblFrom.Location = new System.Drawing.Point(20, 70); this.lblFrom.Text = "Από:";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpFrom.Location = new System.Drawing.Point(60, 67); this.dtpFrom.Size = new System.Drawing.Size(120, 23);

            this.lblTo.AutoSize = true; this.lblTo.Location = new System.Drawing.Point(200, 70); this.lblTo.Text = "Έως:";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpTo.Location = new System.Drawing.Point(240, 67); this.dtpTo.Size = new System.Drawing.Size(120, 23);

            this.btnByDate.BackColor = System.Drawing.Color.FromArgb(212, 175, 55); this.btnByDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnByDate.ForeColor = System.Drawing.Color.Black;
            this.btnByDate.Location = new System.Drawing.Point(380, 65); this.btnByDate.Size = new System.Drawing.Size(150, 27); this.btnByDate.Text = "📅 Ανά Ημερομηνία"; this.btnByDate.Click += new System.EventHandler(this.btnReceiptsByDate_Click);

            this.btnByParty.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnByParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnByParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnByParty.ForeColor = System.Drawing.Color.White;
            this.btnByParty.Location = new System.Drawing.Point(540, 65); this.btnByParty.Size = new System.Drawing.Size(150, 27); this.btnByParty.Text = "👥 Ανά Μέρος"; this.btnByParty.Click += new System.EventHandler(this.btnByParty_Click);

            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(33, 150, 83); this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnExportCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnExportCsv.ForeColor = System.Drawing.Color.White;
            this.btnExportCsv.Location = new System.Drawing.Point(700, 65); this.btnExportCsv.Size = new System.Drawing.Size(80, 27); this.btnExportCsv.Text = "📊 CSV"; this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);

            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(155, 89, 182); this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(790, 65); this.btnPrint.Size = new System.Drawing.Size(80, 27); this.btnPrint.Text = "🖨️ Print"; this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            this.btnClose.BackColor = System.Drawing.Color.Gray; this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(880, 65); this.btnClose.Size = new System.Drawing.Size(80, 27); this.btnClose.Text = "◀ Κλείσιμο"; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvReport.AllowUserToAddRows = false; this.dgvReport.AllowUserToDeleteRows = false; this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White; this.dgvReport.Location = new System.Drawing.Point(20, 110); this.dgvReport.Name = "dgvReport"; this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false; this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvReport.Size = new System.Drawing.Size(940, 450);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.White; this.ClientSize = new System.Drawing.Size(980, 580);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.lblFrom); this.Controls.Add(this.dtpFrom); this.Controls.Add(this.lblTo); this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.btnByDate); this.Controls.Add(this.btnByParty); this.Controls.Add(this.btnExportCsv); this.Controls.Add(this.btnPrint); this.Controls.Add(this.btnClose); this.Controls.Add(this.dgvReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; this.MaximizeBox = false; this.Name = "ReceiptsReportsForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Αναφορές Ταμείου";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle, lblFrom, lblTo;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Button btnByDate, btnByParty, btnExportCsv, btnPrint, btnClose;
        private System.Windows.Forms.DataGridView dgvReport;
    }
}