using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class SupportRequestType
    {
        public SupportRequestType()
        {
            SupportRequests = new HashSet<SupportRequest>();
        }

        public int RequestTypeId { get; set; }
        public string? RequestTypeName { get; set; }

        public virtual ICollection<SupportRequest> SupportRequests { get; set; }
    }
}
