namespace DormitoryServer.DTOs
{
    public class RequestDTO
    {
        public int RequestID { get; set; }
        public string? StudentID { get; set; }
        public string? StudentName { get; set; }
        public string? RoomID { get; set; }
        public string? RoomName { get; set; }
        public string? BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public string? StaffID { get; set; }
        public string? StaffName { get; set; }
        public int? RequestTypeID { get; set; }
        public string? RequestTypeName { get; set; }
        public string? Description { get; set; }
        public DateTime? RequestSentDate { get; set; }
        public DateTime? RequestProcessDate { get; set; }
        public string? Image { get; set; }
        public string? Reply { get; set; }
        public string? Status { get; set; }
    }
}
