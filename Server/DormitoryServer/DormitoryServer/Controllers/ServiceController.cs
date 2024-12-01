using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize(Roles ="Admin")]
        [HttpGet("getservicebyid/{id}")]
        public IActionResult GetServiceById(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            var serviceDTO = new ServiceDTO
            {
                ServiceID = service.ServiceId,
                ServiceName = service.ServiceName,
                Unit = service.Unit,
                Price = service.Price
            };
            return Ok(serviceDTO);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("updateservice")]
        public IActionResult UpdateService(ServiceDTO serviceDTO)
        {
            var service = _context.Services.Find(serviceDTO.ServiceID);
            if (service == null)
            {
                return NotFound();
            }
            service.Unit = serviceDTO.Unit;
            service.Price = serviceDTO.Price;
            _context.SaveChanges();
            return Ok();
        }



        [Authorize(Roles = "Admin,Staff")]
        [HttpPost("addservice")]
        public IActionResult AddService(ServiceDTO serviceDTO)
        {
            var service = new Service
            {
                ServiceName = serviceDTO.ServiceName,
                Unit = serviceDTO.Unit,
                Price = serviceDTO.Price
            };
            _context.Services.Add(service);
            _context.SaveChanges();
            return Ok();
        }

    }
}
