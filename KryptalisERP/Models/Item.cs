using System;

namespace KryptalisERP.Models
{
    public class Item
    {
        public int Id { get; set; }

        // Core Identity
        public string Code { get; set; }
        public string ItemCode { get; set; } // Duplicate/Legacy
        public string Name { get; set; }
        public string ItemName { get; set; } // Duplicate/Legacy
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public string Barcode { get; set; }
        public string AlternativeCode { get; set; }

        // Categorization
        public string ItemType { get; set; } // Product/Service/etc
        public string Category { get; set; }
        public int? CategoryId { get; set; }
        public string SubCategory { get; set; }
        public string Classification { get; set; }
        public string LogCategory { get; set; }
        public string ItemFamily { get; set; }

        // Branding
        public string Brand { get; set; }
        public int? BrandId { get; set; }
        public string Manufacturer { get; set; }
        public int? ManufacturerId { get; set; }

        // Pricing & Costs
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }

        // Taxes & myDATA
        public decimal VatRate { get; set; }
        public int? VatArticleId { get; set; }
        public string VatArticle { get; set; }
        public string VATClass { get; set; }
        public string ExemptionArticle { get; set; }
        public string ArticleExemption { get; set; }
        public string ExemptionReason { get; set; }
        public string MydataCategory { get; set; }
        public string MydataCode { get; set; }
        public string CPVCode { get; set; }
        public string CPVDescription { get; set; }
        public string TARICCode { get; set; }
        public string CountryOfOrigin { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderDescription { get; set; }
        public string ProviderURL { get; set; }
        public string ProviderCharacteristics { get; set; }

        // Inventory & Units
        public string Unit { get; set; }
        public string PrimaryUnit { get; set; }
        public int? UomId { get; set; }
        public int? PrimaryUnitId { get; set; }
        public int? UnitId { get; set; } // Another unit ref?
        public int? PurchaseUomId { get; set; }
        public int? SalesUomId { get; set; }
        public string UnitType { get; set; }

        // Stock Levels
        public decimal Stock { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal MinStock { get; set; }
        public decimal MaxStock { get; set; }
        public decimal ReorderPoint { get; set; }
        public decimal SafetyStock { get; set; }
        public string Location { get; set; }

        // Physical Characteristics
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public string Dimensions { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Color { get; set; }
        public int? ColorId { get; set; }
        public string Size { get; set; }
        public int? SizeId { get; set; }
        public bool ShowSizeColor { get; set; }

        // Tracking Flags
        public bool LotTracking { get; set; }
        public bool SerialTracking { get; set; }
        public bool ExpiryTracking { get; set; }
        public bool IsService { get; set; }

        // Media & Meta
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Tags { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFavorite { get; set; }
        public bool IsFeatured { get; set; }

        // Suppliers
        public string Supplier { get; set; }
        public string SupplierCode { get; set; }
        public int? DefaultSupplierId { get; set; }
        public int? SupplierId { get; set; }

        // Audit
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}