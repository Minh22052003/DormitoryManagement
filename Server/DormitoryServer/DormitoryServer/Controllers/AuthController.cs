using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", account.StudentId),
                    new Claim(ClaimTypes.Role, "Student")
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
                new Claim(ClaimTypes.Role, string.Join(",", roles.Select(r => r.Role?.RoleName)))
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


        //Đăng ký nhân viên
        [HttpPost("signupnv")]
        public async Task<IActionResult> SignUpNV([FromBody] StaffRegistration registration)
        {
            var accountStafftmp = await _context.AccountStaffs
                .Where(a => a.Username == registration.UserName || a.Email == registration.Email)
                .FirstOrDefaultAsync();
            if(accountStafftmp != null)
            {
                return BadRequest("Tài khoản hoặc email đã tồn tại");
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

        [Authorize(Roles = "Admin")]
        [HttpPost("acceptaccount")]
        public async Task<IActionResult> AcceptAccount([FromBody] AccountStaffDTO accountStaffDTO)
        {
            var accountStaff = await _context.AccountStaffs
                .Where(a => a.Username == accountStaffDTO.Username)
                .FirstOrDefaultAsync();
            if (accountStaff == null)
            {
                return NotFound("Tài khoản không tồn tại");
            }
            accountStaff.RoleId = accountStaffDTO.RoleId;
            var staff = new staff
            {
                StaffId = GenerateUniqueIdMD5(accountStaffDTO.Username),
                Email = accountStaffDTO.Email,
            };
            accountStaff.StaffId = staff.StaffId;
            _context.staff.Add(staff);
            _context.AccountStaffs.Update(accountStaff);
            await _context.SaveChangesAsync();
            return Ok("Chấp nhận tài khoản thành công");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("rejectaccount")]
        public async Task<IActionResult> RejectAccount([FromBody] StaffRegistration staffRegistration)
        {
            var accountStaff = await _context.AccountStaffs
                .Where(a => a.Email == staffRegistration.Email)
                .FirstOrDefaultAsync();
            if (accountStaff == null)
            {
                return NotFound("Tài khoản không tồn tại");
            }
            _context.AccountStaffs.Remove(accountStaff);
            await _context.SaveChangesAsync();
            return Ok("Không chấp nhận tài khoản thành công");
        }

        //Đổi mật khẩu nhân viên 
        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var staffId = User.FindFirst("UserId")?.Value;
            var accountStaff = await _context.AccountStaffs
                .Where(a => a.StaffId == staffId)
                .FirstOrDefaultAsync();
            if(accountStaff == null)
            {
                Console.WriteLine("Tài khoản không tồn tại");
                return NotFound("Tài khoản không tồn tại");
            }
            if(accountStaff.Password != changePasswordDTO.MkCu)
            {
                Console.WriteLine("Mật khẩu cũ không đúng");
                return BadRequest("Mật khẩu cũ không đúng");
            }
            accountStaff.Password = changePasswordDTO.MkMoi;
            _context.AccountStaffs.Update(accountStaff);
            await _context.SaveChangesAsync();
            return Ok("Đổi mật khẩu thành công");
        }

        //Đổi mật khẩu sinh viên
        [Authorize(Roles = "Student")]
        [HttpPost("changepasswordsv")]
        public async Task<IActionResult> ChangePasswordSV(ChangePasswordDTO changePasswordDTO)
        {
            var studentId = User.FindFirst("UserId")?.Value;
            var accountStudent = await _context.AccountStudents
                .Where(a => a.StudentId == studentId)
                .FirstOrDefaultAsync();
            if (accountStudent == null)
            {
                Console.WriteLine("Tài khoản không tồn tại");
                return NotFound("Tài khoản không tồn tại");
            }
            if (accountStudent.Password != changePasswordDTO.MkCu)
            {
                Console.WriteLine("Mật khẩu cũ không đúng");
                return BadRequest("Mật khẩu cũ không đúng");
            }
            accountStudent.Password = changePasswordDTO.MkMoi;
            _context.AccountStudents.Update(accountStudent);
            await _context.SaveChangesAsync();
            return Ok("Đổi mật khẩu thành công");
        }

        [HttpGet("logout")]
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

        [Authorize]
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


        private string GenerateUniqueIdMD5(string name)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(name);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString().Substring(0, 10);
            }
        }
    }
}
