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
            try
            {
                List<AnnouncementRQ> announcementrqs = _announcementData.GetAllAnnouncement().Result;
                return View(announcementrqs);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
        }

        [HttpGet]
        public IActionResult SearchNews(string searchTerm)
        {
            List<News> news = _newData.GetAllNews().Result;
            List<News> searchResults;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchResults = news.Where(r =>
                r.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.StaffName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
                if (DateTime.TryParse(searchTerm, out DateTime date))
                    searchResults = news.Where(a => a.CreationDate?.Date == date.Date).ToList();
                return View("News", searchResults);
            }
            else
            {
                searchResults = news.ToList();
            }
            return View("News", searchResults);
        }
        [HttpGet]
        public IActionResult SortNews(string sortBy)
        {
            List<News> news = _newData.GetAllNews().Result;
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

            return View("News", news);
        }
        public IActionResult Create_Announcement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create_Announcement(Announcement announcementrq)
        {
            try
            {
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
                // Gửi thông báo thành công
                TempData["SuccessMessage"] = "Thông báo đã được tạo thành công!";
                return RedirectToAction("Announcement"); // Chuyển hướng đến trang danh sách thông báo hoặc trang khác
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            } 
        }



        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                await _announcementData.DeleteAnnouncement(id);
                return RedirectToAction("Announcement");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
            
        }


        public IActionResult News()
        {
            try
            {
                List<News> newsList = _newData.GetAllNews().Result;
                return View(newsList);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error401");
            }
        }
        [HttpGet]
        public IActionResult SearchAndSortNews(string searchTerm, string searchBy, string sortBy)
        {
            List<News> news = _newData.GetAllNews().Result;

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
        [HttpPost]
        public async Task<IActionResult> Create_NewsAsync(News @new)
        {
            try
            {
                var staffid = HttpContext.Session.GetString("staffid");
                if (@new == null)
                {
                    return RedirectToAction("Create_News");
                }
                var news = new News
                {
                    Title = @new.Title,
                    Content = @new.Content,
                    StaffID = staffid,
                    StaffName = HttpContext.Session.GetString("fullname"),
                    Status = "Active",
                    CreationDate = DateTime.Now,
                };
                await _newData.CreateNews(news);
                List<News> newsList = _newData.GetAllNews().Result;
                return View("News", newsList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
            
        }


        public IActionResult SeeAnnouncement()
        {
            var sampleAnnouncements = _announcementData.GetAllAnnouncement().Result;
            sampleAnnouncements = sampleAnnouncements.Where(sa => sa.Target == "NhanVien" || sa.Target == "TatCa").ToList();
            return View(sampleAnnouncements);
        }
        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }
    }
}
