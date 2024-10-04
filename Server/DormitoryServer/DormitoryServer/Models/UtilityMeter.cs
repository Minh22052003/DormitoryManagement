using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class UtilityMeter
    {
        public int UtilityMeterId { get; set; }
        public string? RoomId { get; set; }
        public string? StaffId { get; set; }
        public int? Electricity { get; set; }
        public int? Water { get; set; }
        public DateTime? RecordingDate { get; set; }

        public virtual Room? Room { get; set; }
        public virtual staff? Staff { get; set; }
    }
}
