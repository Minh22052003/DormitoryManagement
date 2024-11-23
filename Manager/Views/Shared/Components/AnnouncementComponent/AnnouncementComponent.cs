using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Views.Shared.Components.AnnouncementComponent
{
    public class AnnouncementComponent : ViewComponent
    {
        private AnnouncementData _announcementData;
        public AnnouncementComponent(IHttpContextAccessor httpContextAccessor)
        {
            _announcementData = new AnnouncementData(httpContextAccessor);
        }
        public IViewComponentResult Invoke()
        {
            var announcementrqs = _announcementData.GetAllAnnouncement().Result;
            //Chuyển đổi AnnouncementRQ sang Announcement
            List<Announcement> sampleAnnouncements = new List<Announcement>();
            foreach (var item in announcementrqs)
            {
                Announcement announcement = new Announcement
                {
                    AnnouncementID = item.AnnouncementID,
                    StaffID = item.StaffID,
                    StaffName = item.StaffName,
                    Title = item.Title,
                    Content = item.Content,
                    Target = item.Target,
                    CreationDate = item.CreationDate,
                    Status = item.Status
                };
                sampleAnnouncements.Add(announcement);
            }
            sampleAnnouncements = sampleAnnouncements.Where(sa => sa.Target == "NhanVien" || sa.Target == "TatCa").ToList();
            return View(sampleAnnouncements);
        }
    }
}
