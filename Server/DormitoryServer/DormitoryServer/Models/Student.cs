using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Student
    {
        public Student()
        {
            AccountStudents = new HashSet<AccountStudent>();
            ParkingTickets = new HashSet<ParkingTicket>();
            Registrations = new HashSet<Registration>();
            Relatives = new HashSet<Relative>();
            SupportRequests = new HashSet<SupportRequest>();
        }

        public string StudentId { get; set; } = null!;
        public string? ClassId { get; set; }
        public string? RoomId { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Hometown { get; set; }
        public string? Idcard { get; set; }
        public string? InsuranceNumber { get; set; }
        public bool? IsLeader { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<AccountStudent> AccountStudents { get; set; }
        public virtual ICollection<ParkingTicket> ParkingTickets { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Relative> Relatives { get; set; }
        public virtual ICollection<SupportRequest> SupportRequests { get; set; }
    }
}
