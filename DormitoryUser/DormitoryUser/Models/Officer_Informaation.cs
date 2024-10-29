namespace DormitoryUser.Models
{
    public class Officer_Information
    {
        public int Id { get; set; } // Có thể dùng để xác định cán bộ
        public string Name { get; set; } // Tên cán bộ
        public string Position { get; set; } // Chức vụ
        public string Room { get; set; } // Phòng
        public string Email { get; set; } // Địa chỉ email
        public string Phone { get; set; } // Số điện thoại
        public string AvatarUrl { get; set; } // Đường dẫn tới ảnh đại diện
    }
}

