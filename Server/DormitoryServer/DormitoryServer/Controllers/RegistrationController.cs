using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
                    Relatives = registration.Student?.Relatives?
                                .Select(r => new RelativeDTO
                                {
                                    RelativeId = r.RelativeId,
                                    RelativeName = r.FullName,
                                    RalativePhoneNumber = r.PhoneNumber,
                                    RalativeAddress = r.Address
                                }).ToList() ?? new List<RelativeDTO>()

                });
            }
            return result;
        }
    }
}
