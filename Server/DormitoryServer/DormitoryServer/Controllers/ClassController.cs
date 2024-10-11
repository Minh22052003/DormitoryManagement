using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public ClassController(DataDormitoryContext context)
        {
            _context = context;
        }
        //Lấy danh sách các lớp
        [HttpGet("getlistclass")]
        public async Task<IActionResult> GetListClass()
        {
            var listClass = await _context.Classes.ToListAsync();
            return Ok(listClass);
        }
        //Thêm lớp
        [HttpPost("addclass")]
        public async Task<IActionResult> AddClass(string className)
        {
            var newClass = new Class()
            {
                ClassName = className
            };
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();
            return Ok("Thêm lớp thành công");
        }
        //Sửa lớp
        [HttpPut("editclass")]
        public async Task<IActionResult> EditClass([FromBody] Class @class)
        {
            _context.Classes.Update(@class);
            await _context.SaveChangesAsync();
            return Ok("Chỉnh sửa thành công");
        }
        //Xóa lớp
        [HttpDelete("deleteclass")]
        public async Task<IActionResult> DeleteClass(string classId)
        {
            var @class = await _context.Classes.FindAsync(classId);
            if (@class == null)
            {
                return NotFound("Không tìm thấy lớp");
            }
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return Ok("Xóa lớp thành công");
        }
    }
}
