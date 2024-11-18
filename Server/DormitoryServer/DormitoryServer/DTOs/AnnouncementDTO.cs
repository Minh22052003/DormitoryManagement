namespace DormitoryServer.DTOs
{
    public class AnnouncementDTO
    {
        public int? AnnouncementID { get; set; }
        public string? StaffID { get; set; }
        public string? StaffName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Target { get; set; }
        public string? Image { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Status { get; set; }
    }
}
