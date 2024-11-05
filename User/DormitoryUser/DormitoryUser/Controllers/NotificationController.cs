using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DormitoryUser.Controllers
{
    public class NotificationController : Controller
    {
        private AnnouncementData announcementData;
        public NotificationController(IHttpContextAccessor httpContextAccessor)
        {

            announcementData = new AnnouncementData(httpContextAccessor);
        }
        public IActionResult Index(int id)
        {
            List<Announcement> notifications = announcementData.GetAllAnnouncement().Result;
            var notification1 = notifications.Where(n => n.AnnouncementID == id).FirstOrDefault();
            ViewBag.notifications = notification1;
            return View(notifications);
        }
    }
}
