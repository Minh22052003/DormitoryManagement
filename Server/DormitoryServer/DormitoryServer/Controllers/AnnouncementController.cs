using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public AnnouncementController(DataDormitoryContext context)
        {
            _context = context;
        }

        [HttpGet("getallannouncement")]
        public IActionResult GetAllAnnouncement()
        {
            var announcement = _context.Announcements.Include("Staff").ToList();
            List<AnnouncementDTO> announcementDTOs = new List<AnnouncementDTO>();
            foreach (var item in announcement)
            {
                AnnouncementDTO announcementDTO = new AnnouncementDTO();
                announcementDTO.AnnouncementID = item.AnnouncementId;
                announcementDTO.StaffID = item.StaffId;
                announcementDTO.StaffName = item.Staff?.FullName;
                announcementDTO.Title = item.Title;
                announcementDTO.Content = item.Content;
                announcementDTO.Target = item.Target;
                announcementDTO.Image = item.Image;
                announcementDTO.Status = item.Status;
                announcementDTO.CreationDate = item.CreationDate;
                announcementDTOs.Add(announcementDTO);
            }
            return Ok(announcementDTOs);
        }
    }
}
