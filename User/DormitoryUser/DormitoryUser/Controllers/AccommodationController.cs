using Microsoft.AspNetCore.Mvc;
using DormitoryUser.Models;
using System.Collections.Generic;

namespace DormitoryUser.Controllers
{
    public class AccommodationController : Controller
    {
        public IActionResult Index()
        {
            var accommodations = new List<Accommodation>
            {
                new Accommodation
                {
                    Id = 1,
                    Title = "Phòng đơn",
                    Description = "Phòng đơn dành cho một người, được trang bị giường, bàn học và tủ cá nhân.",
                    Price = 2000000,
                    ImageUrl = "path-to-room-image1.jpg",
                    CampusDescription = "Khuôn viên rộng rãi với nhiều cây xanh, có khu vực thể thao và sinh hoạt ngoài trời.",
                    CampusImageUrls = new List<string>
                    {
                        "path-to-campus-image1.jpg",
                        "path-to-campus-image2.jpg"
                    }
                },
                // Thêm các phòng khác
                new Accommodation
                {
                    Id = 1,
                    Title = "Phòng đơn",
                    Description = "Phòng đơn dành cho một người, được trang bị giường, bàn học và tủ cá nhân.",
                    Price = 2000000,
                    ImageUrl = "path-to-room-image1.jpg",
                    CampusDescription = "Khuôn viên rộng rãi với nhiều cây xanh, có khu vực thể thao và sinh hoạt ngoài trời.",
                    CampusImageUrls = new List<string>
                    {
                        "path-to-campus-image1.jpg",
                        "path-to-campus-image2.jpg"
                    }
                },
                new Accommodation
                {
                    Id = 1,
                    Title = "Phòng đơn",
                    Description = "Phòng đơn dành cho một người, được trang bị giường, bàn học và tủ cá nhân.",
                    Price = 2000000,
                    ImageUrl = "path-to-room-image1.jpg",
                    CampusDescription = "Khuôn viên rộng rãi với nhiều cây xanh, có khu vực thể thao và sinh hoạt ngoài trời.",
                    CampusImageUrls = new List<string>
                    {
                        "path-to-campus-image1.jpg",
                        "path-to-campus-image2.jpg"
                    }
                },

            };

            return View(accommodations);
        }
    }
}
