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

        [Authorize(Policy = "ManagerOrStudent")]
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
                return NotFound("Khong tim thay thong tin nguoi dung");
            }
            else
            {
                var staff = _context.staff
                    .Include(s => s.AccountStaffs)
                        .ThenInclude(a => a.Role)
                    .FirstOrDefault(s => s.StaffId == staffId);
                if (staff == null)
                {
                    return NotFound("Không tìm thấy thông tin người dùng");
                }
                else
                {
                    var firstAccountStaff = staff.AccountStaffs.FirstOrDefault();
                    var staffDTO = new StaffDTO
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
                    };
                    return Ok(staffDTO);
                }
            }
        }


        //cho nhân viên cập nhật thông tin cá nhân thường xuyên
        
        [HttpPut("updateprofilestaff")]
        public IActionResult UpdateProfileStaff([FromBody] StaffDTO staffDTO)
        {
            if (staffDTO == null)
            {
                return NotFound("Không tìm thấy dữ liệu gửi lên");
            }
            var staff = _context.staff.Find(staffDTO.StaffID);
            if (staff == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng");
            }
            staff.Office = staffDTO.Office;
            staff.WorkSchedule = staffDTO.WorkSchedule;
            _context.SaveChanges();
            return Ok();
        }


        //cho nhân viên quản lý cập nhật thông tin nhân viên

        [HttpPut("updatestaffbymanager")]
        public IActionResult UpdateStaff([FromBody] StaffDTO staffDTO)
        {
            if (staffDTO == null)
            {
                return NotFound("Không tìm thấy dữ liệu gửi lên");
            }
            var staff = _context.staff.Find(staffDTO.StaffID);
            if (staff == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng");
            }
            staff.FullName = staffDTO.FullName;
            staff.BirthDate = staffDTO.BirthDate;
            staff.Gender = staffDTO.Gender;
            staff.PhoneNumber = staffDTO.PhoneNumber;
            staff.Email = staffDTO.Email;
            staff.Hometown = staffDTO.Hometown;
            staff.Idcard = staffDTO.IDCard;
            staff.InsuranceNumber = staffDTO.InsuranceNumber;
            staff.Ethnicity = staffDTO.Ethnicity;
            staff.Religion = staffDTO.Religion;
            staff.Nationality = staffDTO.Nationality;
            staff.Office = staffDTO.Office;
            staff.WorkSchedule = staffDTO.WorkSchedule;

            var accountStaff = _context.AccountStaffs.FirstOrDefault(a => a.StaffId == staff.StaffId);
            if (accountStaff != null)
            {
                accountStaff.RoleId = staffDTO.RoleID;
            }
            _context.SaveChanges();
            return Ok();
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
