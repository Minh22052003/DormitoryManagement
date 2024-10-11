using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public StaffController(DataDormitoryContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Quản lý")]
        [HttpGet("getallstaff")]
        public IActionResult GetAllStaff()
        {
            var staffs = _context.staff.ToList();
            return Ok(staffs);
        }
        [HttpGet("getprofilestaff")]
        public IActionResult GetStaff()
        {
            var staffId = User.FindFirst("UserId")?.Value;
            if (staffId == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng");
            }
            else
            {
                var staff = _context.staff.FirstOrDefault(s => s.StaffId == staffId);
                if (staff == null)
                {
                    return NotFound("Không tìm thấy thông tin người dùng");
                }
                else
                {
                    return Ok(staff);
                }
            }
        }

        //[HttpPost("addstaff")]
        //public IActionResult AddStaff([FromBody] Staff staff)
        //{
        //    _context.staff.Add(staff);
        //    _context.SaveChanges();
        //    return Ok();
        //}
    }
}
