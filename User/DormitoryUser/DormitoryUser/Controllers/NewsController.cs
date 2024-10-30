using Microsoft.AspNetCore.Mvc;
using DormitoryUser.Models;
using System;
using System.Collections.Generic;

namespace DormitoryUser.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            // Tạo dữ liệu mẫu
            var newsList = new List<News>
            {
                new News
                {
                    Id = 1,
                    Title = "Tin tức 1",
                    Content = "Nội dung ngắn gọn của tin tức số 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    ImageUrl = "https://via.placeholder.com/64",
                    PublishDate = DateTime.Now.AddDays(-1)
                },
                new News
                {
                    Id = 2,
                    Title = "Tin tức 2",
                    Content = "Nội dung ngắn gọn của tin tức số 2. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    ImageUrl = "https://via.placeholder.com/64",
                    PublishDate = DateTime.Now.AddDays(-2)
                },
                new News
                {
                    Id = 3,
                    Title = "Tin tức 3",
                    Content = "Nội dung ngắn gọn của tin tức số 3. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    ImageUrl = "https://via.placeholder.com/64",
                    PublishDate = DateTime.Now.AddDays(-3)
                }
            };

            // Truyền dữ liệu mẫu vào view
            return View(newsList);
        }
    }
}
