using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Registration
    {
        public int RegistrationId { get; set; }
        public int? RoomTypeId { get; set; }
        public string? StudentId { get; set; }
        public string? Semester { get; set; }
        public string? AcademicYear { get; set; }
        public string? ApplicationStatus { get; set; }

        public virtual RoomType? RoomType { get; set; }
        public virtual Student? Student { get; set; }
    }
}
