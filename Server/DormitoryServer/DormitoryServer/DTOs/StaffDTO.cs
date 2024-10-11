namespace DormitoryServer.DTOs
{
    public class StaffDTO
    {
        public string StaffId { get; set; } = null!;
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Hometown { get; set; }
        public string? Idcard { get; set; }
        public string? InsuranceNumber { get; set; }
    }
}
