namespace DormitoryUser.Models
{
    public class VehicleInfo
    {
        public string VehicleType { get; set; }      // Loại xe (ví dụ: Ô tô, Xe máy)
        public string LicensePlate { get; set; }     // Biển số xe
        public string VehicleImageUrl { get; set; }  // URL hình ảnh của xe
        public List<ParkingHistory> ParkingHistories { get; set; }
    }
}

