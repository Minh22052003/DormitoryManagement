using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Course
    {
        public Course()
        {
            Classes = new HashSet<Class>();
        }

        public string CourseId { get; set; } = null!;
        public string? CourseName { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
