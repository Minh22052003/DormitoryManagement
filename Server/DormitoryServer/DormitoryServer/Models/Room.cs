using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Room
    {
        public Room()
        {
            Invoices = new HashSet<Invoice>();
            RoomEquipments = new HashSet<RoomEquipment>();
            Students = new HashSet<Student>();
            UtilityMeters = new HashSet<UtilityMeter>();
        }

        public string RoomId { get; set; } = null!;
        public int? RoomTypeId { get; set; }
        public string? BuildingId { get; set; }
        public string? RoomName { get; set; }
        public int? NumberOfStudent { get; set; }
        public int? RoomStatusId { get; set; }
        public string? RoomNote { get; set; }

        public virtual Building? Building { get; set; }
        public virtual RoomStatus? RoomStatus { get; set; }
        public virtual RoomType? RoomType { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<RoomEquipment> RoomEquipments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<UtilityMeter> UtilityMeters { get; set; }
    }
}
