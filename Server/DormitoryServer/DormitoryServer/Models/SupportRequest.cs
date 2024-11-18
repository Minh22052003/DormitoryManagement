using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class SupportRequest
    {
        public int RequestId { get; set; }
        public string? StudentId { get; set; }
        public string? StaffId { get; set; }
        public int? RequestTypeId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime? RequestSentDate { get; set; }
        public DateTime? RequestProcessDate { get; set; }
        public string? Reply { get; set; }
        public string? Status { get; set; }

        public virtual SupportRequestType? RequestType { get; set; }
        public virtual staff? Staff { get; set; }
        public virtual Student? Student { get; set; }
    }
}
