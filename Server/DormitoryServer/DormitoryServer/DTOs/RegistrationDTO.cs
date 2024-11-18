namespace DormitoryServer.DTOs
{
    public class RegistrationDTO
    {
        public int? RegistrationID { get; set; }
        public int? RoomTypeID { get; set; }
        public string? RoomTypeName { get; set; }
        public string? StudentID { get; set; }
        public string? StudentName { get; set; }
        public string? Semester { get; set; }
        public string? AcademicYear { get; set; }
        public string? ApplicationStatus { get; set; }
        public string? ClassID { get; set; }
        public string? ClassName { get; set; }
        public string? CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? FacultyID { get; set; }
        public string? FacultyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? ProvinceID { get; set; }
        public string? ProvinceName { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Street { get; set; }
        public string? IDCard { get; set; }
        public string? Ethnicity { get; set; }
        public string? Religion { get; set; }
        public string? Nationality { get; set; }
        public DateTime? DateOfIssueOfIDCard { get; set; }
        public string? PlaceOfIssueOfIDCard { get; set; }
        //Diện chính sách
        public string? PolicyCoverage { get; set; }
        public string? InsuranceNumber { get; set; }
        public DateTime? NgayCapBHXH { get; set; }
        public DateTime? GiaTriSuDungTuNgay { get; set; }
        public DateTime? ThoiDiem5NamLienTuc { get; set; }
        public int? IDTinhCapBHXH { get; set; }
        public string? TenTinhCapBHXH { get; set; }
        public string? KhamBenhBanDau { get; set; }
        public string? AnhThe4x6 { get; set; }
        public string? AnhCMNDMatTruoc { get; set; }
        public string? AnhCMNDMatSau { get; set; }
        public string? AnhBHYTMatTruoc { get; set; }
        public int? RelativeID { get; set; }
        public string? RelativeName { get; set; }
        public string? RelativePhoneNumber { get; set; }
        public string? RelativeAddress { get; set; }
    }
}
