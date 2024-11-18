using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Classes = new HashSet<Class>();
        }

        public string FacultyId { get; set; } = null!;
        public string? FacultyName { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
