using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Role
    {
        public Role()
        {
            AccountStaffs = new HashSet<AccountStaff>();
        }

        public string RoleId { get; set; } = null!;
        public string? RoleName { get; set; }

        public virtual ICollection<AccountStaff> AccountStaffs { get; set; }
    }
}
