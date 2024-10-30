using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DormitoryUser.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            // Tạo 3 dữ liệu mẫu cho Notification
            List<Notification> notifications = new List<Notification>
            {
                new Notification
                {
                    title = "Thông báo 1.1",
                    content = "Nội dung chi tiết của thông báo 1..1.1..",
                    date = "10/10/2024"
                },
                new Notification
                {
                    title = "Thông báo 2.2",
                    content = "Nội dung chi tiết của thông báo 2.",
                    date = "09/10/2024"
                },
                new Notification
                {
                    title = "Thông báo 3.6",
                    content = "Nội dung chi tiết của thông báo 3.",
                    date = "08/10/2024"
                }
            };

            // Truyền dữ liệu mẫu sang View
            return View(notifications);
        }
    }
}
