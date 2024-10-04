using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class AccountStudent
    {
        public int AccountStudent1 { get; set; }
        public string? StudentId { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }

        public virtual Student? Student { get; set; }
    }
}
