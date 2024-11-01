using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InformationController : Controller
    {
        private AnnouncementData _announcementData;
        private NewsData _newData;
        List<Announcement> announcements = new List<Announcement>();
        List<New> newsList = new List<New>();
        public InformationController(IHttpContextAccessor httpContextAccessor)
        {
            _newData = new NewsData(httpContextAccessor);
            _announcementData = new AnnouncementData(httpContextAccessor);
        }
        public IActionResult Announcement()
        {
            try
            {
                List<Announcement> announcements = _announcementData.GetAllAnnouncement().Result;
                return View(announcements);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách sinh viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = ex });
            }
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
