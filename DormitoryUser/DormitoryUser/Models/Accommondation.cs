namespace DormitoryUser.Models
{
    public class Accommodation
    {
        // Thông tin phòng ở
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        // Thông tin khuôn viên ký túc xá
        public string CampusDescription { get; set; } // Mô tả khuôn viên
        public List<string> CampusImageUrls { get; set; } // Danh sách ảnh của khuôn viên

        public Accommodation()
        {
            CampusImageUrls = new List<string>(); // Khởi tạo danh sách ảnh
        }
    }
}
