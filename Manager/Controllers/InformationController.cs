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
        [HttpGet]
        public IActionResult SearchAndSortAnnouncements(string searchTerm, string searchBy, string sortBy)
        {
            List<AnnouncementRQ> announcements = _announcementData.GetAllAnnouncement().Result;

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchBy)
                {
                    case "content":
                        announcements = announcements.Where(a => a.Content != null && a.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "poster":
                        announcements = announcements.Where(a => a.StaffName != null && a.StaffName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "date":
                        if (DateTime.TryParse(searchTerm, out DateTime date))
                            announcements = announcements.Where(a => a.CreationDate?.Date == date.Date).ToList();
                        break;
                }
            }

            // Sắp xếp
            switch (sortBy)
            {
                case "name":
                    announcements = announcements.OrderBy(a => a.StaffName).ToList();
                    break;
                case "dateAsc":
                    announcements = announcements.OrderBy(a => a.CreationDate).ToList();
                    break;
                case "dateDesc":
                    announcements = announcements.OrderByDescending(a => a.CreationDate).ToList();
                    break;
            }

            return View("Announcement", announcements); // Trả về view với danh sách đã được lọc và sắp xếp
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
        [HttpGet]
        public IActionResult SearchAndSortNews(string searchTerm, string searchBy, string sortBy)
        {
            List<New> news = _newData.GetAllNews().Result;

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchBy)
                {
                    case "content":
                        news = news.Where(n => n.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "poster":
                        news = news.Where(n => n.StaffName != null && n.StaffName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "date":
                        if (DateTime.TryParse(searchTerm, out DateTime date))
                            news = news.Where(n => n.CreationDate == date.Date).ToList();
                        break;
                }
            }

            // Sắp xếp
            switch (sortBy)
            {
                case "name":
                    news = news.OrderBy(n => n.StaffName).ToList();
                    break;
                case "dateAsc":
                    news = news.OrderBy(n => n.CreationDate).ToList();
                    break;
                case "dateDesc":
                    news = news.OrderByDescending(n => n.CreationDate).ToList();
                    break;
            }

            return View("News", news); // Trả về view "News" với danh sách đã được lọc và sắp xếp
        }

        public IActionResult Create_News()
        {
            return View();
        }
    }
}
