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
        [Authorize(Roles = "Quản lý")]
        [HttpGet("getallstudent")]
        public async Task<ActionResult<List<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach (var student in students)
            {
                studentDTOs.Add(new StudentDTO
                {
                    StudentId = student.StudentId,
                    ClassId = student.ClassId,
                    RoomId = student.RoomId,
                    FullName = student.FullName,
                    BirthDate = student.BirthDate,
                });
            }
            return studentDTOs;
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
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
