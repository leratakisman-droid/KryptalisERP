namespace KryptalisERP
{
    partial class CompaniesForm
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
            this.dgvCompanies = new System.Windows.Forms.DataGridView();
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.tabCompany = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabContact = new System.Windows.Forms.TabPage();
            this.tabBank = new System.Windows.Forms.TabPage();
            this.tabLogo = new System.Windows.Forms.TabPage();

            this.lblCompanyName = new System.Windows.Forms.Label(); this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblTradeName = new System.Windows.Forms.Label(); this.txtTradeName = new System.Windows.Forms.TextBox();
            this.lblVatNumber = new System.Windows.Forms.Label(); this.txtVatNumber = new System.Windows.Forms.TextBox();
            this.lblDoy = new System.Windows.Forms.Label(); this.txtDoy = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label(); this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label(); this.txtCity = new System.Windows.Forms.TextBox();
            this.lblPostalCode = new System.Windows.Forms.Label(); this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label(); this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone2 = new System.Windows.Forms.Label(); this.txtPhone2 = new System.Windows.Forms.TextBox();
            this.lblPhone3 = new System.Windows.Forms.Label(); this.txtPhone3 = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label(); this.txtMobile = new System.Windows.Forms.TextBox();
            this.lblMobile2 = new System.Windows.Forms.Label(); this.txtMobile2 = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label(); this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label(); this.txtWebsite = new System.Windows.Forms.TextBox();
            this.lblBankName = new System.Windows.Forms.Label(); this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblBankHolder = new System.Windows.Forms.Label(); this.txtBankHolder = new System.Windows.Forms.TextBox();
            this.lblBankIban = new System.Windows.Forms.Label(); this.txtBankIban = new System.Windows.Forms.TextBox();
            this.lblBankIban2 = new System.Windows.Forms.Label(); this.txtBankIban2 = new System.Windows.Forms.TextBox();
            this.lblBankIban3 = new System.Windows.Forms.Label(); this.txtBankIban3 = new System.Windows.Forms.TextBox();
            this.lblBankIban4 = new System.Windows.Forms.Label(); this.txtBankIban4 = new System.Windows.Forms.TextBox();
            this.lblBankIban5 = new System.Windows.Forms.Label(); this.txtBankIban5 = new System.Windows.Forms.TextBox();
            this.lblBankIban6 = new System.Windows.Forms.Label(); this.txtBankIban6 = new System.Windows.Forms.TextBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnUploadLogo = new System.Windows.Forms.Button();
            this.btnRemoveLogo = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnNewCompany = new System.Windows.Forms.Button();
            this.btnSaveCompany = new System.Windows.Forms.Button();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.btnDeleteCompany = new System.Windows.Forms.Button();
            this.btnWarehouses = new System.Windows.Forms.Button(); // ✅ ΝΕΟ

            this.grpBranches = new System.Windows.Forms.GroupBox();
            this.dgvBranches = new System.Windows.Forms.DataGridView();
            this.tabBranch = new System.Windows.Forms.TabControl();
            this.tabBranchGeneral = new System.Windows.Forms.TabPage();
            this.tabBranchContact = new System.Windows.Forms.TabPage();
            this.tabBranchBank = new System.Windows.Forms.TabPage();
            this.tabBranchLogo = new System.Windows.Forms.TabPage();

            this.lblBranchName = new System.Windows.Forms.Label(); this.txtBranchName = new System.Windows.Forms.TextBox();
            this.lblBranchCode = new System.Windows.Forms.Label(); this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.lblBranchVat = new System.Windows.Forms.Label(); this.txtBranchVat = new System.Windows.Forms.TextBox();
            this.lblBranchDoy = new System.Windows.Forms.Label(); this.txtBranchDoy = new System.Windows.Forms.TextBox();
            this.lblBranchAddress = new System.Windows.Forms.Label(); this.txtBranchAddress = new System.Windows.Forms.TextBox();
            this.lblBranchCity = new System.Windows.Forms.Label(); this.txtBranchCity = new System.Windows.Forms.TextBox();
            this.lblBranchPostal = new System.Windows.Forms.Label(); this.txtBranchPostal = new System.Windows.Forms.TextBox();
            this.chkBranchMain = new System.Windows.Forms.CheckBox();
            this.lblBranchPhone = new System.Windows.Forms.Label(); this.txtBranchPhone = new System.Windows.Forms.TextBox();
            this.lblBranchPhone2 = new System.Windows.Forms.Label(); this.txtBranchPhone2 = new System.Windows.Forms.TextBox();
            this.lblBranchPhone3 = new System.Windows.Forms.Label(); this.txtBranchPhone3 = new System.Windows.Forms.TextBox();
            this.lblBranchMobile = new System.Windows.Forms.Label(); this.txtBranchMobile = new System.Windows.Forms.TextBox();
            this.lblBranchMobile2 = new System.Windows.Forms.Label(); this.txtBranchMobile2 = new System.Windows.Forms.TextBox();
            this.lblBranchEmail = new System.Windows.Forms.Label(); this.txtBranchEmail = new System.Windows.Forms.TextBox();
            this.lblBranchWebsite = new System.Windows.Forms.Label(); this.txtBranchWebsite = new System.Windows.Forms.TextBox();
            this.lblBranchManager = new System.Windows.Forms.Label(); this.txtBranchManager = new System.Windows.Forms.TextBox();
            this.lblBranchBankName = new System.Windows.Forms.Label(); this.txtBranchBankName = new System.Windows.Forms.TextBox();
            this.lblBranchBankHolder = new System.Windows.Forms.Label(); this.txtBranchBankHolder = new System.Windows.Forms.TextBox();
            this.lblBranchIban = new System.Windows.Forms.Label(); this.txtBranchIban = new System.Windows.Forms.TextBox();
            this.lblBranchIban2 = new System.Windows.Forms.Label(); this.txtBranchIban2 = new System.Windows.Forms.TextBox();
            this.lblBranchIban3 = new System.Windows.Forms.Label(); this.txtBranchIban3 = new System.Windows.Forms.TextBox();
            this.lblBranchIban4 = new System.Windows.Forms.Label(); this.txtBranchIban4 = new System.Windows.Forms.TextBox();
            this.lblBranchIban5 = new System.Windows.Forms.Label(); this.txtBranchIban5 = new System.Windows.Forms.TextBox();
            this.lblBranchIban6 = new System.Windows.Forms.Label(); this.txtBranchIban6 = new System.Windows.Forms.TextBox();
            this.picBranchLogo = new System.Windows.Forms.PictureBox();
            this.btnUploadBranchLogo = new System.Windows.Forms.Button();
            this.btnRemoveBranchLogo = new System.Windows.Forms.Button();
            this.btnNewBranch = new System.Windows.Forms.Button();
            this.btnSaveBranch = new System.Windows.Forms.Button();
            this.btnDeleteBranch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanies)).BeginInit();
            this.grpCompany.SuspendLayout();
            this.tabCompany.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.grpBranches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).BeginInit();
            this.tabBranch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBranchLogo)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text = "🏢 Διαχείριση Εταιρειών & Υποκαταστημάτων";
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.AutoSize = true;

            // dgvCompanies
            this.dgvCompanies.Location = new System.Drawing.Point(20, 50);
            this.dgvCompanies.Size = new System.Drawing.Size(330, 400);
            this.dgvCompanies.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompanies.RowHeadersVisible = false;
            this.dgvCompanies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompanies.AllowUserToAddRows = false;
            this.dgvCompanies.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.dgvCompanies.SelectionChanged += new System.EventHandler(this.dgvCompanies_SelectionChanged);

            // grpCompany
            this.grpCompany.Text = "Στοιχεία Εταιρείας";
            this.grpCompany.Location = new System.Drawing.Point(370, 50);
            this.grpCompany.Size = new System.Drawing.Size(790, 400);
            this.grpCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.tabCompany.Location = new System.Drawing.Point(15, 25);
            this.tabCompany.Size = new System.Drawing.Size(760, 310);
            this.tabCompany.Controls.Add(this.tabGeneral);
            this.tabCompany.Controls.Add(this.tabContact);
            this.tabCompany.Controls.Add(this.tabBank);
            this.tabCompany.Controls.Add(this.tabLogo);

            // TAB ΓΕΝΙΚΑ (Εταιρεία)
            this.tabGeneral.Text = "Γενικά";
            int gy = 20;
            this.lblCompanyName.Text = "Επωνυμία:"; this.lblCompanyName.Location = new System.Drawing.Point(15, gy); this.lblCompanyName.AutoSize = true;
            this.txtCompanyName.Location = new System.Drawing.Point(130, gy - 2); this.txtCompanyName.Size = new System.Drawing.Size(280, 23);
            this.lblTradeName.Text = "Διακρ. Τίτλος:"; this.lblTradeName.Location = new System.Drawing.Point(430, gy); this.lblTradeName.AutoSize = true;
            this.txtTradeName.Location = new System.Drawing.Point(530, gy - 2); this.txtTradeName.Size = new System.Drawing.Size(210, 23);
            gy += 35;
            this.lblVatNumber.Text = "ΑΦΜ:"; this.lblVatNumber.Location = new System.Drawing.Point(15, gy); this.lblVatNumber.AutoSize = true;
            this.txtVatNumber.Location = new System.Drawing.Point(130, gy - 2); this.txtVatNumber.Size = new System.Drawing.Size(150, 23);
            this.lblDoy.Text = "ΔΟΥ:"; this.lblDoy.Location = new System.Drawing.Point(300, gy); this.lblDoy.AutoSize = true;
            this.txtDoy.Location = new System.Drawing.Point(350, gy - 2); this.txtDoy.Size = new System.Drawing.Size(180, 23);
            gy += 35;
            this.lblAddress.Text = "Διεύθυνση:"; this.lblAddress.Location = new System.Drawing.Point(15, gy); this.lblAddress.AutoSize = true;
            this.txtAddress.Location = new System.Drawing.Point(130, gy - 2); this.txtAddress.Size = new System.Drawing.Size(400, 23);
            gy += 35;
            this.lblCity.Text = "Πόλη:"; this.lblCity.Location = new System.Drawing.Point(15, gy); this.lblCity.AutoSize = true;
            this.txtCity.Location = new System.Drawing.Point(130, gy - 2); this.txtCity.Size = new System.Drawing.Size(180, 23);
            this.lblPostalCode.Text = "ΤΚ:"; this.lblPostalCode.Location = new System.Drawing.Point(330, gy); this.lblPostalCode.AutoSize = true;
            this.txtPostalCode.Location = new System.Drawing.Point(370, gy - 2); this.txtPostalCode.Size = new System.Drawing.Size(80, 23);
            this.tabGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCompanyName, this.txtCompanyName, this.lblTradeName, this.txtTradeName,
                this.lblVatNumber, this.txtVatNumber, this.lblDoy, this.txtDoy,
                this.lblAddress, this.txtAddress, this.lblCity, this.txtCity, this.lblPostalCode, this.txtPostalCode });

            // TAB ΕΠΙΚΟΙΝΩΝΙΑ (Εταιρεία)
            this.tabContact.Text = "Επικοινωνία";
            int cy = 20;
            this.lblPhone.Text = "Τηλέφωνο 1:"; this.lblPhone.Location = new System.Drawing.Point(15, cy); this.lblPhone.AutoSize = true;
            this.txtPhone.Location = new System.Drawing.Point(110, cy - 2); this.txtPhone.Size = new System.Drawing.Size(130, 23);
            this.lblPhone2.Text = "Τηλέφωνο 2:"; this.lblPhone2.Location = new System.Drawing.Point(260, cy); this.lblPhone2.AutoSize = true;
            this.txtPhone2.Location = new System.Drawing.Point(360, cy - 2); this.txtPhone2.Size = new System.Drawing.Size(130, 23);
            this.lblPhone3.Text = "Τηλέφωνο 3:"; this.lblPhone3.Location = new System.Drawing.Point(510, cy); this.lblPhone3.AutoSize = true;
            this.txtPhone3.Location = new System.Drawing.Point(610, cy - 2); this.txtPhone3.Size = new System.Drawing.Size(130, 23);
            cy += 35;
            this.lblMobile.Text = "Κινητό 1:"; this.lblMobile.Location = new System.Drawing.Point(15, cy); this.lblMobile.AutoSize = true;
            this.txtMobile.Location = new System.Drawing.Point(110, cy - 2); this.txtMobile.Size = new System.Drawing.Size(130, 23);
            this.lblMobile2.Text = "Κινητό 2:"; this.lblMobile2.Location = new System.Drawing.Point(260, cy); this.lblMobile2.AutoSize = true;
            this.txtMobile2.Location = new System.Drawing.Point(360, cy - 2); this.txtMobile2.Size = new System.Drawing.Size(130, 23);
            cy += 35;
            this.lblEmail.Text = "Email:"; this.lblEmail.Location = new System.Drawing.Point(15, cy); this.lblEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(110, cy - 2); this.txtEmail.Size = new System.Drawing.Size(280, 23);
            cy += 35;
            this.lblWebsite.Text = "Website:"; this.lblWebsite.Location = new System.Drawing.Point(15, cy); this.lblWebsite.AutoSize = true;
            this.txtWebsite.Location = new System.Drawing.Point(110, cy - 2); this.txtWebsite.Size = new System.Drawing.Size(280, 23);
            this.tabContact.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblPhone, this.txtPhone, this.lblPhone2, this.txtPhone2, this.lblPhone3, this.txtPhone3,
                this.lblMobile, this.txtMobile, this.lblMobile2, this.txtMobile2,
                this.lblEmail, this.txtEmail, this.lblWebsite, this.txtWebsite });

            // TAB ΤΡΑΠΕΖΑ (Εταιρεία)
            this.tabBank.Text = "Τράπεζα";
            int byy = 20;
            this.lblBankName.Text = "Τράπεζα:"; this.lblBankName.Location = new System.Drawing.Point(15, byy); this.lblBankName.AutoSize = true;
            this.txtBankName.Location = new System.Drawing.Point(110, byy - 2); this.txtBankName.Size = new System.Drawing.Size(250, 23);
            this.lblBankHolder.Text = "Δικαιούχος:"; this.lblBankHolder.Location = new System.Drawing.Point(390, byy); this.lblBankHolder.AutoSize = true;
            this.txtBankHolder.Location = new System.Drawing.Point(470, byy - 2); this.txtBankHolder.Size = new System.Drawing.Size(270, 23);
            byy += 35;
            this.lblBankIban.Text = "IBAN 1:"; this.lblBankIban.Location = new System.Drawing.Point(15, byy); this.lblBankIban.AutoSize = true;
            this.txtBankIban.Location = new System.Drawing.Point(110, byy - 2); this.txtBankIban.Size = new System.Drawing.Size(280, 23);
            this.lblBankIban2.Text = "IBAN 2:"; this.lblBankIban2.Location = new System.Drawing.Point(410, byy); this.lblBankIban2.AutoSize = true;
            this.txtBankIban2.Location = new System.Drawing.Point(480, byy - 2); this.txtBankIban2.Size = new System.Drawing.Size(260, 23);
            byy += 35;
            this.lblBankIban3.Text = "IBAN 3:"; this.lblBankIban3.Location = new System.Drawing.Point(15, byy); this.lblBankIban3.AutoSize = true;
            this.txtBankIban3.Location = new System.Drawing.Point(110, byy - 2); this.txtBankIban3.Size = new System.Drawing.Size(280, 23);
            this.lblBankIban4.Text = "IBAN 4:"; this.lblBankIban4.Location = new System.Drawing.Point(410, byy); this.lblBankIban4.AutoSize = true;
            this.txtBankIban4.Location = new System.Drawing.Point(480, byy - 2); this.txtBankIban4.Size = new System.Drawing.Size(260, 23);
            byy += 35;
            this.lblBankIban5.Text = "IBAN 5:"; this.lblBankIban5.Location = new System.Drawing.Point(15, byy); this.lblBankIban5.AutoSize = true;
            this.txtBankIban5.Location = new System.Drawing.Point(110, byy - 2); this.txtBankIban5.Size = new System.Drawing.Size(280, 23);
            this.lblBankIban6.Text = "IBAN 6:"; this.lblBankIban6.Location = new System.Drawing.Point(410, byy); this.lblBankIban6.AutoSize = true;
            this.txtBankIban6.Location = new System.Drawing.Point(480, byy - 2); this.txtBankIban6.Size = new System.Drawing.Size(260, 23);
            this.tabBank.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBankName, this.txtBankName, this.lblBankHolder, this.txtBankHolder,
                this.lblBankIban, this.txtBankIban, this.lblBankIban2, this.txtBankIban2,
                this.lblBankIban3, this.txtBankIban3, this.lblBankIban4, this.txtBankIban4,
                this.lblBankIban5, this.txtBankIban5, this.lblBankIban6, this.txtBankIban6 });

            // TAB LOGO (Εταιρεία)
            this.tabLogo.Text = "Logo";
            this.picLogo.Location = new System.Drawing.Point(15, 15); this.picLogo.Size = new System.Drawing.Size(200, 150);
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom; this.picLogo.BackColor = System.Drawing.Color.White;
            this.btnUploadLogo.Text = "📤 Upload Logo"; this.btnUploadLogo.Location = new System.Drawing.Point(240, 20); this.btnUploadLogo.Size = new System.Drawing.Size(140, 40);
            this.btnUploadLogo.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnUploadLogo.ForeColor = System.Drawing.Color.White; this.btnUploadLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadLogo.Click += new System.EventHandler(this.btnUploadLogo_Click);
            this.btnRemoveLogo.Text = "❌ Αφαίρεση"; this.btnRemoveLogo.Location = new System.Drawing.Point(240, 70); this.btnRemoveLogo.Size = new System.Drawing.Size(140, 40);
            this.btnRemoveLogo.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnRemoveLogo.ForeColor = System.Drawing.Color.White; this.btnRemoveLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveLogo.Click += new System.EventHandler(this.btnRemoveLogo_Click);
            this.tabLogo.Controls.AddRange(new System.Windows.Forms.Control[] { this.picLogo, this.btnUploadLogo, this.btnRemoveLogo });

            // Κουμπιά εταιρείας
            int ky = 345;
            this.chkActive.Text = "Ενεργή"; this.chkActive.Location = new System.Drawing.Point(680, 30); this.chkActive.AutoSize = true;
            this.btnNewCompany.Text = "➕ Νέα"; this.btnNewCompany.Location = new System.Drawing.Point(15, ky); this.btnNewCompany.Size = new System.Drawing.Size(90, 35);
            this.btnNewCompany.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnNewCompany.ForeColor = System.Drawing.Color.White; this.btnNewCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCompany.Click += new System.EventHandler(this.btnNewCompany_Click);
            this.btnSaveCompany.Text = "💾 Αποθήκευση"; this.btnSaveCompany.Location = new System.Drawing.Point(115, ky); this.btnSaveCompany.Size = new System.Drawing.Size(120, 35);
            this.btnSaveCompany.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSaveCompany.ForeColor = System.Drawing.Color.White; this.btnSaveCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCompany.Click += new System.EventHandler(this.btnSaveCompany_Click);
            this.btnSetDefault.Text = "⭐ Προεπιλογή"; this.btnSetDefault.Location = new System.Drawing.Point(245, ky); this.btnSetDefault.Size = new System.Drawing.Size(110, 35);
            this.btnSetDefault.BackColor = System.Drawing.Color.FromArgb(243, 156, 18); this.btnSetDefault.ForeColor = System.Drawing.Color.White; this.btnSetDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            this.btnDeleteCompany.Text = "🗑️ Διαγραφή"; this.btnDeleteCompany.Location = new System.Drawing.Point(365, ky); this.btnDeleteCompany.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteCompany.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDeleteCompany.ForeColor = System.Drawing.Color.White; this.btnDeleteCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCompany.Click += new System.EventHandler(this.btnDeleteCompany_Click);

            // ✅ ΝΕΟ: Κουμπί Αποθηκών
            this.btnWarehouses.Text = "📦 Αποθήκες"; this.btnWarehouses.Location = new System.Drawing.Point(475, ky); this.btnWarehouses.Size = new System.Drawing.Size(110, 35);
            this.btnWarehouses.BackColor = System.Drawing.Color.FromArgb(155, 89, 182); this.btnWarehouses.ForeColor = System.Drawing.Color.White; this.btnWarehouses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarehouses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnWarehouses.Click += new System.EventHandler(this.btnWarehouses_Click);

            this.grpCompany.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.tabCompany, this.chkActive,
                this.btnNewCompany, this.btnSaveCompany, this.btnSetDefault, this.btnDeleteCompany, this.btnWarehouses }); // ✅ Προστέθηκε το btnWarehouses

            // ═══════════════════════════════════════
            // ΥΠΟΚΑΤΑΣΤΗΜΑΤΑ (με ΟΛΑ τα πεδία - Tabs)
            // ═══════════════════════════════════════
            this.grpBranches.Text = "Υποκαταστήματα Εταιρείας";
            this.grpBranches.Location = new System.Drawing.Point(20, 470);
            this.grpBranches.Size = new System.Drawing.Size(1140, 400);
            this.grpBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            this.dgvBranches.Location = new System.Drawing.Point(15, 25);
            this.dgvBranches.Size = new System.Drawing.Size(380, 355);
            this.dgvBranches.BackgroundColor = System.Drawing.Color.White;
            this.dgvBranches.RowHeadersVisible = false;
            this.dgvBranches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBranches.AllowUserToAddRows = false;
            this.dgvBranches.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(212, 175, 55);
            this.dgvBranches.SelectionChanged += new System.EventHandler(this.dgvBranches_SelectionChanged);

            this.tabBranch.Location = new System.Drawing.Point(410, 25);
            this.tabBranch.Size = new System.Drawing.Size(715, 310);
            this.tabBranch.Controls.Add(this.tabBranchGeneral);
            this.tabBranch.Controls.Add(this.tabBranchContact);
            this.tabBranch.Controls.Add(this.tabBranchBank);
            this.tabBranch.Controls.Add(this.tabBranchLogo);

            // TAB ΓΕΝΙΚΑ (Υποκατάστημα)
            this.tabBranchGeneral.Text = "Γενικά";
            int bgy = 20;
            this.lblBranchName.Text = "Όνομα:"; this.lblBranchName.Location = new System.Drawing.Point(15, bgy); this.lblBranchName.AutoSize = true;
            this.txtBranchName.Location = new System.Drawing.Point(110, bgy - 2); this.txtBranchName.Size = new System.Drawing.Size(220, 23);
            this.lblBranchCode.Text = "Κωδικός:"; this.lblBranchCode.Location = new System.Drawing.Point(350, bgy); this.lblBranchCode.AutoSize = true;
            this.txtBranchCode.Location = new System.Drawing.Point(420, bgy - 2); this.txtBranchCode.Size = new System.Drawing.Size(120, 23);
            this.chkBranchMain.Text = "Κεντρικό"; this.chkBranchMain.Location = new System.Drawing.Point(560, bgy); this.chkBranchMain.AutoSize = true;
            bgy += 35;
            this.lblBranchVat.Text = "ΑΦΜ:"; this.lblBranchVat.Location = new System.Drawing.Point(15, bgy); this.lblBranchVat.AutoSize = true;
            this.txtBranchVat.Location = new System.Drawing.Point(110, bgy - 2); this.txtBranchVat.Size = new System.Drawing.Size(150, 23);
            this.lblBranchDoy.Text = "ΔΟΥ:"; this.lblBranchDoy.Location = new System.Drawing.Point(280, bgy); this.lblBranchDoy.AutoSize = true;
            this.txtBranchDoy.Location = new System.Drawing.Point(330, bgy - 2); this.txtBranchDoy.Size = new System.Drawing.Size(180, 23);
            bgy += 35;
            this.lblBranchAddress.Text = "Διεύθυνση:"; this.lblBranchAddress.Location = new System.Drawing.Point(15, bgy); this.lblBranchAddress.AutoSize = true;
            this.txtBranchAddress.Location = new System.Drawing.Point(110, bgy - 2); this.txtBranchAddress.Size = new System.Drawing.Size(400, 23);
            bgy += 35;
            this.lblBranchCity.Text = "Πόλη:"; this.lblBranchCity.Location = new System.Drawing.Point(15, bgy); this.lblBranchCity.AutoSize = true;
            this.txtBranchCity.Location = new System.Drawing.Point(110, bgy - 2); this.txtBranchCity.Size = new System.Drawing.Size(180, 23);
            this.lblBranchPostal.Text = "ΤΚ:"; this.lblBranchPostal.Location = new System.Drawing.Point(310, bgy); this.lblBranchPostal.AutoSize = true;
            this.txtBranchPostal.Location = new System.Drawing.Point(350, bgy - 2); this.txtBranchPostal.Size = new System.Drawing.Size(80, 23);
            this.lblBranchManager.Text = "Υπεύθυνος:"; this.lblBranchManager.Location = new System.Drawing.Point(450, bgy); this.lblBranchManager.AutoSize = true;
            this.txtBranchManager.Location = new System.Drawing.Point(530, bgy - 2); this.txtBranchManager.Size = new System.Drawing.Size(160, 23);
            this.tabBranchGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBranchName, this.txtBranchName, this.lblBranchCode, this.txtBranchCode, this.chkBranchMain,
                this.lblBranchVat, this.txtBranchVat, this.lblBranchDoy, this.txtBranchDoy,
                this.lblBranchAddress, this.txtBranchAddress, this.lblBranchCity, this.txtBranchCity,
                this.lblBranchPostal, this.txtBranchPostal, this.lblBranchManager, this.txtBranchManager });

            // TAB ΕΠΙΚΟΙΝΩΝΙΑ (Υποκατάστημα)
            this.tabBranchContact.Text = "Επικοινωνία";
            int bcy = 20;
            this.lblBranchPhone.Text = "Τηλέφωνο 1:"; this.lblBranchPhone.Location = new System.Drawing.Point(15, bcy); this.lblBranchPhone.AutoSize = true;
            this.txtBranchPhone.Location = new System.Drawing.Point(110, bcy - 2); this.txtBranchPhone.Size = new System.Drawing.Size(130, 23);
            this.lblBranchPhone2.Text = "Τηλέφωνο 2:"; this.lblBranchPhone2.Location = new System.Drawing.Point(260, bcy); this.lblBranchPhone2.AutoSize = true;
            this.txtBranchPhone2.Location = new System.Drawing.Point(360, bcy - 2); this.txtBranchPhone2.Size = new System.Drawing.Size(130, 23);
            this.lblBranchPhone3.Text = "Τηλέφωνο 3:"; this.lblBranchPhone3.Location = new System.Drawing.Point(510, bcy); this.lblBranchPhone3.AutoSize = true;
            this.txtBranchPhone3.Location = new System.Drawing.Point(610, bcy - 2); this.txtBranchPhone3.Size = new System.Drawing.Size(90, 23);
            bcy += 35;
            this.lblBranchMobile.Text = "Κινητό 1:"; this.lblBranchMobile.Location = new System.Drawing.Point(15, bcy); this.lblBranchMobile.AutoSize = true;
            this.txtBranchMobile.Location = new System.Drawing.Point(110, bcy - 2); this.txtBranchMobile.Size = new System.Drawing.Size(130, 23);
            this.lblBranchMobile2.Text = "Κινητό 2:"; this.lblBranchMobile2.Location = new System.Drawing.Point(260, bcy); this.lblBranchMobile2.AutoSize = true;
            this.txtBranchMobile2.Location = new System.Drawing.Point(360, bcy - 2); this.txtBranchMobile2.Size = new System.Drawing.Size(130, 23);
            bcy += 35;
            this.lblBranchEmail.Text = "Email:"; this.lblBranchEmail.Location = new System.Drawing.Point(15, bcy); this.lblBranchEmail.AutoSize = true;
            this.txtBranchEmail.Location = new System.Drawing.Point(110, bcy - 2); this.txtBranchEmail.Size = new System.Drawing.Size(280, 23);
            bcy += 35;
            this.lblBranchWebsite.Text = "Website:"; this.lblBranchWebsite.Location = new System.Drawing.Point(15, bcy); this.lblBranchWebsite.AutoSize = true;
            this.txtBranchWebsite.Location = new System.Drawing.Point(110, bcy - 2); this.txtBranchWebsite.Size = new System.Drawing.Size(280, 23);
            this.tabBranchContact.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBranchPhone, this.txtBranchPhone, this.lblBranchPhone2, this.txtBranchPhone2, this.lblBranchPhone3, this.txtBranchPhone3,
                this.lblBranchMobile, this.txtBranchMobile, this.lblBranchMobile2, this.txtBranchMobile2,
                this.lblBranchEmail, this.txtBranchEmail, this.lblBranchWebsite, this.txtBranchWebsite });

            // TAB ΤΡΑΠΕΖΑ (Υποκατάστημα)
            this.tabBranchBank.Text = "Τράπεζα";
            int bby = 20;
            this.lblBranchBankName.Text = "Τράπεζα:"; this.lblBranchBankName.Location = new System.Drawing.Point(15, bby); this.lblBranchBankName.AutoSize = true;
            this.txtBranchBankName.Location = new System.Drawing.Point(110, bby - 2); this.txtBranchBankName.Size = new System.Drawing.Size(250, 23);
            this.lblBranchBankHolder.Text = "Δικαιούχος:"; this.lblBranchBankHolder.Location = new System.Drawing.Point(390, bby); this.lblBranchBankHolder.AutoSize = true;
            this.txtBranchBankHolder.Location = new System.Drawing.Point(470, bby - 2); this.txtBranchBankHolder.Size = new System.Drawing.Size(230, 23);
            bby += 35;
            this.lblBranchIban.Text = "IBAN 1:"; this.lblBranchIban.Location = new System.Drawing.Point(15, bby); this.lblBranchIban.AutoSize = true;
            this.txtBranchIban.Location = new System.Drawing.Point(110, bby - 2); this.txtBranchIban.Size = new System.Drawing.Size(280, 23);
            this.lblBranchIban2.Text = "IBAN 2:"; this.lblBranchIban2.Location = new System.Drawing.Point(410, bby); this.lblBranchIban2.AutoSize = true;
            this.txtBranchIban2.Location = new System.Drawing.Point(480, bby - 2); this.txtBranchIban2.Size = new System.Drawing.Size(220, 23);
            bby += 35;
            this.lblBranchIban3.Text = "IBAN 3:"; this.lblBranchIban3.Location = new System.Drawing.Point(15, bby); this.lblBranchIban3.AutoSize = true;
            this.txtBranchIban3.Location = new System.Drawing.Point(110, bby - 2); this.txtBranchIban3.Size = new System.Drawing.Size(280, 23);
            this.lblBranchIban4.Text = "IBAN 4:"; this.lblBranchIban4.Location = new System.Drawing.Point(410, bby); this.lblBranchIban4.AutoSize = true;
            this.txtBranchIban4.Location = new System.Drawing.Point(480, bby - 2); this.txtBranchIban4.Size = new System.Drawing.Size(220, 23);
            bby += 35;
            this.lblBranchIban5.Text = "IBAN 5:"; this.lblBranchIban5.Location = new System.Drawing.Point(15, bby); this.lblBranchIban5.AutoSize = true;
            this.txtBranchIban5.Location = new System.Drawing.Point(110, bby - 2); this.txtBranchIban5.Size = new System.Drawing.Size(280, 23);
            this.lblBranchIban6.Text = "IBAN 6:"; this.lblBranchIban6.Location = new System.Drawing.Point(410, bby); this.lblBranchIban6.AutoSize = true;
            this.txtBranchIban6.Location = new System.Drawing.Point(480, bby - 2); this.txtBranchIban6.Size = new System.Drawing.Size(220, 23);
            this.tabBranchBank.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBranchBankName, this.txtBranchBankName, this.lblBranchBankHolder, this.txtBranchBankHolder,
                this.lblBranchIban, this.txtBranchIban, this.lblBranchIban2, this.txtBranchIban2,
                this.lblBranchIban3, this.txtBranchIban3, this.lblBranchIban4, this.txtBranchIban4,
                this.lblBranchIban5, this.txtBranchIban5, this.lblBranchIban6, this.txtBranchIban6 });

            // TAB LOGO (Υποκατάστημα)
            this.tabBranchLogo.Text = "Logo";
            this.picBranchLogo.Location = new System.Drawing.Point(15, 15); this.picBranchLogo.Size = new System.Drawing.Size(200, 150);
            this.picBranchLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.picBranchLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom; this.picBranchLogo.BackColor = System.Drawing.Color.White;
            this.btnUploadBranchLogo.Text = "📤 Upload Logo"; this.btnUploadBranchLogo.Location = new System.Drawing.Point(240, 20); this.btnUploadBranchLogo.Size = new System.Drawing.Size(140, 40);
            this.btnUploadBranchLogo.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnUploadBranchLogo.ForeColor = System.Drawing.Color.White; this.btnUploadBranchLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadBranchLogo.Click += new System.EventHandler(this.btnUploadBranchLogo_Click);
            this.btnRemoveBranchLogo.Text = "❌ Αφαίρεση"; this.btnRemoveBranchLogo.Location = new System.Drawing.Point(240, 70); this.btnRemoveBranchLogo.Size = new System.Drawing.Size(140, 40);
            this.btnRemoveBranchLogo.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnRemoveBranchLogo.ForeColor = System.Drawing.Color.White; this.btnRemoveBranchLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveBranchLogo.Click += new System.EventHandler(this.btnRemoveBranchLogo_Click);
            this.tabBranchLogo.Controls.AddRange(new System.Windows.Forms.Control[] { this.picBranchLogo, this.btnUploadBranchLogo, this.btnRemoveBranchLogo });

            // Κουμπιά υποκαταστημάτων
            int bky = 345;
            this.btnNewBranch.Text = "➕ Νέο"; this.btnNewBranch.Location = new System.Drawing.Point(410, bky); this.btnNewBranch.Size = new System.Drawing.Size(90, 35);
            this.btnNewBranch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnNewBranch.ForeColor = System.Drawing.Color.White; this.btnNewBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBranch.Click += new System.EventHandler(this.btnNewBranch_Click);
            this.btnSaveBranch.Text = "💾 Αποθήκευση"; this.btnSaveBranch.Location = new System.Drawing.Point(510, bky); this.btnSaveBranch.Size = new System.Drawing.Size(120, 35);
            this.btnSaveBranch.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnSaveBranch.ForeColor = System.Drawing.Color.White; this.btnSaveBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveBranch.Click += new System.EventHandler(this.btnSaveBranch_Click);
            this.btnDeleteBranch.Text = "🗑️ Διαγραφή"; this.btnDeleteBranch.Location = new System.Drawing.Point(640, bky); this.btnDeleteBranch.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteBranch.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDeleteBranch.ForeColor = System.Drawing.Color.White; this.btnDeleteBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBranch.Click += new System.EventHandler(this.btnDeleteBranch_Click);

            this.grpBranches.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvBranches, this.tabBranch,
                this.btnNewBranch, this.btnSaveBranch, this.btnDeleteBranch });

            // btnClose
            this.btnClose.Text = "❌ Κλείσιμο"; this.btnClose.Location = new System.Drawing.Point(1020, 885); this.btnClose.Size = new System.Drawing.Size(140, 40);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); this.btnClose.ForeColor = System.Drawing.Color.White; this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 940);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvCompanies);
            this.Controls.Add(this.grpCompany);
            this.Controls.Add(this.grpBranches);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CompaniesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Εταιρείες & Υποκαταστήματα";

            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanies)).EndInit();
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.tabCompany.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.grpBranches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).EndInit();
            this.tabBranch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBranchLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvCompanies;
        private System.Windows.Forms.GroupBox grpCompany;
        private System.Windows.Forms.TabControl tabCompany;
        private System.Windows.Forms.TabPage tabGeneral, tabContact, tabBank, tabLogo;
        private System.Windows.Forms.Label lblCompanyName, lblTradeName, lblVatNumber, lblDoy, lblAddress, lblCity, lblPostalCode;
        private System.Windows.Forms.TextBox txtCompanyName, txtTradeName, txtVatNumber, txtDoy, txtAddress, txtCity, txtPostalCode;
        private System.Windows.Forms.Label lblPhone, lblPhone2, lblPhone3, lblMobile, lblMobile2, lblEmail, lblWebsite;
        private System.Windows.Forms.TextBox txtPhone, txtPhone2, txtPhone3, txtMobile, txtMobile2, txtEmail, txtWebsite;
        private System.Windows.Forms.Label lblBankName, lblBankHolder, lblBankIban, lblBankIban2, lblBankIban3, lblBankIban4, lblBankIban5, lblBankIban6;
        private System.Windows.Forms.TextBox txtBankName, txtBankHolder, txtBankIban, txtBankIban2, txtBankIban3, txtBankIban4, txtBankIban5, txtBankIban6;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnUploadLogo, btnRemoveLogo;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnNewCompany, btnSaveCompany, btnSetDefault, btnDeleteCompany, btnWarehouses; // ✅ Προστέθηκε το btnWarehouses

        private System.Windows.Forms.GroupBox grpBranches;
        private System.Windows.Forms.DataGridView dgvBranches;
        private System.Windows.Forms.TabControl tabBranch;
        private System.Windows.Forms.TabPage tabBranchGeneral, tabBranchContact, tabBranchBank, tabBranchLogo;
        private System.Windows.Forms.Label lblBranchName, lblBranchCode, lblBranchVat, lblBranchDoy, lblBranchAddress, lblBranchCity, lblBranchPostal, lblBranchManager;
        private System.Windows.Forms.TextBox txtBranchName, txtBranchCode, txtBranchVat, txtBranchDoy, txtBranchAddress, txtBranchCity, txtBranchPostal, txtBranchManager;
        private System.Windows.Forms.Label lblBranchPhone, lblBranchPhone2, lblBranchPhone3, lblBranchMobile, lblBranchMobile2, lblBranchEmail, lblBranchWebsite;
        private System.Windows.Forms.TextBox txtBranchPhone, txtBranchPhone2, txtBranchPhone3, txtBranchMobile, txtBranchMobile2, txtBranchEmail, txtBranchWebsite;
        private System.Windows.Forms.Label lblBranchBankName, lblBranchBankHolder, lblBranchIban, lblBranchIban2, lblBranchIban3, lblBranchIban4, lblBranchIban5, lblBranchIban6;
        private System.Windows.Forms.TextBox txtBranchBankName, txtBranchBankHolder, txtBranchIban, txtBranchIban2, txtBranchIban3, txtBranchIban4, txtBranchIban5, txtBranchIban6;
        private System.Windows.Forms.PictureBox picBranchLogo;
        private System.Windows.Forms.Button btnUploadBranchLogo, btnRemoveBranchLogo;
        private System.Windows.Forms.CheckBox chkBranchMain;
        private System.Windows.Forms.Button btnNewBranch, btnSaveBranch, btnDeleteBranch;
        private System.Windows.Forms.Button btnClose;
    }
}