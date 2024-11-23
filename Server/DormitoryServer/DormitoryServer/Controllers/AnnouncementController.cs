using DormitoryServer.DTOs;
using DormitoryServer.Helpers;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DormitoryServer.Controllers
{
    [Route("api/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        private FormFileHelper _formFileHelper = new FormFileHelper();

        public AnnouncementController(DataDormitoryContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "Manager")]
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
                //announcementDTO.Image = _formFileHelper.GetImage(item.Image);
                announcementDTO.Status = item.Status;
                announcementDTO.CreationDate = item.CreationDate;
                announcementDTOs.Add(announcementDTO);
            }
            return Ok(announcementDTOs);
        }

        [Authorize(Policy ="Manager")]
        [HttpPost("createannouncement")]
        public async Task<IActionResult> CreateAnnouncementAsync([FromBody] AnnouncementDTO announcementDTO)
        {
            var staffid = User.FindFirst("UserID").Value;
            var staff = _context.staff.Find(staffid);
            var announcement = new Announcement
            {
                StaffId = staffid,
                Title = announcementDTO.Title,
                Content = announcementDTO.Content,
                Target = announcementDTO.Target,
                //Image = await _formFileHelper.SaveImageAsync(announcementDTO.Image),
                CreationDate = DateTime.Now,
            };
            _context.Announcements.Add(announcement);
            _context.SaveChanges();


            return Ok();
        }

        [Authorize(Policy = "Manager")]
        [HttpDelete("deleteannouncement")]
        public async Task<IActionResult> DeleteAnnouncementAsync([FromBody] int id)
        {
            Announcement announcement = _context.Announcements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }
            _context.Announcements.Remove(announcement);
            _context.SaveChanges();


            return Ok();
        }
    }
}
