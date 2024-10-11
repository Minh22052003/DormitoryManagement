using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class DormInvoice
    {
        public int InvoiceId { get; set; }
        public string? StaffId { get; set; }
        public string? InvoiceType { get; set; }
        public string? Description { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? Status { get; set; }

        public virtual staff? Staff { get; set; }
    }
}
