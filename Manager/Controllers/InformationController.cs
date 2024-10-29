using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InformationController : Controller
    {
        private AnnouncementData _announcementData = new AnnouncementData();
        private NewsData _newData = new NewsData();
        List<Announcement> announcements = new List<Announcement>();
        List<New> newsList = new List<New>();
        public InformationController()
        {
            announcements = _announcementData.GetAllAnnouncement().Result;
            newsList = _newData.GetAllNews().Result;
        }
        public IActionResult Announcement()
        {
            return View(announcements);
        }
        public IActionResult Create_Announcement()
        {
            return View();
        }
        public IActionResult News()
        {
            return View(newsList);
        }
        public IActionResult Create_News()
        {
            return View();
        }
    }
}
