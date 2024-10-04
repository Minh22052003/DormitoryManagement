using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Service
    {
        public Service()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public string ServiceId { get; set; } = null!;
        public string? ServiceName { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
