﻿using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataDormitoryContext _context = new DataDormitoryContext();
        private readonly IConfiguration _configuration;

        public AuthController(DataDormitoryContext dataDormitoryContext, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Đăng nhập cho sinh viên 

        [HttpPost("loginsv")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await _context.AccountStudents
            .Where(a => a.StudentId == request.User_Name && a.Password == request.Password)
            .FirstOrDefaultAsync();
            if (account == null)
            {
                return NotFound("Sai tài khoản");
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.User_Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", account.StudentId),
                    new Claim("Roles", "Student")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwtToken);
            }
        }

        //Đăng xuất
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(-1)
            };

            Response.Cookies.Append("AuthToken", "", cookieOptions);
            return Ok("Đăng xuất thành công");
        }

        //Đăng nhập dành cho nhân viên
        [HttpPost("loginnv")]
        public async Task<IActionResult> LoginNV([FromBody] LoginRequest request)
        {
            var user = await _context.AccountStaffs
                            .Where(a => a.Username == request.User_Name && a.Password == request.Password)
                            .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            if(user.StaffId == null)
            {
                return BadRequest("Tài khoản không tồn tại");
            }
            var iduser = await _context.AccountStaffs
            .Where(a => a.Username == request.User_Name && a.Password == request.Password)
            .Select(a => a.StaffId)
            .FirstOrDefaultAsync();
            
            var roles = await _context.AccountStaffs
                .Include(a => a.Role)
                .Where(a => a.StaffId == iduser)
                .ToListAsync();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", iduser.ToString()),
                new Claim("Roles", string.Join(",", roles.Select(r => r.Role?.RoleName)))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwtToken);
        }

        [HttpPost("signupnv")]
        public async Task<IActionResult> SignUpNV([FromBody] StaffRegistration registration)
        {
            var accountStafftmp = await _context.AccountStaffs
                .Where(a => a.Username == registration.UserName || a.Email == registration.Email)
                .FirstOrDefaultAsync();
            if(accountStafftmp != null)
            {
                return BadRequest("Tài khoản đã tồn tại");
            }
            var accountStaff = new AccountStaff
            {
                Username = registration.UserName,
                Password = registration.Password,
                Email = registration.Email,
            };
            _context.AccountStaffs.Add(accountStaff);
            await _context.SaveChangesAsync();
            return Ok("Đăng ký thành công");
            
        }














        //Xuất danh sách chức vụ của nhân viên
        [HttpGet("getallrole")]
        public async Task<ActionResult<List<RoleDTO>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            List<RoleDTO> roleDTOs = new List<RoleDTO>();
            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleDTO
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                });
            }
            return roleDTOs;
        }
    }
}
