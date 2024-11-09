using Manager.Data;
using Manager.Helpers;
using Manager.ModelRequest;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InformationController : Controller
    {
        private AnnouncementData _announcementData;
        private NewsData _newData;
        private ViewImage ViewImage;
        public InformationController(IHttpContextAccessor httpContextAccessor)
        {
            _newData = new NewsData(httpContextAccessor);
            _announcementData = new AnnouncementData(httpContextAccessor);
            ViewImage = new ViewImage();
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
        public async Task<IActionResult> Create_Announcement(Announcement announcementrq)
        {
            if(announcementrq == null)
            {
                return RedirectToAction("Create_Announcement");
            }
            Console.WriteLine("Test");
            var announcement = new AnnouncementRQ
            {
                Title = announcementrq.Title,
                Content = announcementrq.Content,
                Target = announcementrq.Target,
                //Image = await ViewImage.ConvertFormFileToBase64Async(announcementrq.Image),
            };
            Console.WriteLine(announcement.Image);
            await _announcementData.CreateAnnouncement(announcement);
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
