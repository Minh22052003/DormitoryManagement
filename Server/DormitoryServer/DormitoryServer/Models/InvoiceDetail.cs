using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int? InvoiceId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual Service? Service { get; set; }
    }
}
