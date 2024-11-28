using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Staff,Manager, Admin")]
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


        [Authorize(Roles = "Student")]
        [HttpGet("getsupportrequestbystudent")]
        public async Task<IActionResult> GetSupportRequestByStudentAsync()
        {

            var studentId = User.FindFirst("UserId")?.Value;
            if (studentId == "")
            {
                return NotFound();
            }
            var supportRequests = _context.SupportRequests
                .Include(s => s.Student)
                    .ThenInclude(s => s.Room)
                        .ThenInclude(r => r.Building)
                .Include(s => s.RequestType)
                .Include(s => s.Staff)
                .Where(s => s.StudentId == studentId)
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

        [Authorize(Roles = "Student")]
        [HttpPost("createsupportrequest")]
        public async Task<IActionResult> CreateSupportRequestAsync(RequestDTO requestDTO)
        {
            var studentId = User.FindFirst("UserId")?.Value;
            var student = await _context.Students
                .Where(s => s.StudentId == studentId)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                Console.WriteLine("Student not found");
                return NotFound();
            }
            SupportRequest supportRequest = new SupportRequest();
            supportRequest.StudentId = studentId;
            supportRequest.RequestTypeId = requestDTO.RequestTypeID;
            supportRequest.Description = requestDTO.Description;
            supportRequest.RequestSentDate = DateTime.Now;
            //supportRequest.Image = requestDTO.Image;
            supportRequest.Status = "Chưa xử lý";
            _context.SupportRequests.Add(supportRequest);
            await _context.SaveChangesAsync();
            return Ok("Tạo yêu cầu hỗ trợ thành công");
        }


        [Authorize(Roles = "Staff,Manager, Admin")]
        [HttpPut("processrequest")]
        public async Task<IActionResult> ProcessRequestAsync(RequestDTO requestDTO)
        {
            var staffId = User.FindFirst("UserId")?.Value;
            var supportRequest = await _context.SupportRequests
                .Where(s => s.RequestId == requestDTO.RequestID)
                .FirstOrDefaultAsync();
            if (supportRequest == null)
            {
                return NotFound();
            }
            supportRequest.StaffId = staffId;
            supportRequest.RequestProcessDate = DateTime.Now;
            supportRequest.Reply = requestDTO.Reply;
            supportRequest.Status = requestDTO.Status;
            _context.SupportRequests.Update(supportRequest);
            await _context.SaveChangesAsync();
            return Ok("Xử lý yêu cầu thành công");
        }

        [Authorize]
        [HttpGet("getrequesttype")]
        public IActionResult GetRequestType()
        {
            var requestTypes = _context.SupportRequestTypes.ToList();
            List<RequestTypeDTO> requestTypeDTOs = new List<RequestTypeDTO>();
            foreach (var requestType in requestTypes)
            {
                RequestTypeDTO requestTypeDTO = new RequestTypeDTO();
                requestTypeDTO.RequestTypeId = requestType.RequestTypeId;
                requestTypeDTO.RequestTypeName = requestType.RequestTypeName;
                requestTypeDTOs.Add(requestTypeDTO);
            }
            return Ok(requestTypeDTOs);
        }

    }
}
