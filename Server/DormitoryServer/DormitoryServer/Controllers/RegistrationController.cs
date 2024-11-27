using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public RegistrationController(DataDormitoryContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("getallregistration")]
        public ActionResult<List<RegistrationDTO>> GetAllRegistration()
        {
            var registrations = _context.Registrations
                .Include(r=>r.RoomType)
                .Include(r=>r.Student)
                    .ThenInclude(s=>s.Class)
                        .ThenInclude(c=>c.Course)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Class)
                        .ThenInclude(c => c.Faculty)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Province)
                .Include(r => r.Student)
                    .ThenInclude(s => s.IdtinhCapBhxhNavigation)
                .Include(r => r.Student)
                    .ThenInclude(s => s.Relatives)
                .ToList();
            List<RegistrationDTO> result = new List<RegistrationDTO>();
            foreach (var registration in registrations)
            {
                result.Add(new RegistrationDTO
                {
                    RegistrationID = registration.RegistrationId,
                    RoomTypeID = registration.RoomTypeId,
                    RoomTypeName = registration.RoomType?.RoomTypeName,
                    StudentID = registration.StudentId,
                    StudentName = registration.Student?.FullName,
                    Semester = registration.Semester,
                    AcademicYear = registration.AcademicYear,
                    ApplicationStatus = registration.ApplicationStatus,
                    ClassID = registration.Student?.ClassId,
                    ClassName = registration.Student?.Class?.ClassName,
                    CourseID = registration.Student?.Class?.CourseId,
                    CourseName = registration.Student?.Class?.Course?.CourseName,
                    FacultyID = registration.Student?.Class?.FacultyId,
                    FacultyName = registration.Student?.Class?.Faculty?.FacultyName,
                    BirthDate = registration.Student?.BirthDate,
                    Gender = registration.Student?.Gender,
                    PhoneNumber = registration.Student?.PhoneNumber,
                    Email = registration.Student?.Email,
                    ProvinceID = registration.Student?.ProvinceId,
                    ProvinceName = registration.Student?.Province?.ProvinceName,
                    District = registration.Student?.District,
                    Ward = registration.Student?.Ward,
                    Street = registration.Student?.Street,
                    IDCard = registration.Student?.Idcard,
                    Ethnicity = registration.Student?.Ethnicity,
                    Religion = registration.Student?.Religion,
                    Nationality = registration.Student?.Nationality,
                    DateOfIssueOfIDCard = registration.Student?.DateOfIssueOfIdcard,
                    PlaceOfIssueOfIDCard = registration.Student?.PlaceOfIssueOfIdcard,
                    PolicyCoverage = registration.Student?.PolicyCoverage,
                    InsuranceNumber = registration.Student?.InsuranceNumber,
                    NgayCapBHXH = registration.Student?.NgayCapBhxh,
                    GiaTriSuDungTuNgay = registration.Student?.GiaTriSuDungTuNgay,
                    ThoiDiem5NamLienTuc = registration.Student?.ThoiDiem5NamLienTuc,
                    IDTinhCapBHXH = registration.Student?.IdtinhCapBhxh,
                    TenTinhCapBHXH = registration.Student?.IdtinhCapBhxhNavigation?.ProvinceName,
                    KhamBenhBanDau = registration.Student?.KhamBenhBanDau,
                    AnhThe4x6 = registration.Student?.AnhThe4x6,
                    AnhCMNDMatTruoc = registration.Student?.AnhCmndmatTruoc,
                    AnhCMNDMatSau = registration.Student?.AnhCmndmatSau,
                    AnhBHYTMatTruoc = registration.Student?.AnhBhytmatTruoc,
                    RelativeID = GetRelative(registration.Student.StudentId).RelativeId,
                    RelativeName = GetRelative(registration.Student.StudentId).FullName,
                    RelativePhoneNumber = GetRelative(registration.Student.StudentId).PhoneNumber,
                    RelativeAddress = GetRelative(registration.Student.StudentId).Address,

                });
            }
            return result;
        }

        private Relative GetRelative(string studentId)
        {
            return _context.Relatives.Where(r => r.StudentId == studentId).FirstOrDefault();
        }



        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("updatestatus")]
        public IActionResult UpdateStatus([FromBody] RegistrationDTO registrationDTO)
        {
            var registration = _context.Registrations.Find(registrationDTO.RegistrationID);
            registration.ApplicationStatus = registrationDTO.ApplicationStatus;
            if(registrationDTO.ApplicationStatus == "Approved")
            {
                Student student = _context.Students.Find(registration.StudentId);
                DateTime birthday;
                string pass;
                if (student.BirthDate.HasValue)
                {
                    birthday = student.BirthDate.Value;
                    pass = birthday.ToString("ddMMyyyy");
                }
                else
                {
                    pass = student.StudentId;
                }
                AccountStudent accountStudent = new AccountStudent
                {
                    StudentId = registration.StudentId,
                    Password = HashPassword(registration.StudentId, pass),
                    Status = true
                };
                _context.AccountStudents.Add(accountStudent);
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("addregistration")]
        public IActionResult AddRegistration([FromBody] RegistrationDTO registrationDTO)
        {
            string semester="1";
            DateTime currentDate = DateTime.Now;

            if ((currentDate.Month == 8 && currentDate.Day >= 15) ||
                (currentDate.Month > 8 && currentDate.Month <= 12) ||
                (currentDate.Month == 1 && currentDate.Day <= 1))
            {
                semester = "1";
            }
            else if ((currentDate.Month == 1 && currentDate.Day >= 2) ||
                     (currentDate.Month >= 2 && currentDate.Month <= 7) ||
                     (currentDate.Month == 8 && currentDate.Day <= 14))
            {
                semester = "2";
            }
            Registration registration = new Registration
            {
                RoomTypeId = registrationDTO.RoomTypeID,
                StudentId = registrationDTO.StudentID,
                Semester = semester,
                AcademicYear = DateTime.Now.Year.ToString(),
                ApplicationStatus = "Pending"
            };
            Student student = new Student
            {
                StudentId = registrationDTO.StudentID,
                FullName = registrationDTO.StudentName,
                BirthDate = registrationDTO.BirthDate,
                Gender = registrationDTO.Gender,
                PhoneNumber = registrationDTO.PhoneNumber,
                Email = registrationDTO.Email,
                ProvinceId = registrationDTO.ProvinceID,
                District = registrationDTO.District,
                Ward = registrationDTO.Ward,
                Street = registrationDTO.Street,
                Idcard = registrationDTO.IDCard,
                Ethnicity = registrationDTO.Ethnicity,
                Religion = registrationDTO.Religion,
                Nationality = registrationDTO.Nationality,
                DateOfIssueOfIdcard = registrationDTO.DateOfIssueOfIDCard,
                PlaceOfIssueOfIdcard = registrationDTO.PlaceOfIssueOfIDCard,
                PolicyCoverage = registrationDTO.PolicyCoverage,
                InsuranceNumber = registrationDTO.InsuranceNumber,
                NgayCapBhxh = registrationDTO.NgayCapBHXH,
                GiaTriSuDungTuNgay = registrationDTO.GiaTriSuDungTuNgay,
                ThoiDiem5NamLienTuc = registrationDTO.ThoiDiem5NamLienTuc,
                IdtinhCapBhxh = registrationDTO.IDTinhCapBHXH,
                KhamBenhBanDau = registrationDTO.KhamBenhBanDau,
                AnhThe4x6 = registrationDTO.AnhThe4x6,
                AnhCmndmatTruoc = registrationDTO.AnhCMNDMatTruoc,
                AnhCmndmatSau = registrationDTO.AnhCMNDMatSau,
                AnhBhytmatTruoc = registrationDTO.AnhBHYTMatTruoc
            };
            Relative relative = new Relative
            {
                StudentId = registrationDTO.StudentID,
                FullName = registrationDTO.RelativeName,
                PhoneNumber = registrationDTO.RelativePhoneNumber,
                Address = registrationDTO.RelativeAddress
            };



            _context.Students.Add(student);
            _context.Relatives.Add(relative);
            _context.Registrations.Add(registration);
            _context.SaveChanges();
            return Ok();
        }


        private static string HashPassword(string username, string password)
        {
            string input = username + password;

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
