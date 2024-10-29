using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class DormInvoice
    {
        public int InvoiceId { get; set; }
        public string? StaffIdCreate { get; set; }
        public string? StaffIdPay { get; set; }
        public string? InvoiceType { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? PayDate { get; set; }
        public string? Status { get; set; }

        public virtual staff? StaffIdCreateNavigation { get; set; }
        public virtual staff? StaffIdPayNavigation { get; set; }
    }
}
