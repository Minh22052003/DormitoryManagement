using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Student()
        {
            List<Student> students = new List<Student>();

            // Thêm sinh viên vào danh sách
            students.Add(new Student
            {
                StudentID = "S001",
                ClassID = "C01",
                ClassName = "Computer Science",
                CourseID = "CS101",
                CourseName = "Introduction to Programming",
                FacultyID = "F01",
                FacultyName = "Information Technology",
                RoomID = "R01",
                RoomName = "Lab A",
                BuildingID = "B01",
                BuildingName = "IT Building",
                FullName = "Nguyen Ann An",
                BirthDate = new DateTime(2002, 5, 20),
                Gender = true,
                PhoneNumber = "0123456789",
                Email = "nguyenan@example.com",
                ProvinceID = 1,
                ProvinceName = "Hanoi",
                District = "Hoan Kiem",
                Ward = "Trang Tien",
                Street = "Le Thanh Tong",
                IDCard = "012345678",
                IsLeader = false,
                Ethnicity = "Kinh",
                Religion = "None",
                Nationality = "Vietnamese",
                DateOfIssueOfIDCard = new DateTime(2020, 6, 10),
                PlaceOfIssueOfIDCard = "Hanoi",
                PolicyCoverage = "None",
                InsuranceNumber = "123456789",
                NgayCapBHXH = new DateTime(2021, 3, 5),
                GiaTriSuDungTuNgay = new DateTime(2021, 3, 6),
                ThoiDiem5NamLienTuc = new DateTime(2026, 3, 6),
                IDTinhCapBHXH = 1,
                TenTinhCapBHXH = "Hanoi",
                KhamBenhBanDau = "Hanoi Hospital",
                AnhThe4x6 = "photo1.png",
                AnhCMNDMatTruoc = "cmnd_front_1.png",
                AnhCMNDMatSau = "cmnd_back_1.png",
                AnhBHYTMatTruoc = "bhyt_front_1.png",
                RelativeID = 1,
                RelativeName = "Nguyen Van B",
                RelativePhoneNumber = "0987654321",
                RelativeAddress = "Hanoi, Vietnam"
            });

            students.Add(new Student
            {
                StudentID = "S002",
                ClassID = "C02",
                ClassName = "Mathematics",
                CourseID = "MATH201",
                CourseName = "Linear Algebra",
                FacultyID = "F02",
                FacultyName = "Mathematics",
                RoomID = "R02",
                RoomName = "Room 202",
                BuildingID = "B02",
                BuildingName = "Science Building",
                FullName = "Tran Binh",
                BirthDate = new DateTime(2001, 8, 15),
                Gender = true,
                PhoneNumber = "0987123456",
                Email = "tranbinh@example.com",
                ProvinceID = 2,
                ProvinceName = "Ho Chi Minh City",
                District = "District 1",
                Ward = "Ben Nghe",
                Street = "Nguyen Hue",
                IDCard = "987654321",
                IsLeader = true,
                Ethnicity = "Kinh",
                Religion = "None",
                Nationality = "Vietnamese",
                DateOfIssueOfIDCard = new DateTime(2019, 9, 1),
                PlaceOfIssueOfIDCard = "Ho Chi Minh City",
                PolicyCoverage = "Scholarship",
                InsuranceNumber = "987654321",
                NgayCapBHXH = new DateTime(2020, 10, 5),
                GiaTriSuDungTuNgay = new DateTime(2020, 10, 6),
                ThoiDiem5NamLienTuc = new DateTime(2025, 10, 6),
                IDTinhCapBHXH = 2,
                TenTinhCapBHXH = "Ho Chi Minh City",
                KhamBenhBanDau = "HCM Hospital",
                AnhThe4x6 = "photo2.png",
                AnhCMNDMatTruoc = "cmnd_front_2.png",
                AnhCMNDMatSau = "cmnd_back_2.png",
                AnhBHYTMatTruoc = "bhyt_front_2.png",
                RelativeID = 2,
                RelativeName = "Tran Thi C",
                RelativePhoneNumber = "0934567890",
                RelativeAddress = "Ho Chi Minh City, Vietnam"
            });

            students.Add(new Student
            {
                StudentID = "S003",
                ClassID = "C03",
                ClassName = "Physics",
                CourseID = "PHY101",
                CourseName = "Mechanics",
                FacultyID = "F03",
                FacultyName = "Physics",
                RoomID = "R03",
                RoomName = "Physics Lab",
                BuildingID = "B03",
                BuildingName = "Science Complex",
                FullName = "Le Loi",
                BirthDate = new DateTime(2003, 11, 30),
                Gender = false,
                PhoneNumber = "0909123456",
                Email = "ledung@example.com",
                ProvinceID = 3,
                ProvinceName = "Da Nang",
                District = "Hai Chau",
                Ward = "Thuan Phuoc",
                Street = "Bach Dang",
                IDCard = "456789123",
                IsLeader = false,
                Ethnicity = "Kinh",
                Religion = "Buddhism",
                Nationality = "Vietnamese",
                DateOfIssueOfIDCard = new DateTime(2021, 7, 12),
                PlaceOfIssueOfIDCard = "Da Nang",
                PolicyCoverage = "Underprivileged",
                InsuranceNumber = "456789123",
                NgayCapBHXH = new DateTime(2021, 9, 20),
                GiaTriSuDungTuNgay = new DateTime(2021, 9, 21),
                ThoiDiem5NamLienTuc = new DateTime(2026, 9, 21),
                IDTinhCapBHXH = 3,
                TenTinhCapBHXH = "Da Nang",
                KhamBenhBanDau = "Da Nang Hospital",
                AnhThe4x6 = "photo3.png",
                AnhCMNDMatTruoc = "cmnd_front_3.png",
                AnhCMNDMatSau = "cmnd_back_3.png",
                AnhBHYTMatTruoc = "bhyt_front_3.png",
                RelativeID = 3,
                RelativeName = "Le Thi D",
                RelativePhoneNumber = "0912345678",
                RelativeAddress = "Da Nang, Vietnam"
            });
            return View(students);
        }
        [HttpGet("{id}")]
        public IActionResult StudentDetail(string msv)
        {
            Student student = new Student
            {
                StudentID = "S001",
                ClassID = "C01",
                ClassName = "Computer Science",
                CourseID = "CS101",
                CourseName = "Introduction to Programming",
                FacultyID = "F01",
                FacultyName = "Information Technology",
                RoomID = "R01",
                RoomName = "Lab A",
                BuildingID = "B01",
                BuildingName = "IT Building",
                FullName = "Nguyen Binh An",
                BirthDate = new DateTime(2002, 5, 20),
                Gender = false,
                PhoneNumber = "0123456789",
                Email = "nguyenan@example.com",
                ProvinceID = 1,
                ProvinceName = "Hanoi",
                District = "Hoan Kiem",
                Ward = "Trang Tien",
                Street = "Le Thanh Tong",
                IDCard = "012345678",
                IsLeader = false,
                Ethnicity = "Kinh",
                Religion = "None",
                Nationality = "Vietnamese",
                DateOfIssueOfIDCard = new DateTime(2020, 6, 10),
                PlaceOfIssueOfIDCard = "Hanoi",
                PolicyCoverage = "None",
                InsuranceNumber = "123456789",
                NgayCapBHXH = new DateTime(2021, 3, 5),
                GiaTriSuDungTuNgay = new DateTime(2021, 3, 6),
                ThoiDiem5NamLienTuc = new DateTime(2026, 3, 6),
                IDTinhCapBHXH = 1,
                TenTinhCapBHXH = "Hanoi",
                KhamBenhBanDau = "Hanoi Hospital",
                AnhThe4x6 = "photo1.png",
                AnhCMNDMatTruoc = "cmnd_front_1.png",
                AnhCMNDMatSau = "cmnd_back_1.png",
                AnhBHYTMatTruoc = "bhyt_front_1.png",
                RelativeID = 1,
                RelativeName = "Nguyen Van B",
                RelativePhoneNumber = "0987654321",
                RelativeAddress = "Hanoi, Vietnam"
            };
            return View(student);
        }
        [HttpGet]
        public IActionResult Staff()
        {
            var staffList = new List<Staff>
{
    new Staff
    {
        StaffID = "ST001",
        FullName = "Nguyen Van A",
        BirthDate = new DateTime(1985, 4, 10),
        Gender = true,
        PhoneNumber = "0912345678",
        Email = "nguyenvana@example.com",
        Hometown = "Hanoi",
        IDCard = "012345678",
        InsuranceNumber = "INS001234",
        Ethnicity = "Kinh",
        Religion = "None",
        Nationality = "Vietnamese",
        Office = "Human Resources",
        WorkSchedule = "Full-time",
        RoleName = "Quan ly"
    },
    new Staff
    {
        StaffID = "ST002",
        FullName = "Tran Thi B",
        BirthDate = new DateTime(1990, 7, 15),
        Gender = false,
        PhoneNumber = "0987654321",
        Email = "tranthib@example.com",
        Hometown = "Ho Chi Minh City",
        IDCard = "987654321",
        InsuranceNumber = "INS987654",
        Ethnicity = "Kinh",
        Religion = "Buddhism",
        Nationality = "Vietnamese",
        Office = "Finance Department",
        WorkSchedule = "Part-time",
        RoleName = "Quet nha"
    },
    new Staff
    {
        StaffID = "ST003",
        FullName = "Le Quoc C",
        BirthDate = new DateTime(1982, 12, 20),
        Gender = true,
        PhoneNumber = "0934567890",
        Email = "lequocc@example.com",
        Hometown = "Da Nang",
        IDCard = "456789123",
        InsuranceNumber = "INS456789",
        Ethnicity = "Kinh",
        Religion = "None",
        Nationality = "Vietnamese",
        Office = "IT Department",
        WorkSchedule = "Full-time",
        RoleName = "Bao ve"
    }
};

            return View(staffList);
        }
        public IActionResult StaffDetail()
        {
            Staff staff = new Staff
            {
                StaffID = "ST003",
                FullName = "Le Quoc C",
                BirthDate = new DateTime(1982, 12, 20),
                Gender = true,
                PhoneNumber = "0934567890",
                Email = "lequocc@example.com",
                Hometown = "Da Nang",
                IDCard = "456789123",
                InsuranceNumber = "INS456789",
                Ethnicity = "Kinh",
                Religion = "None",
                Nationality = "Vietnamese",
                Office = "IT Department",
                WorkSchedule = "Full-time",
                RoleName = "Bao ve"
            };
            return View(staff);
        }
        public IActionResult TTCN()
        {
            Staff staff = new Staff
            {
                StaffID = "ST003",
                FullName = "Le Quoc C",
                BirthDate = new DateTime(1982, 12, 20),
                Gender = true,
                PhoneNumber = "0934567890",
                Email = "lequocc@example.com",
                Hometown = "Da Nang",
                IDCard = "456789123",
                InsuranceNumber = "INS456789",
                Ethnicity = "Kinh",
                Religion = "None",
                Nationality = "Vietnamese",
                Office = "IT Department",
                WorkSchedule = "Full-time",
                RoleName = "Bao ve"
            };
            return View(staff);
        }

    }
}
