using System;
using System.Collections.Generic;

namespace KryptalisERP.Models
{
    public class SupplierContact
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }

    public class SupplierBankAccount
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int? BankId { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
        public string Currency { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }

    public class SupplierPaymentTerm
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int? PaymentTermId { get; set; }
        public bool IsDefault { get; set; }
        public string Notes { get; set; }
    }

    public class SupplierItem
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int? ItemId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string Currency { get; set; }
        public decimal? MinOrderQty { get; set; }
        public int? LeadTime { get; set; }
        public bool IsDefaultSupplier { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public decimal? LastPurchasePrice { get; set; }
        public decimal? TotalPurchases { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }

    public class SupplierPriceList
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsActive { get; set; }
    }
}