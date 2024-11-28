using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffRegistrationController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public StaffRegistrationController(DataDormitoryContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("getalllregistration")]
        public IActionResult GetAllRegistration()
        {
            var registrations = _context.AccountStaffs.Where(a=>a.RoleId==null).ToList();
            List<StaffRegistration> staffRegistrations = new List<StaffRegistration>() { };
            foreach (var item in registrations)
            {
                staffRegistrations.Add(new StaffRegistration
                {
                    UserName = item.Username,
                    Email = item.Email,
                });
            }
            return Ok(registrations);
        }

    }
}
