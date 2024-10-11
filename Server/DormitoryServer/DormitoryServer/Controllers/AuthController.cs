using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
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
            .Select(a => a.AccountStudent1)
            .FirstOrDefaultAsync();
            if (account == 0)
            {
                return NotFound("Sai tài khoản");
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.User_Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", account.ToString())
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

                // Tạo cookie chứa JWT
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddMinutes(30)
                };

                Response.Cookies.Append("AuthToken", jwtToken, cookieOptions);
                return Ok("Đăng nhập thành công");
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
            var iduser = await _context.AccountStaffs
            .Where(a => a.Username == request.User_Name && a.Password == request.Password)
            .Select(a => a.StaffId)
            .FirstOrDefaultAsync();
            if (iduser == "")
            {
                return NotFound("Sai tài khoản");
            }
            else
            {
                var roles = await _context.AccountStaffs
                    .Where(a => a.StaffId == iduser)
                    .Select(a => a.Role.RoleName)
                    .ToListAsync();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.User_Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", iduser.ToString())
                };
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                // Tạo cookie chứa JWT
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddMinutes(30)
                };

                Response.Cookies.Append("AuthToken", jwtToken, cookieOptions);
                return Ok("Đăng nhập thành công");
            }
        }
    }
}
