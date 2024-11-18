namespace Manager.Models
{
    public class Announcement
    {
        public int? AnnouncementID { get; set; }
        public string? StaffID { get; set; }
        public string? StaffName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Target { get; set; }
        public FormFile? Image { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Status { get; set; }
    }
}
