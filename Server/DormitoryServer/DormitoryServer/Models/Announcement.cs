using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Announcement
    {
        public int AnnouncementId { get; set; }
        public string? StaffId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Target { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Status { get; set; }

        public virtual staff? Staff { get; set; }
    }
}
