namespace KryptalisERP.Models
{
    public class PosTransactionDto
    {
        public long Id { get; set; }
        public int? SaleDocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public int TerminalId { get; set; }
        public string TerminalName { get; set; } // Joined field
        public decimal Amount { get; set; }
        public string TxStatus { get; set; }
        public string Rrn { get; set; }
        public string AuthCode { get; set; }
        public string CardBrand { get; set; }
        public string MaskedPan { get; set; }
        public DateTime RequestTime { get; set; }
        public string ResponseMessage { get; set; }
    }
}