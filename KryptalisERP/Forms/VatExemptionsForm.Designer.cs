namespace KryptalisERP
{
    partial class VatExemptionsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpFields = new System.Windows.Forms.GroupBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dgvExemptions = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.grpFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemptions)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55); this.lblTitle.Location = new System.Drawing.Point(20, 15); this.lblTitle.Name = "lblTitle"; this.lblTitle.Text = "⚖️ Άρθρα Απαλλαγής ΦΠΑ";

            this.grpFields.Text = " Στοιχεία Άρθρου"; this.grpFields.Location = new System.Drawing.Point(20, 50); this.grpFields.Size = new System.Drawing.Size(600, 110);
            this.grpFields.Controls.Add(this.lblCode); this.grpFields.Controls.Add(this.txtCode);
            this.grpFields.Controls.Add(this.lblName); this.grpFields.Controls.Add(this.txtName);
            this.grpFields.Controls.Add(this.lblDescription); this.grpFields.Controls.Add(this.txtDescription);

            this.lblCode.AutoSize = true; this.lblCode.Location = new System.Drawing.Point(15, 25); this.lblCode.Text = "Κωδικός:";
            this.txtCode.Location = new System.Drawing.Point(80, 22); this.txtCode.Size = new System.Drawing.Size(100, 23);

            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(200, 25); this.lblName.Text = "Άρθρο:";
            this.txtName.Location = new System.Drawing.Point(270, 22); this.txtName.Size = new System.Drawing.Size(200, 23);

            this.lblDescription.AutoSize = true; this.lblDescription.Location = new System.Drawing.Point(15, 60); this.lblDescription.Text = "Περιγραφή:";
            this.txtDescription.Location = new System.Drawing.Point(100, 57); this.txtDescription.Size = new System.Drawing.Size(370, 23);

            this.dgvExemptions.Location = new System.Drawing.Point(20, 170); this.dgvExemptions.Size = new System.Drawing.Size(600, 250);
            this.dgvExemptions.AllowUserToAddRows = false; this.dgvExemptions.AllowUserToDeleteRows = false; this.dgvExemptions.ReadOnly = true;
            this.dgvExemptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExemptions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExemptions_CellClick);

            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 430); this.btnAdd.Size = new System.Drawing.Size(100, 35); this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(130, 430); this.btnEdit.Size = new System.Drawing.Size(100, 35); this.btnEdit.Text = "✏️ Επεξεργασία"; this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(240, 430); this.btnDelete.Size = new System.Drawing.Size(100, 35); this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.BackColor = System.Drawing.Color.Gray; this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(520, 430); this.btnClose.Size = new System.Drawing.Size(100, 35); this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.grpFields); this.Controls.Add(this.dgvExemptions);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnEdit); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable; this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "VatExemptionsForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Άρθρα Απαλλαγής ΦΠΑ";

            this.grpFields.ResumeLayout(false); this.grpFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemptions)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpFields;
        private System.Windows.Forms.Label lblCode; private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblName; private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription; private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DataGridView dgvExemptions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
    }
}