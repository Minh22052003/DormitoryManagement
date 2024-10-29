using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        public string ClassId { get; set; } = null!;
        public string? FacultyId { get; set; }
        public string? CourseId { get; set; }
        public string? ClassName { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
