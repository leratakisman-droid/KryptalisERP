namespace KryptalisERP
{
    partial class ChargesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCharges;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvCharges = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharges)).BeginInit();
            this.SuspendLayout();

            this.Text = "💰 Πρόσθετες Χρεώσεις / Κρατήσεις";
            this.Size = new System.Drawing.Size(600, 450);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.dgvCharges.Location = new System.Drawing.Point(20, 20);
            this.dgvCharges.Size = new System.Drawing.Size(540, 300);
            this.dgvCharges.AutoGenerateColumns = false;
            this.dgvCharges.AllowUserToAddRows = false;
            this.dgvCharges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCharges.BackgroundColor = System.Drawing.Color.White;

            this.dgvCharges.Columns.Add(new System.Windows.Forms.DataGridViewCheckBoxColumn { Name = "colSelect", HeaderText = "✓", Width = 40 });
            this.dgvCharges.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Περιγραφή", Width = 250 });
            this.dgvCharges.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colType", HeaderText = "Τύπος", Width = 120 });
            this.dgvCharges.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colRate", HeaderText = "Ποσοστό %", Width = 100 });

            this.btnSave.Text = "✅ Αποθήκευση";
            this.btnSave.Location = new System.Drawing.Point(150, 340);
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Location = new System.Drawing.Point(300, 340);
            this.btnCancel.Size = new System.Drawing.Size(140, 35);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(this.dgvCharges);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharges)).EndInit();
            this.ResumeLayout(false);
        }
    }
}