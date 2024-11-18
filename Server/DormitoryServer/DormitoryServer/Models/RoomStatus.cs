using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class RoomStatus
    {
        public RoomStatus()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomStatusId { get; set; }
        public string? RoomStatusName { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
