using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;

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

        //[Authorize(Roles = "Quản lý")]
        [HttpGet("getallstaff")]
        public IActionResult GetAllStaff()
        {
            var staffs = _context.staff
                .Include(s=>s.AccountStaffs)
                    .ThenInclude(a => a.Role)
                .ToList();
            List<StaffDTO> staffDTOs = new List<StaffDTO>();
            foreach (var staff in staffs)
            {
                var firstAccountStaff = staff.AccountStaffs.FirstOrDefault();
                staffDTOs.Add(new StaffDTO
                {
                    StaffID = staff.StaffId,
                    FullName = staff.FullName,
                    BirthDate = staff.BirthDate,
                    Gender = staff.Gender,
                    PhoneNumber = staff.PhoneNumber,
                    Email = staff.Email,
                    Hometown = staff.Hometown,
                    IDCard = staff.Idcard,
                    InsuranceNumber = staff.InsuranceNumber,
                    Ethnicity = staff.Ethnicity,
                    Religion = staff.Religion,
                    Nationality = staff.Nationality,
                    Office = staff.Office,
                    WorkSchedule = staff.WorkSchedule,
                    RoleID = firstAccountStaff?.RoleId,
                    RoleName = firstAccountStaff?.Role?.RoleName
                });

            }

            return Ok(staffDTOs);
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
