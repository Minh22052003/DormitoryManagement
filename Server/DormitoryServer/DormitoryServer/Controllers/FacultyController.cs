using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public FacultyController( DataDormitoryContext context)
        {
            _context = context;
        }

        //Lấy danh sách các khoa
        [HttpGet("getlistfaculty")]
        public async Task<IActionResult> GetListFaculty()
        {
            var listFaculty = await _context.Faculties.ToListAsync();
            return Ok(listFaculty);
        }
        [HttpPost("addfaculty")]
        public async Task<IActionResult> AddFaculty(string facultyName)
        {
            var faculty = new Faculty
            {
                FacultyName = facultyName
            };
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("updatefaculty")]
        public async Task<IActionResult> UpdateFaculty([FromBody] Faculty faculty)
        {
            _context.Faculties.Update(faculty);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("deletefaculty/{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
