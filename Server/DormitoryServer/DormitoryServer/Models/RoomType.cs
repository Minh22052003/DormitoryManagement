using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public int? Capacity { get; set; }
        public decimal? RoomPrice { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
