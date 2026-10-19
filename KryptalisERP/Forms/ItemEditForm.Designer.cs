namespace KryptalisERP
{
    partial class ItemEditForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.tabPrices = new System.Windows.Forms.TabPage();
            this.tabStock = new System.Windows.Forms.TabPage();
            this.tabBarcodes = new System.Windows.Forms.TabPage();
            this.tabFeatures = new System.Windows.Forms.TabPage();
            this.tabSupplier = new System.Windows.Forms.TabPage();
            this.tabMyData = new System.Windows.Forms.TabPage();
            this.tabTracking = new System.Windows.Forms.TabPage();
            this.tabNotes = new System.Windows.Forms.TabPage();
            this.tabLots = new System.Windows.Forms.TabPage();
            this.tabVariants = new System.Windows.Forms.TabPage();
            this.tabBarcodesMultiple = new System.Windows.Forms.TabPage();
            this.tabUnitRelations = new System.Windows.Forms.TabPage();
            this.tabWarehouseStock = new System.Windows.Forms.TabPage();
            this.tabImages = new System.Windows.Forms.TabPage();
            this.tabItemSuppliers = new System.Windows.Forms.TabPage();
            this.tabRelated = new System.Windows.Forms.TabPage();
            this.tabPriceHistory = new System.Windows.Forms.TabPage();
            this.tabItemAttributes = new System.Windows.Forms.TabPage();
            this.tabAlternativeCodes = new System.Windows.Forms.TabPage();
            this.tabColors = new System.Windows.Forms.TabPage();
            this.tabCPVCodes = new System.Windows.Forms.TabPage();
            this.tabSerialNumbers = new System.Windows.Forms.TabPage();

            // Controls - Tabs 1-11
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtShortDescription = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.chkIsFavorite = new System.Windows.Forms.CheckBox();
            this.chkIsFeatured = new System.Windows.Forms.CheckBox();
            this.numPurchasePrice = new System.Windows.Forms.NumericUpDown();
            this.numSalePrice = new System.Windows.Forms.NumericUpDown();
            this.numWholesalePrice = new System.Windows.Forms.NumericUpDown();
            this.numCostPrice = new System.Windows.Forms.NumericUpDown();
            this.cmbVatRate = new System.Windows.Forms.ComboBox();
            this.numDiscountPercent = new System.Windows.Forms.NumericUpDown();
            this.numDiscountAmount = new System.Windows.Forms.NumericUpDown();
            this.numStock = new System.Windows.Forms.NumericUpDown();
            this.numMinStock = new System.Windows.Forms.NumericUpDown();
            this.numMaxStock = new System.Windows.Forms.NumericUpDown();
            this.numReorderPoint = new System.Windows.Forms.NumericUpDown();
            this.numSafetyStock = new System.Windows.Forms.NumericUpDown();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.txtAlternativeCode = new System.Windows.Forms.TextBox();
            this.txtCPVCode = new System.Windows.Forms.TextBox();
            this.txtCPVDescription = new System.Windows.Forms.TextBox();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.numWeight = new System.Windows.Forms.NumericUpDown();
            this.numVolume = new System.Windows.Forms.NumericUpDown();
            this.txtDimensions = new System.Windows.Forms.TextBox();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.txtProviderURL = new System.Windows.Forms.TextBox();
            this.txtMydataCategory = new System.Windows.Forms.TextBox();
            this.txtMydataCode = new System.Windows.Forms.TextBox();
            this.txtExemptionArticle = new System.Windows.Forms.TextBox();
            this.txtExemptionReason = new System.Windows.Forms.TextBox();
            this.txtVatArticle = new System.Windows.Forms.TextBox();
            this.txtTARICCode = new System.Windows.Forms.TextBox();
            this.txtCountryOfOrigin = new System.Windows.Forms.TextBox();
            this.chkLotTracking = new System.Windows.Forms.CheckBox();
            this.chkSerialTracking = new System.Windows.Forms.CheckBox();
            this.chkExpiryTracking = new System.Windows.Forms.CheckBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtLongDescription = new System.Windows.Forms.TextBox();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.dgvLots = new System.Windows.Forms.DataGridView();
            this.btnAddLot = new System.Windows.Forms.Button();
            this.btnEditLot = new System.Windows.Forms.Button();
            this.btnDeleteLot = new System.Windows.Forms.Button();
            this.dgvVariants = new System.Windows.Forms.DataGridView();
            this.btnAddVariant = new System.Windows.Forms.Button();
            this.btnEditVariant = new System.Windows.Forms.Button();
            this.btnDeleteVariant = new System.Windows.Forms.Button();

            // Controls - Tabs 12-23 (Ενσωματωμένα)
            this.dgvBarcodesMultiple = new System.Windows.Forms.DataGridView();
            this.txtBarcodeCode = new System.Windows.Forms.TextBox();
            this.cmbBarcodeType = new System.Windows.Forms.ComboBox();
            this.txtBarcodeDesc = new System.Windows.Forms.TextBox();
            this.btnAddBarcode = new System.Windows.Forms.Button();
            this.btnEditBarcode = new System.Windows.Forms.Button();
            this.btnDeleteBarcode = new System.Windows.Forms.Button();

            this.dgvUnitRelations = new System.Windows.Forms.DataGridView();
            this.cmbUnitFrom = new System.Windows.Forms.ComboBox();
            this.numUnitCoeff = new System.Windows.Forms.NumericUpDown();
            this.btnAddUnit = new System.Windows.Forms.Button();
            this.btnDeleteUnit = new System.Windows.Forms.Button();

            this.dgvWarehouseStock = new System.Windows.Forms.DataGridView();
            this.cmbStockWarehouse = new System.Windows.Forms.ComboBox();
            this.cmbStockVariant = new System.Windows.Forms.ComboBox();
            this.numStockQty = new System.Windows.Forms.NumericUpDown();
            this.btnAddStock = new System.Windows.Forms.Button();
            this.btnDeleteStock = new System.Windows.Forms.Button();

            this.btnManageImages = new System.Windows.Forms.Button();

            this.dgvItemSuppliers = new System.Windows.Forms.DataGridView();
            this.cmbItemSupplier = new System.Windows.Forms.ComboBox();
            this.txtItemSupplierCode = new System.Windows.Forms.TextBox();
            this.numItemSupplierPrice = new System.Windows.Forms.NumericUpDown();
            this.btnAddItemSupplier = new System.Windows.Forms.Button();
            this.btnDeleteItemSupplier = new System.Windows.Forms.Button();

            this.dgvRelated = new System.Windows.Forms.DataGridView();
            this.txtRelatedCode = new System.Windows.Forms.TextBox();
            this.cmbRelatedType = new System.Windows.Forms.ComboBox();
            this.numRelatedQty = new System.Windows.Forms.NumericUpDown();
            this.btnAddRelated = new System.Windows.Forms.Button();
            this.btnDeleteRelated = new System.Windows.Forms.Button();

            this.dgvPriceHistory = new System.Windows.Forms.DataGridView();

            this.dgvItemAttributes = new System.Windows.Forms.DataGridView();
            this.txtAttrName = new System.Windows.Forms.TextBox();
            this.txtAttrValue = new System.Windows.Forms.TextBox();
            this.cmbAttrType = new System.Windows.Forms.ComboBox();
            this.btnAddAttr = new System.Windows.Forms.Button();
            this.btnDeleteAttr = new System.Windows.Forms.Button();

            this.dgvAlternativeCodes = new System.Windows.Forms.DataGridView();
            this.txtAltCode = new System.Windows.Forms.TextBox();
            this.txtAltDesc = new System.Windows.Forms.TextBox();
            this.btnAddAlt = new System.Windows.Forms.Button();
            this.btnDeleteAlt = new System.Windows.Forms.Button();

            this.dgvItemColors = new System.Windows.Forms.DataGridView();
            this.txtColorCode = new System.Windows.Forms.TextBox();
            this.txtColorName = new System.Windows.Forms.TextBox();
            this.txtColorHex = new System.Windows.Forms.TextBox();
            this.btnAddColor = new System.Windows.Forms.Button();
            this.btnDeleteColor = new System.Windows.Forms.Button();

            this.dgvCPVCodes = new System.Windows.Forms.DataGridView();
            this.txtCPVCodeId = new System.Windows.Forms.TextBox();
            this.chkCPVPrimary = new System.Windows.Forms.CheckBox();
            this.txtCPVNotes = new System.Windows.Forms.TextBox();
            this.btnAddCPV = new System.Windows.Forms.Button();
            this.btnDeleteCPV = new System.Windows.Forms.Button();

            this.dgvSerialNumbers = new System.Windows.Forms.DataGridView();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.cmbSerialLotId = new System.Windows.Forms.ComboBox();
            this.cmbSerialStatus = new System.Windows.Forms.ComboBox();
            this.txtSerialNotes = new System.Windows.Forms.TextBox();
            this.dtpSerialWarrantyStart = new System.Windows.Forms.DateTimePicker();
            this.dtpSerialWarrantyEnd = new System.Windows.Forms.DateTimePicker();
            this.btnAddSerial = new System.Windows.Forms.Button();
            this.btnEditSerial = new System.Windows.Forms.Button();
            this.btnDeleteSerial = new System.Windows.Forms.Button();
            this.btnCancelSerial = new System.Windows.Forms.Button();
            this.txtSerialSearch = new System.Windows.Forms.TextBox();
            this.btnSerialSearch = new System.Windows.Forms.Button();
            this.btnSerialFirst = new System.Windows.Forms.Button();
            this.btnSerialPrev = new System.Windows.Forms.Button();
            this.btnSerialNext = new System.Windows.Forms.Button();
            this.btnSerialLast = new System.Windows.Forms.Button();
            this.lblSerialRecordInfo = new System.Windows.Forms.Label();
            this.serialBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.tabControl1.SuspendLayout();
            this.tabBasic.SuspendLayout(); this.tabPrices.SuspendLayout(); this.tabStock.SuspendLayout();
            this.tabBarcodes.SuspendLayout(); this.tabFeatures.SuspendLayout(); this.tabSupplier.SuspendLayout();
            this.tabMyData.SuspendLayout(); this.tabTracking.SuspendLayout(); this.tabNotes.SuspendLayout();
            this.tabLots.SuspendLayout(); this.tabVariants.SuspendLayout();
            this.tabBarcodesMultiple.SuspendLayout(); this.tabUnitRelations.SuspendLayout(); this.tabWarehouseStock.SuspendLayout();
            this.tabImages.SuspendLayout(); this.tabItemSuppliers.SuspendLayout(); this.tabRelated.SuspendLayout();
            this.tabPriceHistory.SuspendLayout(); this.tabItemAttributes.SuspendLayout(); this.tabAlternativeCodes.SuspendLayout();
            this.tabColors.SuspendLayout(); this.tabCPVCodes.SuspendLayout(); this.tabSerialNumbers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchasePrice)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numSalePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWholesalePrice)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numCostPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountPercent)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numDiscountAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numMinStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxStock)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numReorderPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSafetyStock)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLots)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvVariants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarcodesMultiple)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvUnitRelations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarehouseStock)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvItemSuppliers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelated)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvPriceHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemAttributes)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvAlternativeCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemColors)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvCPVCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialNumbers)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.serialBindingSource)).BeginInit();
            this.SuspendLayout();

            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;  // ✅ Γεμίζει όλο το πλάτος
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.Controls.Add(this.tabBasic); this.tabControl1.Controls.Add(this.tabPrices);
            this.tabControl1.Controls.Add(this.tabStock); this.tabControl1.Controls.Add(this.tabBarcodes);
            this.tabControl1.Controls.Add(this.tabFeatures); this.tabControl1.Controls.Add(this.tabSupplier);
            this.tabControl1.Controls.Add(this.tabMyData); this.tabControl1.Controls.Add(this.tabTracking);
            this.tabControl1.Controls.Add(this.tabNotes); this.tabControl1.Controls.Add(this.tabLots);
            this.tabControl1.Controls.Add(this.tabVariants); this.tabControl1.Controls.Add(this.tabBarcodesMultiple);
            this.tabControl1.Controls.Add(this.tabUnitRelations); this.tabControl1.Controls.Add(this.tabWarehouseStock);
            this.tabControl1.Controls.Add(this.tabImages); this.tabControl1.Controls.Add(this.tabItemSuppliers);
            this.tabControl1.Controls.Add(this.tabRelated); this.tabControl1.Controls.Add(this.tabPriceHistory);
            this.tabControl1.Controls.Add(this.tabItemAttributes); this.tabControl1.Controls.Add(this.tabAlternativeCodes);
            this.tabControl1.Controls.Add(this.tabColors);
            this.tabControl1.Controls.Add(this.tabCPVCodes);
            this.tabControl1.Controls.Add(this.tabSerialNumbers);
           

            int y = 20, labelX = 20, controlX = 180, rowHeight = 42;

            // TAB 1
            var lblCode = new System.Windows.Forms.Label(); lblCode.AutoSize = true; lblCode.Location = new System.Drawing.Point(labelX, y + 10); lblCode.Text = "Κωδικός:"; lblCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblCode);
            this.txtCode.Location = new System.Drawing.Point(controlX, y); this.txtCode.Size = new System.Drawing.Size(200, 30); this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.txtCode); y += rowHeight;
            var lblName = new System.Windows.Forms.Label(); lblName.AutoSize = true; lblName.Location = new System.Drawing.Point(labelX, y + 10); lblName.Text = "Όνομα*:"; lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblName);
            this.txtName.Location = new System.Drawing.Point(controlX, y); this.txtName.Size = new System.Drawing.Size(400, 30); this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.txtName); y += rowHeight;
            var lblShortDesc = new System.Windows.Forms.Label(); lblShortDesc.AutoSize = true; lblShortDesc.Location = new System.Drawing.Point(labelX, y + 10); lblShortDesc.Text = "Σύντομη Περιγραφή:"; lblShortDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblShortDesc);
            this.txtShortDescription.Location = new System.Drawing.Point(controlX, y); this.txtShortDescription.Size = new System.Drawing.Size(500, 30); this.txtShortDescription.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.txtShortDescription); y += rowHeight;
            var lblDesc = new System.Windows.Forms.Label(); lblDesc.AutoSize = true; lblDesc.Location = new System.Drawing.Point(labelX, y + 10); lblDesc.Text = "Περιγραφή:"; lblDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblDesc);
            this.txtDescription.Location = new System.Drawing.Point(controlX, y); this.txtDescription.Size = new System.Drawing.Size(500, 80); this.txtDescription.Multiline = true; this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical; this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.txtDescription); y += 100;
            var lblCategory = new System.Windows.Forms.Label(); lblCategory.AutoSize = true; lblCategory.Location = new System.Drawing.Point(labelX, y + 10); lblCategory.Text = "Κατηγορία:"; lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblCategory);
            this.cmbCategory.Location = new System.Drawing.Point(controlX, y); this.cmbCategory.Size = new System.Drawing.Size(250, 30); this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.cmbCategory); y += rowHeight;
            var lblUnit = new System.Windows.Forms.Label(); lblUnit.AutoSize = true; lblUnit.Location = new System.Drawing.Point(labelX, y + 10); lblUnit.Text = "Μονάδα:"; lblUnit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblUnit);
            this.cmbUnit.Location = new System.Drawing.Point(controlX, y); this.cmbUnit.Size = new System.Drawing.Size(150, 30); this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.cmbUnit); y += rowHeight;
            var lblBrand = new System.Windows.Forms.Label(); lblBrand.AutoSize = true; lblBrand.Location = new System.Drawing.Point(labelX, y + 10); lblBrand.Text = "Brand:"; lblBrand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblBrand);
            this.txtBrand.Location = new System.Drawing.Point(controlX, y); this.txtBrand.Size = new System.Drawing.Size(200, 30); this.txtBrand.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.txtBrand); y += rowHeight;
            var lblManufacturer = new System.Windows.Forms.Label(); lblManufacturer.AutoSize = true; lblManufacturer.Location = new System.Drawing.Point(labelX, y + 10); lblManufacturer.Text = "Κατασκευαστής:"; lblManufacturer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(lblManufacturer);
            this.txtManufacturer.Location = new System.Drawing.Point(controlX, y); this.txtManufacturer.Size = new System.Drawing.Size(300, 30); this.txtManufacturer.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.txtManufacturer); y += rowHeight;
            this.chkIsActive.AutoSize = true; this.chkIsActive.Location = new System.Drawing.Point(controlX, y + 8); this.chkIsActive.Text = "✅ Ενεργό"; this.chkIsActive.Checked = true; this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.tabBasic.Controls.Add(this.chkIsActive);
            this.chkIsFavorite.AutoSize = true; this.chkIsFavorite.Location = new System.Drawing.Point(controlX + 150, y + 8); this.chkIsFavorite.Text = "⭐ Αγαπημένο"; this.chkIsFavorite.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.chkIsFavorite);
            this.chkIsFeatured.AutoSize = true; this.chkIsFeatured.Location = new System.Drawing.Point(controlX + 320, y + 8); this.chkIsFeatured.Text = "🌟 Προτεινόμενο"; this.chkIsFeatured.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabBasic.Controls.Add(this.chkIsFeatured);
            this.tabBasic.Text = "📋 Βασικά"; this.tabBasic.UseVisualStyleBackColor = true; this.tabBasic.AutoScroll = true;// ✅ Κουμπιά Αποθήκευση/Ακύρωση στο Tab 1
                                                                                                                            // ✅ Κουμπιά Αποθήκευση/Ακύρωση (κάτω από τα checkboxes)
            y += rowHeight + 20; // Νέα γραμμή, πιο κάτω

            var btnSaveBasic = new System.Windows.Forms.Button();
            var btnCancelBasic = new System.Windows.Forms.Button();

            btnSaveBasic.Text = "💾 Αποθήκευση";
            btnSaveBasic.Size = new System.Drawing.Size(140, 40);
            btnSaveBasic.Location = new System.Drawing.Point(controlX, y);
            btnSaveBasic.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSaveBasic.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSaveBasic.ForeColor = System.Drawing.Color.White;
            btnSaveBasic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSaveBasic.FlatAppearance.BorderSize = 0;
            btnSaveBasic.Click += new System.EventHandler(this.btnSave_Click);
            this.tabBasic.Controls.Add(btnSaveBasic);

            btnCancelBasic.Text = "❌ Ακύρωση";
            btnCancelBasic.Size = new System.Drawing.Size(120, 40);
            btnCancelBasic.Location = new System.Drawing.Point(controlX + 160, y);
            btnCancelBasic.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancelBasic.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            btnCancelBasic.ForeColor = System.Drawing.Color.White;
            btnCancelBasic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancelBasic.FlatAppearance.BorderSize = 0;
            btnCancelBasic.Click += new System.EventHandler(this.btnCancel_Click);
            this.tabBasic.Controls.Add(btnCancelBasic);

            // TAB 2
            y = 20;
            AddLabelAndControl(this.tabPrices, "Τιμή Αγοράς (€):", labelX, y, controlX, this.numPurchasePrice, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabPrices, "Τιμή Πώλησης (€):", labelX, y, controlX, this.numSalePrice, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabPrices, "Χονδρική Τιμή (€):", labelX, y, controlX, this.numWholesalePrice, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabPrices, "Κόστος (€):", labelX, y, controlX, this.numCostPrice, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabPrices, "ΦΠΑ %:", labelX, y, controlX, this.cmbVatRate, 100, isCombo: true); y += rowHeight;
            AddLabelAndControl(this.tabPrices, "Έκπτωση (%):", labelX, y, controlX, this.numDiscountPercent, 100, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabPrices, "Έκπτωση (€):", labelX, y, controlX, this.numDiscountAmount, 100, isNumeric: true);
            this.tabPrices.Text = "💰 Τιμές"; this.tabPrices.UseVisualStyleBackColor = true;

            // TAB 3
            y = 20;
            AddLabelAndControl(this.tabStock, "Τρέχον Stock:", labelX, y, controlX, this.numStock, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabStock, "Ελάχιστο Stock:", labelX, y, controlX, this.numMinStock, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabStock, "Μέγιστο Stock:", labelX, y, controlX, this.numMaxStock, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabStock, "Σημείο Παραγγελίας:", labelX, y, controlX, this.numReorderPoint, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabStock, "Safety Stock:", labelX, y, controlX, this.numSafetyStock, 150, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabStock, "Θέση Αποθήκης:", labelX, y, controlX, this.txtLocation, 200);
            this.tabStock.Text = "📦 Αποθήκη"; this.tabStock.UseVisualStyleBackColor = true;

            // TAB 4
            y = 20;
            AddLabelAndControl(this.tabBarcodes, "Barcode / EAN:", labelX, y, controlX, this.txtBarcode, 250); y += rowHeight;
            AddLabelAndControl(this.tabBarcodes, "SKU:", labelX, y, controlX, this.txtSKU, 200); y += rowHeight;
            AddLabelAndControl(this.tabBarcodes, "Εναλλακτικός Κωδικός:", labelX, y, controlX, this.txtAlternativeCode, 200); y += rowHeight;
            AddLabelAndControl(this.tabBarcodes, "CPV Code:", labelX, y, controlX, this.txtCPVCode, 150); y += rowHeight;
            AddLabelAndControl(this.tabBarcodes, "CPV Περιγραφή:", labelX, y, controlX, this.txtCPVDescription, 400);
            this.tabBarcodes.Text = "🏷️ Barcodes & Κωδικοί"; this.tabBarcodes.UseVisualStyleBackColor = true;

            // TAB 5
            y = 20;
            AddLabelAndControl(this.tabFeatures, "Χρώμα:", labelX, y, controlX, this.cmbColor, 200, isCombo: true); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Μέγεθος:", labelX, y, controlX, this.cmbSize, 150, isCombo: true); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Βάρος (kg):", labelX, y, controlX, this.numWeight, 100, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Όγκος (m³):", labelX, y, controlX, this.numVolume, 100, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Διαστάσεις:", labelX, y, controlX, this.txtDimensions, 200); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Μήκος:", labelX, y, controlX, this.numLength, 100, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Πλάτος:", labelX, y, controlX, this.numWidth, 100, isNumeric: true); y += rowHeight;
            AddLabelAndControl(this.tabFeatures, "Ύψος:", labelX, y, controlX, this.numHeight, 100, isNumeric: true);
            this.tabFeatures.Text = "🎨 Χαρακτηριστικά"; this.tabFeatures.UseVisualStyleBackColor = true;

            // TAB 6
            y = 20;
            AddLabelAndControl(this.tabSupplier, "Προμηθευτής:", labelX, y, controlX, this.cmbSupplier, 300, isCombo: true); y += rowHeight;
            AddLabelAndControl(this.tabSupplier, "Κωδικός Προμηθευτή:", labelX, y, controlX, this.txtSupplierCode, 200); y += rowHeight;
            AddLabelAndControl(this.tabSupplier, "URL Προμηθευτή:", labelX, y, controlX, this.txtProviderURL, 400);
            this.tabSupplier.Text = "🚚 Προμηθευτής"; this.tabSupplier.UseVisualStyleBackColor = true;

            // TAB 7
            y = 20;
            AddLabelAndControl(this.tabMyData, "myData Κατηγορία:", labelX, y, controlX, this.txtMydataCategory, 200); y += rowHeight;
            AddLabelAndControl(this.tabMyData, "myData Κωδικός:", labelX, y, controlX, this.txtMydataCode, 150); y += rowHeight;
            AddLabelAndControl(this.tabMyData, "Άρθρο Απαλλαγής:", labelX, y, controlX, this.txtExemptionArticle, 150); y += rowHeight;
            AddLabelAndControl(this.tabMyData, "Λόγος Απαλλαγής:", labelX, y, controlX, this.txtExemptionReason, 400); y += rowHeight;
            AddLabelAndControl(this.tabMyData, "Άρθρο ΦΠΑ:", labelX, y, controlX, this.txtVatArticle, 150); y += rowHeight;
            AddLabelAndControl(this.tabMyData, "TARIC Code:", labelX, y, controlX, this.txtTARICCode, 150); y += rowHeight;
            AddLabelAndControl(this.tabMyData, "Χώρα Προέλευσης:", labelX, y, controlX, this.txtCountryOfOrigin, 200);
            this.tabMyData.Text = "📊 myData & Φορολογικά"; this.tabMyData.UseVisualStyleBackColor = true;

            // TAB 8
            y = 20;
            this.chkLotTracking.AutoSize = true; this.chkLotTracking.Location = new System.Drawing.Point(controlX, y + 8); this.chkLotTracking.Text = "📦 Παρακολούθηση Παρτίδων (Lot)"; this.chkLotTracking.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabTracking.Controls.Add(this.chkLotTracking); y += rowHeight;
            this.chkSerialTracking.AutoSize = true; this.chkSerialTracking.Location = new System.Drawing.Point(controlX, y + 8); this.chkSerialTracking.Text = "🔢 Παρακολούθηση Σειριακών Αριθμών"; this.chkSerialTracking.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabTracking.Controls.Add(this.chkSerialTracking); y += rowHeight;
            this.chkExpiryTracking.AutoSize = true; this.chkExpiryTracking.Location = new System.Drawing.Point(controlX, y + 8); this.chkExpiryTracking.Text = "📅 Παρακολούθηση Ημερομηνίας Λήξης"; this.chkExpiryTracking.Font = new System.Drawing.Font("Segoe UI", 10F); this.tabTracking.Controls.Add(this.chkExpiryTracking);
            this.tabTracking.Text = "🔍 Tracking"; this.tabTracking.UseVisualStyleBackColor = true;

            // TAB 9
            y = 20;
            AddLabelAndControl(this.tabNotes, "Σημειώσεις:", labelX, y, controlX, this.txtNotes, 600, multiline: true, height: 100); y += 120;
            AddLabelAndControl(this.tabNotes, "Αναλυτική Περιγραφή:", labelX, y, controlX, this.txtLongDescription, 600, multiline: true, height: 150); y += 170;
            AddLabelAndControl(this.tabNotes, "Tags:", labelX, y, controlX, this.txtTags, 600);
            this.tabNotes.Text = "📝 Σημειώσεις"; this.tabNotes.UseVisualStyleBackColor = true;

            // TAB 10
            this.dgvLots.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvLots.AllowUserToAddRows = false; this.dgvLots.AllowUserToDeleteRows = false; this.dgvLots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvLots.MultiSelect = false; this.dgvLots.ReadOnly = true; this.dgvLots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvLots.BackgroundColor = System.Drawing.Color.White; this.dgvLots.BorderStyle = System.Windows.Forms.BorderStyle.None; this.dgvLots.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80); this.dgvLots.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White; this.dgvLots.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.dgvLots.ColumnHeadersHeight = 40; this.dgvLots.RowTemplate.Height = 35;
            var btnPanelLots = new System.Windows.Forms.Panel(); btnPanelLots.Dock = System.Windows.Forms.DockStyle.Bottom; btnPanelLots.Height = 55; btnPanelLots.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnAddLot.Text = "➕ Προσθήκη"; this.btnAddLot.Size = new System.Drawing.Size(140, 40); this.btnAddLot.Location = new System.Drawing.Point(10, 7); this.btnAddLot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.btnAddLot.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAddLot.ForeColor = System.Drawing.Color.White; this.btnAddLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAddLot.FlatAppearance.BorderSize = 0; this.btnAddLot.Click += new System.EventHandler(this.btnAddLot_Click);
            this.btnEditLot.Text = "✏️ Επεξεργασία"; this.btnEditLot.Size = new System.Drawing.Size(150, 40); this.btnEditLot.Location = new System.Drawing.Point(160, 7); this.btnEditLot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.btnEditLot.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnEditLot.ForeColor = System.Drawing.Color.White; this.btnEditLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnEditLot.FlatAppearance.BorderSize = 0; this.btnEditLot.Click += new System.EventHandler(this.btnEditLot_Click);
            this.btnDeleteLot.Text = "🗑️ Διαγραφή"; this.btnDeleteLot.Size = new System.Drawing.Size(140, 40); this.btnDeleteLot.Location = new System.Drawing.Point(320, 7); this.btnDeleteLot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.btnDeleteLot.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDeleteLot.ForeColor = System.Drawing.Color.White; this.btnDeleteLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDeleteLot.FlatAppearance.BorderSize = 0; this.btnDeleteLot.Click += new System.EventHandler(this.btnDeleteLot_Click);
            btnPanelLots.Controls.Add(this.btnAddLot); btnPanelLots.Controls.Add(this.btnEditLot); btnPanelLots.Controls.Add(this.btnDeleteLot);
            this.tabLots.Controls.Add(this.dgvLots); this.tabLots.Controls.Add(btnPanelLots); this.tabLots.Text = "📦 Παρτίδες"; this.tabLots.UseVisualStyleBackColor = true;

            // TAB 11
            this.dgvVariants.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvVariants.AllowUserToAddRows = false; this.dgvVariants.AllowUserToDeleteRows = false; this.dgvVariants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvVariants.MultiSelect = false; this.dgvVariants.ReadOnly = true; this.dgvVariants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvVariants.BackgroundColor = System.Drawing.Color.White; this.dgvVariants.BorderStyle = System.Windows.Forms.BorderStyle.None; this.dgvVariants.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80); this.dgvVariants.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White; this.dgvVariants.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.dgvVariants.ColumnHeadersHeight = 40; this.dgvVariants.RowTemplate.Height = 35;
            var btnPanelVariants = new System.Windows.Forms.Panel(); btnPanelVariants.Dock = System.Windows.Forms.DockStyle.Bottom; btnPanelVariants.Height = 55; btnPanelVariants.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnAddVariant.Text = "➕ Προσθήκη"; this.btnAddVariant.Size = new System.Drawing.Size(140, 40); this.btnAddVariant.Location = new System.Drawing.Point(10, 7); this.btnAddVariant.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.btnAddVariant.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); this.btnAddVariant.ForeColor = System.Drawing.Color.White; this.btnAddVariant.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAddVariant.FlatAppearance.BorderSize = 0; this.btnAddVariant.Click += new System.EventHandler(this.btnAddVariant_Click);
            this.btnEditVariant.Text = "✏️ Επεξεργασία"; this.btnEditVariant.Size = new System.Drawing.Size(150, 40); this.btnEditVariant.Location = new System.Drawing.Point(160, 7); this.btnEditVariant.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.btnEditVariant.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnEditVariant.ForeColor = System.Drawing.Color.White; this.btnEditVariant.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnEditVariant.FlatAppearance.BorderSize = 0; this.btnEditVariant.Click += new System.EventHandler(this.btnEditVariant_Click);
            this.btnDeleteVariant.Text = "🗑️ Διαγραφή"; this.btnDeleteVariant.Size = new System.Drawing.Size(140, 40); this.btnDeleteVariant.Location = new System.Drawing.Point(320, 7); this.btnDeleteVariant.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.btnDeleteVariant.BackColor = System.Drawing.Color.FromArgb(231, 76, 60); this.btnDeleteVariant.ForeColor = System.Drawing.Color.White; this.btnDeleteVariant.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDeleteVariant.FlatAppearance.BorderSize = 0; this.btnDeleteVariant.Click += new System.EventHandler(this.btnDeleteVariant_Click);
            btnPanelVariants.Controls.Add(this.btnAddVariant); btnPanelVariants.Controls.Add(this.btnEditVariant); btnPanelVariants.Controls.Add(this.btnDeleteVariant);
            this.tabVariants.Controls.Add(this.dgvVariants); this.tabVariants.Controls.Add(btnPanelVariants); this.tabVariants.Text = "🎨 Παραλλαγές"; this.tabVariants.UseVisualStyleBackColor = true;

            // ==========================================
            // TABS 12-23: ΕΝΣΩΜΑΤΩΜΕΝΑ (ΔΙΟΡΘΩΜΕΝΑ)
            // ==========================================

            // ==========================================
            // Tab 12: Πολλαπλά Barcodes (ΤΕΛΙΚΗ ΔΙΟΡΘΩΣΗ)
            // ==========================================
            this.cmbBarcodeUom = new System.Windows.Forms.ComboBox();
            this.numBarcodeQty = new System.Windows.Forms.NumericUpDown();
            this.chkBarcodePrimary = new System.Windows.Forms.CheckBox();

            // ✅ Panel για τα inputs (πάνω)
            var panelInputs = new System.Windows.Forms.Panel();
            panelInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelInputs.Height = 120;
            panelInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelInputs.Padding = new System.Windows.Forms.Padding(10);

            // Row 1: Barcode, Τύπος, Μονάδα, Ποσότητα
            var lblBC = new System.Windows.Forms.Label();
            lblBC.AutoSize = true; lblBC.Location = new System.Drawing.Point(10, 15);
            lblBC.Text = "Barcode*:"; lblBC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelInputs.Controls.Add(lblBC);

            this.txtBarcodeCode.Location = new System.Drawing.Point(10, 35);
            this.txtBarcodeCode.Size = new System.Drawing.Size(200, 28);
            this.txtBarcodeCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelInputs.Controls.Add(this.txtBarcodeCode);

            var lblBCType = new System.Windows.Forms.Label();
            lblBCType.AutoSize = true; lblBCType.Location = new System.Drawing.Point(220, 15);
            lblBCType.Text = "Τύπος:"; lblBCType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelInputs.Controls.Add(lblBCType);

            this.cmbBarcodeType.Location = new System.Drawing.Point(220, 35);
            this.cmbBarcodeType.Size = new System.Drawing.Size(150, 28);
            this.cmbBarcodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodeType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbBarcodeType.Items.AddRange(new object[] { "EAN-13", "EAN-8", "UPC-A", "Code128", "QR", "Εσωτερικός" });
            this.cmbBarcodeType.SelectedIndex = 0;
            panelInputs.Controls.Add(this.cmbBarcodeType);

            var lblBCUom = new System.Windows.Forms.Label();
            lblBCUom.AutoSize = true; lblBCUom.Location = new System.Drawing.Point(380, 15);
            lblBCUom.Text = "Μονάδα:"; lblBCUom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelInputs.Controls.Add(lblBCUom);

            this.cmbBarcodeUom.Location = new System.Drawing.Point(380, 35);
            this.cmbBarcodeUom.Size = new System.Drawing.Size(150, 28);
            this.cmbBarcodeUom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodeUom.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelInputs.Controls.Add(this.cmbBarcodeUom);

            var lblBCQty = new System.Windows.Forms.Label();
            lblBCQty.AutoSize = true; lblBCQty.Location = new System.Drawing.Point(540, 15);
            lblBCQty.Text = "Ποσότητα:"; lblBCQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelInputs.Controls.Add(lblBCQty);

            this.numBarcodeQty.Location = new System.Drawing.Point(540, 35);
            this.numBarcodeQty.Size = new System.Drawing.Size(100, 28);
            this.numBarcodeQty.DecimalPlaces = 4;
            this.numBarcodeQty.Value = 1;
            this.numBarcodeQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelInputs.Controls.Add(this.numBarcodeQty);

            this.chkBarcodePrimary.AutoSize = true;
            this.chkBarcodePrimary.Location = new System.Drawing.Point(660, 37);
            this.chkBarcodePrimary.Text = "👑 Κύριο Barcode";
            this.chkBarcodePrimary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            panelInputs.Controls.Add(this.chkBarcodePrimary);

            // Row 2: Περιγραφή
            var lblBCDesc = new System.Windows.Forms.Label();
            lblBCDesc.AutoSize = true; lblBCDesc.Location = new System.Drawing.Point(10, 70);
            lblBCDesc.Text = "Περιγραφή:"; lblBCDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelInputs.Controls.Add(lblBCDesc);

            this.txtBarcodeDesc.Location = new System.Drawing.Point(10, 90);
            this.txtBarcodeDesc.Size = new System.Drawing.Size(860, 28);
            this.txtBarcodeDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelInputs.Controls.Add(this.txtBarcodeDesc);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelBarcodes = new System.Windows.Forms.Panel();
            btnPanelBarcodes.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelBarcodes.Height = 55;
            btnPanelBarcodes.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddBarcode.Text = "➕ Προσθήκη";
            this.btnAddBarcode.Size = new System.Drawing.Size(140, 40);
            this.btnAddBarcode.Location = new System.Drawing.Point(10, 7);
            this.btnAddBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddBarcode.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddBarcode.ForeColor = System.Drawing.Color.White;
            this.btnAddBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBarcode.FlatAppearance.BorderSize = 0;
            this.btnAddBarcode.Click += new System.EventHandler(this.btnAddBarcode_Click);

            this.btnEditBarcode.Text = "️ Επεξεργασία";
            this.btnEditBarcode.Size = new System.Drawing.Size(150, 40);
            this.btnEditBarcode.Location = new System.Drawing.Point(160, 7);
            this.btnEditBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditBarcode.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditBarcode.ForeColor = System.Drawing.Color.White;
            this.btnEditBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditBarcode.FlatAppearance.BorderSize = 0;
            this.btnEditBarcode.Click += new System.EventHandler(this.btnEditBarcode_Click);

            this.btnDeleteBarcode.Text = "🗑️ Διαγραφή";
            this.btnDeleteBarcode.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteBarcode.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteBarcode.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteBarcode.ForeColor = System.Drawing.Color.White;
            this.btnDeleteBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBarcode.FlatAppearance.BorderSize = 0;
            this.btnDeleteBarcode.Click += new System.EventHandler(this.btnDeleteBarcode_Click);

            btnPanelBarcodes.Controls.Add(this.btnAddBarcode);
            btnPanelBarcodes.Controls.Add(this.btnEditBarcode);
            btnPanelBarcodes.Controls.Add(this.btnDeleteBarcode);

            // ✅ DataGridView (μέση - γεμίζει τον υπόλοιπο χώρο)
            this.dgvBarcodesMultiple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBarcodesMultiple.AllowUserToAddRows = false;
            this.dgvBarcodesMultiple.AllowUserToDeleteRows = false;
            this.dgvBarcodesMultiple.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBarcodesMultiple.MultiSelect = false;
            this.dgvBarcodesMultiple.ReadOnly = true;
            this.dgvBarcodesMultiple.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBarcodesMultiple.BackgroundColor = System.Drawing.Color.White;
            this.dgvBarcodesMultiple.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBarcodesMultiple.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvBarcodesMultiple.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBarcodesMultiple.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvBarcodesMultiple.ColumnHeadersHeight = 40;
            this.dgvBarcodesMultiple.RowTemplate.Height = 35;
            this.dgvBarcodesMultiple.AutoGenerateColumns = false;
            this.dgvBarcodesMultiple.Columns.Clear();
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Barcode", DataPropertyName = "Barcode", HeaderText = "Barcode", Width = 150 });
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τύπος", DataPropertyName = "Τύπος", HeaderText = "Τύπος", Width = 120 });
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Μονάδα", DataPropertyName = "Μονάδα", HeaderText = "Μονάδα", Width = 120 });
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ποσότητα", DataPropertyName = "Ποσότητα", HeaderText = "Ποσότητα", Width = 100 });
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κύριο", DataPropertyName = "Κύριο", HeaderText = "Κύριο", Width = 80 });
            this.dgvBarcodesMultiple.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Περιγραφή", DataPropertyName = "Περιγραφή", HeaderText = "Περιγραφή", Width = 250 });

            // ✅ Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabBarcodesMultiple.Controls.Add(this.dgvBarcodesMultiple);
            this.tabBarcodesMultiple.Controls.Add(panelInputs);
            this.tabBarcodesMultiple.Controls.Add(btnPanelBarcodes);
            this.tabBarcodesMultiple.Text = "️ Πολλαπλά Barcodes";
            this.tabBarcodesMultiple.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 13: Μετατροπές Μονάδων
            // ==========================================
            this.cmbUnitTo = new System.Windows.Forms.ComboBox();
            this.cmbRelationType = new System.Windows.Forms.ComboBox();
            this.txtUnitFormula = new System.Windows.Forms.TextBox();
            this.btnEditUnit = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelUnitInputs = new System.Windows.Forms.Panel();
            panelUnitInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelUnitInputs.Height = 100;
            panelUnitInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelUnitInputs.Padding = new System.Windows.Forms.Padding(10);

            var lblUnitFrom = new System.Windows.Forms.Label();
            lblUnitFrom.AutoSize = true; lblUnitFrom.Location = new System.Drawing.Point(10, 15);
            lblUnitFrom.Text = "Από Μονάδα:"; lblUnitFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelUnitInputs.Controls.Add(lblUnitFrom);

            this.cmbUnitFrom.Location = new System.Drawing.Point(10, 35);
            this.cmbUnitFrom.Size = new System.Drawing.Size(200, 28);
            this.cmbUnitFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelUnitInputs.Controls.Add(this.cmbUnitFrom);

            var lblUnitTo = new System.Windows.Forms.Label();
            lblUnitTo.AutoSize = true; lblUnitTo.Location = new System.Drawing.Point(220, 15);
            lblUnitTo.Text = "Προς Μονάδα:"; lblUnitTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelUnitInputs.Controls.Add(lblUnitTo);

            this.cmbUnitTo.Location = new System.Drawing.Point(220, 35);
            this.cmbUnitTo.Size = new System.Drawing.Size(200, 28);
            this.cmbUnitTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelUnitInputs.Controls.Add(this.cmbUnitTo);

            var lblRelType = new System.Windows.Forms.Label();
            lblRelType.AutoSize = true; lblRelType.Location = new System.Drawing.Point(430, 15);
            lblRelType.Text = "Τύπος:"; lblRelType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelUnitInputs.Controls.Add(lblRelType);

            this.cmbRelationType.Location = new System.Drawing.Point(430, 35);
            this.cmbRelationType.Size = new System.Drawing.Size(150, 28);
            this.cmbRelationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelationType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRelationType.Items.AddRange(new object[] { "Πολλαπλασιασμός", "Διαίρεση" });
            this.cmbRelationType.SelectedIndex = 0;
            panelUnitInputs.Controls.Add(this.cmbRelationType);

            var lblCoeff = new System.Windows.Forms.Label();
            lblCoeff.AutoSize = true; lblCoeff.Location = new System.Drawing.Point(590, 15);
            lblCoeff.Text = "Συντελεστής:"; lblCoeff.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelUnitInputs.Controls.Add(lblCoeff);

            this.numUnitCoeff.Location = new System.Drawing.Point(590, 35);
            this.numUnitCoeff.Size = new System.Drawing.Size(120, 28);
            this.numUnitCoeff.DecimalPlaces = 4;
            this.numUnitCoeff.Maximum = 9999999;
            this.numUnitCoeff.Value = 1;
            this.numUnitCoeff.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelUnitInputs.Controls.Add(this.numUnitCoeff);

            var lblFormula = new System.Windows.Forms.Label();
            lblFormula.AutoSize = true; lblFormula.Location = new System.Drawing.Point(720, 15);
            lblFormula.Text = "Τύπος:"; lblFormula.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelUnitInputs.Controls.Add(lblFormula);

            this.txtUnitFormula.Location = new System.Drawing.Point(720, 35);
            this.txtUnitFormula.Size = new System.Drawing.Size(160, 28);
            this.txtUnitFormula.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUnitFormula.PlaceholderText = "π.χ. x * 1000";
            panelUnitInputs.Controls.Add(this.txtUnitFormula);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelUnits = new System.Windows.Forms.Panel();
            btnPanelUnits.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelUnits.Height = 55;
            btnPanelUnits.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddUnit.Text = "➕ Προσθήκη";
            this.btnAddUnit.Size = new System.Drawing.Size(140, 40);
            this.btnAddUnit.Location = new System.Drawing.Point(10, 7);
            this.btnAddUnit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddUnit.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddUnit.ForeColor = System.Drawing.Color.White;
            this.btnAddUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddUnit.FlatAppearance.BorderSize = 0;
            this.btnAddUnit.Click += new System.EventHandler(this.btnAddUnit_Click);

            this.btnEditUnit.Text = "✏️ Επεξεργασία";
            this.btnEditUnit.Size = new System.Drawing.Size(150, 40);
            this.btnEditUnit.Location = new System.Drawing.Point(160, 7);
            this.btnEditUnit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditUnit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditUnit.ForeColor = System.Drawing.Color.White;
            this.btnEditUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditUnit.FlatAppearance.BorderSize = 0;
            this.btnEditUnit.Click += new System.EventHandler(this.btnEditUnit_Click);

            this.btnDeleteUnit.Text = "🗑️ Διαγραφή";
            this.btnDeleteUnit.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteUnit.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteUnit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteUnit.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteUnit.ForeColor = System.Drawing.Color.White;
            this.btnDeleteUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteUnit.FlatAppearance.BorderSize = 0;
            this.btnDeleteUnit.Click += new System.EventHandler(this.btnDeleteUnit_Click);

            btnPanelUnits.Controls.Add(this.btnAddUnit);
            btnPanelUnits.Controls.Add(this.btnEditUnit);
            btnPanelUnits.Controls.Add(this.btnDeleteUnit);

            // ✅ DataGridView (μέση - γεμίζει τον υπόλοιπο χώρο)
            this.dgvUnitRelations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnitRelations.AllowUserToAddRows = false;
            this.dgvUnitRelations.AllowUserToDeleteRows = false;
            this.dgvUnitRelations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnitRelations.MultiSelect = false;
            this.dgvUnitRelations.ReadOnly = true;
            this.dgvUnitRelations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUnitRelations.BackgroundColor = System.Drawing.Color.White;
            this.dgvUnitRelations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUnitRelations.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvUnitRelations.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvUnitRelations.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvUnitRelations.ColumnHeadersHeight = 40;
            this.dgvUnitRelations.RowTemplate.Height = 35;
            this.dgvUnitRelations.AutoGenerateColumns = false;
            this.dgvUnitRelations.Columns.Clear();
            this.dgvUnitRelations.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvUnitRelations.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Από", DataPropertyName = "Από", HeaderText = "Από Μονάδα", Width = 200 });
            this.dgvUnitRelations.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Προς", DataPropertyName = "Προς", HeaderText = "Προς Μονάδα", Width = 200 });
            this.dgvUnitRelations.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τύπος", DataPropertyName = "Τύπος", HeaderText = "Τύπος", Width = 150 });
            this.dgvUnitRelations.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Συντελεστής", DataPropertyName = "Συντελεστής", HeaderText = "Συντελεστής", Width = 150 });
            this.dgvUnitRelations.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τύπος Υπολογισμού", DataPropertyName = "Τύπος Υπολογισμού", HeaderText = "Τύπος Υπολογισμού", Width = 200 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabUnitRelations.Controls.Add(this.dgvUnitRelations);
            this.tabUnitRelations.Controls.Add(panelUnitInputs);
            this.tabUnitRelations.Controls.Add(btnPanelUnits);
            this.tabUnitRelations.Text = " Μετατροπές Μονάδων";
            this.tabUnitRelations.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 14: Stock ανά Αποθήκη
            // ==========================================
            this.cmbStockWarehouse = new System.Windows.Forms.ComboBox();
            this.cmbStockVariant = new System.Windows.Forms.ComboBox();
            this.cmbStockLot = new System.Windows.Forms.ComboBox();
            this.numStockQty = new System.Windows.Forms.NumericUpDown();
            this.numStockReserved = new System.Windows.Forms.NumericUpDown();
            this.numStockMin = new System.Windows.Forms.NumericUpDown();
            this.numStockMax = new System.Windows.Forms.NumericUpDown();
            this.txtStockNotes = new System.Windows.Forms.TextBox();
            this.btnEditStock = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω) - 2 σειρές
            var panelStockInputs = new System.Windows.Forms.Panel();
            panelStockInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelStockInputs.Height = 130;
            panelStockInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelStockInputs.Padding = new System.Windows.Forms.Padding(10);

            // Row 1
            var lblWH = new System.Windows.Forms.Label();
            lblWH.AutoSize = true; lblWH.Location = new System.Drawing.Point(10, 15);
            lblWH.Text = "Αποθήκη:"; lblWH.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblWH);

            this.cmbStockWarehouse.Location = new System.Drawing.Point(10, 35);
            this.cmbStockWarehouse.Size = new System.Drawing.Size(180, 28);
            this.cmbStockWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockWarehouse.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.cmbStockWarehouse);

            var lblVar = new System.Windows.Forms.Label();
            lblVar.AutoSize = true; lblVar.Location = new System.Drawing.Point(200, 15);
            lblVar.Text = "Παραλλαγή:"; lblVar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblVar);

            this.cmbStockVariant.Location = new System.Drawing.Point(200, 35);
            this.cmbStockVariant.Size = new System.Drawing.Size(180, 28);
            this.cmbStockVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockVariant.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.cmbStockVariant);

            var lblLot = new System.Windows.Forms.Label();
            lblLot.AutoSize = true; lblLot.Location = new System.Drawing.Point(390, 15);
            lblLot.Text = "Παρτίδα:"; lblLot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblLot);

            this.cmbStockLot.Location = new System.Drawing.Point(390, 35);
            this.cmbStockLot.Size = new System.Drawing.Size(150, 28);
            this.cmbStockLot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockLot.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.cmbStockLot);

            var lblQty = new System.Windows.Forms.Label();
            lblQty.AutoSize = true; lblQty.Location = new System.Drawing.Point(550, 15);
            lblQty.Text = "Ποσότητα:"; lblQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblQty);

            this.numStockQty.Location = new System.Drawing.Point(550, 35);
            this.numStockQty.Size = new System.Drawing.Size(90, 28);
            this.numStockQty.DecimalPlaces = 4;
            this.numStockQty.Value = 0;
            this.numStockQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.numStockQty);

            var lblRes = new System.Windows.Forms.Label();
            lblRes.AutoSize = true; lblRes.Location = new System.Drawing.Point(650, 15);
            lblRes.Text = "Δεσμευμένο:"; lblRes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblRes);

            this.numStockReserved.Location = new System.Drawing.Point(650, 35);
            this.numStockReserved.Size = new System.Drawing.Size(90, 28);
            this.numStockReserved.DecimalPlaces = 4;
            this.numStockReserved.Value = 0;
            this.numStockReserved.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.numStockReserved);

            var lblMin = new System.Windows.Forms.Label();
            lblMin.AutoSize = true; lblMin.Location = new System.Drawing.Point(750, 15);
            lblMin.Text = "Ελάχιστο:"; lblMin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblMin);

            this.numStockMin.Location = new System.Drawing.Point(750, 35);
            this.numStockMin.Size = new System.Drawing.Size(90, 28);
            this.numStockMin.DecimalPlaces = 4;
            this.numStockMin.Value = 0;
            this.numStockMin.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.numStockMin);

            // Row 2
            var lblMax = new System.Windows.Forms.Label();
            lblMax.AutoSize = true; lblMax.Location = new System.Drawing.Point(10, 70);
            lblMax.Text = "Μέγιστο:"; lblMax.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblMax);

            this.numStockMax.Location = new System.Drawing.Point(10, 90);
            this.numStockMax.Size = new System.Drawing.Size(90, 28);
            this.numStockMax.DecimalPlaces = 4;
            this.numStockMax.Value = 0;
            this.numStockMax.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.numStockMax);

            var lblNotes = new System.Windows.Forms.Label();
            lblNotes.AutoSize = true; lblNotes.Location = new System.Drawing.Point(110, 70);
            lblNotes.Text = "Σημειώσεις:"; lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelStockInputs.Controls.Add(lblNotes);

            this.txtStockNotes.Location = new System.Drawing.Point(110, 90);
            this.txtStockNotes.Size = new System.Drawing.Size(730, 28);
            this.txtStockNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelStockInputs.Controls.Add(this.txtStockNotes);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelStock = new System.Windows.Forms.Panel();
            btnPanelStock.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelStock.Height = 55;
            btnPanelStock.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddStock.Text = "➕ Προσθήκη";
            this.btnAddStock.Size = new System.Drawing.Size(140, 40);
            this.btnAddStock.Location = new System.Drawing.Point(10, 7);
            this.btnAddStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddStock.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddStock.ForeColor = System.Drawing.Color.White;
            this.btnAddStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStock.FlatAppearance.BorderSize = 0;
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);

            this.btnEditStock.Text = "✏️ Επεξεργασία";
            this.btnEditStock.Size = new System.Drawing.Size(150, 40);
            this.btnEditStock.Location = new System.Drawing.Point(160, 7);
            this.btnEditStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditStock.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditStock.ForeColor = System.Drawing.Color.White;
            this.btnEditStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditStock.FlatAppearance.BorderSize = 0;
            this.btnEditStock.Click += new System.EventHandler(this.btnEditStock_Click);

            this.btnDeleteStock.Text = "🗑️ Διαγραφή";
            this.btnDeleteStock.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteStock.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteStock.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteStock.ForeColor = System.Drawing.Color.White;
            this.btnDeleteStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteStock.FlatAppearance.BorderSize = 0;
            this.btnDeleteStock.Click += new System.EventHandler(this.btnDeleteStock_Click);

            btnPanelStock.Controls.Add(this.btnAddStock);
            btnPanelStock.Controls.Add(this.btnEditStock);
            btnPanelStock.Controls.Add(this.btnDeleteStock);

            // ✅ DataGridView (μέση)
            this.dgvWarehouseStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWarehouseStock.AllowUserToAddRows = false;
            this.dgvWarehouseStock.AllowUserToDeleteRows = false;
            this.dgvWarehouseStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWarehouseStock.MultiSelect = false;
            this.dgvWarehouseStock.ReadOnly = true;
            this.dgvWarehouseStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWarehouseStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvWarehouseStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWarehouseStock.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvWarehouseStock.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvWarehouseStock.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvWarehouseStock.ColumnHeadersHeight = 40;
            this.dgvWarehouseStock.RowTemplate.Height = 35;
            this.dgvWarehouseStock.AutoGenerateColumns = false;
            this.dgvWarehouseStock.Columns.Clear();
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Αποθήκη", DataPropertyName = "Αποθήκη", HeaderText = "Αποθήκη", Width = 150 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Παραλλαγή", DataPropertyName = "Παραλλαγή", HeaderText = "Παραλλαγή", Width = 150 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Παρτίδα", DataPropertyName = "Παρτίδα", HeaderText = "Παρτίδα", Width = 100 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ποσότητα", DataPropertyName = "Ποσότητα", HeaderText = "Ποσότητα", Width = 100 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Δεσμευμένο", DataPropertyName = "Δεσμευμένο", HeaderText = "Δεσμευμένο", Width = 100 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Διαθέσιμο", DataPropertyName = "Διαθέσιμο", HeaderText = "Διαθέσιμο", Width = 100 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ελάχιστο", DataPropertyName = "Ελάχιστο", HeaderText = "Ελάχιστο", Width = 100 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Μέγιστο", DataPropertyName = "Μέγιστο", HeaderText = "Μέγιστο", Width = 100 });
            this.dgvWarehouseStock.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σημειώσεις", DataPropertyName = "Σημειώσεις", HeaderText = "Σημειώσεις", Width = 200 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabWarehouseStock.Controls.Add(this.dgvWarehouseStock);
            this.tabWarehouseStock.Controls.Add(panelStockInputs);
            this.tabWarehouseStock.Controls.Add(btnPanelStock);
            this.tabWarehouseStock.Text = "📦 Stock ανά Αποθήκη";
            this.tabWarehouseStock.UseVisualStyleBackColor = true;

            // Tab 15
            this.btnManageImages.Text = "📸 Διαχείριση Φωτογραφιών"; this.btnManageImages.Size = new System.Drawing.Size(300, 50); this.btnManageImages.Location = new System.Drawing.Point(300, 200); this.btnManageImages.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); this.btnManageImages.ForeColor = System.Drawing.Color.White; this.btnManageImages.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnManageImages.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.btnManageImages.Click += new System.EventHandler(this.btnManageImages_Click);
            this.tabImages.Controls.Add(this.btnManageImages); this.tabImages.Text = "📸 Φωτογραφίες"; this.tabImages.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 16: Προμηθευτές Είδους
            // ==========================================
            this.cmbItemSupplierCurrency = new System.Windows.Forms.ComboBox();
            this.numItemSupplierLead = new System.Windows.Forms.NumericUpDown();
            this.numItemSupplierMinQty = new System.Windows.Forms.NumericUpDown();
            this.chkItemSupplierDefault = new System.Windows.Forms.CheckBox();
            this.txtItemSupplierNotes = new System.Windows.Forms.TextBox();
            this.btnEditItemSupplier = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω) - 2 σειρές
            var panelSupplierInputs = new System.Windows.Forms.Panel();
            panelSupplierInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelSupplierInputs.Height = 130;
            panelSupplierInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelSupplierInputs.Padding = new System.Windows.Forms.Padding(10);

            // Row 1
            var lblSup = new System.Windows.Forms.Label();
            lblSup.AutoSize = true; lblSup.Location = new System.Drawing.Point(10, 15);
            lblSup.Text = "Προμηθευτής:"; lblSup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSup);

            this.cmbItemSupplier.Location = new System.Drawing.Point(10, 35);
            this.cmbItemSupplier.Size = new System.Drawing.Size(250, 28);
            this.cmbItemSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemSupplier.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSupplierInputs.Controls.Add(this.cmbItemSupplier);

            var lblSupCode = new System.Windows.Forms.Label();
            lblSupCode.AutoSize = true; lblSupCode.Location = new System.Drawing.Point(270, 15);
            lblSupCode.Text = "Κωδικός Προμηθ.:"; lblSupCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSupCode);

            this.txtItemSupplierCode.Location = new System.Drawing.Point(270, 35);
            this.txtItemSupplierCode.Size = new System.Drawing.Size(150, 28);
            this.txtItemSupplierCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSupplierInputs.Controls.Add(this.txtItemSupplierCode);

            var lblSupPrice = new System.Windows.Forms.Label();
            lblSupPrice.AutoSize = true; lblSupPrice.Location = new System.Drawing.Point(430, 15);
            lblSupPrice.Text = "Τιμή Αγοράς:"; lblSupPrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSupPrice);

            this.numItemSupplierPrice.Location = new System.Drawing.Point(430, 35);
            this.numItemSupplierPrice.Size = new System.Drawing.Size(100, 28);
            this.numItemSupplierPrice.DecimalPlaces = 4;
            this.numItemSupplierPrice.Maximum = 9999999;
            this.numItemSupplierPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSupplierInputs.Controls.Add(this.numItemSupplierPrice);

            var lblSupCurr = new System.Windows.Forms.Label();
            lblSupCurr.AutoSize = true; lblSupCurr.Location = new System.Drawing.Point(540, 15);
            lblSupCurr.Text = "Νόμισμα:"; lblSupCurr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSupCurr);

            this.cmbItemSupplierCurrency.Location = new System.Drawing.Point(540, 35);
            this.cmbItemSupplierCurrency.Size = new System.Drawing.Size(80, 28);
            this.cmbItemSupplierCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemSupplierCurrency.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbItemSupplierCurrency.Items.AddRange(new object[] { "€", "$", "£" });
            this.cmbItemSupplierCurrency.SelectedIndex = 0;
            panelSupplierInputs.Controls.Add(this.cmbItemSupplierCurrency);

            var lblSupLead = new System.Windows.Forms.Label();
            lblSupLead.AutoSize = true; lblSupLead.Location = new System.Drawing.Point(630, 15);
            lblSupLead.Text = "Ημέρες Παρ.:"; lblSupLead.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSupLead);

            this.numItemSupplierLead.Location = new System.Drawing.Point(630, 35);
            this.numItemSupplierLead.Size = new System.Drawing.Size(80, 28);
            this.numItemSupplierLead.DecimalPlaces = 0;
            this.numItemSupplierLead.Maximum = 365;
            this.numItemSupplierLead.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSupplierInputs.Controls.Add(this.numItemSupplierLead);

            var lblSupMin = new System.Windows.Forms.Label();
            lblSupMin.AutoSize = true; lblSupMin.Location = new System.Drawing.Point(720, 15);
            lblSupMin.Text = "Ελάχ. Ποσότητα:"; lblSupMin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSupMin);

            this.numItemSupplierMinQty.Location = new System.Drawing.Point(720, 35);
            this.numItemSupplierMinQty.Size = new System.Drawing.Size(100, 28);
            this.numItemSupplierMinQty.DecimalPlaces = 4;
            this.numItemSupplierMinQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSupplierInputs.Controls.Add(this.numItemSupplierMinQty);

            // Row 2
            this.chkItemSupplierDefault.AutoSize = true;
            this.chkItemSupplierDefault.Location = new System.Drawing.Point(10, 75);
            this.chkItemSupplierDefault.Text = "👑 Βασικός Προμηθευτής";
            this.chkItemSupplierDefault.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(this.chkItemSupplierDefault);

            var lblSupNotes = new System.Windows.Forms.Label();
            lblSupNotes.AutoSize = true; lblSupNotes.Location = new System.Drawing.Point(270, 75);
            lblSupNotes.Text = "Σημειώσεις:"; lblSupNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSupplierInputs.Controls.Add(lblSupNotes);

            this.txtItemSupplierNotes.Location = new System.Drawing.Point(270, 95);
            this.txtItemSupplierNotes.Size = new System.Drawing.Size(550, 28);
            this.txtItemSupplierNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSupplierInputs.Controls.Add(this.txtItemSupplierNotes);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelSuppliers = new System.Windows.Forms.Panel();
            btnPanelSuppliers.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelSuppliers.Height = 55;
            btnPanelSuppliers.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddItemSupplier.Text = "➕ Προσθήκη";
            this.btnAddItemSupplier.Size = new System.Drawing.Size(140, 40);
            this.btnAddItemSupplier.Location = new System.Drawing.Point(10, 7);
            this.btnAddItemSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddItemSupplier.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddItemSupplier.ForeColor = System.Drawing.Color.White;
            this.btnAddItemSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItemSupplier.FlatAppearance.BorderSize = 0;
            this.btnAddItemSupplier.Click += new System.EventHandler(this.btnAddItemSupplier_Click);

            this.btnEditItemSupplier.Text = "✏️ Επεξεργασία";
            this.btnEditItemSupplier.Size = new System.Drawing.Size(150, 40);
            this.btnEditItemSupplier.Location = new System.Drawing.Point(160, 7);
            this.btnEditItemSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditItemSupplier.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditItemSupplier.ForeColor = System.Drawing.Color.White;
            this.btnEditItemSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditItemSupplier.FlatAppearance.BorderSize = 0;
            this.btnEditItemSupplier.Click += new System.EventHandler(this.btnEditItemSupplier_Click);

            this.btnDeleteItemSupplier.Text = "🗑️ Διαγραφή";
            this.btnDeleteItemSupplier.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteItemSupplier.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteItemSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteItemSupplier.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteItemSupplier.ForeColor = System.Drawing.Color.White;
            this.btnDeleteItemSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteItemSupplier.FlatAppearance.BorderSize = 0;
            this.btnDeleteItemSupplier.Click += new System.EventHandler(this.btnDeleteItemSupplier_Click);

            btnPanelSuppliers.Controls.Add(this.btnAddItemSupplier);
            btnPanelSuppliers.Controls.Add(this.btnEditItemSupplier);
            btnPanelSuppliers.Controls.Add(this.btnDeleteItemSupplier);

            // ✅ DataGridView (μέση)
            this.dgvItemSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemSuppliers.AllowUserToAddRows = false;
            this.dgvItemSuppliers.AllowUserToDeleteRows = false;
            this.dgvItemSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemSuppliers.MultiSelect = false;
            this.dgvItemSuppliers.ReadOnly = true;
            this.dgvItemSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemSuppliers.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItemSuppliers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvItemSuppliers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvItemSuppliers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvItemSuppliers.ColumnHeadersHeight = 40;
            this.dgvItemSuppliers.RowTemplate.Height = 35;
            this.dgvItemSuppliers.AutoGenerateColumns = false;
            this.dgvItemSuppliers.Columns.Clear();
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Προμηθευτής", DataPropertyName = "Προμηθευτής", HeaderText = "Προμηθευτής", Width = 200 });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός Προμηθ.", Width = 120 });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τιμή Αγοράς", DataPropertyName = "Τιμή Αγοράς", HeaderText = "Τιμή Αγοράς", Width = 100 });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Νόμισμα", DataPropertyName = "Νόμισμα", HeaderText = "Νόμισμα", Width = 80 });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ημέρες", DataPropertyName = "Ημέρες", HeaderText = "Ημέρες Παράδοσης", Width = 100 });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Βασικός", DataPropertyName = "Βασικός", HeaderText = "Βασικός", Width = 80 });
            this.dgvItemSuppliers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τελευταία Αγορά", DataPropertyName = "Τελευταία Αγορά", HeaderText = "Τελευταία Αγορά", Width = 120 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabItemSuppliers.Controls.Add(this.dgvItemSuppliers);
            this.tabItemSuppliers.Controls.Add(panelSupplierInputs);
            this.tabItemSuppliers.Controls.Add(btnPanelSuppliers);
            this.tabItemSuppliers.Text = "🚚 Προμηθευτές Είδους";
            this.tabItemSuppliers.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 17: Σχετικά Είδη
            // ==========================================
            this.btnEditRelated = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelRelatedInputs = new System.Windows.Forms.Panel();
            panelRelatedInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelRelatedInputs.Height = 100;
            panelRelatedInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelRelatedInputs.Padding = new System.Windows.Forms.Padding(10);

            var lblRelCode = new System.Windows.Forms.Label();
            lblRelCode.AutoSize = true; lblRelCode.Location = new System.Drawing.Point(10, 15);
            lblRelCode.Text = "Κωδικός/ID Είδους*:"; lblRelCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelRelatedInputs.Controls.Add(lblRelCode);

            this.txtRelatedCode.Location = new System.Drawing.Point(10, 35);
            this.txtRelatedCode.Size = new System.Drawing.Size(200, 28);
            this.txtRelatedCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRelatedCode.PlaceholderText = "π.χ. NIKE-001 ή 123";
            panelRelatedInputs.Controls.Add(this.txtRelatedCode);

            var lblRelatedType = new System.Windows.Forms.Label();
            lblRelatedType.AutoSize = true; lblRelatedType.Location = new System.Drawing.Point(220, 15);
            lblRelatedType.Text = "Τύπος Σχέσης:"; lblRelatedType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelRelatedInputs.Controls.Add(lblRelatedType);

            this.cmbRelatedType.Location = new System.Drawing.Point(220, 35);
            this.cmbRelatedType.Size = new System.Drawing.Size(200, 28);
            this.cmbRelatedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelatedType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRelatedType.Items.AddRange(new object[] { "Σχετικό", "Αντικαταστάσιμο", "Αξεσουάρ", "Προτεινόμενο", "Συμπληρωματικό" });
            this.cmbRelatedType.SelectedIndex = 0;
            panelRelatedInputs.Controls.Add(this.cmbRelatedType);

            var lblRelQty = new System.Windows.Forms.Label();
            lblRelQty.AutoSize = true; lblRelQty.Location = new System.Drawing.Point(430, 15);
            lblRelQty.Text = "Ποσότητα:"; lblRelQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelRelatedInputs.Controls.Add(lblRelQty);

            this.numRelatedQty.Location = new System.Drawing.Point(430, 35);
            this.numRelatedQty.Size = new System.Drawing.Size(100, 28);
            this.numRelatedQty.DecimalPlaces = 2;
            this.numRelatedQty.Value = 1;
            this.numRelatedQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelRelatedInputs.Controls.Add(this.numRelatedQty);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelRelated = new System.Windows.Forms.Panel();
            btnPanelRelated.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelRelated.Height = 55;
            btnPanelRelated.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddRelated.Text = "➕ Προσθήκη";
            this.btnAddRelated.Size = new System.Drawing.Size(140, 40);
            this.btnAddRelated.Location = new System.Drawing.Point(10, 7);
            this.btnAddRelated.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddRelated.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddRelated.ForeColor = System.Drawing.Color.White;
            this.btnAddRelated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRelated.FlatAppearance.BorderSize = 0;
            this.btnAddRelated.Click += new System.EventHandler(this.btnAddRelated_Click);

            this.btnEditRelated.Text = "✏️ Επεξεργασία";
            this.btnEditRelated.Size = new System.Drawing.Size(150, 40);
            this.btnEditRelated.Location = new System.Drawing.Point(160, 7);
            this.btnEditRelated.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditRelated.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditRelated.ForeColor = System.Drawing.Color.White;
            this.btnEditRelated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditRelated.FlatAppearance.BorderSize = 0;
            this.btnEditRelated.Click += new System.EventHandler(this.btnEditRelated_Click);

            this.btnDeleteRelated.Text = "️ Διαγραφή";
            this.btnDeleteRelated.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteRelated.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteRelated.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteRelated.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteRelated.ForeColor = System.Drawing.Color.White;
            this.btnDeleteRelated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteRelated.FlatAppearance.BorderSize = 0;
            this.btnDeleteRelated.Click += new System.EventHandler(this.btnDeleteRelated_Click);

            btnPanelRelated.Controls.Add(this.btnAddRelated);
            btnPanelRelated.Controls.Add(this.btnEditRelated);
            btnPanelRelated.Controls.Add(this.btnDeleteRelated);

            // ✅ DataGridView (μέση)
            this.dgvRelated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRelated.AllowUserToAddRows = false;
            this.dgvRelated.AllowUserToDeleteRows = false;
            this.dgvRelated.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRelated.MultiSelect = false;
            this.dgvRelated.ReadOnly = true;
            this.dgvRelated.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRelated.BackgroundColor = System.Drawing.Color.White;
            this.dgvRelated.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRelated.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvRelated.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRelated.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvRelated.ColumnHeadersHeight = 40;
            this.dgvRelated.RowTemplate.Height = 35;
            this.dgvRelated.AutoGenerateColumns = false;
            this.dgvRelated.Columns.Clear();
            this.dgvRelated.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvRelated.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Είδος", DataPropertyName = "Είδος", HeaderText = "Σχετικό Είδος", Width = 250 });
            this.dgvRelated.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 150 });
            this.dgvRelated.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τύπος Σχέσης", DataPropertyName = "Τύπος Σχέσης", HeaderText = "Τύπος Σχέσης", Width = 200 });
            this.dgvRelated.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ποσότητα", DataPropertyName = "Ποσότητα", HeaderText = "Ποσότητα", Width = 120 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabRelated.Controls.Add(this.dgvRelated);
            this.tabRelated.Controls.Add(panelRelatedInputs);
            this.tabRelated.Controls.Add(btnPanelRelated);
            this.tabRelated.Text = " Σχετικά Είδη";
            this.tabRelated.UseVisualStyleBackColor = true;

            // Tab 18
            SetupDataGridView(this.tabPriceHistory, this.dgvPriceHistory, 20, new[] { "Id", "Ημερομηνία", "Παλιά Τιμή", "Νέα Τιμή", "Νόμισμα", "Λόγος", "Τύπος" }, new[] { "Id", "Ημερομηνία", "Παλιά Τιμή", "Νέα Τιμή", "Νόμισμα", "Λόγος", "Τύπος" }, new object[] { false, 150, 120, 120, 100, 200, 150 });
            this.tabPriceHistory.Text = "📊 Ιστορικό Τιμών";

            // ==========================================
            // Tab 19: Χαρακτηριστικά (Attributes)
            // ==========================================
            this.txtAttrCode = new System.Windows.Forms.TextBox();
            this.numAttrSortOrder = new System.Windows.Forms.NumericUpDown();
            this.btnEditAttr = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelAttrInputs = new System.Windows.Forms.Panel();
            panelAttrInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelAttrInputs.Height = 100;
            panelAttrInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelAttrInputs.Padding = new System.Windows.Forms.Padding(10);

            // Row 1
            var lblAttrCode = new System.Windows.Forms.Label();
            lblAttrCode.AutoSize = true; lblAttrCode.Location = new System.Drawing.Point(10, 15);
            lblAttrCode.Text = "Κωδικός:"; lblAttrCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAttrInputs.Controls.Add(lblAttrCode);

            this.txtAttrCode.Location = new System.Drawing.Point(10, 35);
            this.txtAttrCode.Size = new System.Drawing.Size(150, 28);
            this.txtAttrCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAttrCode.PlaceholderText = "π.χ. MATERIAL";
            panelAttrInputs.Controls.Add(this.txtAttrCode);

            var lblAttrName = new System.Windows.Forms.Label();
            lblAttrName.AutoSize = true; lblAttrName.Location = new System.Drawing.Point(170, 15);
            lblAttrName.Text = "Όνομα Χαρακτηριστικού*:"; lblAttrName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAttrInputs.Controls.Add(lblAttrName);

            this.txtAttrName.Location = new System.Drawing.Point(170, 35);
            this.txtAttrName.Size = new System.Drawing.Size(200, 28);
            this.txtAttrName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAttrName.PlaceholderText = "π.χ. Υλικό";
            panelAttrInputs.Controls.Add(this.txtAttrName);

            var lblAttrValue = new System.Windows.Forms.Label();
            lblAttrValue.AutoSize = true; lblAttrValue.Location = new System.Drawing.Point(380, 15);
            lblAttrValue.Text = "Τιμή*:"; lblAttrValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAttrInputs.Controls.Add(lblAttrValue);

            this.txtAttrValue.Location = new System.Drawing.Point(380, 35);
            this.txtAttrValue.Size = new System.Drawing.Size(200, 28);
            this.txtAttrValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAttrValue.PlaceholderText = "π.χ. Πλαστικό";
            panelAttrInputs.Controls.Add(this.txtAttrValue);

            var lblAttrType = new System.Windows.Forms.Label();
            lblAttrType.AutoSize = true; lblAttrType.Location = new System.Drawing.Point(590, 15);
            lblAttrType.Text = "Τύπος:"; lblAttrType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAttrInputs.Controls.Add(lblAttrType);

            this.cmbAttrType.Location = new System.Drawing.Point(590, 35);
            this.cmbAttrType.Size = new System.Drawing.Size(120, 28);
            this.cmbAttrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttrType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAttrType.Items.AddRange(new object[] { "Κείμενο", "Αριθμός", "Ημερομηνία", "Boolean" });
            this.cmbAttrType.SelectedIndex = 0;
            panelAttrInputs.Controls.Add(this.cmbAttrType);

            var lblAttrSort = new System.Windows.Forms.Label();
            lblAttrSort.AutoSize = true; lblAttrSort.Location = new System.Drawing.Point(720, 15);
            lblAttrSort.Text = "Σειρά:"; lblAttrSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAttrInputs.Controls.Add(lblAttrSort);

            this.numAttrSortOrder.Location = new System.Drawing.Point(720, 35);
            this.numAttrSortOrder.Size = new System.Drawing.Size(80, 28);
            this.numAttrSortOrder.DecimalPlaces = 0;
            this.numAttrSortOrder.Maximum = 9999;
            this.numAttrSortOrder.Value = 0;
            this.numAttrSortOrder.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelAttrInputs.Controls.Add(this.numAttrSortOrder);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelAttr = new System.Windows.Forms.Panel();
            btnPanelAttr.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelAttr.Height = 55;
            btnPanelAttr.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddAttr.Text = "➕ Προσθήκη";
            this.btnAddAttr.Size = new System.Drawing.Size(140, 40);
            this.btnAddAttr.Location = new System.Drawing.Point(10, 7);
            this.btnAddAttr.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddAttr.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddAttr.ForeColor = System.Drawing.Color.White;
            this.btnAddAttr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAttr.FlatAppearance.BorderSize = 0;
            this.btnAddAttr.Click += new System.EventHandler(this.btnAddAttr_Click);

            this.btnEditAttr.Text = "✏️ Επεξεργασία";
            this.btnEditAttr.Size = new System.Drawing.Size(150, 40);
            this.btnEditAttr.Location = new System.Drawing.Point(160, 7);
            this.btnEditAttr.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditAttr.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditAttr.ForeColor = System.Drawing.Color.White;
            this.btnEditAttr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditAttr.FlatAppearance.BorderSize = 0;
            this.btnEditAttr.Click += new System.EventHandler(this.btnEditAttr_Click);

            this.btnDeleteAttr.Text = "️ Διαγραφή";
            this.btnDeleteAttr.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteAttr.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteAttr.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteAttr.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteAttr.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAttr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAttr.FlatAppearance.BorderSize = 0;
            this.btnDeleteAttr.Click += new System.EventHandler(this.btnDeleteAttr_Click);

            btnPanelAttr.Controls.Add(this.btnAddAttr);
            btnPanelAttr.Controls.Add(this.btnEditAttr);
            btnPanelAttr.Controls.Add(this.btnDeleteAttr);

            // ✅ DataGridView (μέση)
            this.dgvItemAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemAttributes.AllowUserToAddRows = false;
            this.dgvItemAttributes.AllowUserToDeleteRows = false;
            this.dgvItemAttributes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemAttributes.MultiSelect = false;
            this.dgvItemAttributes.ReadOnly = true;
            this.dgvItemAttributes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemAttributes.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemAttributes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItemAttributes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvItemAttributes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvItemAttributes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvItemAttributes.ColumnHeadersHeight = 40;
            this.dgvItemAttributes.RowTemplate.Height = 35;
            this.dgvItemAttributes.AutoGenerateColumns = false;
            this.dgvItemAttributes.Columns.Clear();
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 120 });
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Χαρακτηριστικό", DataPropertyName = "Χαρακτηριστικό", HeaderText = "Χαρακτηριστικό", Width = 200 });
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τιμή", DataPropertyName = "Τιμή", HeaderText = "Τιμή", Width = 200 });
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Τύπος", DataPropertyName = "Τύπος", HeaderText = "Τύπος", Width = 120 });
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σειρά", DataPropertyName = "Σειρά", HeaderText = "Σειρά", Width = 80 });
            this.dgvItemAttributes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", HeaderText = "Ενεργό", Width = 80 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabItemAttributes.Controls.Add(this.dgvItemAttributes);
            this.tabItemAttributes.Controls.Add(panelAttrInputs);
            this.tabItemAttributes.Controls.Add(btnPanelAttr);
            this.tabItemAttributes.Text = "⚙️ Χαρακτηριστικά";
            this.tabItemAttributes.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 20: Εναλλακτικοί Κωδικοί
            // ==========================================
            this.btnEditAlt = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelAltInputs = new System.Windows.Forms.Panel();
            panelAltInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelAltInputs.Height = 100;
            panelAltInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelAltInputs.Padding = new System.Windows.Forms.Padding(10);

            var lblAltCode = new System.Windows.Forms.Label();
            lblAltCode.AutoSize = true; lblAltCode.Location = new System.Drawing.Point(10, 15);
            lblAltCode.Text = "Εναλλακτικός Κωδικός*:"; lblAltCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAltInputs.Controls.Add(lblAltCode);

            this.txtAltCode.Location = new System.Drawing.Point(10, 35);
            this.txtAltCode.Size = new System.Drawing.Size(250, 28);
            this.txtAltCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAltCode.PlaceholderText = "π.χ. ALT-001";
            panelAltInputs.Controls.Add(this.txtAltCode);

            var lblAltDesc = new System.Windows.Forms.Label();
            lblAltDesc.AutoSize = true; lblAltDesc.Location = new System.Drawing.Point(270, 15);
            lblAltDesc.Text = "Περιγραφή:"; lblAltDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelAltInputs.Controls.Add(lblAltDesc);

            this.txtAltDesc.Location = new System.Drawing.Point(270, 35);
            this.txtAltDesc.Size = new System.Drawing.Size(600, 28);
            this.txtAltDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelAltInputs.Controls.Add(this.txtAltDesc);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelAlt = new System.Windows.Forms.Panel();
            btnPanelAlt.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelAlt.Height = 55;
            btnPanelAlt.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddAlt.Text = " Προσθήκη";
            this.btnAddAlt.Size = new System.Drawing.Size(140, 40);
            this.btnAddAlt.Location = new System.Drawing.Point(10, 7);
            this.btnAddAlt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddAlt.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddAlt.ForeColor = System.Drawing.Color.White;
            this.btnAddAlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAlt.FlatAppearance.BorderSize = 0;
            this.btnAddAlt.Click += new System.EventHandler(this.btnAddAlt_Click);

            this.btnEditAlt.Text = "✏️ Επεξεργασία";
            this.btnEditAlt.Size = new System.Drawing.Size(150, 40);
            this.btnEditAlt.Location = new System.Drawing.Point(160, 7);
            this.btnEditAlt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditAlt.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditAlt.ForeColor = System.Drawing.Color.White;
            this.btnEditAlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditAlt.FlatAppearance.BorderSize = 0;
            this.btnEditAlt.Click += new System.EventHandler(this.btnEditAlt_Click);

            this.btnDeleteAlt.Text = "🗑️ Διαγραφή";
            this.btnDeleteAlt.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteAlt.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteAlt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteAlt.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteAlt.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAlt.FlatAppearance.BorderSize = 0;
            this.btnDeleteAlt.Click += new System.EventHandler(this.btnDeleteAlt_Click);

            btnPanelAlt.Controls.Add(this.btnAddAlt);
            btnPanelAlt.Controls.Add(this.btnEditAlt);
            btnPanelAlt.Controls.Add(this.btnDeleteAlt);

            // ✅ DataGridView (μέση)
            this.dgvAlternativeCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlternativeCodes.AllowUserToAddRows = false;
            this.dgvAlternativeCodes.AllowUserToDeleteRows = false;
            this.dgvAlternativeCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlternativeCodes.MultiSelect = false;
            this.dgvAlternativeCodes.ReadOnly = true;
            this.dgvAlternativeCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlternativeCodes.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlternativeCodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlternativeCodes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvAlternativeCodes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvAlternativeCodes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvAlternativeCodes.ColumnHeadersHeight = 40;
            this.dgvAlternativeCodes.RowTemplate.Height = 35;
            this.dgvAlternativeCodes.AutoGenerateColumns = false;
            this.dgvAlternativeCodes.Columns.Clear();
            this.dgvAlternativeCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvAlternativeCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Εναλλακτικός Κωδικός", DataPropertyName = "Εναλλακτικός Κωδικός", HeaderText = "Εναλλακτικός Κωδικός", Width = 300 });
            this.dgvAlternativeCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Περιγραφή", DataPropertyName = "Περιγραφή", HeaderText = "Περιγραφή", Width = 500 });
            this.dgvAlternativeCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", HeaderText = "Ενεργό", Width = 100 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabAlternativeCodes.Controls.Add(this.dgvAlternativeCodes);
            this.tabAlternativeCodes.Controls.Add(panelAltInputs);
            this.tabAlternativeCodes.Controls.Add(btnPanelAlt);
            this.tabAlternativeCodes.Text = " Εναλλακτικοί Κωδικοί";
            this.tabAlternativeCodes.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 21: Χρώματα (με Color Picker)
            // ==========================================
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnEditColor = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelColorInputs = new System.Windows.Forms.Panel();
            panelColorInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelColorInputs.Height = 100;
            panelColorInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelColorInputs.Padding = new System.Windows.Forms.Padding(10);

            var lblColorCode = new System.Windows.Forms.Label();
            lblColorCode.AutoSize = true; lblColorCode.Location = new System.Drawing.Point(10, 15);
            lblColorCode.Text = "Κωδικός:"; lblColorCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelColorInputs.Controls.Add(lblColorCode);

            this.txtColorCode.Location = new System.Drawing.Point(10, 35);
            this.txtColorCode.Size = new System.Drawing.Size(120, 28);
            this.txtColorCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColorCode.PlaceholderText = "π.χ. RED-01";
            panelColorInputs.Controls.Add(this.txtColorCode);

            var lblColorName = new System.Windows.Forms.Label();
            lblColorName.AutoSize = true; lblColorName.Location = new System.Drawing.Point(140, 15);
            lblColorName.Text = "Όνομα Χρώματος*:"; lblColorName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelColorInputs.Controls.Add(lblColorName);

            this.txtColorName.Location = new System.Drawing.Point(140, 35);
            this.txtColorName.Size = new System.Drawing.Size(200, 28);
            this.txtColorName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColorName.PlaceholderText = "π.χ. Κόκκινο Ferrari";
            panelColorInputs.Controls.Add(this.txtColorName);

            // 🎨 Color Preview Panel (αντί για Hex textbox)
            var lblColor = new System.Windows.Forms.Label();
            lblColor.AutoSize = true; lblColor.Location = new System.Drawing.Point(350, 15);
            lblColor.Text = "Χρώμα:"; lblColor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelColorInputs.Controls.Add(lblColor);

            this.pnlColorPreview.Location = new System.Drawing.Point(350, 35);
            this.pnlColorPreview.Size = new System.Drawing.Size(80, 28);
            this.pnlColorPreview.BackColor = System.Drawing.Color.White;
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlColorPreview.Click += new System.EventHandler(this.pnlColorPreview_Click);

            // Tooltip για το panel
            var toolTipColor = new System.Windows.Forms.ToolTip();
            toolTipColor.SetToolTip(this.pnlColorPreview, "Κλικ για επιλογή χρώματος");

            panelColorInputs.Controls.Add(this.pnlColorPreview);

            // Hex label (read-only, ενημερώνεται αυτόματα)
            var lblColorHex = new System.Windows.Forms.Label();
            lblColorHex.AutoSize = true; lblColorHex.Location = new System.Drawing.Point(440, 15);
            lblColorHex.Text = "Hex:"; lblColorHex.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelColorInputs.Controls.Add(lblColorHex);

            this.txtColorHex.Location = new System.Drawing.Point(440, 35);
            this.txtColorHex.Size = new System.Drawing.Size(100, 28);
            this.txtColorHex.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColorHex.PlaceholderText = "#FF0000";
            this.txtColorHex.ReadOnly = true;
            this.txtColorHex.BackColor = System.Drawing.Color.WhiteSmoke;
            panelColorInputs.Controls.Add(this.txtColorHex);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelColor = new System.Windows.Forms.Panel();
            btnPanelColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelColor.Height = 55;
            btnPanelColor.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddColor.Text = "➕ Προσθήκη";
            this.btnAddColor.Size = new System.Drawing.Size(140, 40);
            this.btnAddColor.Location = new System.Drawing.Point(10, 7);
            this.btnAddColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddColor.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddColor.ForeColor = System.Drawing.Color.White;
            this.btnAddColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddColor.FlatAppearance.BorderSize = 0;
            this.btnAddColor.Click += new System.EventHandler(this.btnAddColor_Click);

            this.btnEditColor.Text = "️ Επεξεργασία";
            this.btnEditColor.Size = new System.Drawing.Size(150, 40);
            this.btnEditColor.Location = new System.Drawing.Point(160, 7);
            this.btnEditColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditColor.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditColor.ForeColor = System.Drawing.Color.White;
            this.btnEditColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditColor.FlatAppearance.BorderSize = 0;
            this.btnEditColor.Click += new System.EventHandler(this.btnEditColor_Click);

            this.btnDeleteColor.Text = "🗑️ Διαγραφή";
            this.btnDeleteColor.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteColor.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteColor.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteColor.ForeColor = System.Drawing.Color.White;
            this.btnDeleteColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteColor.FlatAppearance.BorderSize = 0;
            this.btnDeleteColor.Click += new System.EventHandler(this.btnDeleteColor_Click);

            btnPanelColor.Controls.Add(this.btnAddColor);
            btnPanelColor.Controls.Add(this.btnEditColor);
            btnPanelColor.Controls.Add(this.btnDeleteColor);

            // ✅ DataGridView (μέση)
            this.dgvItemColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemColors.AllowUserToAddRows = false;
            this.dgvItemColors.AllowUserToDeleteRows = false;
            this.dgvItemColors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemColors.MultiSelect = false;
            this.dgvItemColors.ReadOnly = true;
            this.dgvItemColors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemColors.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemColors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItemColors.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvItemColors.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvItemColors.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvItemColors.ColumnHeadersHeight = 40;
            this.dgvItemColors.RowTemplate.Height = 35;
            this.dgvItemColors.AutoGenerateColumns = false;
            this.dgvItemColors.Columns.Clear();
            this.dgvItemColors.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvItemColors.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 120 });
            this.dgvItemColors.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", HeaderText = "Όνομα Χρώματος", Width = 250 });
            this.dgvItemColors.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Hex", DataPropertyName = "Hex", HeaderText = "Hex", Width = 100 });
            this.dgvItemColors.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", HeaderText = "Ενεργό", Width = 100 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabColors.Controls.Add(this.dgvItemColors);
            this.tabColors.Controls.Add(panelColorInputs);
            this.tabColors.Controls.Add(btnPanelColor);
            this.tabColors.Text = "🎨 Χρώματα";
            this.tabColors.UseVisualStyleBackColor = true;

            

            // ==========================================
            // Tab 22: CPV Codes
            // ==========================================
            this.cmbCPVCode = new System.Windows.Forms.ComboBox();
            this.btnEditCPV = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelCPVInputs = new System.Windows.Forms.Panel();
            panelCPVInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelCPVInputs.Height = 100;
            panelCPVInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelCPVInputs.Padding = new System.Windows.Forms.Padding(10);

            var lblCPVCode = new System.Windows.Forms.Label();
            lblCPVCode.AutoSize = true; lblCPVCode.Location = new System.Drawing.Point(10, 15);
            lblCPVCode.Text = "CPV Κωδικός*:"; lblCPVCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelCPVInputs.Controls.Add(lblCPVCode);

            this.cmbCPVCode.Location = new System.Drawing.Point(10, 35);
            this.cmbCPVCode.Size = new System.Drawing.Size(300, 28);
            this.cmbCPVCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbCPVCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelCPVInputs.Controls.Add(this.cmbCPVCode);

            this.chkCPVPrimary.AutoSize = true;
            this.chkCPVPrimary.Location = new System.Drawing.Point(320, 37);
            this.chkCPVPrimary.Text = "👑 Κύριο CPV";
            this.chkCPVPrimary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            panelCPVInputs.Controls.Add(this.chkCPVPrimary);

            var lblCPVNotes = new System.Windows.Forms.Label();
            lblCPVNotes.AutoSize = true; lblCPVNotes.Location = new System.Drawing.Point(450, 15);
            lblCPVNotes.Text = "Σημειώσεις:"; lblCPVNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelCPVInputs.Controls.Add(lblCPVNotes);

            this.txtCPVNotes.Location = new System.Drawing.Point(450, 35);
            this.txtCPVNotes.Size = new System.Drawing.Size(420, 28);
            this.txtCPVNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelCPVInputs.Controls.Add(this.txtCPVNotes);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelCPV = new System.Windows.Forms.Panel();
            btnPanelCPV.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelCPV.Height = 55;
            btnPanelCPV.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddCPV.Text = "➕ Προσθήκη";
            this.btnAddCPV.Size = new System.Drawing.Size(140, 40);
            this.btnAddCPV.Location = new System.Drawing.Point(10, 7);
            this.btnAddCPV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddCPV.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddCPV.ForeColor = System.Drawing.Color.White;
            this.btnAddCPV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCPV.FlatAppearance.BorderSize = 0;
            this.btnAddCPV.Click += new System.EventHandler(this.btnAddCPV_Click);

            this.btnEditCPV.Text = "✏️ Επεξεργασία";
            this.btnEditCPV.Size = new System.Drawing.Size(150, 40);
            this.btnEditCPV.Location = new System.Drawing.Point(160, 7);
            this.btnEditCPV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditCPV.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditCPV.ForeColor = System.Drawing.Color.White;
            this.btnEditCPV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditCPV.FlatAppearance.BorderSize = 0;
            this.btnEditCPV.Click += new System.EventHandler(this.btnEditCPV_Click);

            this.btnDeleteCPV.Text = "️ Διαγραφή";
            this.btnDeleteCPV.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteCPV.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteCPV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteCPV.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteCPV.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCPV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCPV.FlatAppearance.BorderSize = 0;
            this.btnDeleteCPV.Click += new System.EventHandler(this.btnDeleteCPV_Click);

            btnPanelCPV.Controls.Add(this.btnAddCPV);
            btnPanelCPV.Controls.Add(this.btnEditCPV);
            btnPanelCPV.Controls.Add(this.btnDeleteCPV);

            // ✅ DataGridView (μέση)
            this.dgvCPVCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCPVCodes.AllowUserToAddRows = false;
            this.dgvCPVCodes.AllowUserToDeleteRows = false;
            this.dgvCPVCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCPVCodes.MultiSelect = false;
            this.dgvCPVCodes.ReadOnly = true;
            this.dgvCPVCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCPVCodes.BackgroundColor = System.Drawing.Color.White;
            this.dgvCPVCodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCPVCodes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvCPVCodes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCPVCodes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvCPVCodes.ColumnHeadersHeight = 40;
            this.dgvCPVCodes.RowTemplate.Height = 35;
            this.dgvCPVCodes.AutoGenerateColumns = false;
            this.dgvCPVCodes.Columns.Clear();
            this.dgvCPVCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvCPVCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "CPV Κωδικός", DataPropertyName = "CPV Κωδικός", HeaderText = "CPV Κωδικός", Width = 150 });
            this.dgvCPVCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Περιγραφή", DataPropertyName = "Περιγραφή", HeaderText = "Περιγραφή CPV", Width = 400 });
            this.dgvCPVCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κύριο", DataPropertyName = "Κύριο", HeaderText = "Κύριο", Width = 100 });
            this.dgvCPVCodes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σημειώσεις", DataPropertyName = "Σημειώσεις", HeaderText = "Σημειώσεις", Width = 200 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabCPVCodes.Controls.Add(this.dgvCPVCodes);
            this.tabCPVCodes.Controls.Add(panelCPVInputs);
            this.tabCPVCodes.Controls.Add(btnPanelCPV);
            this.tabCPVCodes.Text = "🏷️ CPV Codes";
            this.tabCPVCodes.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 23: Σειριακοί Αριθμοί (ΤΕΛΙΚΗ ΔΙΟΡΘΩΣΗ)
            // ==========================================
            this.cmbSerialStatus = new System.Windows.Forms.ComboBox();
            this.dtpSerialWarrantyStart = new System.Windows.Forms.DateTimePicker();
            this.dtpSerialWarrantyEnd = new System.Windows.Forms.DateTimePicker();
            this.txtSerialSearch = new System.Windows.Forms.TextBox();
            this.btnEditSerial = new System.Windows.Forms.Button();
            this.btnCancelSerial = new System.Windows.Forms.Button();
            this.btnSerialSearch = new System.Windows.Forms.Button();
            this.btnSerialFirst = new System.Windows.Forms.Button();
            this.btnSerialPrev = new System.Windows.Forms.Button();
            this.btnSerialNext = new System.Windows.Forms.Button();
            this.btnSerialLast = new System.Windows.Forms.Button();
            this.lblSerialRecordInfo = new System.Windows.Forms.Label();
            this.serialBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // ✅ Panel για τα inputs (πάνω) - 2 σειρές
            var panelSerialInputs = new System.Windows.Forms.Panel();
            panelSerialInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelSerialInputs.Height = 130;
            panelSerialInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelSerialInputs.Padding = new System.Windows.Forms.Padding(10);

            // Row 1
            var lblSerNum = new System.Windows.Forms.Label();
            lblSerNum.AutoSize = true; lblSerNum.Location = new System.Drawing.Point(10, 15);
            lblSerNum.Text = "Σειριακός Αριθμός*:"; lblSerNum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSerialInputs.Controls.Add(lblSerNum);

            this.txtSerialNumber.Location = new System.Drawing.Point(10, 35);
            this.txtSerialNumber.Size = new System.Drawing.Size(160, 28);
            this.txtSerialNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSerialNumber.PlaceholderText = "π.χ. SN-12345";
            panelSerialInputs.Controls.Add(this.txtSerialNumber);

            var lblSerLot = new System.Windows.Forms.Label();
            lblSerLot.AutoSize = true; lblSerLot.Location = new System.Drawing.Point(180, 15);
            lblSerLot.Text = "Παρτίδα:"; lblSerLot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSerialInputs.Controls.Add(lblSerLot);

            this.cmbSerialLotId.Location = new System.Drawing.Point(180, 35);
            this.cmbSerialLotId.Size = new System.Drawing.Size(150, 28);
            this.cmbSerialLotId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialLotId.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSerialInputs.Controls.Add(this.cmbSerialLotId);

            var lblSerStatus = new System.Windows.Forms.Label();
            lblSerStatus.AutoSize = true; lblSerStatus.Location = new System.Drawing.Point(340, 15);
            lblSerStatus.Text = "Κατάσταση:"; lblSerStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSerialInputs.Controls.Add(lblSerStatus);

            this.cmbSerialStatus.Location = new System.Drawing.Point(340, 35);
            this.cmbSerialStatus.Size = new System.Drawing.Size(140, 28);
            this.cmbSerialStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSerialStatus.Items.AddRange(new object[] { "Διαθέσιμο", "Πωλημένο", "Σε Επισκευή", "Απενεργοποιημένο" });
            this.cmbSerialStatus.SelectedIndex = 0;
            panelSerialInputs.Controls.Add(this.cmbSerialStatus);

            var lblSerNotes = new System.Windows.Forms.Label();
            lblSerNotes.AutoSize = true; lblSerNotes.Location = new System.Drawing.Point(490, 15);
            lblSerNotes.Text = "Σημειώσεις:"; lblSerNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSerialInputs.Controls.Add(lblSerNotes);

            this.txtSerialNotes.Location = new System.Drawing.Point(490, 35);
            this.txtSerialNotes.Size = new System.Drawing.Size(380, 28);
            this.txtSerialNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSerialInputs.Controls.Add(this.txtSerialNotes);

            // Row 2
            var lblSerWStart = new System.Windows.Forms.Label();
            lblSerWStart.AutoSize = true; lblSerWStart.Location = new System.Drawing.Point(10, 70);
            lblSerWStart.Text = "📅 Εγγύηση Από:"; lblSerWStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSerialInputs.Controls.Add(lblSerWStart);

            this.dtpSerialWarrantyStart.Location = new System.Drawing.Point(10, 90);
            this.dtpSerialWarrantyStart.Size = new System.Drawing.Size(150, 28);
            this.dtpSerialWarrantyStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpSerialWarrantyStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSerialWarrantyStart.CustomFormat = "dd/MM/yyyy";
            this.dtpSerialWarrantyStart.ShowCheckBox = true;
            this.dtpSerialWarrantyStart.Checked = false;
            panelSerialInputs.Controls.Add(this.dtpSerialWarrantyStart);

            var lblSerWEnd = new System.Windows.Forms.Label();
            lblSerWEnd.AutoSize = true; lblSerWEnd.Location = new System.Drawing.Point(170, 70);
            lblSerWEnd.Text = "📅 Εγγύηση Έως:"; lblSerWEnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSerialInputs.Controls.Add(lblSerWEnd);

            this.dtpSerialWarrantyEnd.Location = new System.Drawing.Point(170, 90);
            this.dtpSerialWarrantyEnd.Size = new System.Drawing.Size(150, 28);
            this.dtpSerialWarrantyEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpSerialWarrantyEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSerialWarrantyEnd.CustomFormat = "dd/MM/yyyy";
            this.dtpSerialWarrantyEnd.ShowCheckBox = true;
            this.dtpSerialWarrantyEnd.Checked = false;
            panelSerialInputs.Controls.Add(this.dtpSerialWarrantyEnd);

            // ✅ Panel για αναζήτηση & πλοήγηση (κάτω - 1ο, ΠΑΝΩ από τα κουμπιά)
            var btnPanelSerialSearch = new System.Windows.Forms.Panel();
            btnPanelSerialSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelSerialSearch.Height = 45;
            btnPanelSerialSearch.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            var lblSerSearch = new System.Windows.Forms.Label();
            lblSerSearch.AutoSize = true; lblSerSearch.Location = new System.Drawing.Point(10, 12);
            lblSerSearch.Text = "🔍 Αναζήτηση:"; lblSerSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnPanelSerialSearch.Controls.Add(lblSerSearch);

            this.txtSerialSearch.Location = new System.Drawing.Point(110, 8);
            this.txtSerialSearch.Size = new System.Drawing.Size(250, 28);
            this.txtSerialSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSerialSearch.PlaceholderText = "Αναζήτηση σειριακού...";
            this.txtSerialSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerialSearch_KeyPress);
            btnPanelSerialSearch.Controls.Add(this.txtSerialSearch);

            this.btnSerialSearch.Text = "Εύρεση";
            this.btnSerialSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSerialSearch.Location = new System.Drawing.Point(370, 8);
            this.btnSerialSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSerialSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSerialSearch.ForeColor = System.Drawing.Color.White;
            this.btnSerialSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerialSearch.FlatAppearance.BorderSize = 0;
            this.btnSerialSearch.Click += new System.EventHandler(this.btnSerialSearch_Click);
            btnPanelSerialSearch.Controls.Add(this.btnSerialSearch);

            var lblSerNav = new System.Windows.Forms.Label();
            lblSerNav.AutoSize = true; lblSerNav.Location = new System.Drawing.Point(470, 12);
            lblSerNav.Text = "Πλοήγηση:"; lblSerNav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnPanelSerialSearch.Controls.Add(lblSerNav);

            this.btnSerialFirst.Text = "<<";
            this.btnSerialFirst.Size = new System.Drawing.Size(40, 28);
            this.btnSerialFirst.Location = new System.Drawing.Point(550, 8);
            this.btnSerialFirst.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSerialFirst.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnSerialFirst.ForeColor = System.Drawing.Color.White;
            this.btnSerialFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerialFirst.FlatAppearance.BorderSize = 0;
            this.btnSerialFirst.Click += new System.EventHandler(this.btnSerialFirst_Click);
            btnPanelSerialSearch.Controls.Add(this.btnSerialFirst);

            this.btnSerialPrev.Text = "<";
            this.btnSerialPrev.Size = new System.Drawing.Size(40, 28);
            this.btnSerialPrev.Location = new System.Drawing.Point(595, 8);
            this.btnSerialPrev.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSerialPrev.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnSerialPrev.ForeColor = System.Drawing.Color.White;
            this.btnSerialPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerialPrev.FlatAppearance.BorderSize = 0;
            this.btnSerialPrev.Click += new System.EventHandler(this.btnSerialPrev_Click);
            btnPanelSerialSearch.Controls.Add(this.btnSerialPrev);

            this.btnSerialNext.Text = ">";
            this.btnSerialNext.Size = new System.Drawing.Size(40, 28);
            this.btnSerialNext.Location = new System.Drawing.Point(640, 8);
            this.btnSerialNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSerialNext.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnSerialNext.ForeColor = System.Drawing.Color.White;
            this.btnSerialNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerialNext.FlatAppearance.BorderSize = 0;
            this.btnSerialNext.Click += new System.EventHandler(this.btnSerialNext_Click);
            btnPanelSerialSearch.Controls.Add(this.btnSerialNext);

            this.btnSerialLast.Text = ">>";
            this.btnSerialLast.Size = new System.Drawing.Size(40, 28);
            this.btnSerialLast.Location = new System.Drawing.Point(685, 8);
            this.btnSerialLast.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSerialLast.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnSerialLast.ForeColor = System.Drawing.Color.White;
            this.btnSerialLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerialLast.FlatAppearance.BorderSize = 0;
            this.btnSerialLast.Click += new System.EventHandler(this.btnSerialLast_Click);
            btnPanelSerialSearch.Controls.Add(this.btnSerialLast);

            this.lblSerialRecordInfo.AutoSize = true;
            this.lblSerialRecordInfo.Location = new System.Drawing.Point(735, 12);
            this.lblSerialRecordInfo.Text = "0 / 0";
            this.lblSerialRecordInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnPanelSerialSearch.Controls.Add(this.lblSerialRecordInfo);

            // ✅ Panel για τα κουμπιά λειτουργίας (κάτω - 2ο, στο ΠΟΛΥ ΚΑΤΩ μέρος)
            var btnPanelSerialActions = new System.Windows.Forms.Panel();
            btnPanelSerialActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelSerialActions.Height = 55;
            btnPanelSerialActions.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            // Κουμπί Αποθήκευση
            var btnSaveSerial = new System.Windows.Forms.Button();
            btnSaveSerial.Text = "💾 Αποθήκευση";
            btnSaveSerial.Size = new System.Drawing.Size(140, 40);
            btnSaveSerial.Location = new System.Drawing.Point(10, 7);
            btnSaveSerial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSaveSerial.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSaveSerial.ForeColor = System.Drawing.Color.White;
            btnSaveSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSaveSerial.FlatAppearance.BorderSize = 0;
            btnSaveSerial.Click += new System.EventHandler(this.btnSaveSerial_Click);
            btnPanelSerialActions.Controls.Add(btnSaveSerial);

            this.btnAddSerial.Text = "➕ Προσθήκη";
            this.btnAddSerial.Size = new System.Drawing.Size(120, 40);
            this.btnAddSerial.Location = new System.Drawing.Point(160, 7);
            this.btnAddSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddSerial.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnAddSerial.ForeColor = System.Drawing.Color.White;
            this.btnAddSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSerial.FlatAppearance.BorderSize = 0;
            this.btnAddSerial.Click += new System.EventHandler(this.btnAddSerial_Click);
            btnPanelSerialActions.Controls.Add(this.btnAddSerial);

            this.btnEditSerial.Text = "✏️ Επεξεργασία";
            this.btnEditSerial.Size = new System.Drawing.Size(130, 40);
            this.btnEditSerial.Location = new System.Drawing.Point(290, 7);
            this.btnEditSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditSerial.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnEditSerial.ForeColor = System.Drawing.Color.White;
            this.btnEditSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditSerial.FlatAppearance.BorderSize = 0;
            this.btnEditSerial.Click += new System.EventHandler(this.btnEditSerial_Click);
            btnPanelSerialActions.Controls.Add(this.btnEditSerial);

            this.btnDeleteSerial.Text = "🗑️ Διαγραφή";
            this.btnDeleteSerial.Size = new System.Drawing.Size(120, 40);
            this.btnDeleteSerial.Location = new System.Drawing.Point(430, 7);
            this.btnDeleteSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteSerial.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteSerial.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSerial.FlatAppearance.BorderSize = 0;
            this.btnDeleteSerial.Click += new System.EventHandler(this.btnDeleteSerial_Click);
            btnPanelSerialActions.Controls.Add(this.btnDeleteSerial);

            this.btnCancelSerial.Text = "❌ Ακύρωση";
            this.btnCancelSerial.Size = new System.Drawing.Size(110, 40);
            this.btnCancelSerial.Location = new System.Drawing.Point(560, 7);
            this.btnCancelSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelSerial.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancelSerial.ForeColor = System.Drawing.Color.White;
            this.btnCancelSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSerial.FlatAppearance.BorderSize = 0;
            this.btnCancelSerial.Visible = false;
            this.btnCancelSerial.Click += new System.EventHandler(this.btnCancelSerial_Click);
            btnPanelSerialActions.Controls.Add(this.btnCancelSerial);

            // ✅ DataGridView (μέση - γεμίζει τον υπόλοιπο χώρο)
            this.dgvSerialNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSerialNumbers.AllowUserToAddRows = false;
            this.dgvSerialNumbers.AllowUserToDeleteRows = false;
            this.dgvSerialNumbers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSerialNumbers.MultiSelect = false;
            this.dgvSerialNumbers.ReadOnly = true;
            this.dgvSerialNumbers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSerialNumbers.BackgroundColor = System.Drawing.Color.White;
            this.dgvSerialNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSerialNumbers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvSerialNumbers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSerialNumbers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvSerialNumbers.ColumnHeadersHeight = 40;
            this.dgvSerialNumbers.RowTemplate.Height = 35;
            this.dgvSerialNumbers.AutoGenerateColumns = false;
            this.dgvSerialNumbers.DataSource = this.serialBindingSource;
            this.dgvSerialNumbers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSerialNumbers_CellDoubleClick);
            this.dgvSerialNumbers.SelectionChanged += new System.EventHandler(this.dgvSerialNumbers_SelectionChanged);
            this.dgvSerialNumbers.Columns.Clear();
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σειριακός", DataPropertyName = "Σειριακός", HeaderText = "Σειριακός Αριθμός", Width = 150 });
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Παρτίδα", DataPropertyName = "Παρτίδα", HeaderText = "Παρτίδα", Width = 100 });
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Εγγύηση Από", DataPropertyName = "Εγγύηση Από", HeaderText = "📅 Εγγύηση Από", Width = 120 });
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Εγγύηση Έως", DataPropertyName = "Εγγύηση Έως", HeaderText = "📅 Εγγύηση Έως", Width = 120 });
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κατάσταση", DataPropertyName = "Κατάσταση", HeaderText = "Κατάσταση", Width = 120 });
            this.dgvSerialNumbers.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σημειώσεις", DataPropertyName = "Σημειώσεις", HeaderText = "Σημειώσεις", Width = 200 });
            this.dgvSerialNumbers.Columns["Id"].Visible = false;
            // ✅ Σωστή σειρά: Panel Top → DataGridView → Panel Search → Panel Actions
            this.tabSerialNumbers.Controls.Add(this.dgvSerialNumbers);
            this.tabSerialNumbers.Controls.Add(panelSerialInputs);
            this.tabSerialNumbers.Controls.Add(btnPanelSerialSearch); // Προστίθεται 2ο, άρα θα είναι ΠΑΝΩ από το Actions
            this.tabSerialNumbers.Controls.Add(btnPanelSerialActions); // Προστίθεται 3ο, άρα θα είναι στο ΠΟΛΥ ΚΑΤΩ μέρος
            this.tabSerialNumbers.Text = "🔢 Σειριακοί Αριθμοί";
            this.tabSerialNumbers.UseVisualStyleBackColor = true;

            // ==========================================
            // Tab 24: Μεγεθολόγιο
            // ==========================================
            this.tabSizes = new System.Windows.Forms.TabPage();
            this.dgvSizes = new System.Windows.Forms.DataGridView();
            this.txtSizeCode = new System.Windows.Forms.TextBox();
            this.txtSizeName = new System.Windows.Forms.TextBox();
            this.numSizeSortOrder = new System.Windows.Forms.NumericUpDown();
            this.chkSizeActive = new System.Windows.Forms.CheckBox();
            this.btnAddSize = new System.Windows.Forms.Button();
            this.btnEditSize = new System.Windows.Forms.Button();
            this.btnDeleteSize = new System.Windows.Forms.Button();

            // ✅ Panel για τα inputs (πάνω)
            var panelSizeInputs = new System.Windows.Forms.Panel();
            panelSizeInputs.Dock = System.Windows.Forms.DockStyle.Top;
            panelSizeInputs.Height = 100;
            panelSizeInputs.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            panelSizeInputs.Padding = new System.Windows.Forms.Padding(10);

            var lblSizeCode = new System.Windows.Forms.Label();
            lblSizeCode.AutoSize = true; lblSizeCode.Location = new System.Drawing.Point(10, 15);
            lblSizeCode.Text = "Κωδικός:"; lblSizeCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSizeInputs.Controls.Add(lblSizeCode);

            this.txtSizeCode.Location = new System.Drawing.Point(10, 35);
            this.txtSizeCode.Size = new System.Drawing.Size(120, 28);
            this.txtSizeCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSizeCode.PlaceholderText = "π.χ. XL";
            panelSizeInputs.Controls.Add(this.txtSizeCode);

            var lblSizeName = new System.Windows.Forms.Label();
            lblSizeName.AutoSize = true; lblSizeName.Location = new System.Drawing.Point(140, 15);
            lblSizeName.Text = "Όνομα Μεγέθους*:"; lblSizeName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSizeInputs.Controls.Add(lblSizeName);

            this.txtSizeName.Location = new System.Drawing.Point(140, 35);
            this.txtSizeName.Size = new System.Drawing.Size(200, 28);
            this.txtSizeName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSizeName.PlaceholderText = "π.χ. Extra Large";
            panelSizeInputs.Controls.Add(this.txtSizeName);

            var lblSizeSort = new System.Windows.Forms.Label();
            lblSizeSort.AutoSize = true; lblSizeSort.Location = new System.Drawing.Point(350, 15);
            lblSizeSort.Text = "Σειρά:"; lblSizeSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            panelSizeInputs.Controls.Add(lblSizeSort);

            this.numSizeSortOrder.Location = new System.Drawing.Point(350, 35);
            this.numSizeSortOrder.Size = new System.Drawing.Size(80, 28);
            this.numSizeSortOrder.DecimalPlaces = 0;
            this.numSizeSortOrder.Maximum = 9999;
            this.numSizeSortOrder.Value = 0;
            this.numSizeSortOrder.Font = new System.Drawing.Font("Segoe UI", 10F);
            panelSizeInputs.Controls.Add(this.numSizeSortOrder);

            this.chkSizeActive.AutoSize = true;
            this.chkSizeActive.Location = new System.Drawing.Point(450, 37);
            this.chkSizeActive.Text = "✅ Ενεργό";
            this.chkSizeActive.Checked = true;
            this.chkSizeActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            panelSizeInputs.Controls.Add(this.chkSizeActive);

            // ✅ Panel για τα κουμπιά (κάτω)
            var btnPanelSize = new System.Windows.Forms.Panel();
            btnPanelSize.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnPanelSize.Height = 55;
            btnPanelSize.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);

            this.btnAddSize.Text = "➕ Προσθήκη";
            this.btnAddSize.Size = new System.Drawing.Size(140, 40);
            this.btnAddSize.Location = new System.Drawing.Point(10, 7);
            this.btnAddSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddSize.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddSize.ForeColor = System.Drawing.Color.White;
            this.btnAddSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSize.FlatAppearance.BorderSize = 0;
            this.btnAddSize.Click += new System.EventHandler(this.btnAddSize_Click);

            this.btnEditSize.Text = "✏️ Επεξεργασία";
            this.btnEditSize.Size = new System.Drawing.Size(150, 40);
            this.btnEditSize.Location = new System.Drawing.Point(160, 7);
            this.btnEditSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditSize.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEditSize.ForeColor = System.Drawing.Color.White;
            this.btnEditSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditSize.FlatAppearance.BorderSize = 0;
            this.btnEditSize.Click += new System.EventHandler(this.btnEditSize_Click);

            this.btnDeleteSize.Text = "🗑️ Διαγραφή";
            this.btnDeleteSize.Size = new System.Drawing.Size(140, 40);
            this.btnDeleteSize.Location = new System.Drawing.Point(320, 7);
            this.btnDeleteSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteSize.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteSize.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSize.FlatAppearance.BorderSize = 0;
            this.btnDeleteSize.Click += new System.EventHandler(this.btnDeleteSize_Click);

            btnPanelSize.Controls.Add(this.btnAddSize);
            btnPanelSize.Controls.Add(this.btnEditSize);
            btnPanelSize.Controls.Add(this.btnDeleteSize);

            // ✅ DataGridView (μέση)
            this.dgvSizes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSizes.AllowUserToAddRows = false;
            this.dgvSizes.AllowUserToDeleteRows = false;
            this.dgvSizes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizes.MultiSelect = false;
            this.dgvSizes.ReadOnly = true;
            this.dgvSizes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSizes.BackgroundColor = System.Drawing.Color.White;
            this.dgvSizes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSizes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            this.dgvSizes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSizes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvSizes.ColumnHeadersHeight = 40;
            this.dgvSizes.RowTemplate.Height = 35;
            this.dgvSizes.AutoGenerateColumns = false;
            this.dgvSizes.Columns.Clear();
            this.dgvSizes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            this.dgvSizes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 120 });
            this.dgvSizes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", HeaderText = "Όνομα Μεγέθους", Width = 250 });
            this.dgvSizes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Σειρά", DataPropertyName = "Σειρά", HeaderText = "Σειρά", Width = 100 });
            this.dgvSizes.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ενεργό", DataPropertyName = "Ενεργό", HeaderText = "Ενεργό", Width = 100 });

            // Σωστή σειρά: Panel Top → DataGridView → Panel Bottom
            this.tabSizes.Controls.Add(this.dgvSizes);
            this.tabSizes.Controls.Add(panelSizeInputs);
            this.tabSizes.Controls.Add(btnPanelSize);
            this.tabSizes.Text = "📏 Μεγεθολόγιο";
            this.tabSizes.UseVisualStyleBackColor = true;
            this.tabControl1.Controls.Add(this.tabSizes);
            // ==========================================
            // FORM SETUP (ΖΩΤΙΚΟ ΓΙΑ ΝΑ ΕΜΦΑΝΙΣΤΕΙ Η ΦΟΡΜΑ!)
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 720);

            // ✅ ΠΡΟΣΘΗΚΗ ΤΩΝ CONTROLS ΣΤΗ ΦΟΡΜΑ (Αυτό έλειπε!)
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.Name = "ItemEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "✏️ Επεξεργασία Είδους";

            this.tabControl1.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false); this.tabPrices.ResumeLayout(false); this.tabStock.ResumeLayout(false);
            this.tabBarcodes.ResumeLayout(false); this.tabFeatures.ResumeLayout(false); this.tabSupplier.ResumeLayout(false);
            this.tabMyData.ResumeLayout(false); this.tabTracking.ResumeLayout(false); this.tabNotes.ResumeLayout(false);
            this.tabLots.ResumeLayout(false); this.tabVariants.ResumeLayout(false);
            this.tabBarcodesMultiple.ResumeLayout(false); this.tabUnitRelations.ResumeLayout(false); this.tabWarehouseStock.ResumeLayout(false);
            this.tabImages.ResumeLayout(false); this.tabItemSuppliers.ResumeLayout(false); this.tabRelated.ResumeLayout(false);
            this.tabPriceHistory.ResumeLayout(false); this.tabItemAttributes.ResumeLayout(false); this.tabAlternativeCodes.ResumeLayout(false);
            this.tabColors.ResumeLayout(false); this.tabCPVCodes.ResumeLayout(false); this.tabSerialNumbers.ResumeLayout(false);
            this.tabSizes.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSizes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchasePrice)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numSalePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWholesalePrice)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numCostPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscountPercent)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numDiscountAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numMinStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxStock)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numReorderPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSafetyStock)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLots)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvVariants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarcodesMultiple)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvUnitRelations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarehouseStock)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvItemSuppliers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelated)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvPriceHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemAttributes)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvAlternativeCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemColors)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvCPVCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialNumbers)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.serialBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSizeSortOrder)).EndInit();
            this.ResumeLayout(false);
        } // <-- Αυτό κλείνει σωστά την InitializeComponent


        // ==========================================
        // HELPER METHODS
        // ==========================================
        private void AddLabelAndControl(System.Windows.Forms.TabPage tab, string labelText, int labelX, int y, int controlX, System.Windows.Forms.Control control, int width, bool multiline = false, int height = 30, bool isCombo = false, bool isNumeric = false)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(labelX, y + 10);
            lbl.Text = labelText;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            tab.Controls.Add(lbl);

            control.Location = new System.Drawing.Point(controlX, y);
            control.Size = new System.Drawing.Size(width, height);
            control.Font = new System.Drawing.Font("Segoe UI", 10F);

            if (multiline && control is System.Windows.Forms.TextBox tb)
            {
                tb.Multiline = true;
                tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            }

            if (isNumeric && control is System.Windows.Forms.NumericUpDown nud)
            {
                nud.DecimalPlaces = 2;
                nud.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            }

            tab.Controls.Add(control);
        }

        private void SetupSimpleTab(System.Windows.Forms.TabPage tab, System.Windows.Forms.Control ctrl, string labelText, int x, int y, int width, bool isCombo = false, bool isNumeric = false)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = labelText;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            tab.Controls.Add(lbl);
            ctrl.Location = new System.Drawing.Point(x, y + 22);
            ctrl.Size = new System.Drawing.Size(width, 28);
            ctrl.Font = new System.Drawing.Font("Segoe UI", 10F);
            if (isCombo && ctrl is System.Windows.Forms.ComboBox cb) cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            if (isNumeric && ctrl is System.Windows.Forms.NumericUpDown nud) { nud.DecimalPlaces = 2; }
            tab.Controls.Add(ctrl);
        }

        private void SetupTabButtons(System.Windows.Forms.TabPage tab, System.Windows.Forms.Button btnAdd, System.Windows.Forms.Button btnEdit, System.Windows.Forms.Button btnDelete, int yPos)
        {
            if (btnAdd != null)
            {
                btnAdd.Text = "➕ Προσθήκη";
                btnAdd.Size = new System.Drawing.Size(140, 35);
                btnAdd.Location = new System.Drawing.Point(20, yPos);
                btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
                btnAdd.ForeColor = System.Drawing.Color.White;
                btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnAdd.FlatAppearance.BorderSize = 0;
                btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                tab.Controls.Add(btnAdd);
            }
            if (btnEdit != null)
            {
                btnEdit.Text = "✏️ Επεξεργασία";
                btnEdit.Size = new System.Drawing.Size(150, 35);
                btnEdit.Location = new System.Drawing.Point(170, yPos);
                btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                btnEdit.ForeColor = System.Drawing.Color.White;
                btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnEdit.FlatAppearance.BorderSize = 0;
                btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                tab.Controls.Add(btnEdit);
            }
            if (btnDelete != null)
            {
                btnDelete.Text = "️ Διαγραφή";
                btnDelete.Size = new System.Drawing.Size(130, 35);
                btnDelete.Location = new System.Drawing.Point(330, yPos);
                btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
                btnDelete.ForeColor = System.Drawing.Color.White;
                btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnDelete.FlatAppearance.BorderSize = 0;
                btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                tab.Controls.Add(btnDelete);
            }
        }

        private void SetupDataGridView(System.Windows.Forms.TabPage tab, System.Windows.Forms.DataGridView dgv, int yPos, string[] colNames, string[] dataProps, object[] widths)
        {
            dgv.Location = new System.Drawing.Point(20, yPos);
            dgv.Size = new System.Drawing.Size(860, 395);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 50, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            for (int i = 0; i < colNames.Length; i++)
            {
                var col = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = colNames[i], DataPropertyName = dataProps[i], HeaderText = colNames[i] };
                if (widths[i] is bool b) col.Visible = b;
                else if (widths[i] is int w) col.Width = w;
                dgv.Columns.Add(col);
            }
            tab.Controls.Add(dgv);
        }

        #endregion

        // ==========================================
        // ==========================================
        // DECLARATIONS (ΚΑΘΑΡΟ & ΟΛΟΚΛΗΡΩΜΕΝΟ)
        // ==========================================
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBasic, tabPrices, tabStock, tabBarcodes, tabFeatures, tabSupplier, tabMyData, tabTracking, tabNotes;
        private System.Windows.Forms.TabPage tabLots, tabVariants;
        private System.Windows.Forms.TabPage tabBarcodesMultiple, tabUnitRelations, tabWarehouseStock, tabImages, tabItemSuppliers, tabRelated, tabPriceHistory, tabItemAttributes, tabAlternativeCodes, tabColors, tabCPVCodes, tabSerialNumbers;

        // Tab 1-9 Controls
        private System.Windows.Forms.TextBox txtCode, txtName, txtDescription, txtShortDescription, txtBrand, txtManufacturer;
        private System.Windows.Forms.ComboBox cmbCategory, cmbUnit;
        private System.Windows.Forms.CheckBox chkIsActive, chkIsFavorite, chkIsFeatured;
        private System.Windows.Forms.NumericUpDown numPurchasePrice, numSalePrice, numWholesalePrice, numCostPrice, numDiscountPercent, numDiscountAmount;
        private System.Windows.Forms.ComboBox cmbVatRate;
        private System.Windows.Forms.NumericUpDown numStock, numMinStock, numMaxStock, numReorderPoint, numSafetyStock;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtBarcode, txtSKU, txtAlternativeCode, txtCPVCode, txtCPVDescription;
        private System.Windows.Forms.ComboBox cmbColor, cmbSize;
        private System.Windows.Forms.NumericUpDown numWeight, numVolume, numLength, numWidth, numHeight;
        private System.Windows.Forms.TextBox txtDimensions;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode, txtProviderURL;
        private System.Windows.Forms.TextBox txtMydataCategory, txtMydataCode, txtExemptionArticle, txtExemptionReason, txtVatArticle, txtTARICCode, txtCountryOfOrigin;
        private System.Windows.Forms.CheckBox chkLotTracking, chkSerialTracking, chkExpiryTracking;
        private System.Windows.Forms.TextBox txtNotes, txtLongDescription, txtTags;

        // Tab 10-11 Controls
        private System.Windows.Forms.DataGridView dgvLots, dgvVariants;
        private System.Windows.Forms.Button btnAddLot, btnEditLot, btnDeleteLot, btnAddVariant, btnEditVariant, btnDeleteVariant;

        // Tabs 12-23 DataGridViews
        private System.Windows.Forms.DataGridView dgvBarcodesMultiple, dgvUnitRelations, dgvWarehouseStock, dgvItemSuppliers, dgvRelated, dgvPriceHistory, dgvItemAttributes, dgvAlternativeCodes, dgvItemColors, dgvCPVCodes, dgvSerialNumbers;

        // Tabs 12-23 TextBoxes
        private System.Windows.Forms.TextBox txtBarcodeCode, txtBarcodeDesc, txtUnitFormula, txtStockNotes, txtItemSupplierCode, txtItemSupplierNotes, txtRelatedCode, txtAttrName, txtAttrValue, txtAltCode, txtAltDesc, txtColorCode, txtColorName, txtColorHex, txtCPVCodeId, txtCPVNotes;
        private System.Windows.Forms.TextBox txtSerialNumber, txtSerialNotes, txtSerialSearch;

        // Tabs 12-23 ComboBoxes
        private System.Windows.Forms.ComboBox cmbBarcodeType, cmbBarcodeUom, cmbUnitFrom, cmbUnitTo, cmbRelationType;
        private System.Windows.Forms.ComboBox cmbStockWarehouse, cmbStockVariant, cmbStockLot;
        private System.Windows.Forms.ComboBox cmbItemSupplier, cmbItemSupplierCurrency, cmbRelatedType, cmbAttrType, cmbSerialLotId, cmbSerialStatus;

        // Tabs 12-23 NumericUpDowns
        private System.Windows.Forms.NumericUpDown numUnitCoeff, numStockQty, numStockReserved, numStockMin, numStockMax;
        private System.Windows.Forms.NumericUpDown numBarcodeQty, numItemSupplierPrice, numItemSupplierLead, numItemSupplierMinQty, numRelatedQty;

        // Tabs 12-23 DateTimePickers & CheckBoxes
        private System.Windows.Forms.DateTimePicker dtpSerialWarrantyStart, dtpSerialWarrantyEnd;
        private System.Windows.Forms.CheckBox chkCPVPrimary, chkItemSupplierDefault, chkBarcodePrimary;

        // Tabs 12-23 Buttons
        private System.Windows.Forms.Button btnAddBarcode, btnEditBarcode, btnDeleteBarcode;
        private System.Windows.Forms.Button btnAddUnit, btnEditUnit, btnDeleteUnit;
        private System.Windows.Forms.Button btnAddStock, btnEditStock, btnDeleteStock;
        private System.Windows.Forms.Button btnManageImages;
        private System.Windows.Forms.Button btnAddItemSupplier, btnEditItemSupplier, btnDeleteItemSupplier;
        private System.Windows.Forms.Button btnAddRelated, btnDeleteRelated;
        private System.Windows.Forms.Button btnAddAttr, btnDeleteAttr;
        private System.Windows.Forms.Button btnAddAlt, btnDeleteAlt;
        private System.Windows.Forms.Button btnAddColor, btnDeleteColor;
        private System.Windows.Forms.Button btnAddCPV, btnDeleteCPV;
        private System.Windows.Forms.Button btnAddSerial, btnEditSerial, btnDeleteSerial, btnCancelSerial, btnSerialSearch, btnSerialFirst, btnSerialPrev, btnSerialNext, btnSerialLast;

        // Tabs 12-23 Others
        private System.Windows.Forms.Label lblSerialRecordInfo;
        private System.Windows.Forms.BindingSource serialBindingSource;
        private System.Windows.Forms.Button btnEditRelated;
        private System.Windows.Forms.TextBox txtAttrCode;
        private System.Windows.Forms.NumericUpDown numAttrSortOrder;
        private System.Windows.Forms.Button btnEditAttr;
        private System.Windows.Forms.Button btnEditAlt;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button btnEditColor;
        private System.Windows.Forms.ComboBox cmbCPVCode;
        private System.Windows.Forms.Button btnEditCPV;
        // Tab 24 - Μεγεθολόγιο
        private System.Windows.Forms.TabPage tabSizes;
        private System.Windows.Forms.DataGridView dgvSizes;
        private System.Windows.Forms.TextBox txtSizeCode, txtSizeName;
        private System.Windows.Forms.NumericUpDown numSizeSortOrder;
        private System.Windows.Forms.CheckBox chkSizeActive;
        private System.Windows.Forms.Button btnAddSize, btnEditSize, btnDeleteSize;

        // Main Buttons
        private System.Windows.Forms.Button btnSave, btnCancel;
    }
}