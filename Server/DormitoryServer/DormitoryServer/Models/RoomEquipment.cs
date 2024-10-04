using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class RoomEquipment
    {
        public string RoomId { get; set; } = null!;
        public int EquipmentId { get; set; }
        public int? Quantity { get; set; }
        public string? Condition { get; set; }

        public virtual Equipment Equipment { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
