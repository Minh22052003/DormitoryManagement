using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            RoomEquipments = new HashSet<RoomEquipment>();
        }

        public int EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<RoomEquipment> RoomEquipments { get; set; }
    }
}
