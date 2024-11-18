namespace Manager.Models
{
    public class RoomInvoice
    {
        public int InvoiceID { get; set; } 
        public string? StaffID { get; set; }
        public string? RoomID { get; set; }
        public string? RoomName { get; set; }
        public string? BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public string? PayerID { get; set; }
        public string? PayerName { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? Description { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<Service>? Services { get; set; }
    }
}
