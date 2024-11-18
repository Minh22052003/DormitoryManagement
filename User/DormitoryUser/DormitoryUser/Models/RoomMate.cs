namespace DormitoryUser.Models
{
    public class RoomMate
    {
        public string Id { get; set; }                // ID thành viên
        public string? FullName { get; set; }       // Họ và tên
        public DateTime BirthDate { get; set; }    // Ngày sinh
        public bool? Gender { get; set; }         // Giới tính
        public string? PhoneNumber { get; set; }    // Số điện thoại
        public string? Hometown { get; set; }       // Quê quán
    }
}
