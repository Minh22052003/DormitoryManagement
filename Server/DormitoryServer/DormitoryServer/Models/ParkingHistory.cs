using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class ParkingHistory
    {
        public int ParkingHistoryId { get; set; }
        public int? TicketId { get; set; }
        public string? EntryExit { get; set; }
        public DateTime? Time { get; set; }
        public string? Image { get; set; }

        public virtual ParkingTicket? Ticket { get; set; }
    }
}
