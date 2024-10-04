using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Building
    {
        public Building()
        {
            Rooms = new HashSet<Room>();
        }

        public string BuildingId { get; set; } = null!;
        public string? BuildingName { get; set; }
        public int? RoomCount { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
