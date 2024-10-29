namespace DormitoryUser.Models
{
    public class RoomIsActive
    {
        public int RoomId { get; set; }            // ID phòng trọ
        public string RoomName { get; set; }       // Tên phòng trọ
        public int CurrentNumber { get; set; }     // Số người hiện tại
        public int MaxNumber { get; set; }         // Số người tối đa
        public List<RoomMate> Roommates { get; set; } // Danh sách thành viên trong phòng
    }
}
