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
            List<Announcement> announcementsforstudent = notifications.FindAll(a => a.Target == "SinhVien" || a.Target == "TatCa");
            var notification1 = notifications.Where(n => n.AnnouncementID == id).FirstOrDefault();
            ViewBag.notifications = notification1;
            return View(announcementsforstudent);
        }
    }
}
