namespace DormitoryServer.DTOs
{
    public class StaffDTO
    {
        public string? StaffID { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Hometown { get; set; }
        public string? IDCard { get; set; }
        //Số bảo hiểm y tế
        public string? InsuranceNumber { get; set; }
        //Dân tộc
        public string? Ethnicity { get; set; }
        //Tôn giáo
        public string? Religion { get; set; }
        //Quốc tịch
        public string? Nationality { get; set; }
        //Văn phòng làm việc
        public string? Office { get; set; }
        //Lịch làm việc
        public string? WorkSchedule { get; set; }
        public int? RoleID { get; set; }
        public string? RoleName { get; set; }
    }
}
