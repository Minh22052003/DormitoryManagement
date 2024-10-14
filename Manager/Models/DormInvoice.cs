namespace Manager.Models
{
    public class DormInvoice
    {
        public int? InvoiceID { get; set; } 
        public string? StaffID_Create { get; set; }
        public string? StaffName_Create { get; set; }
        public string? StaffID_Pay { get; set; }
        public string? StaffName_Pay { get; set; }
        public string? InvoiceType { get; set; }
        public string? InvoiceTypeName { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? Status { get; set; }
        public int? ServiceID { get; set; }
        public string? ServiceName { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; }
    }
}
