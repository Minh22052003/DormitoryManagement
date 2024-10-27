using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public ServiceController(DataDormitoryContext context)
        {
            _context = context;
        }

        [HttpGet("getallservice")]
        public IActionResult GetAllService()
        {
            var services = _context.Services.ToList();
            List<ServiceDTO> service = new List<ServiceDTO>() { };
            foreach (var item in services)
            {
                service.Add(new ServiceDTO
                {
                    ServiceID = item.ServiceId,
                    ServiceName = item.ServiceName,
                    Unit = item.Unit,
                    Price = item.Price
                });
            }
            return Ok(service);
        }

    }
}
