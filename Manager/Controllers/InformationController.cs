using Manager.Data;
using Manager.ModelRequest;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InformationController : Controller
    {
        private AnnouncementData _announcementData;
        private NewsData _newData;
        public InformationController(IHttpContextAccessor httpContextAccessor)
        {
            _newData = new NewsData(httpContextAccessor);
            _announcementData = new AnnouncementData(httpContextAccessor);
        }
        public IActionResult Announcement()
        {
            List<AnnouncementRQ> announcementrqs = _announcementData.GetAllAnnouncement().Result;
            return View(announcementrqs);
           
        }
        public IActionResult Create_Announcement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create_Announcement(Announcement announcement)
        {
            return View();
        }
        public IActionResult News()
        {
            try
            {
                List<New> newsList = _newData.GetAllNews().Result;
                return View(newsList);
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
        public IActionResult Create_News()
        {
            return View();
        }
    }
}
