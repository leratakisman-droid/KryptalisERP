namespace KryptalisERP
{
    partial class ItemRelatedForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvRelated = new System.Windows.Forms.DataGridView();
            this.txtRelatedItemId = new System.Windows.Forms.TextBox();
            this.txtRelatedName = new System.Windows.Forms.TextBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRelated)).BeginInit();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 30); this.lblTitle.Text = "🔗 Σχετικά Είδη";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.txtRelatedItemId.Location = new System.Drawing.Point(20, 60); this.txtRelatedItemId.Size = new System.Drawing.Size(80, 30); this.txtRelatedItemId.ReadOnly = true;
            this.txtRelatedName.Location = new System.Drawing.Point(110, 60); this.txtRelatedName.Size = new System.Drawing.Size(300, 30); this.txtRelatedName.ReadOnly = true;
            this.btnSearch.Text = "🔍"; this.btnSearch.Size = new System.Drawing.Size(40, 30); this.btnSearch.Location = new System.Drawing.Point(420, 60);
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.cmbType.Location = new System.Drawing.Point(480, 60); this.cmbType.Size = new System.Drawing.Size(150, 30); this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtNotes.Location = new System.Drawing.Point(20, 100); this.txtNotes.Size = new System.Drawing.Size(600, 30);

            this.btnAdd.Text = "➕ Προσθήκη"; this.btnAdd.Size = new System.Drawing.Size(140, 35); this.btnAdd.Location = new System.Drawing.Point(20, 140);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Text = "🗑️ Διαγραφή"; this.btnDelete.Size = new System.Drawing.Size(140, 35); this.btnDelete.Location = new System.Drawing.Point(170, 140);
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Size = new System.Drawing.Size(130, 35); this.btnClose.Location = new System.Drawing.Point(650, 140);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.dgvRelated.Location = new System.Drawing.Point(20, 190); this.dgvRelated.Size = new System.Drawing.Size(760, 340);
            this.dgvRelated.AllowUserToAddRows = false; this.dgvRelated.AllowUserToDeleteRows = false;
            this.dgvRelated.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRelated.ReadOnly = true; this.dgvRelated.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRelated.BackgroundColor = System.Drawing.Color.White;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.txtRelatedItemId); this.Controls.Add(this.txtRelatedName);
            this.Controls.Add(this.btnSearch); this.Controls.Add(this.cmbType); this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvRelated);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "ItemRelatedForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🔗 Σχετικά Είδη";

            ((System.ComponentModel.ISupportInitialize)(this.dgvRelated)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvRelated;
        private System.Windows.Forms.TextBox txtRelatedItemId, txtRelatedName, txtNotes;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnSearch, btnAdd, btnDelete, btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}