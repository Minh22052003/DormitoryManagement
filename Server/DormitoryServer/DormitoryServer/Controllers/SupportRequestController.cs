using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportRequestController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public SupportRequestController(DataDormitoryContext context)
        {
            _context = context;
        }
        [HttpGet("getallsupportrequest")]
        public IActionResult GetAllSupportRequest()
        {
            var supportRequests = _context.SupportRequests
                .Include(s=>s.Student)
                    .ThenInclude(s=>s.Room)
                        .ThenInclude(r => r.Building)
                .Include(s=>s.RequestType)
                .Include(s => s.Staff)
                .ToList();
            List<RequestDTO> requestDTOs = new List<RequestDTO>();
            foreach (var supportRequest in supportRequests)
            {
                RequestDTO requestDTO = new RequestDTO();
                requestDTO.RequestID = supportRequest.RequestId;
                requestDTO.StudentID = supportRequest.StudentId;
                requestDTO.StudentName = supportRequest.Student?.FullName;
                requestDTO.RoomID = supportRequest.Student?.RoomId;
                requestDTO.RoomName = supportRequest.Student?.Room?.RoomName;
                requestDTO.BuildingID = supportRequest.Student?.Room?.Building?.BuildingId;
                requestDTO.BuildingName = supportRequest.Student?.Room?.Building?.BuildingName;
                requestDTO.StaffID = supportRequest.StaffId;
                requestDTO.StaffName = supportRequest.Staff?.FullName;
                requestDTO.RequestTypeID = supportRequest.RequestTypeId;
                requestDTO.RequestTypeName = supportRequest.RequestType?.RequestTypeName;
                requestDTO.Description = supportRequest.Description;
                requestDTO.RequestSentDate = supportRequest.RequestSentDate;
                requestDTO.RequestProcessDate = supportRequest.RequestProcessDate;
                requestDTO.Image = supportRequest.Image;
                requestDTO.Reply = supportRequest.Reply;
                requestDTO.Status = supportRequest.Status;
                requestDTOs.Add(requestDTO);
            }
            return Ok(requestDTOs);
        }

    }
}
