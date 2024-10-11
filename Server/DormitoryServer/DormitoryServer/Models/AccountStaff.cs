using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class AccountStaff
    {
        public int AccountStaff1 { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? StaffId { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }

        public virtual Role? Role { get; set; }
        public virtual staff? Staff { get; set; }
    }
}
