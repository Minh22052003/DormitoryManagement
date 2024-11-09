using Microsoft.AspNetCore.Http;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryServer.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.IO;

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

        [Authorize(Policy = "Manager")]
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

        [Authorize(Policy = "Student")]
        [HttpGet("getprofilestudent")]
        public async Task<ActionResult<StudentDTO>> GetStudent()
        {
            var studenId = User.FindFirst("UserId")?.Value;
            var student = await _context.Students
                .Include(s => s.Class)
                    .ThenInclude(c => c.Faculty)
                .Include(s => s.Class)
                    .ThenInclude(c => c.Course)
                .Include(s => s.Room)
                    .ThenInclude(r => r.Building)
                .Include(s => s.Province)
                .FirstOrDefaultAsync(s => s.StudentId == studenId);

            if (student == null)
            {
                return NotFound();
            }
            var studentDTO = new StudentDTO
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
            };

            return Ok(studentDTO);
        }


        [Authorize(Policy = "Manager")]
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


        [Authorize(Policy = "Student")]
        [HttpGet("getroommate")]
        public ActionResult<List<StudentDTO>> GetRoomMate()
        {
            var studenId = User.FindFirst("UserId")?.Value;
            var roomid = _context.Students.Where(s => s.StudentId == studenId).Select(s => s.RoomId).FirstOrDefault();
            List<Student> students = _context.Students
                .Include(s => s.Class)
                    .ThenInclude(c => c.Faculty)
                .Include(s => s.Class)
                    .ThenInclude(c => c.Course)
                .Include(s => s.Room)
                    .ThenInclude(r => r.Building)
                .Include(s => s.Province).Where(s => s.RoomId == roomid).ToList();
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

        [Authorize(Policy = "Manager")]
        [HttpPut("editstudent")]
        public async Task<IActionResult> EditStudent(StudentDTO studentdto)
        {
            if(studentdto == null)
            {
                return BadRequest();
            }
            var student = await _context.Students.FindAsync(studentdto.StudentID);
            if (student == null) {
                return NotFound();
            }
            student.FullName = studentdto.FullName;
            student.BirthDate = studentdto.BirthDate;
            student.Gender = studentdto.Gender;
            student.PhoneNumber = studentdto.PhoneNumber;
            student.Email = studentdto.Email;
            student.ProvinceId = studentdto.ProvinceID;
            student.District = studentdto.District;
            student.Ward = studentdto.Ward;
            student.Street = studentdto.Street;
            student.Idcard = studentdto.IDCard;
            student.IsLeader = studentdto.IsLeader;
            student.Ethnicity = studentdto.Ethnicity;
            student.Religion  = studentdto.Religion;
            student.Nationality = studentdto.Nationality;
            student.DateOfIssueOfIdcard = studentdto.DateOfIssueOfIDCard;
            student.PlaceOfIssueOfIdcard = studentdto.PlaceOfIssueOfIDCard;
            student.PolicyCoverage = studentdto.PolicyCoverage;
            student.InsuranceNumber = studentdto.InsuranceNumber;
            student.NgayCapBhxh = studentdto.NgayCapBHXH;
            student.GiaTriSuDungTuNgay = studentdto.GiaTriSuDungTuNgay;
            student.ThoiDiem5NamLienTuc = studentdto.ThoiDiem5NamLienTuc;
            student.IdtinhCapBhxh  = studentdto.IDTinhCapBHXH;
            student.KhamBenhBanDau = studentdto.KhamBenhBanDau;
            student.AnhThe4x6 = studentdto.AnhThe4x6;
            student.AnhCmndmatTruoc = studentdto.AnhCMNDMatTruoc;
            student.AnhCmndmatSau = studentdto.AnhCMNDMatSau;
            student.AnhBhytmatTruoc = studentdto.AnhBHYTMatTruoc;


            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Policy = "Manager")]
        [HttpPut("editstudentwithroom")]
        public async Task<IActionResult> EditStudentWithRoom(StudentDTO studentdto)
        {
            if (studentdto == null)
            {
                return BadRequest();
            }
            var student = await _context.Students.FindAsync(studentdto.StudentID);
            if (student == null)
            {
                return NotFound();
            }
            var roomold = await _context.Rooms.FindAsync(student.RoomId);
            var room = await _context.Rooms.FindAsync(studentdto.RoomID);
            if (room == null)
            {
                return NotFound();
            }
            if(student.RoomId == studentdto.RoomID)
            {
                return BadRequest("Sinh viên đã ở trong phòng");
            }
            if(room.NumberOfStudent == room?.RoomType?.Capacity)
            {
                return BadRequest("Phòng đã đầy");
            }
            else
            {
                room.NumberOfStudent += 1;
                roomold.NumberOfStudent -= 1;
                student.RoomId = studentdto.RoomID;

                _context.SaveChanges();
            }   
            return NoContent();
        }
        [Authorize(Policy = "Manager")]
        [HttpPut("addstudentwithroom")]
        public async Task<IActionResult> AddStudentWithRoom(StudentDTO studentdto)
        {
            if (studentdto == null)
            {
                return BadRequest();
            }
            var student = await _context.Students.FindAsync(studentdto.StudentID);
            if (student == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(studentdto.RoomID);
            if (room == null)
            {
                return NotFound();
            }
            if (room.NumberOfStudent == room?.RoomType?.Capacity)
            {
                return BadRequest("Phòng đã đầy");
            }
            else
            {
                room.NumberOfStudent += 1;
                student.RoomId = studentdto.RoomID;

                _context.SaveChanges();
            }
            return NoContent();
        }

        [Authorize(Policy = "Manager")]
        [HttpPut("deletestudentwithroom")]
        public async Task<IActionResult> DeleteStudentWithRoom(StudentDTO studentdto)
        {
            if (studentdto == null)
            {
                return BadRequest();
            }
            var student = await _context.Students.FindAsync(studentdto.StudentID);
            if (student == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(studentdto.RoomID);
            if (room == null)
            {
                return NotFound();
            }
            room.NumberOfStudent -= 1;
            student.RoomId = null;

            _context.SaveChanges();

            return NoContent();
        }

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
