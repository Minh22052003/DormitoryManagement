﻿using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class Relative
    {
        public int RelativeId { get; set; }
        public string? StudentId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual Student? Student { get; set; }
    }
}
