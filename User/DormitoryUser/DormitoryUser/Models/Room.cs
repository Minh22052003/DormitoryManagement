namespace DormitoryUser.Models
{
    public class Room
    {
        public string RoomID { get; set; } = null!;
        public int? RoomTypeID { get; set; }
        public string? BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public string? LeaderID { get; set; }
        public string? LeaderName { get; set; }
        public string? RoomName { get; set; }
        public decimal? RoomPrice { get; set; }
        public int? NumberOfStudent { get; set; }
        public int? Capacity { get; set; }
        public int? RoomStatusID { get; set; }
        public string? RoomStatusName { get; set; }
        public string? RoomNote { get; set; }

    }
}
