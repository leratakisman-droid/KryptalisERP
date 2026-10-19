namespace KryptalisERP
{
    partial class ItemSerialNumbersForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvSerials = new System.Windows.Forms.DataGridView();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.cmbLotId = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.dtpWarrantyStart = new System.Windows.Forms.DateTimePicker();
            this.dtpWarrantyEnd = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblLot = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblWarrantyStart = new System.Windows.Forms.Label();
            this.lblWarrantyEnd = new System.Windows.Forms.Label();

            // ✅ ΝΕΑ CONTROLS ΓΙΑ ΑΝΑΖΗΤΗΣΗ & ΠΛΟΗΓΗΣΗ
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblRecordInfo = new System.Windows.Forms.Label();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSerials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30);
            this.lblTitle.Text = " Σειριακοί Αριθμοί";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            // Row 1: Σειριακός Αριθμός
            this.lblSerial.AutoSize = true;
            this.lblSerial.Location = new System.Drawing.Point(20, 60);
            this.lblSerial.Text = "Σειριακός Αριθμός*:";
            this.lblSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.txtSerialNumber.Location = new System.Drawing.Point(20, 82);
            this.txtSerialNumber.Size = new System.Drawing.Size(160, 28);
            this.txtSerialNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSerialNumber.PlaceholderText = "π.χ. SN-12345";

            // Row 1: Παρτίδα
            this.lblLot.AutoSize = true;
            this.lblLot.Location = new System.Drawing.Point(200, 60);
            this.lblLot.Text = "Παρτίδα:";
            this.lblLot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.cmbLotId.Location = new System.Drawing.Point(200, 82);
            this.cmbLotId.Size = new System.Drawing.Size(200, 28);
            this.cmbLotId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLotId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLotId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Row 1: Κατάσταση
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(420, 60);
            this.lblStatus.Text = "Κατάσταση:";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.cmbStatus.Location = new System.Drawing.Point(420, 82);
            this.cmbStatus.Size = new System.Drawing.Size(140, 28);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Row 1: Σημειώσεις
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(580, 60);
            this.lblNotes.Text = "Σημειώσεις:";
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.txtNotes.Location = new System.Drawing.Point(580, 82);
            this.txtNotes.Size = new System.Drawing.Size(300, 28);
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.PlaceholderText = "Προαιρετικές σημειώσεις...";

            // Row 2: Εγγύηση Από
            this.lblWarrantyStart.AutoSize = true;
            this.lblWarrantyStart.Location = new System.Drawing.Point(20, 120);
            this.lblWarrantyStart.Text = " Εγγύηση Από:";
            this.lblWarrantyStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dtpWarrantyStart.Location = new System.Drawing.Point(20, 142);
            this.dtpWarrantyStart.Size = new System.Drawing.Size(160, 28);
            this.dtpWarrantyStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpWarrantyStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWarrantyStart.CustomFormat = "dd/MM/yyyy";
            this.dtpWarrantyStart.ShowCheckBox = true;
            this.dtpWarrantyStart.Checked = false;

            // Row 2: Εγγύηση Έως
            this.lblWarrantyEnd.AutoSize = true;
            this.lblWarrantyEnd.Location = new System.Drawing.Point(200, 120);
            this.lblWarrantyEnd.Text = " Εγγύηση Έως:";
            this.lblWarrantyEnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.dtpWarrantyEnd.Location = new System.Drawing.Point(200, 142);
            this.dtpWarrantyEnd.Size = new System.Drawing.Size(160, 28);
            this.dtpWarrantyEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpWarrantyEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWarrantyEnd.CustomFormat = "dd/MM/yyyy";
            this.dtpWarrantyEnd.ShowCheckBox = true;
            this.dtpWarrantyEnd.Checked = false;

            // ==========================================
            // ΚΟΥΜΠΙΑ ΛΕΙΤΟΥΡΓΙΩΝ
            // ==========================================
            this.btnAdd.Text = " Προσθήκη";
            this.btnAdd.Size = new System.Drawing.Size(140, 35);
            this.btnAdd.Location = new System.Drawing.Point(20, 185);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "✏️ Επεξεργασία";
            this.btnEdit.Size = new System.Drawing.Size(150, 35);
            this.btnEdit.Location = new System.Drawing.Point(170, 185);
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή";
            this.btnDelete.Size = new System.Drawing.Size(130, 35);
            this.btnDelete.Location = new System.Drawing.Point(330, 185);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Size = new System.Drawing.Size(130, 35);
            this.btnCancel.Location = new System.Drawing.Point(470, 185);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnClose.Text = "✖ Κλείσιμο";
            this.btnClose.Size = new System.Drawing.Size(130, 35);
            this.btnClose.Location = new System.Drawing.Point(750, 185);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // ==========================================
            // ✅ ΑΝΑΖΗΤΗΣΗ (ΝΕΑ ΓΡΑΜΜΗ)
            // ==========================================
            var lblSearch = new System.Windows.Forms.Label();
            lblSearch.AutoSize = true;
            lblSearch.Location = new System.Drawing.Point(20, 230);
            lblSearch.Text = "🔍 Αναζήτηση:";
            lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.txtSearch.Location = new System.Drawing.Point(130, 227);
            this.txtSearch.Size = new System.Drawing.Size(300, 28);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.PlaceholderText = "Αναζήτηση σειριακού, παρτίδας...";
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);

            this.btnSearch.Text = "Εύρεση";
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.Location = new System.Drawing.Point(440, 227);
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // ==========================================
            // ✅ ΠΛΟΗΓΗΣΗ (ΝΕΑ ΓΡΑΜΜΗ)
            // ==========================================
            var lblNav = new System.Windows.Forms.Label();
            lblNav.AutoSize = true;
            lblNav.Location = new System.Drawing.Point(20, 265);
            lblNav.Text = "Πλοήγηση:";
            lblNav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.btnFirst.Text = "<<";
            this.btnFirst.Size = new System.Drawing.Size(50, 28);
            this.btnFirst.Location = new System.Drawing.Point(100, 262);
            this.btnFirst.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnFirst.ForeColor = System.Drawing.Color.White;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);

            this.btnPrevious.Text = "<";
            this.btnPrevious.Size = new System.Drawing.Size(50, 28);
            this.btnPrevious.Location = new System.Drawing.Point(160, 262);
            this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);

            this.btnNext.Text = ">";
            this.btnNext.Size = new System.Drawing.Size(50, 28);
            this.btnNext.Location = new System.Drawing.Point(220, 262);
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            this.btnLast.Text = ">>";
            this.btnLast.Size = new System.Drawing.Size(50, 28);
            this.btnLast.Location = new System.Drawing.Point(280, 262);
            this.btnLast.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnLast.ForeColor = System.Drawing.Color.White;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);

            this.lblRecordInfo.AutoSize = true;
            this.lblRecordInfo.Location = new System.Drawing.Point(350, 267);
            this.lblRecordInfo.Text = "0 / 0";
            this.lblRecordInfo.Font = new System.Drawing.Font("Segoe UI", 9F);

            // ==========================================
            // DataGridView (ΠΙΟ ΚΑΤΩ)
            // ==========================================
            this.dgvSerials.Location = new System.Drawing.Point(20, 305);
            this.dgvSerials.Size = new System.Drawing.Size(860, 210);
            this.dgvSerials.AllowUserToAddRows = false;
            this.dgvSerials.AllowUserToDeleteRows = false;
            this.dgvSerials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSerials.MultiSelect = false;
            this.dgvSerials.ReadOnly = true;
            this.dgvSerials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSerials.BackgroundColor = System.Drawing.Color.White;
            this.dgvSerials.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvSerials.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSerials.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvSerials.ColumnHeadersHeight = 40;
            this.dgvSerials.RowTemplate.Height = 35;
            this.dgvSerials.AutoGenerateColumns = false;
            this.dgvSerials.DataSource = this.bindingSource;
            this.dgvSerials.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSerials_CellDoubleClick);

            // Στήλες
            this.dgvSerials.Columns.Clear();
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σειριακός", DataPropertyName = "Σειριακός", HeaderText = "Σειριακός Αριθμός", Width = 150 });
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Παρτίδα", DataPropertyName = "Παρτίδα", HeaderText = "Παρτίδα", Width = 100 });
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Εγγύηση Από", DataPropertyName = "Εγγύηση Από", HeaderText = "📅 Εγγύηση Από", Width = 120 });
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Εγγύηση Έως", DataPropertyName = "Εγγύηση Έως", HeaderText = "📅 Εγγύηση Έως", Width = 120 });
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κατάσταση", DataPropertyName = "Κατάσταση", HeaderText = "Κατάσταση", Width = 120 });
            this.dgvSerials.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σημειώσεις", DataPropertyName = "Σημειώσεις", HeaderText = "Σημειώσεις", Width = 200 });

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.lblLot);
            this.Controls.Add(this.cmbLotId);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblWarrantyStart);
            this.Controls.Add(this.dtpWarrantyStart);
            this.Controls.Add(this.lblWarrantyEnd);
            this.Controls.Add(this.dtpWarrantyEnd);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(lblNav);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.lblRecordInfo);
            this.Controls.Add(this.dgvSerials);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemSerialNumbersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Σειριακοί Αριθμοί";

            ((System.ComponentModel.ISupportInitialize)(this.dgvSerials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvSerials;
        private System.Windows.Forms.TextBox txtSerialNumber, txtNotes, txtSearch;
        private System.Windows.Forms.ComboBox cmbLotId, cmbStatus;
        private System.Windows.Forms.DateTimePicker dtpWarrantyStart, dtpWarrantyEnd;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete, btnCancel, btnClose;
        private System.Windows.Forms.Button btnSearch, btnFirst, btnPrevious, btnNext, btnLast;
        private System.Windows.Forms.Label lblRecordInfo;
        private System.Windows.Forms.Label lblTitle, lblSerial, lblLot, lblStatus, lblNotes;
        private System.Windows.Forms.Label lblWarrantyStart, lblWarrantyEnd;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}