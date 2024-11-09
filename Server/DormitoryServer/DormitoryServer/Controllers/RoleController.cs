using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public RoleController(DataDormitoryContext context)
        {
            _context = context;
        }

        [HttpGet("getallrole")]
        public IActionResult GetAllRole()
        {
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost("addrole")]
        public IActionResult AddRole(RoleDTO roleDTO)
        {
            Role role = new Role();
            role.RoleName = roleDTO.RoleName;
            _context.Roles.Add(role);
            _context.SaveChanges();
            return Ok();
        }
    }
}
