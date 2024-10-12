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
        public string? DepartmentId { get; set; }
        public string? ClassName { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
