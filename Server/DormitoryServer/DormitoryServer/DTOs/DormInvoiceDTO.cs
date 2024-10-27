namespace DormitoryServer.DTOs
{
    public class DormInvoiceDTO
    {
        public int? InvoiceID { get; set; }
        public string? StaffID_Create { get; set; }
        public string? StaffName_Create { get; set; }
        public string? StaffID_Pay { get; set; }
        public string? StaffName_Pay { get; set; }
        public string? InvoiceTypeName { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? PayDate { get; set; }
        public string? Status { get; set; }
    }
}
