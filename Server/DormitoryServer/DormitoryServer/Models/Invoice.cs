using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int InvoiceId { get; set; }
        public string? StaffId { get; set; }
        public string? RoomId { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? Description { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }

        public virtual Room? Room { get; set; }
        public virtual staff? Staff { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
