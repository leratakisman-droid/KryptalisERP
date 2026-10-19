namespace KryptalisERP
{
    partial class SupplierEditForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.tabPageBranches = new System.Windows.Forms.TabPage();
            this.tabPageContacts = new System.Windows.Forms.TabPage();
            this.tabPageBanks = new System.Windows.Forms.TabPage();
            this.tabPagePayments = new System.Windows.Forms.TabPage();
            this.tabPageItems = new System.Windows.Forms.TabPage();
            this.tabPagePriceLists = new System.Windows.Forms.TabPage();

            this.panelMain = new System.Windows.Forms.Panel();
            this.panelBranches = new System.Windows.Forms.Panel();
            this.panelContacts = new System.Windows.Forms.Panel();
            this.panelBanks = new System.Windows.Forms.Panel();
            this.panelPayments = new System.Windows.Forms.Panel();
            this.panelItems = new System.Windows.Forms.Panel();
            this.panelPriceLists = new System.Windows.Forms.Panel();

            this.lblSupplierName = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblAfm = new System.Windows.Forms.Label();
            this.txtAfm = new System.Windows.Forms.TextBox();
            this.btnValidateAfm = new System.Windows.Forms.Button();
            this.lblDoy = new System.Windows.Forms.Label();
            this.cmbDoy = new System.Windows.Forms.ComboBox();
            this.lblProfession = new System.Windows.Forms.Label();
            this.cmbProfession = new System.Windows.Forms.ComboBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone2 = new System.Windows.Forms.Label();
            this.txtPhone2 = new System.Windows.Forms.TextBox();
            this.lblPhone3 = new System.Windows.Forms.Label();
            this.txtPhone3 = new System.Windows.Forms.TextBox();
            this.lblBank = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.lblBank2 = new System.Windows.Forms.Label();
            this.cmbBank2 = new System.Windows.Forms.ComboBox();
            this.lblBank3 = new System.Windows.Forms.Label();
            this.cmbBank3 = new System.Windows.Forms.ComboBox();
            this.lblIban1 = new System.Windows.Forms.Label();
            this.txtIban1 = new System.Windows.Forms.TextBox();
            this.lblIban2 = new System.Windows.Forms.Label();
            this.txtIban2 = new System.Windows.Forms.TextBox();
            this.lblIban3 = new System.Windows.Forms.Label();
            this.txtIban3 = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lblSupplierType = new System.Windows.Forms.Label();
            this.cmbSupplierType = new System.Windows.Forms.ComboBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.dgvBranches = new System.Windows.Forms.DataGridView();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.dgvBanks = new System.Windows.Forms.DataGridView();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.dgvPriceLists = new System.Windows.Forms.DataGridView();

            this.btnAddBranch = new System.Windows.Forms.Button();
            this.btnEditBranch = new System.Windows.Forms.Button();
            this.btnDeleteBranch = new System.Windows.Forms.Button();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnEditContact = new System.Windows.Forms.Button();
            this.btnDeleteContact = new System.Windows.Forms.Button();
            this.btnAddBank = new System.Windows.Forms.Button();
            this.btnEditBank = new System.Windows.Forms.Button();
            this.btnDeleteBank = new System.Windows.Forms.Button();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.btnEditPayment = new System.Windows.Forms.Button();
            this.btnDeletePayment = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnAddPriceList = new System.Windows.Forms.Button();
            this.btnEditPriceList = new System.Windows.Forms.Button();
            this.btnDeletePriceList = new System.Windows.Forms.Button();

            this.tabControl1.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageBranches.SuspendLayout();
            this.tabPageContacts.SuspendLayout();
            this.tabPageBanks.SuspendLayout();
            this.tabPagePayments.SuspendLayout();
            this.tabPageItems.SuspendLayout();
            this.tabPagePriceLists.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // TAB CONTROL
            // ==========================================
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Controls.Add(this.tabPageMain);
            this.tabControl1.Controls.Add(this.tabPageBranches);
            this.tabControl1.Controls.Add(this.tabPageContacts);
            this.tabControl1.Controls.Add(this.tabPageBanks);
            this.tabControl1.Controls.Add(this.tabPagePayments);
            this.tabControl1.Controls.Add(this.tabPageItems);
            this.tabControl1.Controls.Add(this.tabPagePriceLists);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ==========================================
            // TAB 1: ΒΑΣΙΚΑ ΣΤΟΙΧΕΙΑ
            // ==========================================
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.AutoScroll = true;

            int y = 20;
            int labelX = 20;
            int controlX = 150;

            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblSupplierName.Text = "Επωνυμία:";
            this.lblSupplierName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtSupplierName.Location = new System.Drawing.Point(controlX, y);
            this.txtSupplierName.Size = new System.Drawing.Size(500, 30);
            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblContactPerson.Text = "Υπεύθυνος:";
            this.lblContactPerson.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtContactPerson.Location = new System.Drawing.Point(controlX, y);
            this.txtContactPerson.Size = new System.Drawing.Size(200, 30);
            this.txtContactPerson.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblAfm.AutoSize = true;
            this.lblAfm.Location = new System.Drawing.Point(controlX + 220, y + 12);
            this.lblAfm.Text = "ΑΦΜ:";
            this.lblAfm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtAfm.Location = new System.Drawing.Point(controlX + 270, y);
            this.txtAfm.Size = new System.Drawing.Size(120, 30);
            this.txtAfm.MaxLength = 9;
            this.txtAfm.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.btnValidateAfm.Text = "🔍 Έλεγχος";
            this.btnValidateAfm.Location = new System.Drawing.Point(controlX + 400, y);
            this.btnValidateAfm.Size = new System.Drawing.Size(110, 30);
            this.btnValidateAfm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnValidateAfm.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnValidateAfm.ForeColor = System.Drawing.Color.White;
            this.btnValidateAfm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidateAfm.FlatAppearance.BorderSize = 0;
            this.btnValidateAfm.Click += new System.EventHandler(this.btnValidateAfm_Click);
            y += 45;

            this.lblDoy.AutoSize = true;
            this.lblDoy.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblDoy.Text = "ΔΟΥ:";
            this.lblDoy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbDoy.Location = new System.Drawing.Point(controlX, y);
            this.cmbDoy.Size = new System.Drawing.Size(280, 30);
            this.cmbDoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbDoy.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblProfession.AutoSize = true;
            this.lblProfession.Location = new System.Drawing.Point(controlX + 300, y + 12);
            this.lblProfession.Text = "Επάγγελμα:";
            this.lblProfession.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbProfession.Location = new System.Drawing.Point(controlX + 410, y);
            this.cmbProfession.Size = new System.Drawing.Size(250, 30);
            this.cmbProfession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbProfession.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblPhone.Text = "Τηλέφωνο:";
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone.Location = new System.Drawing.Point(controlX, y);
            this.txtPhone.Size = new System.Drawing.Size(150, 30);
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblPhone2.AutoSize = true;
            this.lblPhone2.Location = new System.Drawing.Point(controlX + 170, y + 12);
            this.lblPhone2.Text = "Τηλ. 2:";
            this.lblPhone2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone2.Location = new System.Drawing.Point(controlX + 230, y);
            this.txtPhone2.Size = new System.Drawing.Size(130, 30);
            this.txtPhone2.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblPhone3.AutoSize = true;
            this.lblPhone3.Location = new System.Drawing.Point(controlX + 380, y + 12);
            this.lblPhone3.Text = "Τηλ. 3:";
            this.lblPhone3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtPhone3.Location = new System.Drawing.Point(controlX + 440, y);
            this.txtPhone3.Size = new System.Drawing.Size(130, 30);
            this.txtPhone3.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBank.Text = "Τράπεζα:";
            this.lblBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBank.Location = new System.Drawing.Point(controlX, y);
            this.cmbBank.Size = new System.Drawing.Size(200, 30);
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbBank.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblIban1.AutoSize = true;
            this.lblIban1.Location = new System.Drawing.Point(controlX + 220, y + 12);
            this.lblIban1.Text = "IBAN 1:";
            this.lblIban1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtIban1.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtIban1.Size = new System.Drawing.Size(280, 30);
            this.txtIban1.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblBank2.AutoSize = true;
            this.lblBank2.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBank2.Text = "Τράπεζα:";
            this.lblBank2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBank2.Location = new System.Drawing.Point(controlX, y);
            this.cmbBank2.Size = new System.Drawing.Size(200, 30);
            this.cmbBank2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbBank2.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblIban2.AutoSize = true;
            this.lblIban2.Location = new System.Drawing.Point(controlX + 220, y + 12);
            this.lblIban2.Text = "IBAN 2:";
            this.lblIban2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtIban2.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtIban2.Size = new System.Drawing.Size(280, 30);
            this.txtIban2.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblBank3.AutoSize = true;
            this.lblBank3.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblBank3.Text = "Τράπεζα:";
            this.lblBank3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbBank3.Location = new System.Drawing.Point(controlX, y);
            this.cmbBank3.Size = new System.Drawing.Size(200, 30);
            this.cmbBank3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbBank3.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblIban3.AutoSize = true;
            this.lblIban3.Location = new System.Drawing.Point(controlX + 220, y + 12);
            this.lblIban3.Text = "IBAN 3:";
            this.lblIban3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtIban3.Location = new System.Drawing.Point(controlX + 290, y);
            this.txtIban3.Size = new System.Drawing.Size(280, 30);
            this.txtIban3.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(controlX, y);
            this.txtEmail.Size = new System.Drawing.Size(400, 30);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblAddress.Text = "Διεύθυνση:";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtAddress.Location = new System.Drawing.Point(controlX, y);
            this.txtAddress.Size = new System.Drawing.Size(600, 30);
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblCity.Text = "Πόλη:";
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtCity.Location = new System.Drawing.Point(controlX, y);
            this.txtCity.Size = new System.Drawing.Size(180, 30);
            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(controlX + 200, y + 12);
            this.lblArea.Text = "Περιοχή:";
            this.lblArea.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtArea.Location = new System.Drawing.Point(controlX + 270, y);
            this.txtArea.Size = new System.Drawing.Size(180, 30);
            this.txtArea.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(controlX + 470, y + 12);
            this.lblCountry.Text = "Χώρα:";
            this.lblCountry.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbCountry.Location = new System.Drawing.Point(controlX + 530, y);
            this.cmbCountry.Size = new System.Drawing.Size(180, 30);
            this.cmbCountry.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblSupplierType.AutoSize = true;
            this.lblSupplierType.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblSupplierType.Text = "Τύπος Προμ.:";
            this.lblSupplierType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cmbSupplierType.Location = new System.Drawing.Point(controlX, y);
            this.cmbSupplierType.Size = new System.Drawing.Size(280, 30);
            this.cmbSupplierType.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 45;

            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(labelX, y + 12);
            this.lblNotes.Text = "Σημειώσεις:";
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(controlX, y);
            this.txtNotes.Size = new System.Drawing.Size(600, 100);
            this.txtNotes.Multiline = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            y += 110;

            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8);
            this.chkIsActive.Text = "Ενεργός";
            this.chkIsActive.Checked = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            this.btnSave.Location = new System.Drawing.Point(550, y);
            this.btnSave.Size = new System.Drawing.Size(160, 45);
            this.btnSave.Text = "💾 Αποθήκευση";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(720, y);
            this.btnCancel.Size = new System.Drawing.Size(140, 45);
            this.btnCancel.Text = "❌ Ακύρωση";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.panelMain.Controls.Add(this.lblSupplierName);
            this.panelMain.Controls.Add(this.txtSupplierName);
            this.panelMain.Controls.Add(this.lblContactPerson);
            this.panelMain.Controls.Add(this.txtContactPerson);
            this.panelMain.Controls.Add(this.lblAfm);
            this.panelMain.Controls.Add(this.txtAfm);
            this.panelMain.Controls.Add(this.btnValidateAfm);
            this.panelMain.Controls.Add(this.lblDoy);
            this.panelMain.Controls.Add(this.cmbDoy);
            this.panelMain.Controls.Add(this.lblProfession);
            this.panelMain.Controls.Add(this.cmbProfession);
            this.panelMain.Controls.Add(this.lblPhone);
            this.panelMain.Controls.Add(this.txtPhone);
            this.panelMain.Controls.Add(this.lblPhone2);
            this.panelMain.Controls.Add(this.txtPhone2);
            this.panelMain.Controls.Add(this.lblPhone3);
            this.panelMain.Controls.Add(this.txtPhone3);
            this.panelMain.Controls.Add(this.lblBank);
            this.panelMain.Controls.Add(this.cmbBank);
            this.panelMain.Controls.Add(this.lblIban1);
            this.panelMain.Controls.Add(this.txtIban1);
            this.panelMain.Controls.Add(this.lblBank2);
            this.panelMain.Controls.Add(this.cmbBank2);
            this.panelMain.Controls.Add(this.lblIban2);
            this.panelMain.Controls.Add(this.txtIban2);
            this.panelMain.Controls.Add(this.lblBank3);
            this.panelMain.Controls.Add(this.cmbBank3);
            this.panelMain.Controls.Add(this.lblIban3);
            this.panelMain.Controls.Add(this.txtIban3);
            this.panelMain.Controls.Add(this.lblEmail);
            this.panelMain.Controls.Add(this.txtEmail);
            this.panelMain.Controls.Add(this.lblAddress);
            this.panelMain.Controls.Add(this.txtAddress);
            this.panelMain.Controls.Add(this.lblCity);
            this.panelMain.Controls.Add(this.txtCity);
            this.panelMain.Controls.Add(this.lblArea);
            this.panelMain.Controls.Add(this.txtArea);
            this.panelMain.Controls.Add(this.lblCountry);
            this.panelMain.Controls.Add(this.cmbCountry);
            this.panelMain.Controls.Add(this.lblSupplierType);
            this.panelMain.Controls.Add(this.cmbSupplierType);
            this.panelMain.Controls.Add(this.lblNotes);
            this.panelMain.Controls.Add(this.txtNotes);
            this.panelMain.Controls.Add(this.chkIsActive);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.btnCancel);

            this.tabPageMain.Controls.Add(this.panelMain);
            this.tabPageMain.Text = "📋 Βασικά Στοιχεία";
            this.tabPageMain.UseVisualStyleBackColor = true;

            // ==========================================
            // TAB 2: ΥΠΟΚΑΤΑΣΤΗΜΑΤΑ (ΟΜΟΡΦΑ ΚΟΥΜΠΙΑ)
            // ==========================================
            this.panelBranches.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dgvBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBranches.AllowUserToAddRows = false;
            this.dgvBranches.AllowUserToDeleteRows = false;
            this.dgvBranches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBranches.MultiSelect = false;
            this.dgvBranches.ReadOnly = true;
            this.dgvBranches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBranches.BackgroundColor = System.Drawing.Color.White;
            this.dgvBranches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBranches.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvBranches.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBranches.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvBranches.ColumnHeadersHeight = 40;
            this.dgvBranches.RowTemplate.Height = 35;

            var btnPanelBranches = new System.Windows.Forms.Panel();
            btnPanelBranches.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelBranches.Height = 55;
            btnPanelBranches.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            btnPanelBranches.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.btnAddBranch.Text = "➕ Προσθήκη";
            this.btnAddBranch.Size = new System.Drawing.Size(140, 40);
            this.btnAddBranch.Location = new System.Drawing.Point(10, 7);
            this.btnAddBranch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddBranch.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddBranch.ForeColor = System.Drawing.Color.White;
            this.btnAddBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBranch.FlatAppearance.BorderSize = 2;
            this.btnAddBranch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddBranch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBranch.Click += new System.EventHandler(this.btnAddBranch_Click);

            this.btnEditBranch.Text = "✏️ Επεξεργασία";
            this.btnEditBranch.Size = new System.Drawing.Size(150, 40);
            this.btnEditBranch.Location = new System.Drawing.Point(160, 7);
            this.btnEditBranch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditBranch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditBranch.ForeColor = System.Drawing.Color.White;
            this.btnEditBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditBranch.FlatAppearance.BorderSize = 2;
            this.btnEditBranch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditBranch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditBranch.Click += new System.EventHandler(this.btnEditBranch_Click);

            this.btnDeleteBranch.Text = "🗑️ Διαγραφή";
            this.btnDeleteBranch.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteBranch.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteBranch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteBranch.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteBranch.ForeColor = System.Drawing.Color.White;
            this.btnDeleteBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBranch.FlatAppearance.BorderSize = 2;
            this.btnDeleteBranch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteBranch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteBranch.Click += new System.EventHandler(this.btnDeleteBranch_Click);

            btnPanelBranches.Controls.Add(this.btnAddBranch);
            btnPanelBranches.Controls.Add(this.btnEditBranch);
            btnPanelBranches.Controls.Add(this.btnDeleteBranch);

            this.panelBranches.Controls.Add(this.dgvBranches);
            this.panelBranches.Controls.Add(btnPanelBranches);
            this.tabPageBranches.Controls.Add(this.panelBranches);
            this.tabPageBranches.Text = "🏢 Υποκαταστήματα";
            this.tabPageBranches.UseVisualStyleBackColor = true;

            // ==========================================
            // TAB 3: ΕΠΑΦΕΣ (ΟΜΟΡΦΑ ΚΟΥΜΠΙΑ)
            // ==========================================
            this.panelContacts.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContacts.AllowUserToAddRows = false;
            this.dgvContacts.AllowUserToDeleteRows = false;
            this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContacts.MultiSelect = false;
            this.dgvContacts.ReadOnly = true;
            this.dgvContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContacts.BackgroundColor = System.Drawing.Color.White;
            this.dgvContacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvContacts.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvContacts.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvContacts.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvContacts.ColumnHeadersHeight = 40;
            this.dgvContacts.RowTemplate.Height = 35;

            var btnPanelContacts = new System.Windows.Forms.Panel();
            btnPanelContacts.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelContacts.Height = 55;
            btnPanelContacts.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            btnPanelContacts.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.btnAddContact.Text = "➕ Προσθήκη";
            this.btnAddContact.Size = new System.Drawing.Size(140, 40);
            this.btnAddContact.Location = new System.Drawing.Point(10, 7);
            this.btnAddContact.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddContact.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddContact.ForeColor = System.Drawing.Color.White;
            this.btnAddContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddContact.FlatAppearance.BorderSize = 2;
            this.btnAddContact.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddContact.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);

            this.btnEditContact.Text = "✏️ Επεξεργασία";
            this.btnEditContact.Size = new System.Drawing.Size(150, 40);
            this.btnEditContact.Location = new System.Drawing.Point(160, 7);
            this.btnEditContact.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditContact.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditContact.ForeColor = System.Drawing.Color.White;
            this.btnEditContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditContact.FlatAppearance.BorderSize = 2;
            this.btnEditContact.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditContact.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditContact.Click += new System.EventHandler(this.btnEditContact_Click);

            this.btnDeleteContact.Text = "🗑️ Διαγραφή";
            this.btnDeleteContact.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteContact.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteContact.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteContact.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteContact.ForeColor = System.Drawing.Color.White;
            this.btnDeleteContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteContact.FlatAppearance.BorderSize = 2;
            this.btnDeleteContact.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteContact.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteContact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteContact.Click += new System.EventHandler(this.btnDeleteContact_Click);

            btnPanelContacts.Controls.Add(this.btnAddContact);
            btnPanelContacts.Controls.Add(this.btnEditContact);
            btnPanelContacts.Controls.Add(this.btnDeleteContact);

            this.panelContacts.Controls.Add(this.dgvContacts);
            this.panelContacts.Controls.Add(btnPanelContacts);
            this.tabPageContacts.Controls.Add(this.panelContacts);
            this.tabPageContacts.Text = "👥 Επαφές";
            this.tabPageContacts.UseVisualStyleBackColor = true;

            // ==========================================
            // TAB 4: ΤΡΑΠΕΖΕΣ (ΟΜΟΡΦΑ ΚΟΥΜΠΙΑ)
            // ==========================================
            this.panelBanks.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dgvBanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBanks.AllowUserToAddRows = false;
            this.dgvBanks.AllowUserToDeleteRows = false;
            this.dgvBanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBanks.MultiSelect = false;
            this.dgvBanks.ReadOnly = true;
            this.dgvBanks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBanks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBanks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBanks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvBanks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBanks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvBanks.ColumnHeadersHeight = 40;
            this.dgvBanks.RowTemplate.Height = 35;

            var btnPanelBanks = new System.Windows.Forms.Panel();
            btnPanelBanks.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelBanks.Height = 55;
            btnPanelBanks.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            btnPanelBanks.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.btnAddBank.Text = "➕ Προσθήκη";
            this.btnAddBank.Size = new System.Drawing.Size(140, 40);
            this.btnAddBank.Location = new System.Drawing.Point(10, 7);
            this.btnAddBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddBank.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddBank.ForeColor = System.Drawing.Color.White;
            this.btnAddBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBank.FlatAppearance.BorderSize = 2;
            this.btnAddBank.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddBank.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddBank.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBank.Click += new System.EventHandler(this.btnAddBank_Click);

            this.btnEditBank.Text = "✏️ Επεξεργασία";
            this.btnEditBank.Size = new System.Drawing.Size(150, 40);
            this.btnEditBank.Location = new System.Drawing.Point(160, 7);
            this.btnEditBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditBank.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditBank.ForeColor = System.Drawing.Color.White;
            this.btnEditBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditBank.FlatAppearance.BorderSize = 2;
            this.btnEditBank.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditBank.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditBank.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditBank.Click += new System.EventHandler(this.btnEditBank_Click);

            this.btnDeleteBank.Text = "🗑️ Διαγραφή";
            this.btnDeleteBank.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteBank.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteBank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteBank.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteBank.ForeColor = System.Drawing.Color.White;
            this.btnDeleteBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBank.FlatAppearance.BorderSize = 2;
            this.btnDeleteBank.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteBank.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteBank.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteBank.Click += new System.EventHandler(this.btnDeleteBank_Click);

            btnPanelBanks.Controls.Add(this.btnAddBank);
            btnPanelBanks.Controls.Add(this.btnEditBank);
            btnPanelBanks.Controls.Add(this.btnDeleteBank);

            this.panelBanks.Controls.Add(this.dgvBanks);
            this.panelBanks.Controls.Add(btnPanelBanks);
            this.tabPageBanks.Controls.Add(this.panelBanks);
            this.tabPageBanks.Text = "🏦 Τράπεζες";
            this.tabPageBanks.UseVisualStyleBackColor = true;

            // ==========================================
            // TAB 5: ΟΡΟΙ ΠΛΗΡΩΜΗΣ (ΟΜΟΡΦΑ ΚΟΥΜΠΙΑ)
            // ==========================================
            this.panelPayments.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            this.dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayments.MultiSelect = false;
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayments.BackgroundColor = System.Drawing.Color.White;
            this.dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPayments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvPayments.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPayments.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvPayments.ColumnHeadersHeight = 40;
            this.dgvPayments.RowTemplate.Height = 35;

            var btnPanelPayments = new System.Windows.Forms.Panel();
            btnPanelPayments.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelPayments.Height = 55;
            btnPanelPayments.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            btnPanelPayments.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.btnAddPayment.Text = "➕ Προσθήκη";
            this.btnAddPayment.Size = new System.Drawing.Size(140, 40);
            this.btnAddPayment.Location = new System.Drawing.Point(10, 7);
            this.btnAddPayment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddPayment.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddPayment.ForeColor = System.Drawing.Color.White;
            this.btnAddPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPayment.FlatAppearance.BorderSize = 2;
            this.btnAddPayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);

            this.btnEditPayment.Text = "✏️ Επεξεργασία";
            this.btnEditPayment.Size = new System.Drawing.Size(150, 40);
            this.btnEditPayment.Location = new System.Drawing.Point(160, 7);
            this.btnEditPayment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditPayment.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditPayment.ForeColor = System.Drawing.Color.White;
            this.btnEditPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditPayment.FlatAppearance.BorderSize = 2;
            this.btnEditPayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditPayment.Click += new System.EventHandler(this.btnEditPayment_Click);

            this.btnDeletePayment.Text = "🗑️ Διαγραφή";
            this.btnDeletePayment.Size = new System.Drawing.Size(140, 40);
            this.btnDeletePayment.Location = new System.Drawing.Point(320, 7);
            this.btnDeletePayment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeletePayment.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeletePayment.ForeColor = System.Drawing.Color.White;
            this.btnDeletePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePayment.FlatAppearance.BorderSize = 2;
            this.btnDeletePayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeletePayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeletePayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeletePayment.Click += new System.EventHandler(this.btnDeletePayment_Click);

            btnPanelPayments.Controls.Add(this.btnAddPayment);
            btnPanelPayments.Controls.Add(this.btnEditPayment);
            btnPanelPayments.Controls.Add(this.btnDeletePayment);

            this.panelPayments.Controls.Add(this.dgvPayments);
            this.panelPayments.Controls.Add(btnPanelPayments);
            this.tabPagePayments.Controls.Add(this.panelPayments);
            this.tabPagePayments.Text = "💳 Όροι Πληρωμής";
            this.tabPagePayments.UseVisualStyleBackColor = true;

            // ==========================================
            // TAB 6: ΕΙΔΗ (ΟΜΟΡΦΑ ΚΟΥΜΠΙΑ)
            // ==========================================
            this.panelItems.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.MultiSelect = false;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.RowTemplate.Height = 35;

            var btnPanelItems = new System.Windows.Forms.Panel();
            btnPanelItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelItems.Height = 55;
            btnPanelItems.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            btnPanelItems.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.btnAddItem.Text = "➕ Προσθήκη";
            this.btnAddItem.Size = new System.Drawing.Size(140, 40);
            this.btnAddItem.Location = new System.Drawing.Point(10, 7);
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.FlatAppearance.BorderSize = 2;
            this.btnAddItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);

            this.btnEditItem.Text = "✏️ Επεξεργασία";
            this.btnEditItem.Size = new System.Drawing.Size(150, 40);
            this.btnEditItem.Location = new System.Drawing.Point(160, 7);
            this.btnEditItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditItem.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditItem.ForeColor = System.Drawing.Color.White;
            this.btnEditItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditItem.FlatAppearance.BorderSize = 2;
            this.btnEditItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);

            this.btnDeleteItem.Text = "🗑️ Διαγραφή";
            this.btnDeleteItem.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteItem.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteItem.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteItem.ForeColor = System.Drawing.Color.White;
            this.btnDeleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteItem.FlatAppearance.BorderSize = 2;
            this.btnDeleteItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeleteItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);

            btnPanelItems.Controls.Add(this.btnAddItem);
            btnPanelItems.Controls.Add(this.btnEditItem);
            btnPanelItems.Controls.Add(this.btnDeleteItem);

            this.panelItems.Controls.Add(this.dgvItems);
            this.panelItems.Controls.Add(btnPanelItems);
            this.tabPageItems.Controls.Add(this.panelItems);
            this.tabPageItems.Text = "📦 Είδη";
            this.tabPageItems.UseVisualStyleBackColor = true;

            // ==========================================
            // TAB 7: ΤΙΜΟΚΑΤΑΛΟΓΟΙ (ΟΜΟΡΦΑ ΚΟΥΜΠΙΑ)
            // ==========================================
            this.panelPriceLists.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dgvPriceLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPriceLists.AllowUserToAddRows = false;
            this.dgvPriceLists.AllowUserToDeleteRows = false;
            this.dgvPriceLists.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPriceLists.MultiSelect = false;
            this.dgvPriceLists.ReadOnly = true;
            this.dgvPriceLists.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPriceLists.BackgroundColor = System.Drawing.Color.White;
            this.dgvPriceLists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPriceLists.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvPriceLists.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPriceLists.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvPriceLists.ColumnHeadersHeight = 40;
            this.dgvPriceLists.RowTemplate.Height = 35;

            var btnPanelPriceLists = new System.Windows.Forms.Panel();
            btnPanelPriceLists.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelPriceLists.Height = 55;
            btnPanelPriceLists.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            btnPanelPriceLists.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);

            this.btnAddPriceList.Text = "➕ Προσθήκη";
            this.btnAddPriceList.Size = new System.Drawing.Size(140, 40);
            this.btnAddPriceList.Location = new System.Drawing.Point(10, 7);
            this.btnAddPriceList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddPriceList.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddPriceList.ForeColor = System.Drawing.Color.White;
            this.btnAddPriceList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPriceList.FlatAppearance.BorderSize = 2;
            this.btnAddPriceList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddPriceList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAddPriceList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPriceList.Click += new System.EventHandler(this.btnAddPriceList_Click);

            this.btnEditPriceList.Text = "✏️ Επεξεργασία";
            this.btnEditPriceList.Size = new System.Drawing.Size(150, 40);
            this.btnEditPriceList.Location = new System.Drawing.Point(160, 7);
            this.btnEditPriceList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditPriceList.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditPriceList.ForeColor = System.Drawing.Color.White;
            this.btnEditPriceList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditPriceList.FlatAppearance.BorderSize = 2;
            this.btnEditPriceList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditPriceList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnEditPriceList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditPriceList.Click += new System.EventHandler(this.btnEditPriceList_Click);

            this.btnDeletePriceList.Text = "🗑️ Διαγραφή";
            this.btnDeletePriceList.Size = new System.Drawing.Size(140, 40);
            this.btnDeletePriceList.Location = new System.Drawing.Point(320, 7);
            this.btnDeletePriceList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeletePriceList.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeletePriceList.ForeColor = System.Drawing.Color.White;
            this.btnDeletePriceList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePriceList.FlatAppearance.BorderSize = 2;
            this.btnDeletePriceList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeletePriceList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDeletePriceList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeletePriceList.Click += new System.EventHandler(this.btnDeletePriceList_Click);

            btnPanelPriceLists.Controls.Add(this.btnAddPriceList);
            btnPanelPriceLists.Controls.Add(this.btnEditPriceList);
            btnPanelPriceLists.Controls.Add(this.btnDeletePriceList);

            this.panelPriceLists.Controls.Add(this.dgvPriceLists);
            this.panelPriceLists.Controls.Add(btnPanelPriceLists);
            this.tabPagePriceLists.Controls.Add(this.panelPriceLists);
            this.tabPagePriceLists.Text = "📊 Τιμοκατάλογοι";
            this.tabPagePriceLists.UseVisualStyleBackColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 750);
            this.Controls.Add(this.tabControl1);
            this.Name = "SupplierEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Επεξεργασία Προμηθευτή";

            this.tabControl1.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageBranches.ResumeLayout(false);
            this.tabPageContacts.ResumeLayout(false);
            this.tabPageBanks.ResumeLayout(false);
            this.tabPagePayments.ResumeLayout(false);
            this.tabPageItems.ResumeLayout(false);
            this.tabPagePriceLists.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMain, tabPageBranches, tabPageContacts, tabPageBanks, tabPagePayments, tabPageItems, tabPagePriceLists;
        private System.Windows.Forms.Panel panelMain, panelBranches, panelContacts, panelBanks, panelPayments, panelItems, panelPriceLists;

        private System.Windows.Forms.Label lblSupplierName, lblContactPerson, lblAfm, lblDoy, lblProfession;
        private System.Windows.Forms.Label lblPhone, lblPhone2, lblPhone3;
        private System.Windows.Forms.Label lblBank, lblBank2, lblBank3, lblIban1, lblIban2, lblIban3;
        private System.Windows.Forms.Label lblEmail, lblAddress, lblCity, lblArea, lblCountry, lblSupplierType, lblNotes;

        private System.Windows.Forms.TextBox txtSupplierName, txtContactPerson, txtAfm, txtPhone, txtPhone2, txtPhone3;
        private System.Windows.Forms.TextBox txtIban1, txtIban2, txtIban3, txtEmail, txtAddress, txtCity, txtArea, txtNotes;
        private System.Windows.Forms.ComboBox cmbDoy, cmbProfession, cmbCountry, cmbSupplierType;
        private System.Windows.Forms.ComboBox cmbBank, cmbBank2, cmbBank3;
        private System.Windows.Forms.Button btnValidateAfm;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave, btnCancel;

        private System.Windows.Forms.DataGridView dgvBranches, dgvContacts, dgvBanks, dgvPayments, dgvItems, dgvPriceLists;
        private System.Windows.Forms.Button btnAddBranch, btnEditBranch, btnDeleteBranch;
        private System.Windows.Forms.Button btnAddContact, btnEditContact, btnDeleteContact;
        private System.Windows.Forms.Button btnAddBank, btnEditBank, btnDeleteBank;
        private System.Windows.Forms.Button btnAddPayment, btnEditPayment, btnDeletePayment;
        private System.Windows.Forms.Button btnAddItem, btnEditItem, btnDeleteItem;
        private System.Windows.Forms.Button btnAddPriceList, btnEditPriceList, btnDeletePriceList;
    }
}