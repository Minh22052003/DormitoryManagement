using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string? StaffId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Tag { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Status { get; set; }

        public virtual staff? Staff { get; set; }
    }
}
