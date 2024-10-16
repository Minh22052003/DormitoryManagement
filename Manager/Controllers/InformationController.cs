using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Announcement()
        {
            var announcements = new List<Announcement>
{
    new Announcement
    {
        AnnouncementID = 1,
        StaffID = "ST001",
        StaffName = "Nguyen Van A",
        Title = "New Office Policy",
        Content = "Starting from next month, the office will adopt a new working hours policy. All employees are expected to be aware of the changes.",
        Target = "All Staff",
        Image = "policy_update.png",
        CreationDate = new DateTime(2024, 10, 10),
        Status = "Published"
    },
    new Announcement
    {
        AnnouncementID = 2,
        StaffID = "ST002",
        StaffName = "Tran Thi B",
        Title = "Upcoming Team Building Event",
        Content = "We are organizing a team-building event this weekend. All staff are invited to join for a day of fun and activities at the beach.",
        Target = "All Staff",
        Image = "teambuilding_event.png",
        CreationDate = new DateTime(2024, 10, 12),
        Status = "Draft"
    },
    new Announcement
    {
        AnnouncementID = 3,
        StaffID = "ST003",
        StaffName = "Le Quoc C",
        Title = "System Maintenance Notice",
        Content = "There will be a scheduled system maintenance on October 20th, 2024. Please save your work and log off before 10:00 PM.",
        Target = "IT Department",
        Image = "maintenance_notice.png",
        CreationDate = new DateTime(2024, 10, 15),
        Status = "Published"
    }
};

            return View(announcements);
        }
        public IActionResult Create_Announcement()
        {
            return View();
        }
        public IActionResult News()
        {
            var newsList = new List<New>
{
    new New
    {
        NewsID = 1,
        StaffID = "ST001",
        StaffName = "Nguyen Van A",
        Title = "University Achieves Top Ranking",
        Content = "Our university has been ranked among the top 5 universities in the country this year. This achievement reflects the dedication of both staff and students.",
        Tag = "Achievement",
        Status = "Active",
        CreationDate = new DateTime(2024, 10, 10)
    },
    new New
    {
        NewsID = 2,
        StaffID = "ST002",
        StaffName = "Tran Thi B",
        Title = "New Library Opening",
        Content = "The university is excited to announce the opening of a new library building next month. The facility will feature state-of-the-art resources for all students.",
        Tag = "Library",
        Status = "Inactive",
        CreationDate = new DateTime(2024, 10, 12)
    },
    new New
    {
        NewsID = 3,
        StaffID = "ST003",
        StaffName = "Le Quoc C",
        Title = "Research Grants Awarded",
        Content = "Several research teams from our university have been awarded grants for their innovative projects. These grants will support further advancements in their respective fields.",
        Tag = "Research",
        Status = "Active",
        CreationDate = new DateTime(2024, 10, 15)
    }
};

            return View(newsList);
        }
        public IActionResult Create_News()
        {
            return View();
        }
    }
}
