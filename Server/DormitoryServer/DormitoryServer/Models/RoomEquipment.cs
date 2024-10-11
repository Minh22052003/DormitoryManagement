using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class RoomEquipment
    {
        public int RoomEquipmentId { get; set; }
        public string? RoomId { get; set; }
        public int? EquipmentId { get; set; }
        public int? Quantity { get; set; }
        public string? Condition { get; set; }

        public virtual Equipment? Equipment { get; set; }
        public virtual Room? Room { get; set; }
    }
}
