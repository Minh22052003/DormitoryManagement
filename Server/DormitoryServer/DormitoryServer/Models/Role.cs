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

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<AccountStaff> AccountStaffs { get; set; }
    }
}
