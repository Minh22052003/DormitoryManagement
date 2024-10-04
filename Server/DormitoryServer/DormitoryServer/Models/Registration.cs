using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Registration
    {
        public int RegistrationId { get; set; }
        public string? RoomId { get; set; }
        public string? StudentId { get; set; }
        public string? Semester { get; set; }
        public int? AcademicYear { get; set; }
        public string? ApplicationStatus { get; set; }

        public virtual Room? Room { get; set; }
        public virtual Student? Student { get; set; }
    }
}
