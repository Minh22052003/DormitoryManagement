namespace DormitoryUser.Models
{
    public class RoomMate
    {
        public int Id { get; set; }                // ID thành viên
        public string FullName { get; set; }       // Họ và tên
        public DateTime BirthDate { get; set; }    // Ngày sinh
        public string Gender { get; set; }         // Giới tính
        public string PhoneNumber { get; set; }    // Số điện thoại
        public string Hometown { get; set; }       // Quê quán
    }
}
