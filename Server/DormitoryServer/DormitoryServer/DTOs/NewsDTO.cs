namespace DormitoryServer.DTOs
{
    public class NewsDTO
    {
        public int? NewsID { get; set; }
        public string? StaffID { get; set; }
        public string? StaffName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Tag { get; set; }
        public string? Status { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
