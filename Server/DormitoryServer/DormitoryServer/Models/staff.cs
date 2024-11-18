using System;
using System.Collections.Generic;

namespace DormitoryServer.Models
{
    public partial class staff
    {
        public staff()
        {
            AccountStaffs = new HashSet<AccountStaff>();
            Announcements = new HashSet<Announcement>();
            DormInvoiceStaffIdCreateNavigations = new HashSet<DormInvoice>();
            DormInvoiceStaffIdPayNavigations = new HashSet<DormInvoice>();
            Invoices = new HashSet<Invoice>();
            News = new HashSet<News>();
            SupportRequests = new HashSet<SupportRequest>();
            UtilityMeters = new HashSet<UtilityMeter>();
        }

        public string StaffId { get; set; } = null!;
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Hometown { get; set; }
        public string? Idcard { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? Ethnicity { get; set; }
        public string? Religion { get; set; }
        public string? Nationality { get; set; }
        public string? Office { get; set; }
        public string? WorkSchedule { get; set; }

        public virtual ICollection<AccountStaff> AccountStaffs { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<DormInvoice> DormInvoiceStaffIdCreateNavigations { get; set; }
        public virtual ICollection<DormInvoice> DormInvoiceStaffIdPayNavigations { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<SupportRequest> SupportRequests { get; set; }
        public virtual ICollection<UtilityMeter> UtilityMeters { get; set; }
    }
}
