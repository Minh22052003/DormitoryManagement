using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Province
    {
        public Province()
        {
            StudentIdtinhCapBhxhNavigations = new HashSet<Student>();
            StudentProvinces = new HashSet<Student>();
        }

        public int ProvinceId { get; set; }
        public string? ProvinceName { get; set; }

        public virtual ICollection<Student> StudentIdtinhCapBhxhNavigations { get; set; }
        public virtual ICollection<Student> StudentProvinces { get; set; }
    }
}
