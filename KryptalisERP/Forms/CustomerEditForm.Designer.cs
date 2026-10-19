namespace KryptalisERP
{
    partial class CustomerEditForm
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabBasics = new System.Windows.Forms.TabPage();
            this.tabBranches = new System.Windows.Forms.TabPage();
            this.tabContacts = new System.Windows.Forms.TabPage();
            this.tabActivities = new System.Windows.Forms.TabPage();
            this.tabCredit = new System.Windows.Forms.TabPage();
            this.tabPriceZones = new System.Windows.Forms.TabPage();

            // --- BASICS CONTROLS ---
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.grpAddress = new System.Windows.Forms.GroupBox();
            this.grpBusiness = new System.Windows.Forms.GroupBox();
            this.grpB2G = new System.Windows.Forms.GroupBox(); // ✅ NEW GROUP FOR PUBLIC ENTITY

            this.lblCompany = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();

            this.lblAfm = new System.Windows.Forms.Label();
            this.txtAfm = new System.Windows.Forms.TextBox();
            this.btnSearchAfm = new System.Windows.Forms.Button();
            this.lblDoy = new System.Windows.Forms.Label();
            this.cmbDoy = new System.Windows.Forms.ComboBox();
            this.btnEditDoy = new System.Windows.Forms.Button();

            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();

            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.lblTK = new System.Windows.Forms.Label();
            this.cmbTK = new System.Windows.Forms.ComboBox();
            this.btnEditTK = new System.Windows.Forms.Button();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.btnEditCountry = new System.Windows.Forms.Button();

            this.lblProfession = new System.Windows.Forms.Label();
            this.cmbProfession = new System.Windows.Forms.ComboBox();
            this.btnEditProfession = new System.Windows.Forms.Button();
            this.lblClientType = new System.Windows.Forms.Label();
            this.cmbClientType = new System.Windows.Forms.ComboBox();
            this.btnEditClientType = new System.Windows.Forms.Button();
            this.lblVatExemption = new System.Windows.Forms.Label();
            this.cmbVatExemption = new System.Windows.Forms.ComboBox();
            this.lblTags = new System.Windows.Forms.Label();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.chkIsAlsoSupplier = new System.Windows.Forms.CheckBox();

            // ✅ B2G FIELD INSIDE GROUPBOX
            this.lblAnathetousaArchi = new System.Windows.Forms.Label();
            this.txtContractingAuthority = new System.Windows.Forms.TextBox();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // Sub-tabs controls (same as before)
            this.dgvBranches = new System.Windows.Forms.DataGridView();
            this.btnAddBranch = new System.Windows.Forms.Button();
            this.btnEditBranch = new System.Windows.Forms.Button();
            this.btnDeleteBranch = new System.Windows.Forms.Button();
            this.dgvContacts = new System.Windows.Forms.DataGridView();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnEditContact = new System.Windows.Forms.Button();
            this.btnDeleteContact = new System.Windows.Forms.Button();
            this.dgvActivities = new System.Windows.Forms.DataGridView();
            this.btnAddActivity = new System.Windows.Forms.Button();
            this.btnEditActivity = new System.Windows.Forms.Button();
            this.btnDeleteActivity = new System.Windows.Forms.Button();
            this.dgvCredit = new System.Windows.Forms.DataGridView();
            this.btnAddCredit = new System.Windows.Forms.Button();
            this.btnEditCredit = new System.Windows.Forms.Button();
            this.btnDeleteCredit = new System.Windows.Forms.Button();
            this.dgvPriceZones = new System.Windows.Forms.DataGridView();
            this.btnAddPriceZone = new System.Windows.Forms.Button();
            this.btnEditPriceZone = new System.Windows.Forms.Button();
            this.btnDeletePriceZone = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ========================================
            // TAB CONTROL
            // ========================================
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(950, 680);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.Controls.Add(this.tabBasics);
            this.tabControlMain.Controls.Add(this.tabBranches);
            this.tabControlMain.Controls.Add(this.tabContacts);
            this.tabControlMain.Controls.Add(this.tabActivities);
            this.tabControlMain.Controls.Add(this.tabCredit);
            this.tabControlMain.Controls.Add(this.tabPriceZones);

            // ========================================
            // TAB BASICS - COMPACT LAYOUT WITH GROUPBOXES
            // ========================================
            this.tabBasics.Text = "📋 Βασικά Στοιχεία";
            this.tabBasics.Padding = new System.Windows.Forms.Padding(5);
            this.tabBasics.AutoScroll = true;

            int y = 10;
            int labelW = 100;
            int inputH = 26;
            int gapY = 8;

            // --- GROUP 1: GENERAL INFO (Top Left) ---
            this.grpGeneral.Text = "Γενικά Στοιχεία";
            this.grpGeneral.Location = new System.Drawing.Point(10, y);
            this.grpGeneral.Size = new System.Drawing.Size(450, 140);

            int gy = 25;
            SetLabel(lblCompany, "Επωνυμία:", 10, gy, grpGeneral); SetInput(txtCompany, 110, gy, 320, grpGeneral); gy += 30;
            SetLabel(lblFirstName, "Όνομα:", 10, gy, grpGeneral); SetInput(txtFirstName, 110, gy, 150, grpGeneral);
            SetLabel(lblLastName, "Επίθετο:", 270, gy, grpGeneral); SetInput(txtLastName, 340, gy, 90, grpGeneral); gy += 30;

            SetLabel(lblAfm, "ΑΦΜ:", 10, gy, grpGeneral); SetInput(txtAfm, 110, gy, 100, grpGeneral);
            btnSearchAfm.Text = "✓"; btnSearchAfm.Location = new System.Drawing.Point(215, gy); btnSearchAfm.Size = new System.Drawing.Size(30, inputH);
            btnSearchAfm.Click += new System.EventHandler(this.btnSearchAfm_Click);
            grpGeneral.Controls.Add(btnSearchAfm);

            SetLabel(lblDoy, "ΔΟΥ:", 270, gy, grpGeneral);
            cmbDoy.Location = new System.Drawing.Point(340, gy); cmbDoy.Size = new System.Drawing.Size(90, inputH);
            btnEditDoy.Text = "+"; btnEditDoy.Location = new System.Drawing.Point(435, gy); btnEditDoy.Size = new System.Drawing.Size(30, inputH);
            btnEditDoy.Click += new System.EventHandler(this.btnEditDoy_Click);
            grpGeneral.Controls.Add(btnEditDoy);

            tabBasics.Controls.Add(grpGeneral);

            // --- GROUP 2: CONTACT INFO (Top Right) ---
            this.grpAddress.Text = "Επικοινωνία & Διεύθυνση"; // Renamed for clarity
            this.grpAddress.Location = new System.Drawing.Point(470, y);
            this.grpAddress.Size = new System.Drawing.Size(460, 140);

            gy = 25;
            SetLabel(lblPhone, "Τηλέφωνο:", 10, gy, grpAddress); SetInput(txtPhone, 100, gy, 100, grpAddress);
            SetLabel(lblMobile, "Κινητό:", 220, gy, grpAddress); SetInput(txtMobile, 290, gy, 100, grpAddress); gy += 30;

            SetLabel(lblEmail, "Email:", 10, gy, grpAddress); SetInput(txtEmail, 100, gy, 340, grpAddress); gy += 30;
            SetLabel(lblWebsite, "Site:", 10, gy, grpAddress); SetInput(txtWebsite, 100, gy, 340, grpAddress);

            tabBasics.Controls.Add(grpAddress);

            y += 150;

            // --- GROUP 3: ADDRESS DETAILS (Middle Full Width) ---
            this.grpAddress.Text = "Διεύθυνση"; // Reusing variable name logic, but let's make a new group visually or reuse space
                                                // Actually, let's put Address in the second row full width or split.
                                                // Let's create a dedicated Address Group below General/Contact

            var grpAddrFull = new System.Windows.Forms.GroupBox { Text = "Διεύθυνση", Location = new System.Drawing.Point(10, y), Size = new System.Drawing.Size(920, 80) };
            int ay = 25;
            SetLabel(lblAddress, "Οδός:", 10, ay, grpAddrFull); SetInput(txtAddress, 60, ay, 400, grpAddrFull);
            SetLabel(lblCity, "Πόλη:", 480, ay, grpAddrFull); SetInput(txtCity, 530, ay, 120, grpAddrFull);
            SetLabel(lblArea, "Περιοχή:", 670, ay, grpAddrFull); SetInput(txtArea, 740, ay, 160, grpAddrFull); ay += 30;

            SetLabel(lblTK, "Τ.Κ.:", 10, ay, grpAddrFull);
            cmbTK.Location = new System.Drawing.Point(60, ay); cmbTK.Size = new System.Drawing.Size(80, inputH);
            btnEditTK.Text = "+"; btnEditTK.Location = new System.Drawing.Point(145, ay); btnEditTK.Size = new System.Drawing.Size(30, inputH);
            btnEditTK.Click += new System.EventHandler(this.btnEditTK_Click);
            grpAddrFull.Controls.Add(btnEditTK);

            SetLabel(lblCountry, "Χώρα:", 200, ay, grpAddrFull);
            cmbCountry.Location = new System.Drawing.Point(260, ay); cmbCountry.Size = new System.Drawing.Size(120, inputH);
            btnEditCountry.Text = "+"; btnEditCountry.Location = new System.Drawing.Point(385, ay); btnEditCountry.Size = new System.Drawing.Size(30, inputH);
            btnEditCountry.Click += new System.EventHandler(this.btnEditCountry_Click);
            grpAddrFull.Controls.Add(btnEditCountry);

            tabBasics.Controls.Add(grpAddrFull);
            y += 90;

            // --- GROUP 4: BUSINESS & TAX (Bottom Left) ---
            this.grpBusiness.Text = "Επαγγελματικά & Φορολογικά";
            this.grpBusiness.Location = new System.Drawing.Point(10, y);
            this.grpBusiness.Size = new System.Drawing.Size(550, 110);

            int by = 25;
            SetLabel(lblProfession, "Επάγγελμα:", 10, by, grpBusiness);
            cmbProfession.Location = new System.Drawing.Point(100, by); cmbProfession.Size = new System.Drawing.Size(150, inputH);
            btnEditProfession.Text = "+"; btnEditProfession.Location = new System.Drawing.Point(255, by); btnEditProfession.Size = new System.Drawing.Size(30, inputH);
            btnEditProfession.Click += new System.EventHandler(this.btnEditProfession_Click);
            grpBusiness.Controls.Add(btnEditProfession);

            SetLabel(lblClientType, "Τύπος:", 300, by, grpBusiness);
            cmbClientType.Location = new System.Drawing.Point(360, by); cmbClientType.Size = new System.Drawing.Size(120, inputH);
            btnEditClientType.Text = "+"; btnEditClientType.Location = new System.Drawing.Point(485, by); btnEditClientType.Size = new System.Drawing.Size(30, inputH);
            btnEditClientType.Click += new System.EventHandler(this.btnEditClientType_Click);
            grpBusiness.Controls.Add(btnEditClientType);
            by += 30;

            SetLabel(lblVatExemption, "ΦΠΑ Απαλλαγή:", 10, by, grpBusiness);
            cmbVatExemption.Location = new System.Drawing.Point(120, by); cmbVatExemption.Size = new System.Drawing.Size(200, inputH);

            SetLabel(lblTags, "Tags:", 340, by, grpBusiness); SetInput(txtTags, 390, by, 140, grpBusiness);

            tabBasics.Controls.Add(grpBusiness);

            // --- GROUP 5: B2G / PUBLIC ENTITY (Bottom Right) - CLEANED ---
            this.grpB2G.Text = "🏛️ Δημόσιος Φορέας (B2G)";
            this.grpB2G.Location = new System.Drawing.Point(570, y);
            this.grpB2G.Size = new System.Drawing.Size(360, 110);
            this.grpB2G.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80); // Dark Blue text for header

            int b2gy = 30;
            SetLabel(lblAnathetousaArchi, "Αναθέτουσα Αρχή:", 10, b2gy, grpB2G);
            SetInput(txtContractingAuthority, 130, b2gy, 210, grpB2G);

            // Checkboxes inside B2G or nearby? Let's put flags here too for context
            chkIsActive.Text = "Ενεργός"; chkIsActive.Location = new System.Drawing.Point(10, 70); chkIsActive.AutoSize = true;
            grpB2G.Controls.Add(chkIsActive);

            chkIsAlsoSupplier.Text = "Και Προμηθευτής"; chkIsAlsoSupplier.Location = new System.Drawing.Point(120, 70); chkIsAlsoSupplier.AutoSize = true;
            grpB2G.Controls.Add(chkIsAlsoSupplier);

            tabBasics.Controls.Add(grpB2G);
            y += 120;

            // --- NOTES (Full Width Bottom) ---
            SetLabel(lblNotes, "Σημειώσεις:", 10, y, tabBasics);
            txtNotes.Location = new System.Drawing.Point(100, y);
            txtNotes.Size = new System.Drawing.Size(830, 60);
            txtNotes.Multiline = true;
            txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            tabBasics.Controls.Add(lblNotes);
            tabBasics.Controls.Add(txtNotes);


            // ========================================
            // SUB-TABS SETUP (Optimized Button Bar)
            // ========================================
            void SetupSubTab(System.Windows.Forms.TabPage tab, string title, System.Windows.Forms.DataGridView dgv,
                             System.Windows.Forms.Button btnAdd, System.Windows.Forms.Button btnEdit, System.Windows.Forms.Button btnDel,
                             System.EventHandler addClick, System.EventHandler editClick, System.EventHandler delClick)
            {
                tab.Text = title;
                tab.Padding = new System.Windows.Forms.Padding(0); // Remove padding to maximize grid space

                // Grid fills everything
                dgv.Dock = System.Windows.Forms.DockStyle.Fill;
                dgv.AllowUserToAddRows = false;
                dgv.RowHeadersVisible = false;
                dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                dgv.BackgroundColor = System.Drawing.Color.White;
                dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;

                // Slim Button Panel at Top
                var pnlBtns = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Top, Height = 35, BackColor = System.Drawing.Color.FromArgb(240, 240, 240) };

                btnAdd.Text = "➕ Νέο"; btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); btnAdd.ForeColor = System.Drawing.Color.White;
                btnAdd.Location = new System.Drawing.Point(5, 3); btnAdd.Size = new System.Drawing.Size(80, 28); btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnAdd.FlatAppearance.BorderSize = 0;
                btnAdd.Click += addClick;

                btnEdit.Text = "✏️ Επεξ."; btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); btnEdit.ForeColor = System.Drawing.Color.White;
                btnEdit.Location = new System.Drawing.Point(90, 3); btnEdit.Size = new System.Drawing.Size(80, 28); btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnEdit.FlatAppearance.BorderSize = 0;
                btnEdit.Click += editClick;

                btnDel.Text = "❌ Διαγρ."; btnDel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); btnDel.ForeColor = System.Drawing.Color.White;
                btnDel.Location = new System.Drawing.Point(175, 3); btnDel.Size = new System.Drawing.Size(80, 28); btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnDel.FlatAppearance.BorderSize = 0;
                btnDel.Click += delClick;

                pnlBtns.Controls.AddRange(new System.Windows.Forms.Control[] { btnAdd, btnEdit, btnDel });

                tab.Controls.Add(dgv);
                tab.Controls.Add(pnlBtns);
            }

            SetupSubTab(tabBranches, "🏢 Υποκαταστήματα", dgvBranches, btnAddBranch, btnEditBranch, btnDeleteBranch,
                        new System.EventHandler(this.btnAddBranch_Click), new System.EventHandler(this.btnEditBranch_Click), new System.EventHandler(this.btnDeleteBranch_Click));

            SetupSubTab(tabContacts, "👥 Επαφές", dgvContacts, btnAddContact, btnEditContact, btnDeleteContact,
                        new System.EventHandler(this.btnAddContact_Click), new System.EventHandler(this.btnEditContact_Click), new System.EventHandler(this.btnDeleteContact_Click));

            SetupSubTab(tabActivities, "📅 Δραστηριότητες", dgvActivities, btnAddActivity, btnEditActivity, btnDeleteActivity,
                        new System.EventHandler(this.btnAddActivity_Click), new System.EventHandler(this.btnEditActivity_Click), new System.EventHandler(this.btnDeleteActivity_Click));

            SetupSubTab(tabCredit, "💳 Πιστωτικά", dgvCredit, btnAddCredit, btnEditCredit, btnDeleteCredit,
                        new System.EventHandler(this.btnAddCredit_Click), new System.EventHandler(this.btnEditCredit_Click), new System.EventHandler(this.btnDeleteCredit_Click));

            SetupSubTab(tabPriceZones, "🏷️ Τιμοκατάλογοι", dgvPriceZones, btnAddPriceZone, btnEditPriceZone, btnDeletePriceZone,
                        new System.EventHandler(this.btnAddPriceZone_Click), new System.EventHandler(this.btnEditPriceZone_Click), new System.EventHandler(this.btnDeletePriceZone_Click));

            // ========================================
            // MAIN BUTTONS (Docked Bottom)
            // ========================================
            var pnlFooter = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Bottom, Height = 50, BackColor = System.Drawing.Color.White, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };

            btnSave.Text = "💾 Αποθήκευση";
            btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            btnSave.Width = 120;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += new System.EventHandler(this.btnSave_Click);

            btnCancel.Text = "❌ Άκυρο";
            btnCancel.BackColor = System.Drawing.Color.Gray;
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            btnCancel.Width = 100;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Controls.Add(btnCancel);

            // ========================================
            // FORM ASSEMBLY
            // ========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 730); // Slightly taller to fit footer + tabs
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(pnlFooter);
            this.Name = "CustomerEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Επεξεργασία Πελάτη";

            this.tabControlMain.ResumeLayout(false);
            this.tabBasics.ResumeLayout(false);
            this.tabBasics.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpAddress.ResumeLayout(false);
            this.grpAddress.PerformLayout();
            this.grpBusiness.ResumeLayout(false);
            this.grpBusiness.PerformLayout();
            this.grpB2G.ResumeLayout(false);
            this.grpB2G.PerformLayout();
            this.ResumeLayout(false);
        }

        private void SetLabel(System.Windows.Forms.Label lbl, string text, int x, int y, System.Windows.Forms.Control parent)
        {
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y + 3);
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            parent.Controls.Add(lbl);
        }

        private void SetInput(System.Windows.Forms.TextBox txt, int x, int y, int w, System.Windows.Forms.Control parent)
        {
            txt.Location = new System.Drawing.Point(x, y);
            txt.Size = new System.Drawing.Size(w, 24);
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            parent.Controls.Add(txt);
        }

        #endregion

        // Fields
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabBasics;
        private System.Windows.Forms.TabPage tabBranches;
        private System.Windows.Forms.TabPage tabContacts;
        private System.Windows.Forms.TabPage tabActivities;
        private System.Windows.Forms.TabPage tabCredit;
        private System.Windows.Forms.TabPage tabPriceZones;

        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.GroupBox grpAddress;
        private System.Windows.Forms.GroupBox grpBusiness;
        private System.Windows.Forms.GroupBox grpB2G;

        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblAfm;
        private System.Windows.Forms.TextBox txtAfm;
        private System.Windows.Forms.Button btnSearchAfm;
        private System.Windows.Forms.Label lblDoy;
        private System.Windows.Forms.ComboBox cmbDoy;
        private System.Windows.Forms.Button btnEditDoy;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label lblTK;
        private System.Windows.Forms.ComboBox cmbTK;
        private System.Windows.Forms.Button btnEditTK;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Button btnEditCountry;
        private System.Windows.Forms.Label lblProfession;
        private System.Windows.Forms.ComboBox cmbProfession;
        private System.Windows.Forms.Button btnEditProfession;
        private System.Windows.Forms.Label lblClientType;
        private System.Windows.Forms.ComboBox cmbClientType;
        private System.Windows.Forms.Button btnEditClientType;
        private System.Windows.Forms.Label lblVatExemption;
        private System.Windows.Forms.ComboBox cmbVatExemption;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.CheckBox chkIsAlsoSupplier;

        private System.Windows.Forms.Label lblAnathetousaArchi;
        private System.Windows.Forms.TextBox txtContractingAuthority;

        private System.Windows.Forms.DataGridView dgvBranches;
        private System.Windows.Forms.Button btnAddBranch;
        private System.Windows.Forms.Button btnEditBranch;
        private System.Windows.Forms.Button btnDeleteBranch;
        private System.Windows.Forms.DataGridView dgvContacts;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Button btnEditContact;
        private System.Windows.Forms.Button btnDeleteContact;
        private System.Windows.Forms.DataGridView dgvActivities;
        private System.Windows.Forms.Button btnAddActivity;
        private System.Windows.Forms.Button btnEditActivity;
        private System.Windows.Forms.Button btnDeleteActivity;
        private System.Windows.Forms.DataGridView dgvCredit;
        private System.Windows.Forms.Button btnAddCredit;
        private System.Windows.Forms.Button btnEditCredit;
        private System.Windows.Forms.Button btnDeleteCredit;
        private System.Windows.Forms.DataGridView dgvPriceZones;
        private System.Windows.Forms.Button btnAddPriceZone;
        private System.Windows.Forms.Button btnEditPriceZone;
        private System.Windows.Forms.Button btnDeletePriceZone;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}