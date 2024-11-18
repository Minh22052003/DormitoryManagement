using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class ParkingTicket
    {
        public ParkingTicket()
        {
            ParkingHistories = new HashSet<ParkingHistory>();
        }

        public int TicketId { get; set; }
        public string? StudentId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? LicensePlate { get; set; }

        public virtual Student? Student { get; set; }
        public virtual ICollection<ParkingHistory> ParkingHistories { get; set; }
    }
}
