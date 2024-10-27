using Microsoft.AspNetCore.Http;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryServer.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public StudentController(DataDormitoryContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet("getallstudent")]
        public ActionResult<List<StudentDTO>> GetStudents()
        {
            var students = _context.Students
                .Include(s => s.Class)
                    .ThenInclude(c => c.Faculty)
                .Include(s => s.Class)
                    .ThenInclude(c => c.Course)
                .Include(s => s.Room)
                    .ThenInclude(r => r.Building)
                .Include(s => s.Province)
                .ToList();

            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach (var student in students)
            {
                studentDTOs.Add(new StudentDTO
                {
                    StudentID = student.StudentId,
                    ClassID = student.ClassId,
                    ClassName = student.Class?.ClassName,
                    CourseID = student.Class?.CourseId,
                    CourseName = student.Class?.Course?.CourseName,
                    FacultyID = student.Class?.FacultyId,
                    FacultyName = student.Class?.Faculty?.FacultyName,
                    RoomID = student.RoomId,
                    RoomName = student.Room?.RoomName,
                    BuildingID = student.Room?.BuildingId,
                    BuildingName = student.Room?.Building?.BuildingName,
                    FullName = student?.FullName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    ProvinceID = student.ProvinceId,
                    ProvinceName = student.Province?.ProvinceName,
                    District = student.District,
                    Ward = student.Ward,
                    Street = student.Street,
                    IDCard = student.Idcard,
                    IsLeader = student.IsLeader,
                    Ethnicity = student.Ethnicity,
                    Religion = student.Religion,
                    Nationality = student.Nationality,
                    DateOfIssueOfIDCard = student.DateOfIssueOfIdcard,
                    PlaceOfIssueOfIDCard = student.PlaceOfIssueOfIdcard,
                    PolicyCoverage = student.PolicyCoverage,
                    InsuranceNumber = student.InsuranceNumber,
                    NgayCapBHXH = student.NgayCapBhxh,
                    GiaTriSuDungTuNgay = student.GiaTriSuDungTuNgay,
                    ThoiDiem5NamLienTuc = student.ThoiDiem5NamLienTuc,
                    IDTinhCapBHXH = student.IdtinhCapBhxh,
                    TenTinhCapBHXH = "Unknown",
                    KhamBenhBanDau = student.KhamBenhBanDau,
                    AnhThe4x6 = student.AnhThe4x6,
                    AnhCMNDMatTruoc = student.AnhCmndmatTruoc,
                    AnhCMNDMatSau = student.AnhCmndmatSau,
                    AnhBHYTMatTruoc = student.AnhBhytmatTruoc,
                    RelativeID = GetRelative(student.StudentId).RelativeId,
                    RelativeName = GetRelative(student.StudentId).FullName,
                    RelativePhoneNumber = GetRelative(student.StudentId).PhoneNumber,
                    RelativeAddress = GetRelative(student.StudentId).Address,
                });
            }

            return studentDTOs;
        }

        private Relative GetRelative(string studentId)
        {
            return _context.Relatives.Where(r => r.StudentId == studentId).FirstOrDefault();
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _context.Students
                .Include(s => s.Class)
                    .ThenInclude(c => c.Faculty)
                .Include(s => s.Class)
                    .ThenInclude(c => c.Course)
                .Include(s => s.Room)
                    .ThenInclude(r => r.Building)
                .Include(s => s.Province)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpGet("getallstudentbyroom")]
        public ActionResult<List<StudentDTO>> GetStudentbyRoom(string idRoom)
        {
            List<Student> students = _context.Students
                .Include(s => s.Class)
                    .ThenInclude(c => c.Faculty)
                .Include(s => s.Class)
                    .ThenInclude(c => c.Course)
                .Include(s => s.Room)
                    .ThenInclude(r => r.Building)
                .Include(s => s.Province).Where(s => s.RoomId == idRoom).ToList();
            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach (var student in students)
            {
                studentDTOs.Add(new StudentDTO
                {
                    StudentID = student.StudentId,
                    ClassID = student.ClassId,
                    ClassName = student.Class?.ClassName,
                    CourseID = student.Class?.CourseId,
                    CourseName = student.Class?.Course?.CourseName,
                    FacultyID = student.Class?.FacultyId,
                    FacultyName = student.Class?.Faculty?.FacultyName,
                    RoomID = student.RoomId,
                    RoomName = student.Room?.RoomName,
                    BuildingID = student.Room?.BuildingId,
                    BuildingName = student.Room?.Building?.BuildingName,
                    FullName = student?.FullName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    ProvinceID = student.ProvinceId,
                    ProvinceName = student.Province?.ProvinceName,
                    District = student.District,
                    Ward = student.Ward,
                    Street = student.Street,
                    IDCard = student.Idcard,
                    IsLeader = student.IsLeader,
                    Ethnicity = student.Ethnicity,
                    Religion = student.Religion,
                    Nationality = student.Nationality,
                    DateOfIssueOfIDCard = student.DateOfIssueOfIdcard,
                    PlaceOfIssueOfIDCard = student.PlaceOfIssueOfIdcard,
                    PolicyCoverage = student.PolicyCoverage,
                    InsuranceNumber = student.InsuranceNumber,
                    NgayCapBHXH = student.NgayCapBhxh,
                    GiaTriSuDungTuNgay = student.GiaTriSuDungTuNgay,
                    ThoiDiem5NamLienTuc = student.ThoiDiem5NamLienTuc,
                    IDTinhCapBHXH = student.IdtinhCapBhxh,
                    TenTinhCapBHXH = "Unknown",
                    KhamBenhBanDau = student.KhamBenhBanDau,
                    AnhThe4x6 = student.AnhThe4x6,
                    AnhCMNDMatTruoc = student.AnhCmndmatTruoc,
                    AnhCMNDMatSau = student.AnhCmndmatSau,
                    AnhBHYTMatTruoc = student.AnhBhytmatTruoc,
                    RelativeID = GetRelative(student.StudentId).RelativeId,
                    RelativeName = GetRelative(student.StudentId).FullName,
                    RelativePhoneNumber = GetRelative(student.StudentId).PhoneNumber,
                    RelativeAddress = GetRelative(student.StudentId).Address,
                });
            }

            if (studentDTOs == null)
            {
                return NotFound();
            }

            return studentDTOs;
        }


        //// PUT: api/Student
        //[HttpPut("editstudent")]
        //public async Task<IActionResult> EditStudent(Student student)
        //{
        //    if (id != student.StudentId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(student).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StudentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Student
        //[HttpPost]
        //public async Task<ActionResult<Student>> PostStudent(Student student)
        //{
        //    _context.Students.Add(student);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        //}

        //// DELETE: api/Student/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Student>> DeleteStudent(int id)
        //{
        //    var student = await _context.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Students.Remove(student);
        //    await _context.SaveChangesAsync();

        //    return student;
        //}

        //private bool StudentExists(int id)
        //{
        //    return _context.Students.Any(e => e.StudentId == id);
        //}
    }
}
