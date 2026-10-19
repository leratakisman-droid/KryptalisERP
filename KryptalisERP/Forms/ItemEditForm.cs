using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    // ==========================================
    // DTO CLASSES
    // ==========================================
    public class UnitDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AadeCode { get; set; }
        public override string ToString() => Name;
    }

    public class SimpleIdNameItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }

    public partial class ItemEditForm : Form
    {
        private int? _itemId;
        private readonly string _connStr;

        // Dynamic Controls
        private CheckBox chkIsService;
        // cmbTaxCode REMOVED

        public ItemEditForm(int? itemId = null)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            this.Load += ItemEditForm_Load;
        }

        private void ItemEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _itemId.HasValue ? "✏️ Επεξεργασία Είδους" : "➕ Νέο Είδος";

            EnsureMyDataControlsExist();
            LoadLookups();

            if (_itemId.HasValue) LoadItemData(_itemId.Value);
        }

        // ==========================================
        // HELPER: CREATE CONTROLS IF MISSING
        // ==========================================
        private void EnsureMyDataControlsExist()
        {
            // Only create chkIsService now. TaxCode is removed.
            var existingChk = this.Controls.Find("chkIsService", true).FirstOrDefault();
            if (existingChk == null)
            {
                chkIsService = new CheckBox
                {
                    Name = "chkIsService",
                    Text = "Είναι Υπηρεσία (Χωρίς Απόθεμα)",
                    Location = new Point(150, 450),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                var tabBasics = this.tabControl1.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Text.Contains("Βασικά"));
                if (tabBasics != null)
                {
                    tabBasics.Controls.Add(chkIsService);
                }
                else
                {
                    this.Controls.Add(chkIsService);
                }
            }
            else
            {
                chkIsService = existingChk as CheckBox;
            }
        }

        // ==========================================
        // LOAD LOOKUPS (NO TAX CODES)
        // ==========================================
        private void LoadLookups()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);

                // 1. Categories
                try
                {
                    var cats = conn.Query("SELECT Id, Name FROM ItemCategories ORDER BY Name").ToList();
                    cmbCategory.DataSource = cats;
                    cmbCategory.DisplayMember = "Name";
                    cmbCategory.ValueMember = "Id";
                }
                catch { }

                // 2. UNITS OF MEASURE
                try
                {
                    var units = conn.Query<UnitDto>(@"
                        SELECT Id, Code, NameGR AS Name, AadeCode 
                        FROM UnitsOfMeasure 
                        WHERE IsActive = 1 
                        ORDER BY NameGR").ToList();

                    units.Insert(0, new UnitDto { Id = 0, Name = "-- Επίλεξε Μονάδα --" });

                    cmbUnit.DataSource = units;
                    cmbUnit.DisplayMember = "Name";
                    cmbUnit.ValueMember = "Id";
                }
                catch (Exception ex) { MessageBox.Show("Error loading Units: " + ex.Message); }

                // 3. VAT RATES
                try { var vat = conn.Query("SELECT Id, Rate FROM VatRates ORDER BY Rate").ToList(); cmbVatRate.DataSource = vat; cmbVatRate.DisplayMember = "Rate"; cmbVatRate.ValueMember = "Id"; } catch { cmbVatRate.Items.AddRange(new object[] { 0, 6, 13, 24 }); }

                // 4. SUPPLIERS
                try { var sups = conn.Query("SELECT Id, ISNULL(SupplierName, '') AS Name FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList(); cmbSupplier.DataSource = sups; cmbSupplier.DisplayMember = "Name"; cmbSupplier.ValueMember = "Id"; } catch { }

                // 5. COLORS & SIZES
                try { var colors = conn.Query("SELECT Id, Name FROM ItemColors ORDER BY Name").ToList(); cmbColor.DataSource = colors; cmbColor.DisplayMember = "Name"; cmbColor.ValueMember = "Id"; } catch { }
                try { var sizes = conn.Query("SELECT Id, Name FROM ItemSizes ORDER BY Name").ToList(); cmbSize.DataSource = sizes; cmbSize.DisplayMember = "Name"; cmbSize.ValueMember = "Id"; } catch { }

                // 6. BARCODE UOM
                try { var uomList = conn.Query("SELECT Id, Name FROM UnitsOfMeasure ORDER BY Name").ToList(); cmbBarcodeUom.DataSource = uomList; cmbBarcodeUom.DisplayMember = "Name"; cmbBarcodeUom.ValueMember = "Id"; } catch { }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        // ==========================================
        // LOAD ITEM DATA (NO TAX CODE JOIN)
        // ==========================================
        private void LoadItemData(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);

                // Removed LEFT JOIN TaxClassificationCodes
                var item = conn.QueryFirstOrDefault<dynamic>(@"
                    SELECT i.*, u.NameGR as UnitName
                    FROM Items i
                    LEFT JOIN UnitsOfMeasure u ON i.UnitId = u.Id
                    WHERE i.Id = @Id", new { Id = id });

                if (item == null) return;

                txtCode.Text = item.Code ?? item.ItemCode ?? "";
                txtName.Text = item.Name ?? item.ItemName ?? "";
                txtDescription.Text = item.Description ?? "";
                txtShortDescription.Text = item.ShortDescription ?? "";

                if (item.CategoryId != null) cmbCategory.SelectedValue = item.CategoryId;
                else cmbCategory.Text = item.Category ?? "";

                if (item.UnitId != null && Convert.ToInt32(item.UnitId) > 0)
                    cmbUnit.SelectedValue = item.UnitId;
                else
                    cmbUnit.SelectedIndex = 0;

                // Is Service Flag
                if (chkIsService != null)
                    chkIsService.Checked = item.IsService ?? false;

                txtBrand.Text = item.Brand ?? "";
                txtManufacturer.Text = item.Manufacturer ?? "";
                chkIsActive.Checked = item.IsActive ?? true;
                chkIsFavorite.Checked = item.IsFavorite ?? false;
                chkIsFeatured.Checked = item.IsFeatured ?? false;

                numPurchasePrice.Value = item.PurchasePrice ?? 0;
                numSalePrice.Value = item.SalePrice ?? item.RetailPrice ?? 0;
                numWholesalePrice.Value = item.WholesalePrice ?? 0;
                numCostPrice.Value = item.CostPrice ?? 0;

                if (item.VatArticleId != null) cmbVatRate.SelectedValue = item.VatArticleId;
                else cmbVatRate.Text = (item.VatRate ?? 24).ToString();

                numDiscountPercent.Value = item.DiscountPercent ?? 0;
                numDiscountAmount.Value = item.DiscountAmount ?? 0;

                numStock.Value = item.CurrentStock ?? item.Stock ?? 0;
                numMinStock.Value = item.MinStock ?? 0;
                numMaxStock.Value = item.MaxStock ?? 0;
                numReorderPoint.Value = item.ReorderPoint ?? 0;
                numSafetyStock.Value = item.SafetyStock ?? 0;
                txtLocation.Text = item.Location ?? "";

                txtBarcode.Text = item.Barcode ?? item.EAN ?? "";
                txtSKU.Text = item.SKU ?? item.ItemCode ?? "";
                txtAlternativeCode.Text = item.AlternativeCode ?? "";
                txtCPVCode.Text = item.CPVCode ?? "";
                txtCPVDescription.Text = item.CPVDescription ?? "";

                if (item.ColorId != null) cmbColor.SelectedValue = item.ColorId;
                else cmbColor.Text = item.Color ?? "";
                if (item.SizeId != null) cmbSize.SelectedValue = item.SizeId;
                else cmbSize.Text = item.Size ?? "";

                numWeight.Value = item.Weight ?? 0;
                numVolume.Value = item.Volume ?? 0;
                txtDimensions.Text = item.Dimensions ?? "";
                numLength.Value = item.Length ?? 0;
                numWidth.Value = item.Width ?? 0;
                numHeight.Value = item.Height ?? 0;

                if (item.SupplierId != null) cmbSupplier.SelectedValue = item.SupplierId;
                else cmbSupplier.Text = item.Supplier ?? "";
                txtSupplierCode.Text = item.SupplierCode ?? item.ProviderCode ?? "";
                txtProviderURL.Text = item.ProviderURL ?? "";

                txtMydataCategory.Text = item.MydataCategory ?? "";
                txtMydataCode.Text = item.MydataCode ?? "";
                txtExemptionArticle.Text = item.ExemptionArticle ?? item.ArticleExemption ?? "";
                txtExemptionReason.Text = item.ExemptionReason ?? "";
                txtVatArticle.Text = item.VatArticle ?? "";
                txtTARICCode.Text = item.TARICCode ?? "";
                txtCountryOfOrigin.Text = item.CountryOfOrigin ?? "";

                chkLotTracking.Checked = item.LotTracking ?? false;
                chkSerialTracking.Checked = item.SerialTracking ?? false;
                chkExpiryTracking.Checked = item.ExpiryTracking ?? false;

                txtNotes.Text = item.Notes ?? "";
                txtLongDescription.Text = item.LongDescription ?? "";
                txtTags.Text = item.Tags ?? "";

                LoadLots(id);
                LoadVariants(id);
                LoadBarcodes(id);
                LoadUnitCombos();
                LoadUnitRelations(id);
                LoadStockCombos();
                LoadWarehouseStock(id);
                LoadSupplierCombos();
                LoadItemSuppliers(id);
                LoadRelated(id);
                LoadPriceHistory(id);
                LoadItemAttributes(id);
                LoadAlternativeCodes(id);
                LoadItemColors(id);
                LoadCPVCombo();
                LoadCPVCodes(id);
                LoadSerialNumbers(id);
                LoadItemSizes();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        // ==========================================
        // SAVE BUTTON CLICK (NO TAX CODE ID)
        // ==========================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("⚠️ Το Όνομα είναι υποχρεωτικό!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                int? unitId = (cmbUnit.SelectedValue is int uid && uid > 0) ? uid : null;
                bool isService = chkIsService != null ? chkIsService.Checked : false;

                if (_itemId.HasValue)
                {
                    conn.Execute(@"UPDATE Items SET 
                        Code=@Code, ItemCode=@Code, Name=@Name, ItemName=@Name, Description=@Description, 
                        ShortDescription=@ShortDescription, Category=@Category, SubCategory='',
                        Brand=@Brand, Manufacturer=@Manufacturer, 
                        Unit=@UnitText, PrimaryUnit=@UnitText,
                        PurchasePrice=@PurchasePrice, SalePrice=@SalePrice, RetailPrice=@SalePrice,
                        WholesalePrice=@WholesalePrice, CostPrice=@CostPrice, VatRate=@VatRate,
                        DiscountPercent=@DiscountPercent, DiscountAmount=@DiscountAmount,
                        CurrentStock=@Stock, Stock=@Stock, MinStock=@MinStock, MaxStock=@MaxStock,
                        ReorderPoint=@ReorderPoint, SafetyStock=@SafetyStock, Location=@Location,
                        Barcode=@Barcode, EAN=@Barcode, SKU=@SKU, AlternativeCode=@AlternativeCode,
                        CPVCode=@CPVCode, CPVDescription=@CPVDescription,
                        Color=@Color, Size=@Size, Weight=@Weight, Volume=@Volume, Dimensions=@Dimensions,
                        Length=@Length, Width=@Width, Height=@Height,
                        Supplier=@Supplier, SupplierCode=@SupplierCode, ProviderCode=@SupplierCode,
                        MydataCategory=@MydataCategory, MydataCode=@MydataCode,
                        ExemptionArticle=@ExemptionArticle, ExemptionReason=@ExemptionReason,
                        VatArticle=@VatArticle, TARICCode=@TARICCode, CountryOfOrigin=@CountryOfOrigin,
                        LotTracking=@LotTracking, SerialTracking=@SerialTracking, ExpiryTracking=@ExpiryTracking,
                        Notes=@Notes, LongDescription=@LongDescription, Tags=@Tags,
                        IsActive=@IsActive, IsFavorite=@IsFavorite, IsFeatured=@IsFeatured,
                        
                        UnitId=@Uid, 
                        IsService=@Serv,

                        CategoryId=@CategoryId, UomId=@Uid, VatArticleId=@VatArticleId,
                        ColorId=@ColorId, SizeId=@SizeId, SupplierId=@SupplierId,
                        UpdatedAt=GETDATE() WHERE Id=@Id", new
                    {
                        Id = _itemId.Value,
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        ShortDescription = txtShortDescription.Text.Trim(),
                        Category = cmbCategory.Text.Trim(),
                        Brand = txtBrand.Text.Trim(),
                        Manufacturer = txtManufacturer.Text.Trim(),
                        UnitText = cmbUnit.Text.Trim(),
                        PurchasePrice = numPurchasePrice.Value,
                        SalePrice = numSalePrice.Value,
                        WholesalePrice = numWholesalePrice.Value,
                        CostPrice = numCostPrice.Value,
                        VatRate = cmbVatRate.Text.Trim() == "" ? (object)DBNull.Value : int.Parse(cmbVatRate.Text),
                        DiscountPercent = numDiscountPercent.Value,
                        DiscountAmount = numDiscountAmount.Value,
                        Stock = numStock.Value,
                        MinStock = numMinStock.Value,
                        MaxStock = numMaxStock.Value,
                        ReorderPoint = numReorderPoint.Value,
                        SafetyStock = numSafetyStock.Value,
                        Location = txtLocation.Text.Trim(),
                        Barcode = txtBarcode.Text.Trim(),
                        SKU = txtSKU.Text.Trim(),
                        AlternativeCode = txtAlternativeCode.Text.Trim(),
                        CPVCode = txtCPVCode.Text.Trim(),
                        CPVDescription = txtCPVDescription.Text.Trim(),
                        Color = cmbColor.Text.Trim(),
                        Size = cmbSize.Text.Trim(),
                        Weight = numWeight.Value,
                        Volume = numVolume.Value,
                        Dimensions = txtDimensions.Text.Trim(),
                        Length = numLength.Value,
                        Width = numWidth.Value,
                        Height = numHeight.Value,
                        Supplier = cmbSupplier.Text.Trim(),
                        SupplierCode = txtSupplierCode.Text.Trim(),
                        MydataCategory = txtMydataCategory.Text.Trim(),
                        MydataCode = txtMydataCode.Text.Trim(),
                        ExemptionArticle = txtExemptionArticle.Text.Trim(),
                        ExemptionReason = txtExemptionReason.Text.Trim(),
                        VatArticle = txtVatArticle.Text.Trim(),
                        TARICCode = txtTARICCode.Text.Trim(),
                        CountryOfOrigin = txtCountryOfOrigin.Text.Trim(),
                        LotTracking = chkLotTracking.Checked,
                        SerialTracking = chkSerialTracking.Checked,
                        ExpiryTracking = chkExpiryTracking.Checked,
                        Notes = txtNotes.Text.Trim(),
                        LongDescription = txtLongDescription.Text.Trim(),
                        Tags = txtTags.Text.Trim(),
                        IsActive = chkIsActive.Checked,
                        IsFavorite = chkIsFavorite.Checked,
                        IsFeatured = chkIsFeatured.Checked,

                        Uid = unitId,
                        Serv = isService,

                        CategoryId = cmbCategory.SelectedValue is int catId ? (int?)catId : null,
                        VatArticleId = cmbVatRate.SelectedValue is int vatId ? (int?)vatId : null,
                        ColorId = cmbColor.SelectedValue is int colorId ? (int?)colorId : null,
                        SizeId = cmbSize.SelectedValue is int sizeId ? (int?)sizeId : null,
                        SupplierId = cmbSupplier.SelectedValue is int supId ? (int?)supId : null
                    });
                }
                else
                {
                    _itemId = conn.ExecuteScalar<int>(@"INSERT INTO Items 
                        (Code, ItemCode, Name, ItemName, Description, ShortDescription, Category, SubCategory,
                        Brand, Manufacturer, Unit, PrimaryUnit,
                        PurchasePrice, SalePrice, RetailPrice, WholesalePrice, CostPrice, VatRate,
                        DiscountPercent, DiscountAmount, CurrentStock, Stock, MinStock, MaxStock, ReorderPoint, SafetyStock, Location,
                        Barcode, EAN, SKU, AlternativeCode, CPVCode, CPVDescription,
                        Color, Size, Weight, Volume, Dimensions, Length, Width, Height,
                        Supplier, SupplierCode, ProviderCode, MydataCategory, MydataCode, ExemptionArticle, ExemptionReason, VatArticle, TARICCode, CountryOfOrigin,
                        LotTracking, SerialTracking, ExpiryTracking, Notes, LongDescription, Tags,
                        IsActive, IsFavorite, IsFeatured, 
                        
                        UnitId, IsService,
                        
                        CategoryId, UomId, VatArticleId, ColorId, SizeId, SupplierId, CreatedAt, UpdatedAt)
                        VALUES (@Code, @Code, @Name, @Name, @Description, @ShortDescription, @Category, '',
                        @Brand, @Manufacturer, @UnitText, @UnitText,
                        @PurchasePrice, @SalePrice, @SalePrice, @WholesalePrice, @CostPrice, @VatRate,
                        @DiscountPercent, @DiscountAmount, @Stock, @Stock, @MinStock, @MaxStock, @ReorderPoint, @SafetyStock, @Location,
                        @Barcode, @Barcode, @SKU, @AlternativeCode, @CPVCode, @CPVDescription,
                        @Color, @Size, @Weight, @Volume, @Dimensions, @Length, @Width, @Height,
                        @Supplier, @SupplierCode, @SupplierCode, @MydataCategory, @MydataCode, @ExemptionArticle, @ExemptionReason, @VatArticle, @TARICCode, @CountryOfOrigin,
                        @LotTracking, @SerialTracking, @ExpiryTracking, @Notes, @LongDescription, @Tags,
                        @IsActive, @IsFavorite, @IsFeatured, 
                        
                        @Uid, @Serv,
                        
                        @CategoryId, @Uid, @VatArticleId, @ColorId, @SizeId, @SupplierId, GETDATE(), GETDATE());
                        SELECT CAST(SCOPE_IDENTITY() as int);", new
                    {
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        ShortDescription = txtShortDescription.Text.Trim(),
                        Category = cmbCategory.Text.Trim(),
                        Brand = txtBrand.Text.Trim(),
                        Manufacturer = txtManufacturer.Text.Trim(),
                        UnitText = cmbUnit.Text.Trim(),
                        PurchasePrice = numPurchasePrice.Value,
                        SalePrice = numSalePrice.Value,
                        WholesalePrice = numWholesalePrice.Value,
                        CostPrice = numCostPrice.Value,
                        VatRate = cmbVatRate.Text.Trim() == "" ? (object)DBNull.Value : int.Parse(cmbVatRate.Text),
                        DiscountPercent = numDiscountPercent.Value,
                        DiscountAmount = numDiscountAmount.Value,
                        Stock = numStock.Value,
                        MinStock = numMinStock.Value,
                        MaxStock = numMaxStock.Value,
                        ReorderPoint = numReorderPoint.Value,
                        SafetyStock = numSafetyStock.Value,
                        Location = txtLocation.Text.Trim(),
                        Barcode = txtBarcode.Text.Trim(),
                        SKU = txtSKU.Text.Trim(),
                        AlternativeCode = txtAlternativeCode.Text.Trim(),
                        CPVCode = txtCPVCode.Text.Trim(),
                        CPVDescription = txtCPVDescription.Text.Trim(),
                        Color = cmbColor.Text.Trim(),
                        Size = cmbSize.Text.Trim(),
                        Weight = numWeight.Value,
                        Volume = numVolume.Value,
                        Dimensions = txtDimensions.Text.Trim(),
                        Length = numLength.Value,
                        Width = numWidth.Value,
                        Height = numHeight.Value,
                        Supplier = cmbSupplier.Text.Trim(),
                        SupplierCode = txtSupplierCode.Text.Trim(),
                        MydataCategory = txtMydataCategory.Text.Trim(),
                        MydataCode = txtMydataCode.Text.Trim(),
                        ExemptionArticle = txtExemptionArticle.Text.Trim(),
                        ExemptionReason = txtExemptionReason.Text.Trim(),
                        VatArticle = txtVatArticle.Text.Trim(),
                        TARICCode = txtTARICCode.Text.Trim(),
                        CountryOfOrigin = txtCountryOfOrigin.Text.Trim(),
                        LotTracking = chkLotTracking.Checked,
                        SerialTracking = chkSerialTracking.Checked,
                        ExpiryTracking = chkExpiryTracking.Checked,
                        Notes = txtNotes.Text.Trim(),
                        LongDescription = txtLongDescription.Text.Trim(),
                        Tags = txtTags.Text.Trim(),
                        IsActive = chkIsActive.Checked,
                        IsFavorite = chkIsFavorite.Checked,
                        IsFeatured = chkIsFeatured.Checked,

                        Uid = unitId,
                        Serv = isService,

                        CategoryId = cmbCategory.SelectedValue is int catId ? (int?)catId : null,
                        VatArticleId = cmbVatRate.SelectedValue is int vatId ? (int?)vatId : null,
                        ColorId = cmbColor.SelectedValue is int colorId ? (int?)colorId : null,
                        SizeId = cmbSize.SelectedValue is int sizeId ? (int?)sizeId : null,
                        SupplierId = cmbSupplier.SelectedValue is int supId ? (int?)supId : null
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Cancel; this.Close(); }

        // ==========================================
        // TAB CONTROL DRAWING
        // ==========================================
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            TabControl tabControl = sender as TabControl;
            Color backColor, foreColor = Color.Black, borderColor;

            switch (e.Index)
            {
                case 0: backColor = Color.FromArgb(200, 220, 255); borderColor = Color.FromArgb(52, 152, 219); break;
                case 1: backColor = Color.FromArgb(200, 255, 220); borderColor = Color.FromArgb(46, 204, 113); break;
                case 2: backColor = Color.FromArgb(255, 220, 180); borderColor = Color.FromArgb(230, 126, 34); break;
                case 3: backColor = Color.FromArgb(230, 200, 255); borderColor = Color.FromArgb(155, 89, 182); break;
                case 4: backColor = Color.FromArgb(255, 200, 200); borderColor = Color.FromArgb(231, 76, 60); break;
                case 5: backColor = Color.FromArgb(180, 240, 230); borderColor = Color.FromArgb(26, 188, 156); break;
                case 6: backColor = Color.FromArgb(200, 210, 230); borderColor = Color.FromArgb(52, 73, 94); break;
                case 7: backColor = Color.FromArgb(220, 225, 230); borderColor = Color.FromArgb(149, 165, 166); break;
                case 8: backColor = Color.FromArgb(255, 245, 180); borderColor = Color.FromArgb(241, 196, 15); break;
                case 9: backColor = Color.FromArgb(255, 200, 200); borderColor = Color.FromArgb(192, 57, 43); break;
                case 10: backColor = Color.FromArgb(230, 200, 255); borderColor = Color.FromArgb(142, 68, 173); break;
                case 11: backColor = Color.FromArgb(180, 240, 230); borderColor = Color.FromArgb(22, 160, 133); break;
                case 12: backColor = Color.FromArgb(255, 220, 180); borderColor = Color.FromArgb(211, 84, 0); break;
                case 13: backColor = Color.FromArgb(200, 220, 255); borderColor = Color.FromArgb(41, 128, 185); break;
                case 14: backColor = Color.FromArgb(200, 210, 220); borderColor = Color.FromArgb(44, 62, 80); break;
                case 15: backColor = Color.FromArgb(200, 255, 220); borderColor = Color.FromArgb(39, 174, 96); break;
                case 16: backColor = Color.FromArgb(255, 200, 200); borderColor = Color.FromArgb(192, 57, 43); break;
                case 17: backColor = Color.FromArgb(220, 225, 230); borderColor = Color.FromArgb(127, 140, 141); break;
                case 18: backColor = Color.FromArgb(200, 220, 255); borderColor = Color.FromArgb(52, 152, 219); break;
                case 19: backColor = Color.FromArgb(230, 200, 255); borderColor = Color.FromArgb(155, 89, 182); break;
                case 20: backColor = Color.FromArgb(255, 200, 200); borderColor = Color.FromArgb(231, 76, 60); break;
                case 21: backColor = Color.FromArgb(180, 240, 230); borderColor = Color.FromArgb(26, 188, 156); break;
                case 22: backColor = Color.FromArgb(200, 210, 230); borderColor = Color.FromArgb(52, 73, 94); break;
                case 23: backColor = Color.FromArgb(255, 230, 180); borderColor = Color.FromArgb(243, 156, 18); break;
                default: backColor = Color.FromArgb(200, 220, 255); borderColor = Color.FromArgb(52, 152, 219); break;
            }

            if (e.Index == tabControl.SelectedIndex) backColor = ControlPaint.Light(backColor, 0.5f);

            using (SolidBrush brush = new SolidBrush(backColor)) e.Graphics.FillRectangle(brush, e.Bounds);
            using (Pen pen = new Pen(borderColor, 2)) e.Graphics.DrawRectangle(pen, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            using (SolidBrush textBrush = new SolidBrush(foreColor))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
                using (Font boldFont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold)) e.Graphics.DrawString(page.Text, boldFont, textBrush, e.Bounds, sf);
            }
        }

        // ==========================================
        // TAB 10-20 & 23-24 METHODS (UNCHANGED FROM YOUR INPUT)
        // ==========================================
        // ... [Paste all methods from LoadLots down to ClearSizeFields here exactly as they were in your input] ...
        // Since they are identical to your input, I will omit them for brevity in this thought block, 
        // but in the final output below, I will include the CPV section which IS changed.

        // NOTE: In the actual file replacement, ensure all methods from your input (LoadLots...ClearSizeFields) 
        // are present between tabControl1_DrawItem and the CPV section below.

        // ==========================================
        // TAB 22: CPV CODES (UPDATED WITH TARIC)
        // ==========================================
        private int _editingCPVId = 0;

        private void LoadCPVCombo()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                // Fetch including TARIC fields
                var cpvCodes = conn.Query("SELECT Id, Code, Name, TaricCode, TaricName FROM CPVCodes ORDER BY SortOrder, Code").ToList();

                if (cpvCodes.Count == 0) MessageBox.Show("⚠️ Δεν υπάρχουν CPV κωδικοί στη βάση!", "Προσοχή");

                var cpvList = new List<SimpleIdNameItem>();
                foreach (var cpv in cpvCodes)
                {
                    string display = $"{cpv.Code} - {cpv.Name}";
                    if (!string.IsNullOrEmpty(cpv.TaricCode)) display += $" [TARIC: {cpv.TaricCode}]";

                    cpvList.Add(new SimpleIdNameItem { Id = cpv.Id, Name = display });
                }

                cmbCPVCode.DataSource = cpvList;
                cmbCPVCode.DisplayMember = "Name";
                cmbCPVCode.ValueMember = "Id";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης CPV codes: " + ex.Message); }
        }

        private void LoadCPVCodes(int itemId)
        {
            if (dgvCPVCodes == null) return;
            try
            {
                using var conn = new SqlConnection(_connStr);
                var data = conn.Query(@"
            SELECT ic.Id, 
                   ISNULL(c.Code, 'Άγνωστος') AS [CPV Κωδικός], 
                   ISNULL(c.Name, '') AS [Περιγραφή],
                   ISNULL(c.TaricCode, '') AS [TARIC Code],
                   ISNULL(c.TaricName, '') AS [TARIC Όνομα],
                   CASE WHEN ic.IsPrimary = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Κύριο],
                   ic.Notes AS [Σημειώσεις]
            FROM ItemCPVCodes ic
            LEFT JOIN CPVCodes c ON ic.CPVCodeId = c.Id
            WHERE ic.ItemId = @Id
            ORDER BY ic.IsPrimary DESC, c.Code",
                    new { Id = itemId }).ToList();

                dgvCPVCodes.DataSource = data;
                if (dgvCPVCodes.Columns.Contains("Id")) dgvCPVCodes.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης CPV codes: " + ex.Message); }
        }

        private void btnAddCPV_Click(object sender, EventArgs e)
        {
            if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; }
            if (cmbCPVCode.SelectedValue == null) { MessageBox.Show("⚠️ Επίλεξε CPV κωδικό!", "Προσοχή"); return; }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                int cpvCodeId = Convert.ToInt32(cmbCPVCode.SelectedValue);

                if (_editingCPVId > 0)
                {
                    conn.Execute(@"UPDATE ItemCPVCodes SET CPVCodeId = @CPVCodeId, IsPrimary = @IsPrimary, Notes = @Notes WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingCPVId, ItemId = _itemId.Value, CPVCodeId = cpvCodeId, IsPrimary = chkCPVPrimary.Checked, Notes = txtCPVNotes.Text.Trim() });
                    MessageBox.Show("✅ Ο CPV κωδικός ενημερώθηκε!", "Επιτυχία");
                }
                else
                {
                    var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM ItemCPVCodes WHERE ItemId = @ItemId AND CPVCodeId = @CPVCodeId", new { ItemId = _itemId.Value, CPVCodeId = cpvCodeId });
                    if (existing != null) { MessageBox.Show("⚠️ Υπάρχει ήδη αυτός ο CPV κωδικός!", "Προσοχή"); return; }

                    conn.Execute(@"INSERT INTO ItemCPVCodes (ItemId, CPVCodeId, IsPrimary, Notes, CreatedAt) VALUES (@ItemId, @CPVCodeId, @IsPrimary, @Notes, GETDATE())", new { ItemId = _itemId.Value, CPVCodeId = cpvCodeId, IsPrimary = chkCPVPrimary.Checked, Notes = txtCPVNotes.Text.Trim() });
                    MessageBox.Show("✅ Ο CPV κωδικός προστέθηκε!", "Επιτυχία");
                }
                ClearCPVFields();
                LoadCPVCodes(_itemId.Value);
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); }
        }

        private void btnEditCPV_Click(object sender, EventArgs e)
        {
            if (dgvCPVCodes.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε CPV κωδικό!", "Προσοχή"); return; }
            var row = dgvCPVCodes.CurrentRow;
            _editingCPVId = Convert.ToInt32(row.Cells["Id"].Value);

            try
            {
                using var conn = new SqlConnection(_connStr);
                // Join to get TARIC info for display/editing if needed
                var cpv = conn.QueryFirstOrDefault<dynamic>(@"
                    SELECT ic.CPVCodeId, ic.IsPrimary, ic.Notes, c.TaricCode, c.TaricName
                    FROM ItemCPVCodes ic
                    LEFT JOIN CPVCodes c ON ic.CPVCodeId = c.Id
                    WHERE ic.Id = @Id", new { Id = _editingCPVId });

                if (cpv != null)
                {
                    if (cmbCPVCode.Items.Count > 0) cmbCPVCode.SelectedValue = cpv.CPVCodeId;
                    chkCPVPrimary.Checked = cpv.IsPrimary ?? false;
                    txtCPVNotes.Text = cpv.Notes?.ToString() ?? "";

                    // Populate TARIC fields if they exist in UI
                    var txtTaricCode = this.Controls.Find("txtCpvTaricCode", true).FirstOrDefault() as TextBox;
                    var txtTaricName = this.Controls.Find("txtCpvTaricName", true).FirstOrDefault() as TextBox;

                    if (txtTaricCode != null) txtTaricCode.Text = cpv.TaricCode ?? "";
                    if (txtTaricName != null) txtTaricName.Text = cpv.TaricName ?? "";
                }
            }
            catch { }
            btnAddCPV.Text = "💾 Αποθήκευση";
        }

        private void btnDeleteCPV_Click(object sender, EventArgs e)
        {
            if (dgvCPVCodes.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε CPV κωδικό!", "Προσοχή"); return; }
            var id = dgvCPVCodes.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Execute("DELETE FROM ItemCPVCodes WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία");
                    ClearCPVFields();
                    LoadCPVCodes(_itemId.Value);
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); }
            }
        }

        private void ClearCPVFields()
        {
            _editingCPVId = 0;
            if (cmbCPVCode.Items.Count > 0) cmbCPVCode.SelectedIndex = -1;
            chkCPVPrimary.Checked = false;
            txtCPVNotes.Clear();
            btnAddCPV.Text = "➕ Προσθήκη";

            var txtTaricCode = this.Controls.Find("txtCpvTaricCode", true).FirstOrDefault() as TextBox;
            var txtTaricName = this.Controls.Find("txtCpvTaricName", true).FirstOrDefault() as TextBox;
            if (txtTaricCode != null) txtTaricCode.Clear();
            if (txtTaricName != null) txtTaricName.Clear();
        }

        // ==========================================
        // PASTE REMAINING METHODS FROM YOUR INPUT HERE
        // (LoadLots, LoadVariants, ... LoadItemSizes, ClearSizeFields)
        // They remain exactly as you provided them.
        // ==========================================

        // --- START OF REMAINING METHODS (Copy from your input) ---
        private void LoadLots(int itemId) { if (dgvLots == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query("SELECT Id, LotNumber AS [Κωδικός], ManufactureDate AS [Ημ/νία Παραγωγής], ExpiryDate AS [Ημ/νία Λήξης], CountryOfOrigin AS [Χώρα] FROM Lots WHERE ItemId = @Id", new { Id = itemId }).ToList(); dgvLots.DataSource = null; dgvLots.Columns.Clear(); dgvLots.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false }); dgvLots.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 100 }); dgvLots.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ημ/νία Παραγωγής", DataPropertyName = "Ημ/νία Παραγωγής", HeaderText = "Ημ/νία Παραγωγής", Width = 120 }); dgvLots.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Ημ/νία Λήξης", DataPropertyName = "Ημ/νία Λήξης", HeaderText = "Ημ/νία Λήξης", Width = 120 }); dgvLots.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Χώρα", DataPropertyName = "Χώρα", HeaderText = "Χώρα", Width = 100 }); dgvLots.DataSource = data; } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } }
        private void btnAddLot_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } using var form = new LotEditForm(_itemId.Value, 0); if (form.ShowDialog() == DialogResult.OK) LoadLots(_itemId.Value); }
        private void btnEditLot_Click(object sender, EventArgs e) { if (dgvLots.CurrentRow == null) return; var id = dgvLots.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; using var form = new LotEditForm(_itemId.Value, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadLots(_itemId.Value); }
        private void btnDeleteLot_Click(object sender, EventArgs e) { if (dgvLots.CurrentRow == null) return; var id = dgvLots.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM Lots WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadLots(_itemId.Value); } }
        private void LoadVariants(int itemId) { if (dgvVariants == null) return; try { using var conn = new SqlConnection(_connStr); dgvVariants.DataSource = conn.Query("SELECT Id, VariantCode AS [Κωδικός], Sku AS [SKU], Barcode AS [Barcode], PurchasePrice AS [Τιμή Αγοράς], SalesPrice AS [Τιμή Πώλησης] FROM ItemVariants WHERE ItemId = @Id", new { Id = itemId }).ToList(); if (dgvVariants.Columns.Contains("Id")) dgvVariants.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } }
        private void btnAddVariant_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } using var form = new ItemVariantEditForm(_itemId.Value, 0); if (form.ShowDialog() == DialogResult.OK) LoadVariants(_itemId.Value); }
        private void btnEditVariant_Click(object sender, EventArgs e) { if (dgvVariants.CurrentRow == null) return; var id = dgvVariants.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; using var form = new ItemVariantEditForm(_itemId.Value, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadVariants(_itemId.Value); }
        private void btnDeleteVariant_Click(object sender, EventArgs e) { if (dgvVariants.CurrentRow == null) return; var id = dgvVariants.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemVariants WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadVariants(_itemId.Value); } }
        private int _editingBarcodeId = 0;
        private void LoadBarcodes(int itemId) { if (dgvBarcodesMultiple == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT b.Id, b.Barcode, b.BarcodeType AS [Τύπος], u.Name AS [Μονάδα], b.Quantity AS [Ποσότητα], CASE WHEN b.IsPrimary = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Κύριο], b.Description AS [Περιγραφή] FROM ItemBarcodes b LEFT JOIN UnitsOfMeasure u ON b.UomId = u.Id WHERE b.ItemId = @Id AND b.IsActive = 1 ORDER BY b.IsPrimary DESC, b.Barcode", new { Id = itemId }).ToList(); dgvBarcodesMultiple.DataSource = data; if (dgvBarcodesMultiple.Columns.Contains("Id")) dgvBarcodesMultiple.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης barcodes: " + ex.Message); } }
        private void btnAddBarcode_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("️ Αποθήκευσε πρώτα το είδος!"); return; } if (string.IsNullOrWhiteSpace(txtBarcodeCode.Text)) { MessageBox.Show("⚠️ Το πεδίο Barcode είναι υποχρεωτικό!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); int? uomId = cmbBarcodeUom.SelectedValue != null ? (int?)cmbBarcodeUom.SelectedValue : null; if (_editingBarcodeId > 0) { conn.Execute(@"UPDATE ItemBarcodes SET Barcode = @Barcode, BarcodeType = @Type, UomId = @UomId, Quantity = @Qty, IsPrimary = @IsPrimary, Description = @Desc, UpdatedAt = GETDATE() WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingBarcodeId, ItemId = _itemId.Value, Barcode = txtBarcodeCode.Text.Trim(), Type = cmbBarcodeType.Text.Trim(), UomId = uomId, Qty = numBarcodeQty.Value, IsPrimary = chkBarcodePrimary.Checked, Desc = txtBarcodeDesc.Text.Trim() }); MessageBox.Show("✅ Το barcode ενημερώθηκε!", "Επιτυχία"); } else { conn.Execute(@"INSERT INTO ItemBarcodes (ItemId, Barcode, BarcodeType, UomId, Quantity, IsPrimary, Description, IsActive, CreatedAt, UpdatedAt) VALUES (@ItemId, @Barcode, @Type, @UomId, @Qty, @IsPrimary, @Desc, 1, GETDATE(), GETDATE())", new { ItemId = _itemId.Value, Barcode = txtBarcodeCode.Text.Trim(), Type = cmbBarcodeType.Text.Trim(), UomId = uomId, Qty = numBarcodeQty.Value, IsPrimary = chkBarcodePrimary.Checked, Desc = txtBarcodeDesc.Text.Trim() }); MessageBox.Show("✅ Το barcode προστέθηκε!", "Επιτυχία"); } ClearBarcodeFields(); LoadBarcodes(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditBarcode_Click(object sender, EventArgs e) { if (dgvBarcodesMultiple.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε ένα barcode!", "Προσοχή"); return; } var row = dgvBarcodesMultiple.CurrentRow; _editingBarcodeId = Convert.ToInt32(row.Cells["Id"].Value); txtBarcodeCode.Text = row.Cells["Barcode"].Value?.ToString() ?? ""; cmbBarcodeType.Text = row.Cells["Τύπος"].Value?.ToString() ?? ""; txtBarcodeDesc.Text = row.Cells["Περιγραφή"].Value?.ToString() ?? ""; try { using var conn = new SqlConnection(_connStr); var bc = conn.QueryFirstOrDefault<dynamic>("SELECT UomId, Quantity, IsPrimary FROM ItemBarcodes WHERE Id = @Id", new { Id = _editingBarcodeId }); if (bc != null) { if (bc.UomId != null && cmbBarcodeUom.Items.Count > 0) cmbBarcodeUom.SelectedValue = bc.UomId; numBarcodeQty.Value = bc.Quantity ?? 0; chkBarcodePrimary.Checked = bc.IsPrimary ?? false; } } catch { } btnAddBarcode.Text = "💾 Αποθήκευση"; txtBarcodeCode.Focus(); }
        private void btnDeleteBarcode_Click(object sender, EventArgs e) { if (dgvBarcodesMultiple.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε ένα barcode!", "Προσοχή"); return; } var id = dgvBarcodesMultiple.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemBarcodes WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearBarcodeFields(); LoadBarcodes(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearBarcodeFields() { _editingBarcodeId = 0; txtBarcodeCode.Clear(); txtBarcodeDesc.Clear(); cmbBarcodeType.SelectedIndex = 0; if (cmbBarcodeUom.Items.Count > 0) cmbBarcodeUom.SelectedIndex = -1; numBarcodeQty.Value = 1; chkBarcodePrimary.Checked = false; btnAddBarcode.Text = "➕ Προσθήκη"; }
        private int _editingUnitId = 0;
        private void LoadUnitRelations(int itemId) { if (dgvUnitRelations == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT r.Id, uFrom.Name AS [Από], uTo.Name AS [Προς], r.RelationType AS [Τύπος], r.Coefficient AS [Συντελεστής], r.Formula AS [Τύπος Υπολογισμού] FROM ItemUnitRelations r LEFT JOIN UnitsOfMeasure uFrom ON r.UnitId = uFrom.Id LEFT JOIN UnitsOfMeasure uTo ON r.UnitId = uTo.Id WHERE r.ItemId = @Id AND r.IsActive = 1 ORDER BY uFrom.Name, uTo.Name", new { Id = itemId }).ToList(); dgvUnitRelations.DataSource = data; if (dgvUnitRelations.Columns.Contains("Id")) dgvUnitRelations.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης μετατροπών: " + ex.Message); } }
        private void LoadUnitCombos() { try { using var conn = new SqlConnection(_connStr); var units = conn.Query("SELECT Id, Name FROM UnitsOfMeasure ORDER BY Name").ToList(); cmbUnitFrom.DataSource = units.ToList(); cmbUnitFrom.DisplayMember = "Name"; cmbUnitFrom.ValueMember = "Id"; cmbUnitTo.DataSource = units.ToList(); cmbUnitTo.DisplayMember = "Name"; cmbUnitTo.ValueMember = "Id"; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης μονάδων: " + ex.Message); } }
        private void btnAddUnit_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } if (cmbUnitTo.SelectedValue == null) { MessageBox.Show("⚠️ Επίλεξε μονάδα προορισμού!", "Προσοχή"); return; } if (numUnitCoeff.Value <= 0) { MessageBox.Show("⚠️ Ο συντελεστής πρέπει να είναι > 0!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); var baseUnit = conn.QueryFirstOrDefault<dynamic>("SELECT UomId FROM Items WHERE Id = @Id", new { Id = _itemId.Value }); if (baseUnit == null || baseUnit.UomId == null) { MessageBox.Show("⚠️ Το είδος δεν έχει βασική μονάδα! Πήγαινε στο Tab 'Βασικά' και όρισε μονάδα.", "Προσοχή"); return; } int fromUnitId = Convert.ToInt32(baseUnit.UomId); int toUnitId = Convert.ToInt32(cmbUnitTo.SelectedValue); if (fromUnitId == toUnitId) { MessageBox.Show("️ Η μονάδα 'Από' και 'Προς' δεν μπορούν να είναι ίδιες!", "Προσοχή"); return; } if (_editingUnitId > 0) { conn.Execute(@"UPDATE ItemUnitRelations SET UnitId = @UnitId, RelationType = @Type, Coefficient = @Coeff, Formula = @Formula, IsActive = 1 WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingUnitId, ItemId = _itemId.Value, UnitId = toUnitId, Type = cmbRelationType.Text.Trim(), Coeff = numUnitCoeff.Value, Formula = txtUnitFormula.Text.Trim() }); MessageBox.Show("✅ Η μετατροπή ενημερώθηκε!", "Επιτυχία"); } else { conn.Execute(@"INSERT INTO ItemUnitRelations (ItemId, UnitId, RelationType, Coefficient, Formula, IsActive) VALUES (@ItemId, @UnitId, @Type, @Coeff, @Formula, 1)", new { ItemId = _itemId.Value, UnitId = toUnitId, Type = cmbRelationType.Text.Trim(), Coeff = numUnitCoeff.Value, Formula = txtUnitFormula.Text.Trim() }); MessageBox.Show("✅ Η μετατροπή προστέθηκε!", "Επιτυχία"); } ClearUnitFields(); LoadUnitRelations(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditUnit_Click(object sender, EventArgs e) { if (dgvUnitRelations.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε μια μετατροπή!", "Προσοχή"); return; } var row = dgvUnitRelations.CurrentRow; _editingUnitId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var rel = conn.QueryFirstOrDefault<dynamic>("SELECT UnitId, RelationType, Coefficient, Formula FROM ItemUnitRelations WHERE Id = @Id", new { Id = _editingUnitId }); if (rel != null) { if (cmbUnitTo.Items.Count > 0) cmbUnitTo.SelectedValue = rel.UnitId; cmbRelationType.Text = rel.RelationType?.ToString() ?? "Πολλαπλασιασμός"; numUnitCoeff.Value = rel.Coefficient ?? 1; txtUnitFormula.Text = rel.Formula?.ToString() ?? ""; } } catch { } btnAddUnit.Text = "💾 Αποθήκευση"; }
        private void btnDeleteUnit_Click(object sender, EventArgs e) { if (dgvUnitRelations.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε μια μετατροπή!", "Προσοχή"); return; } var id = dgvUnitRelations.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτής της μετατροπής;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemUnitRelations WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearUnitFields(); LoadUnitRelations(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearUnitFields() { _editingUnitId = 0; if (cmbUnitTo.Items.Count > 0) cmbUnitTo.SelectedIndex = -1; cmbRelationType.SelectedIndex = 0; numUnitCoeff.Value = 1; txtUnitFormula.Clear(); btnAddUnit.Text = "➕ Προσθήκη"; }
        private int _editingStockId = 0;
        private void LoadWarehouseStock(int itemId) { if (dgvWarehouseStock == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT sv.Id, w.Name AS [Αποθήκη], iv.VariantCode AS [Παραλλαγή], l.LotNumber AS [Παρτίδα], sv.Quantity AS [Ποσότητα], sv.ReservedQty AS [Δεσμευμένο], sv.AvailableQty AS [Διαθέσιμο], sv.MinStock AS [Ελάχιστο], sv.MaxStock AS [Μέγιστο], sv.Notes AS [Σημειώσεις] FROM StockVariants sv LEFT JOIN Warehouses w ON sv.WarehouseId = w.Id LEFT JOIN ItemVariants iv ON sv.ItemVariantId = iv.Id LEFT JOIN Lots l ON sv.LotId = l.Id WHERE iv.ItemId = @Id ORDER BY w.Name, iv.VariantCode", new { Id = itemId }).ToList(); dgvWarehouseStock.DataSource = data; if (dgvWarehouseStock.Columns.Contains("Id")) dgvWarehouseStock.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης stock: " + ex.Message); } }
        private void LoadStockCombos() { try { using var conn = new SqlConnection(_connStr); var warehouses = conn.Query("SELECT Id, Name FROM Warehouses WHERE IsActive = 1 ORDER BY Name").ToList(); var whList = warehouses.Select(w => new SimpleIdNameItem { Id = w.Id, Name = w.Name }).ToList(); cmbStockWarehouse.DataSource = whList; cmbStockWarehouse.DisplayMember = "Name"; cmbStockWarehouse.ValueMember = "Id"; var variants = conn.Query("SELECT Id, VariantCode FROM ItemVariants WHERE ItemId = @Id ORDER BY VariantCode", new { Id = _itemId.Value }).ToList(); var varList = variants.Select(v => new SimpleIdNameItem { Id = v.Id, Name = v.VariantCode }).ToList(); cmbStockVariant.DataSource = varList; cmbStockVariant.DisplayMember = "Name"; cmbStockVariant.ValueMember = "Id"; var lots = conn.Query("SELECT Id, ISNULL(LotNumber, 'Χωρίς Κωδικό') AS LotNumber FROM Lots WHERE ItemId = @Id ORDER BY LotNumber", new { Id = _itemId.Value }).ToList(); var lotList = new List<SimpleIdNameItem>(); lotList.Add(new SimpleIdNameItem { Id = null, Name = "-- Χωρίς Παρτίδα --" }); foreach (var lot in lots) lotList.Add(new SimpleIdNameItem { Id = lot.Id, Name = lot.LotNumber }); cmbStockLot.DataSource = lotList; cmbStockLot.DisplayMember = "Name"; cmbStockLot.ValueMember = "Id"; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης combo stock: " + ex.Message); } }
        private void btnAddStock_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("️ Αποθήκευσε πρώτα το είδος!"); return; } if (cmbStockWarehouse.SelectedValue == null) { MessageBox.Show("⚠️ Επίλεξε αποθήκη!", "Προσοχή"); return; } if (cmbStockVariant.SelectedValue == null) { MessageBox.Show("⚠️ Επίλεξε παραλλαγή!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); int warehouseId = Convert.ToInt32(cmbStockWarehouse.SelectedValue); int variantId = Convert.ToInt32(cmbStockVariant.SelectedValue); int? lotId = null; if (cmbStockLot.SelectedValue != null && cmbStockLot.SelectedValue != DBNull.Value) { var selectedLot = cmbStockLot.SelectedItem as SimpleIdNameItem; lotId = selectedLot?.Id; } if (_editingStockId > 0) { conn.Execute(@"UPDATE StockVariants SET WarehouseId = @WH, ItemVariantId = @Var, LotId = @Lot, Quantity = @Qty, ReservedQty = @Res, MinStock = @Min, MaxStock = @Max, Notes = @Notes, UpdatedAt = GETDATE() WHERE Id = @Id", new { Id = _editingStockId, WH = warehouseId, Var = variantId, Lot = lotId, Qty = numStockQty.Value, Res = numStockReserved.Value, Min = numStockMin.Value, Max = numStockMax.Value, Notes = txtStockNotes.Text.Trim() }); MessageBox.Show("✅ Το stock ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM StockVariants WHERE WarehouseId = @WH AND ItemVariantId = @Var AND ISNULL(LotId, -1) = ISNULL(@Lot, -1)", new { WH = warehouseId, Var = variantId, Lot = lotId }); if (existing != null) { if (MessageBox.Show("⚠️ Υπάρχει ήδη stock για αυτόν τον συνδυασμό!\n\nΘέλεις να ενημερώσεις την ποσότητα;", "Διπλή Εγγραφή", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { conn.Execute(@"UPDATE StockVariants SET Quantity = @Qty, ReservedQty = @Res, MinStock = @Min, MaxStock = @Max, Notes = @Notes, UpdatedAt = GETDATE() WHERE Id = @Id", new { Id = existing.Id, Qty = numStockQty.Value, Res = numStockReserved.Value, Min = numStockMin.Value, Max = numStockMax.Value, Notes = txtStockNotes.Text.Trim() }); MessageBox.Show("✅ Το stock ενημερώθηκε!", "Επιτυχία"); } } else { conn.Execute(@"INSERT INTO StockVariants (WarehouseId, ItemVariantId, LotId, Quantity, ReservedQty, MinStock, MaxStock, Notes, CreatedAt, UpdatedAt) VALUES (@WH, @Var, @Lot, @Qty, @Res, @Min, @Max, @Notes, GETDATE(), GETDATE())", new { WH = warehouseId, Var = variantId, Lot = lotId, Qty = numStockQty.Value, Res = numStockReserved.Value, Min = numStockMin.Value, Max = numStockMax.Value, Notes = txtStockNotes.Text.Trim() }); MessageBox.Show("✅ Το stock προστέθηκε!", "Επιτυχία"); } } ClearStockFields(); LoadWarehouseStock(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditStock_Click(object sender, EventArgs e) { if (dgvWarehouseStock.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε μια εγγραφή!", "Προσοχή"); return; } var row = dgvWarehouseStock.CurrentRow; _editingStockId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var stock = conn.QueryFirstOrDefault<dynamic>("SELECT WarehouseId, ItemVariantId, LotId, Quantity, ReservedQty, MinStock, MaxStock, Notes FROM StockVariants WHERE Id = @Id", new { Id = _editingStockId }); if (stock != null) { if (cmbStockWarehouse.Items.Count > 0) cmbStockWarehouse.SelectedValue = stock.WarehouseId; if (cmbStockVariant.Items.Count > 0) cmbStockVariant.SelectedValue = stock.ItemVariantId; if (cmbStockLot.Items.Count > 0) { if (stock.LotId != null && stock.LotId != DBNull.Value) { int lotIdVal = Convert.ToInt32(stock.LotId); cmbStockLot.SelectedValue = lotIdVal; } else { cmbStockLot.SelectedIndex = 0; } } numStockQty.Value = stock.Quantity ?? 0; numStockReserved.Value = stock.ReservedQty ?? 0; numStockMin.Value = stock.MinStock ?? 0; numStockMax.Value = stock.MaxStock ?? 0; txtStockNotes.Text = stock.Notes?.ToString() ?? ""; } } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); } btnAddStock.Text = "💾 Αποθήκευση"; }
        private void btnDeleteStock_Click(object sender, EventArgs e) { if (dgvWarehouseStock.CurrentRow == null) { MessageBox.Show("️ Επίλεξε μια εγγραφή!", "Προσοχή"); return; } var id = dgvWarehouseStock.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτού του stock;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM StockVariants WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearStockFields(); LoadWarehouseStock(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearStockFields() { _editingStockId = 0; if (cmbStockWarehouse.Items.Count > 0) cmbStockWarehouse.SelectedIndex = -1; if (cmbStockVariant.Items.Count > 0) cmbStockVariant.SelectedIndex = -1; if (cmbStockLot.Items.Count > 0) cmbStockLot.SelectedIndex = 0; numStockQty.Value = 0; numStockReserved.Value = 0; numStockMin.Value = 0; numStockMax.Value = 0; txtStockNotes.Clear(); btnAddStock.Text = " Προσθήκη"; }
        private void btnManageImages_Click(object sender, EventArgs e) { MessageBox.Show("Η διαχείριση φωτογραφιών θα υλοποιηθεί σύντομα.", "Πληροφορία"); }
        private int _editingSupplierId = 0;
        private void LoadItemSuppliers(int itemId) { if (dgvItemSuppliers == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT si.Id, ISNULL(s.SupplierName, 'Άγνωστος') AS [Προμηθευτής], si.SupplierCode AS [Κωδικός], si.PurchasePrice AS [Τιμή Αγοράς], si.Currency AS [Νόμισμα], si.LeadTime AS [Ημέρες], CASE WHEN si.IsDefaultSupplier = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Βασικός], si.LastPurchaseDate AS [Τελευταία Αγορά] FROM SupplierItems si LEFT JOIN Suppliers s ON si.SupplierId = s.Id WHERE si.ItemId = @Id ORDER BY si.IsDefaultSupplier DESC, s.SupplierName", new { Id = itemId }).ToList(); dgvItemSuppliers.DataSource = data; if (dgvItemSuppliers.Columns.Contains("Id")) dgvItemSuppliers.Columns["Id"].Visible = false; System.Diagnostics.Debug.WriteLine($"✅ Φορτώθηκαν {data.Count} προμηθευτές για είδος {itemId}"); } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης προμηθευτών: " + ex.Message); } }
        private void LoadSupplierCombos() { try { using var conn = new SqlConnection(_connStr); var suppliers = conn.Query("SELECT Id, ISNULL(SupplierName, '') AS Name FROM Suppliers ORDER BY SupplierName").ToList(); if (suppliers.Count == 0) MessageBox.Show("⚠️ Δεν υπάρχουν προμηθευτές στη βάση!", "Προσοχή"); cmbItemSupplier.DataSource = suppliers; cmbItemSupplier.DisplayMember = "Name"; cmbItemSupplier.ValueMember = "Id"; System.Diagnostics.Debug.WriteLine($"✅ Φορτώθηκαν {suppliers.Count} προμηθευτές"); } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης προμηθευτών: " + ex.Message); } }
        private void btnAddItemSupplier_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } if (cmbItemSupplier.SelectedValue == null) { MessageBox.Show("⚠️ Επίλεξε προμηθευτή!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); int supplierId = Convert.ToInt32(cmbItemSupplier.SelectedValue); if (_editingSupplierId > 0) { conn.Execute(@"UPDATE SupplierItems SET SupplierId = @Supplier, SupplierCode = @Code, PurchasePrice = @Price, Currency = @Currency, LeadTime = @Lead, MinOrderQty = @MinQty, IsDefaultSupplier = @IsDefault, Notes = @Notes, IsActive = 1 WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingSupplierId, ItemId = _itemId.Value, Supplier = supplierId, Code = txtItemSupplierCode.Text.Trim(), Price = numItemSupplierPrice.Value, Currency = cmbItemSupplierCurrency.Text.Trim(), Lead = (int)numItemSupplierLead.Value, MinQty = numItemSupplierMinQty.Value, IsDefault = chkItemSupplierDefault.Checked, Notes = txtItemSupplierNotes.Text.Trim() }); MessageBox.Show("✅ Ο προμηθευτής ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM SupplierItems WHERE SupplierId = @Supplier AND ItemId = @ItemId AND IsActive = 1", new { Supplier = supplierId, ItemId = _itemId.Value }); if (existing != null) { MessageBox.Show("️ Υπάρχει ήδη εγγραφή για αυτόν τον προμηθευτή! Χρησιμοποίησε Επεξεργασία.", "Προσοχή"); return; } conn.Execute(@"INSERT INTO SupplierItems (SupplierId, ItemId, SupplierCode, PurchasePrice, Currency, MinOrderQty, LeadTime, IsDefaultSupplier, Notes, IsActive, CreatedAt) VALUES (@Supplier, @ItemId, @Code, @Price, @Currency, @MinQty, @Lead, @IsDefault, @Notes, 1, GETDATE())", new { Supplier = supplierId, ItemId = _itemId.Value, Code = txtItemSupplierCode.Text.Trim(), Price = numItemSupplierPrice.Value, Currency = cmbItemSupplierCurrency.Text.Trim(), MinQty = numItemSupplierMinQty.Value, Lead = (int)numItemSupplierLead.Value, IsDefault = chkItemSupplierDefault.Checked, Notes = txtItemSupplierNotes.Text.Trim() }); MessageBox.Show("✅ Ο προμηθευτής προστέθηκε!", "Επιτυχία"); } ClearSupplierFields(); LoadItemSuppliers(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditItemSupplier_Click(object sender, EventArgs e) { if (dgvItemSuppliers.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε προμηθευτή!", "Προσοχή"); return; } var row = dgvItemSuppliers.CurrentRow; _editingSupplierId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var sup = conn.QueryFirstOrDefault<dynamic>("SELECT SupplierId, SupplierCode, PurchasePrice, Currency, LeadTime, MinOrderQty, IsDefaultSupplier, Notes FROM SupplierItems WHERE Id = @Id", new { Id = _editingSupplierId }); if (sup != null) { if (cmbItemSupplier.Items.Count > 0) cmbItemSupplier.SelectedValue = sup.SupplierId; txtItemSupplierCode.Text = sup.SupplierCode?.ToString() ?? ""; numItemSupplierPrice.Value = sup.PurchasePrice ?? 0; var currency = sup.Currency?.ToString() ?? "€"; if (cmbItemSupplierCurrency.Items.Contains(currency)) cmbItemSupplierCurrency.SelectedItem = currency; else cmbItemSupplierCurrency.Text = currency; numItemSupplierLead.Value = sup.LeadTime ?? 0; numItemSupplierMinQty.Value = sup.MinOrderQty ?? 0; chkItemSupplierDefault.Checked = sup.IsDefaultSupplier ?? false; txtItemSupplierNotes.Text = sup.Notes?.ToString() ?? ""; } } catch { } btnAddItemSupplier.Text = "💾 Αποθήκευση"; }
        private void btnDeleteItemSupplier_Click(object sender, EventArgs e) { if (dgvItemSuppliers.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε προμηθευτή!", "Προσοχή"); return; } var id = dgvItemSuppliers.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτού του προμηθευτή;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierItems WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearSupplierFields(); LoadItemSuppliers(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearSupplierFields() { _editingSupplierId = 0; if (cmbItemSupplier.Items.Count > 0) cmbItemSupplier.SelectedIndex = -1; txtItemSupplierCode.Clear(); numItemSupplierPrice.Value = 0; cmbItemSupplierCurrency.SelectedIndex = 0; numItemSupplierLead.Value = 0; numItemSupplierMinQty.Value = 0; chkItemSupplierDefault.Checked = false; txtItemSupplierNotes.Clear(); btnAddItemSupplier.Text = "➕ Προσθήκη"; }
        private int _editingRelatedId = 0;
        private void LoadRelated(int itemId) { if (dgvRelated == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT r.Id, i.Name AS [Είδος], i.Code AS [Κωδικός], r.RelationType AS [Τύπος Σχέσης], r.Quantity AS [Ποσότητα] FROM ItemRelated r LEFT JOIN Items i ON r.RelatedItemId = i.Id WHERE r.ItemId = @Id ORDER BY r.RelationType, i.Name", new { Id = itemId }).ToList(); dgvRelated.DataSource = data; if (dgvRelated.Columns.Contains("Id")) dgvRelated.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης σχετικών ειδών: " + ex.Message); } }
        private void btnAddRelated_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } if (string.IsNullOrWhiteSpace(txtRelatedCode.Text)) { MessageBox.Show("⚠️ Γράψε κωδικό ή ID είδους!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); int relatedItemId = 0; if (int.TryParse(txtRelatedCode.Text.Trim(), out int idVal)) { var found = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM Items WHERE Id = @Id", new { Id = idVal }); if (found != null) relatedItemId = found.Id; } else { var found = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM Items WHERE Code = @Code OR Name = @Name", new { Code = txtRelatedCode.Text.Trim(), Name = txtRelatedCode.Text.Trim() }); if (found != null) relatedItemId = found.Id; } if (relatedItemId == 0) { MessageBox.Show("⚠️ Δεν βρέθηκε είδος με αυτόν τον κωδικό/ID!", "Προσοχή"); return; } if (relatedItemId == _itemId.Value) { MessageBox.Show("⚠️ Δεν μπορείς να συνδέσεις το είδος με τον εαυτό του!", "Προσοχή"); return; } if (_editingRelatedId > 0) { conn.Execute(@"UPDATE ItemRelated SET RelatedItemId = @RelatedId, RelationType = @Type, Quantity = @Qty WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingRelatedId, ItemId = _itemId.Value, RelatedId = relatedItemId, Type = cmbRelatedType.Text.Trim(), Qty = numRelatedQty.Value }); MessageBox.Show("✅ Η σχέση ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM ItemRelated WHERE ItemId = @ItemId AND RelatedItemId = @RelatedId", new { ItemId = _itemId.Value, RelatedId = relatedItemId }); if (existing != null) { MessageBox.Show("⚠️ Υπάρχει ήδη σχέση με αυτό το είδος! Χρησιμοποίησε Επεξεργασία.", "Προσοχή"); return; } conn.Execute(@"INSERT INTO ItemRelated (ItemId, RelatedItemId, RelationType, Quantity) VALUES (@ItemId, @RelatedId, @Type, @Qty)", new { ItemId = _itemId.Value, RelatedId = relatedItemId, Type = cmbRelatedType.Text.Trim(), Qty = numRelatedQty.Value }); MessageBox.Show("✅ Η σχέση προστέθηκε!", "Επιτυχία"); } ClearRelatedFields(); LoadRelated(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditRelated_Click(object sender, EventArgs e) { if (dgvRelated.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε μια σχέση!", "Προσοχή"); return; } var row = dgvRelated.CurrentRow; _editingRelatedId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var rel = conn.QueryFirstOrDefault<dynamic>("SELECT RelatedItemId, RelationType, Quantity FROM ItemRelated WHERE Id = @Id", new { Id = _editingRelatedId }); if (rel != null) { var relatedItem = conn.QueryFirstOrDefault<dynamic>("SELECT Code FROM Items WHERE Id = @Id", new { Id = rel.RelatedItemId }); txtRelatedCode.Text = relatedItem?.Code?.ToString() ?? rel.RelatedItemId.ToString(); cmbRelatedType.Text = rel.RelationType?.ToString() ?? "Σχετικό"; numRelatedQty.Value = rel.Quantity ?? 1; } } catch { } btnAddRelated.Text = "💾 Αποθήκευση"; }
        private void btnDeleteRelated_Click(object sender, EventArgs e) { if (dgvRelated.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε μια σχέση!", "Προσοχή"); return; } var id = dgvRelated.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτής της σχέσης;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemRelated WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearRelatedFields(); LoadRelated(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearRelatedFields() { _editingRelatedId = 0; txtRelatedCode.Clear(); cmbRelatedType.SelectedIndex = 0; numRelatedQty.Value = 1; btnAddRelated.Text = " Προσθήκη"; }
        private void LoadPriceHistory(int itemId) { if (dgvPriceHistory == null) return; try { using var conn = new SqlConnection(_connStr); dgvPriceHistory.DataSource = conn.Query(@"SELECT Id, ValidFrom AS [Ημερομηνία Από], ValidTo AS [Ημερομηνία Έως], OldPrice AS [Παλιά Τιμή], NewPrice AS [Νέα Τιμή], Currency AS [Νόμισμα], ChangeReason AS [Λόγος], ChangeType AS [Τύπος], Notes AS [Σημειώσεις] FROM ItemPriceHistory WHERE ItemId = @Id ORDER BY ValidFrom DESC", new { Id = itemId }).ToList(); if (dgvPriceHistory.Columns.Contains("Id")) dgvPriceHistory.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης ιστορικού: " + ex.Message); } }
        private int _editingAttrId = 0;
        private void LoadItemAttributes(int itemId) { if (dgvItemAttributes == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT Id, AttributeCode AS [Κωδικός], AttributeName AS [Χαρακτηριστικό], AttributeValue AS [Τιμή], AttributeType AS [Τύπος], SortOrder AS [Σειρά], CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό] FROM ItemAttributes WHERE ItemId = @Id ORDER BY SortOrder, AttributeName", new { Id = itemId }).ToList(); dgvItemAttributes.DataSource = data; if (dgvItemAttributes.Columns.Contains("Id")) dgvItemAttributes.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης χαρακτηριστικών: " + ex.Message); } }
        private void btnAddAttr_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } if (string.IsNullOrWhiteSpace(txtAttrName.Text)) { MessageBox.Show("⚠️ Το Όνομα είναι υποχρεωτικό!", "Προσοχή"); return; } if (string.IsNullOrWhiteSpace(txtAttrValue.Text)) { MessageBox.Show("⚠️ Η Τιμή είναι υποχρεωτική!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); if (_editingAttrId > 0) { conn.Execute(@"UPDATE ItemAttributes SET AttributeCode = @Code, AttributeName = @Name, AttributeValue = @Value, AttributeType = @Type, SortOrder = @Sort, IsActive = 1 WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingAttrId, ItemId = _itemId.Value, Code = txtAttrCode.Text.Trim(), Name = txtAttrName.Text.Trim(), Value = txtAttrValue.Text.Trim(), Type = cmbAttrType.Text.Trim(), Sort = (int)numAttrSortOrder.Value }); MessageBox.Show("✅ Το χαρακτηριστικό ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM ItemAttributes WHERE ItemId = @ItemId AND AttributeName = @Name AND AttributeValue = @Value", new { ItemId = _itemId.Value, Name = txtAttrName.Text.Trim(), Value = txtAttrValue.Text.Trim() }); if (existing != null) { MessageBox.Show("⚠️ Υπάρχει ήδη αυτό το χαρακτηριστικό! Χρησιμοποίησε Επεξεργασία.", "Προσοχή"); return; } conn.Execute(@"INSERT INTO ItemAttributes (ItemId, AttributeCode, AttributeName, AttributeValue, AttributeType, SortOrder, IsActive, CreatedAt) VALUES (@ItemId, @Code, @Name, @Value, @Type, @Sort, 1, GETDATE())", new { ItemId = _itemId.Value, Code = txtAttrCode.Text.Trim(), Name = txtAttrName.Text.Trim(), Value = txtAttrValue.Text.Trim(), Type = cmbAttrType.Text.Trim(), Sort = (int)numAttrSortOrder.Value }); MessageBox.Show("✅ Το χαρακτηριστικό προστέθηκε!", "Επιτυχία"); } ClearAttrFields(); LoadItemAttributes(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditAttr_Click(object sender, EventArgs e) { if (dgvItemAttributes.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε ένα χαρακτηριστικό!", "Προσοχή"); return; } var row = dgvItemAttributes.CurrentRow; _editingAttrId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var attr = conn.QueryFirstOrDefault<dynamic>("SELECT AttributeCode, AttributeName, AttributeValue, AttributeType, SortOrder FROM ItemAttributes WHERE Id = @Id", new { Id = _editingAttrId }); if (attr != null) { txtAttrCode.Text = attr.AttributeCode?.ToString() ?? ""; txtAttrName.Text = attr.AttributeName?.ToString() ?? ""; txtAttrValue.Text = attr.AttributeValue?.ToString() ?? ""; var type = attr.AttributeType?.ToString() ?? "Κείμενο"; if (cmbAttrType.Items.Contains(type)) cmbAttrType.SelectedItem = type; else cmbAttrType.Text = type; numAttrSortOrder.Value = attr.SortOrder ?? 0; } } catch { } btnAddAttr.Text = "💾 Αποθήκευση"; }
        private void btnDeleteAttr_Click(object sender, EventArgs e) { if (dgvItemAttributes.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε ένα χαρακτηριστικό!", "Προσοχή"); return; } var id = dgvItemAttributes.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτού του χαρακτηριστικού;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemAttributes WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearAttrFields(); LoadItemAttributes(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearAttrFields() { _editingAttrId = 0; txtAttrCode.Clear(); txtAttrName.Clear(); txtAttrValue.Clear(); cmbAttrType.SelectedIndex = 0; numAttrSortOrder.Value = 0; btnAddAttr.Text = "➕ Προσθήκη"; }
        private int _editingAltId = 0;
        private void LoadAlternativeCodes(int itemId) { if (dgvAlternativeCodes == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT Id, AltCode AS [Εναλλακτικός Κωδικός], AltDescription AS [Περιγραφή], CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό] FROM ItemAlternativeCodes WHERE ItemId = @Id ORDER BY AltCode", new { Id = itemId }).ToList(); dgvAlternativeCodes.DataSource = data; if (dgvAlternativeCodes.Columns.Contains("Id")) dgvAlternativeCodes.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης εναλλακτικών κωδικών: " + ex.Message); } }
        private void btnAddAlt_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!"); return; } if (string.IsNullOrWhiteSpace(txtAltCode.Text)) { MessageBox.Show("⚠️ Ο Κωδικός είναι υποχρεωτικός!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); if (_editingAltId > 0) { conn.Execute(@"UPDATE ItemAlternativeCodes SET AltCode = @Code, AltDescription = @Desc, IsActive = 1 WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingAltId, ItemId = _itemId.Value, Code = txtAltCode.Text.Trim(), Desc = txtAltDesc.Text.Trim() }); MessageBox.Show("✅ Ο κωδικός ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM ItemAlternativeCodes WHERE ItemId = @ItemId AND AltCode = @Code", new { ItemId = _itemId.Value, Code = txtAltCode.Text.Trim() }); if (existing != null) { MessageBox.Show("⚠️ Υπάρχει ήδη αυτός ο κωδικός! Χρησιμοποίησε Επεξεργασία.", "Προσοχή"); return; } conn.Execute(@"INSERT INTO ItemAlternativeCodes (ItemId, AltCode, AltDescription, IsActive, CreatedAt) VALUES (@ItemId, @Code, @Desc, 1, GETDATE())", new { ItemId = _itemId.Value, Code = txtAltCode.Text.Trim(), Desc = txtAltDesc.Text.Trim() }); MessageBox.Show("✅ Ο κωδικός προστέθηκε!", "Επιτυχία"); } ClearAltFields(); LoadAlternativeCodes(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditAlt_Click(object sender, EventArgs e) { if (dgvAlternativeCodes.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε έναν κωδικό!", "Προσοχή"); return; } var row = dgvAlternativeCodes.CurrentRow; _editingAltId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var alt = conn.QueryFirstOrDefault<dynamic>("SELECT AltCode, AltDescription FROM ItemAlternativeCodes WHERE Id = @Id", new { Id = _editingAltId }); if (alt != null) { txtAltCode.Text = alt.AltCode?.ToString() ?? ""; txtAltDesc.Text = alt.AltDescription?.ToString() ?? ""; } } catch { } btnAddAlt.Text = " Αποθήκευση"; }
        private void btnDeleteAlt_Click(object sender, EventArgs e) { if (dgvAlternativeCodes.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε έναν κωδικό!", "Προσοχή"); return; } var id = dgvAlternativeCodes.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτού του κωδικού;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemAlternativeCodes WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearAltFields(); LoadAlternativeCodes(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearAltFields() { _editingAltId = 0; txtAltCode.Clear(); txtAltDesc.Clear(); btnAddAlt.Text = "➕ Προσθήκη"; }
        private int _editingColorId = 0;
        private void pnlColorPreview_Click(object sender, EventArgs e) { colorDialog.FullOpen = true; colorDialog.AnyColor = true; colorDialog.SolidColorOnly = false; if (!string.IsNullOrWhiteSpace(txtColorHex.Text) && txtColorHex.Text.StartsWith("#")) { try { colorDialog.Color = System.Drawing.ColorTranslator.FromHtml(txtColorHex.Text); } catch { } } if (colorDialog.ShowDialog() == DialogResult.OK) { pnlColorPreview.BackColor = colorDialog.Color; txtColorHex.Text = ColorToHex(colorDialog.Color); } }
        private string ColorToHex(System.Drawing.Color color) { return $"#{color.R:X2}{color.G:X2}{color.B:X2}"; }
        private System.Drawing.Color HexToColor(string hex) { if (string.IsNullOrWhiteSpace(hex)) return System.Drawing.Color.White; hex = hex.TrimStart('#'); try { return System.Drawing.ColorTranslator.FromHtml("#" + hex); } catch { return System.Drawing.Color.White; } }
        private void LoadItemColors(int itemId) { if (dgvItemColors == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT Id, ColorCode AS [Κωδικός], ColorName AS [Όνομα], ColorHex AS [Hex], CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό] FROM ItemColors WHERE IsActive = 1 ORDER BY ColorName", new { }).ToList(); dgvItemColors.DataSource = data; if (dgvItemColors.Columns.Contains("Id")) dgvItemColors.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης χρωμάτων: " + ex.Message); } }
        private void btnAddColor_Click(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtColorName.Text)) { MessageBox.Show("⚠️ Το Όνομα είναι υποχρεωτικό!", "Προσοχή"); return; } if (string.IsNullOrWhiteSpace(txtColorHex.Text)) { MessageBox.Show("⚠️ Επίλεξε χρώμα από το panel!", "Προσοχή"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); if (_editingColorId > 0) { conn.Execute(@"UPDATE ItemColors SET ColorCode = @Code, ColorName = @Name, ColorHex = @Hex, IsActive = 1 WHERE Id = @Id", new { Id = _editingColorId, Code = txtColorCode.Text.Trim(), Name = txtColorName.Text.Trim(), Hex = txtColorHex.Text.Trim() }); MessageBox.Show("✅ Το χρώμα ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM ItemColors WHERE ColorName = @Name AND IsActive = 1", new { Name = txtColorName.Text.Trim() }); if (existing != null) { MessageBox.Show("️ Υπάρχει ήδη χρώμα με αυτό το όνομα!", "Προσοχή"); return; } conn.Execute(@"INSERT INTO ItemColors (ColorCode, ColorName, ColorHex, IsActive) VALUES (@Code, @Name, @Hex, 1)", new { Code = txtColorCode.Text.Trim(), Name = txtColorName.Text.Trim(), Hex = txtColorHex.Text.Trim() }); MessageBox.Show("✅ Το χρώμα προστέθηκε!", "Επιτυχία"); } ClearColorFields(); LoadItemColors(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditColor_Click(object sender, EventArgs e) { if (dgvItemColors.CurrentRow == null) { MessageBox.Show("️ Επίλεξε ένα χρώμα!", "Προσοχή"); return; } var row = dgvItemColors.CurrentRow; _editingColorId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var color = conn.QueryFirstOrDefault<dynamic>("SELECT ColorCode, ColorName, ColorHex FROM ItemColors WHERE Id = @Id", new { Id = _editingColorId }); if (color != null) { txtColorCode.Text = color.ColorCode?.ToString() ?? ""; txtColorName.Text = color.ColorName?.ToString() ?? ""; txtColorHex.Text = color.ColorHex?.ToString() ?? ""; pnlColorPreview.BackColor = HexToColor(txtColorHex.Text); } } catch { } btnAddColor.Text = "💾 Αποθήκευση"; }
        private void btnDeleteColor_Click(object sender, EventArgs e) { if (dgvItemColors.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε ένα χρώμα!", "Προσοχή"); return; } var id = dgvItemColors.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτού του χρώματος;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemColors WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearColorFields(); LoadItemColors(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void ClearColorFields() { _editingColorId = 0; txtColorCode.Clear(); txtColorName.Clear(); txtColorHex.Clear(); pnlColorPreview.BackColor = System.Drawing.Color.White; btnAddColor.Text = "➕ Προσθήκη"; }

        // Serial Numbers & Sizes methods omitted for brevity in this thought block but MUST be included in final file
        // They are identical to your input.
        private List<dynamic> _allSerialData = new List<dynamic>(); private List<dynamic> _filteredSerialData = new List<dynamic>(); private int _editingSerialId = 0;
        private void btnSaveSerial_Click(object sender, EventArgs e) { if (!_itemId.HasValue) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος!", "Προσοχή"); return; } try { LoadSerialNumbers(_itemId.Value); MessageBox.Show("✅ Οι σειριακοί αριθμοί ανανεώθηκαν/αποθηκεύτηκαν!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
        private void LoadSerialNumbers(int itemId) { if (dgvSerialNumbers == null) return; try { using var conn = new SqlConnection(_connStr); _allSerialData = conn.Query(@"SELECT sn.Id, sn.SerialNumber AS [Σειριακός], l.LotNumber AS [Παρτίδα], sn.WarrantyStart AS [Εγγύηση Από], sn.WarrantyEnd AS [Εγγύηση Έως], sn.Status AS [Κατάσταση], sn.Notes AS [Σημειώσεις] FROM SerialNumbers sn LEFT JOIN Lots l ON sn.LotId = l.Id WHERE sn.ItemId = @Id ORDER BY sn.SerialNumber", new { Id = itemId }).ToList(); _filteredSerialData = _allSerialData.ToList(); serialBindingSource.DataSource = _filteredSerialData; dgvSerialNumbers.DataSource = serialBindingSource; LoadSerialLots(); UpdateSerialRecordInfo(); } catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης σειριακών: " + ex.Message); } }
        private void LoadSerialLots() { try { using var conn = new SqlConnection(_connStr); var lots = conn.Query("SELECT Id, ISNULL(LotNumber, 'Χωρίς Κωδικό') AS LotNumber FROM Lots WHERE ItemId = @Id ORDER BY LotNumber", new { Id = _itemId.Value }).ToList(); var lotList = new List<SimpleIdNameItem>(); lotList.Add(new SimpleIdNameItem { Id = null, Name = "-- Χωρίς Παρτίδα --" }); foreach (var lot in lots) lotList.Add(new SimpleIdNameItem { Id = lot.Id, Name = lot.LotNumber }); cmbSerialLotId.DataSource = lotList; cmbSerialLotId.DisplayMember = "Name"; cmbSerialLotId.ValueMember = "Id"; } catch { } }
        private void btnAddSerial_Click(object sender, EventArgs e) { if (!_itemId.HasValue || string.IsNullOrWhiteSpace(txtSerialNumber.Text)) { MessageBox.Show("⚠️ Αποθήκευσε πρώτα το είδος και γράψε σειριακό!"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); int? lotId = null; if (cmbSerialLotId.SelectedValue != null && cmbSerialLotId.SelectedValue != DBNull.Value) { var selectedLot = cmbSerialLotId.SelectedItem as SimpleIdNameItem; lotId = selectedLot?.Id; } DateTime? wStart = dtpSerialWarrantyStart.Checked ? dtpSerialWarrantyStart.Value : (DateTime?)null; DateTime? wEnd = dtpSerialWarrantyEnd.Checked ? dtpSerialWarrantyEnd.Value : (DateTime?)null; if (_editingSerialId > 0) { conn.Execute(@"UPDATE SerialNumbers SET SerialNumber = @Serial, LotId = @Lot, WarrantyStart = @WStart, WarrantyEnd = @WEnd, Status = @Status, Notes = @Notes, UpdatedAt = GETDATE() WHERE Id = @Id AND ItemId = @ItemId", new { Id = _editingSerialId, ItemId = _itemId.Value, Serial = txtSerialNumber.Text.Trim(), Lot = lotId, WStart = wStart, WEnd = wEnd, Status = cmbSerialStatus.Text.Trim(), Notes = txtSerialNotes.Text.Trim() }); MessageBox.Show("✅ Ο σειριακός ενημερώθηκε!", "Επιτυχία"); } else { var existing = conn.QueryFirstOrDefault<dynamic>("SELECT Id FROM SerialNumbers WHERE ItemId = @ItemId AND SerialNumber = @Serial", new { ItemId = _itemId.Value, Serial = txtSerialNumber.Text.Trim() }); if (existing != null) { MessageBox.Show("⚠️ Υπάρχει ήδη αυτός ο σειριακός αριθμός!", "Προσοχή"); return; } conn.Execute(@"INSERT INTO SerialNumbers (ItemId, SerialNumber, LotId, WarrantyStart, WarrantyEnd, Status, Notes, CreatedAt, UpdatedAt) VALUES (@ItemId, @Serial, @Lot, @WStart, @WEnd, @Status, @Notes, GETDATE(), GETDATE())", new { ItemId = _itemId.Value, Serial = txtSerialNumber.Text.Trim(), Lot = lotId, WStart = wStart, WEnd = wEnd, Status = cmbSerialStatus.Text.Trim(), Notes = txtSerialNotes.Text.Trim() }); MessageBox.Show("✅ Ο σειριακός προστέθηκε!", "Επιτυχία"); } ClearSerialFields(); LoadSerialNumbers(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } }
        private void btnEditSerial_Click(object sender, EventArgs e) { if (dgvSerialNumbers.CurrentRow == null) { MessageBox.Show("️ Επίλεξε σειριακό!", "Προσοχή"); return; } var row = dgvSerialNumbers.CurrentRow; _editingSerialId = Convert.ToInt32(row.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var serial = conn.QueryFirstOrDefault<dynamic>("SELECT SerialNumber, LotId, WarrantyStart, WarrantyEnd, Status, Notes FROM SerialNumbers WHERE Id = @Id", new { Id = _editingSerialId }); if (serial != null) { txtSerialNumber.Text = serial.SerialNumber?.ToString() ?? ""; txtSerialNotes.Text = serial.Notes?.ToString() ?? ""; if (cmbSerialLotId.Items.Count > 0 && serial.LotId != null && serial.LotId != DBNull.Value) cmbSerialLotId.SelectedValue = Convert.ToInt32(serial.LotId); else cmbSerialLotId.SelectedIndex = 0; if (serial.WarrantyStart != null && serial.WarrantyStart != DBNull.Value) { dtpSerialWarrantyStart.Value = Convert.ToDateTime(serial.WarrantyStart); dtpSerialWarrantyStart.Checked = true; } else dtpSerialWarrantyStart.Checked = false; if (serial.WarrantyEnd != null && serial.WarrantyEnd != DBNull.Value) { dtpSerialWarrantyEnd.Value = Convert.ToDateTime(serial.WarrantyEnd); dtpSerialWarrantyEnd.Checked = true; } else dtpSerialWarrantyEnd.Checked = false; var status = serial.Status?.ToString() ?? "Διαθέσιμο"; if (cmbSerialStatus.Items.Contains(status)) cmbSerialStatus.SelectedItem = status; } } catch { } btnAddSerial.Text = "💾 Αποθήκευση"; btnCancelSerial.Visible = true; }
        private void btnDeleteSerial_Click(object sender, EventArgs e) { if (dgvSerialNumbers.CurrentRow == null) { MessageBox.Show("⚠️ Επίλεξε σειριακό!", "Προσοχή"); return; } var id = dgvSerialNumbers.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή αυτού του σειριακού;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SerialNumbers WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία"); ClearSerialFields(); LoadSerialNumbers(_itemId.Value); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα"); } } }
        private void btnCancelSerial_Click(object sender, EventArgs e) { ClearSerialFields(); }
        private void ClearSerialFields() { _editingSerialId = 0; txtSerialNumber.Clear(); txtSerialNotes.Clear(); if (cmbSerialLotId.Items.Count > 0) cmbSerialLotId.SelectedIndex = 0; cmbSerialStatus.SelectedIndex = 0; dtpSerialWarrantyStart.Checked = false; dtpSerialWarrantyEnd.Checked = false; btnAddSerial.Text = "➕ Προσθήκη"; btnCancelSerial.Visible = false; }
        private void btnSerialSearch_Click(object sender, EventArgs e) { string s = txtSerialSearch.Text.Trim().ToLower(); if (string.IsNullOrEmpty(s)) _filteredSerialData = _allSerialData.ToList(); else _filteredSerialData = _allSerialData.Where(r => r.Σειριακός?.ToString().ToLower().Contains(s) == true || r.Κατάσταση?.ToString().ToLower().Contains(s) == true || r.Σημειώσεις?.ToString().ToLower().Contains(s) == true).ToList(); serialBindingSource.DataSource = _filteredSerialData; UpdateSerialRecordInfo(); }
        private void txtSerialSearch_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { btnSerialSearch_Click(sender, e); e.Handled = true; } }
        private void btnSerialFirst_Click(object sender, EventArgs e) { if (serialBindingSource.Count > 0) { serialBindingSource.MoveFirst(); UpdateSerialRecordInfo(); } }
        private void btnSerialPrev_Click(object sender, EventArgs e) { if (serialBindingSource.Position > 0) { serialBindingSource.MovePrevious(); UpdateSerialRecordInfo(); } }
        private void btnSerialNext_Click(object sender, EventArgs e) { if (serialBindingSource.Position < serialBindingSource.Count - 1) { serialBindingSource.MoveNext(); UpdateSerialRecordInfo(); } }
        private void btnSerialLast_Click(object sender, EventArgs e) { if (serialBindingSource.Count > 0) { serialBindingSource.MoveLast(); UpdateSerialRecordInfo(); } }
        private void UpdateSerialRecordInfo() { int totalCount = _filteredSerialData.Count; int currentPosition = serialBindingSource.Position >= 0 ? serialBindingSource.Position + 1 : 0; lblSerialRecordInfo.Text = $"{currentPosition} / {totalCount}"; }
        private void dgvSerialNumbers_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) btnEditSerial_Click(sender, EventArgs.Empty); }
        private void dgvSerialNumbers_SelectionChanged(object sender, EventArgs e) { UpdateSerialRecordInfo(); }
        private int _editingSizeId = 0;
        private void LoadItemSizes() { if (dgvSizes == null) return; try { using var conn = new SqlConnection(_connStr); var data = conn.Query(@"SELECT Id, SizeCode AS [Κωδικός], SizeName AS [Όνομα], SortOrder AS [Σειρά], CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό] FROM ItemSizes ORDER BY SortOrder, SizeName", new { }).ToList(); dgvSizes.DataSource = data; if (dgvSizes.Columns.Contains("Id")) dgvSizes.Columns["Id"].Visible = false; } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } }
        private void btnAddSize_Click(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtSizeName.Text)) { MessageBox.Show("⚠️ Το Όνομα είναι υποχρεωτικό!"); return; } try { using var conn = new SqlConnection(_connStr); conn.Open(); if (_editingSizeId > 0) { conn.Execute(@"UPDATE ItemSizes SET SizeCode = @Code, SizeName = @Name, SortOrder = @Sort, IsActive = @Active WHERE Id = @Id", new { Id = _editingSizeId, Code = txtSizeCode.Text.Trim(), Name = txtSizeName.Text.Trim(), Sort = (int)numSizeSortOrder.Value, Active = chkSizeActive.Checked }); MessageBox.Show("✅ Ενημερώθηκε!"); } else { conn.Execute(@"INSERT INTO ItemSizes (SizeCode, SizeName, IsActive, SortOrder, CreatedAt) VALUES (@Code, @Name, @Active, @Sort, GETDATE())", new { Code = txtSizeCode.Text.Trim(), Name = txtSizeName.Text.Trim(), Active = chkSizeActive.Checked, Sort = (int)numSizeSortOrder.Value }); MessageBox.Show("✅ Προστέθηκε!"); } ClearSizeFields(); LoadItemSizes(); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } }
        private void btnEditSize_Click(object sender, EventArgs e) { if (dgvSizes.CurrentRow == null) return; _editingSizeId = Convert.ToInt32(dgvSizes.CurrentRow.Cells["Id"].Value); try { using var conn = new SqlConnection(_connStr); var size = conn.QueryFirstOrDefault<dynamic>("SELECT SizeCode, SizeName, SortOrder, IsActive FROM ItemSizes WHERE Id = @Id", new { Id = _editingSizeId }); if (size != null) { txtSizeCode.Text = size.SizeCode?.ToString() ?? ""; txtSizeName.Text = size.SizeName?.ToString() ?? ""; numSizeSortOrder.Value = size.SortOrder ?? 0; chkSizeActive.Checked = size.IsActive ?? true; } } catch { } btnAddSize.Text = "💾 Αποθήκευση"; }
        private void btnDeleteSize_Click(object sender, EventArgs e) { if (dgvSizes.CurrentRow == null) return; var id = dgvSizes.CurrentRow.Cells["Id"].Value; if (id == null || id == DBNull.Value) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { try { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM ItemSizes WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); ClearSizeFields(); LoadItemSizes(); } catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); } } }
        private void ClearSizeFields() { _editingSizeId = 0; txtSizeCode.Clear(); txtSizeName.Clear(); numSizeSortOrder.Value = 0; chkSizeActive.Checked = true; btnAddSize.Text = " Προσθήκη"; }
    }
}