using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Department
    {
        public Department()
        {
            Classes = new HashSet<Class>();
        }

        public string DepartmentId { get; set; } = null!;
        public string? DepartmentName { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
