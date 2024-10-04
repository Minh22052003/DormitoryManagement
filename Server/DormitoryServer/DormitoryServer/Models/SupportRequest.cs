using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class SupportRequest
    {
        public string RequestId { get; set; } = null!;
        public string? StudentId { get; set; }
        public string? StaffId { get; set; }
        public string? RequestType { get; set; }
        public string? Content { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Status { get; set; }

        public virtual staff? Staff { get; set; }
        public virtual Student? Student { get; set; }
    }
}
