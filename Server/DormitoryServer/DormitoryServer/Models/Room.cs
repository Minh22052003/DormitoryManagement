using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Room
    {
        public Room()
        {
            Invoices = new HashSet<Invoice>();
            Registrations = new HashSet<Registration>();
            RoomEquipments = new HashSet<RoomEquipment>();
            Students = new HashSet<Student>();
            UtilityMeters = new HashSet<UtilityMeter>();
        }

        public string RoomId { get; set; } = null!;
        public int? RoomTypeId { get; set; }
        public string? BuildingId { get; set; }

        public virtual Building? Building { get; set; }
        public virtual RoomType? RoomType { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<RoomEquipment> RoomEquipments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<UtilityMeter> UtilityMeters { get; set; }
    }
}
