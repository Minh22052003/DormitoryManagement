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
        public int? ProvinceId { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Street { get; set; }
        public string? Idcard { get; set; }
        public bool? IsLeader { get; set; }
        public string? Ethnicity { get; set; }
        public string? Religion { get; set; }
        public string? Nationality { get; set; }
        public DateTime? DateOfIssueOfIdcard { get; set; }
        public string? PlaceOfIssueOfIdcard { get; set; }
        public string? PolicyCoverage { get; set; }
        public string? InsuranceNumber { get; set; }
        public DateTime? NgayCapBhxh { get; set; }
        public DateTime? GiaTriSuDungTuNgay { get; set; }
        public DateTime? ThoiDiem5NamLienTuc { get; set; }
        public int? IdtinhCapBhxh { get; set; }
        public string? KhamBenhBanDau { get; set; }
        public string? AnhThe4x6 { get; set; }
        public string? AnhCmndmatTruoc { get; set; }
        public string? AnhCmndmatSau { get; set; }
        public string? AnhBhytmatTruoc { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Province? IdtinhCapBhxhNavigation { get; set; }
        public virtual Province? Province { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<AccountStudent> AccountStudents { get; set; }
        public virtual ICollection<ParkingTicket> ParkingTickets { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Relative> Relatives { get; set; }
        public virtual ICollection<SupportRequest> SupportRequests { get; set; }
    }
}
