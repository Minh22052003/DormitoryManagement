using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var profile = new Profile
            {
                FullName = "Mao Tuan Minh",
                StudentId = "211211856",
                CurrentRoom = "IC456879",
                DateOfBirth = new DateTime(2003, 5, 16),
                Gender = "Nam", // có thể sử dụng "Nữ" cho nữ
                PhoneNumber = "0382212381",
                Email = "student@example.com",
                Hometown = "Hà Nội", // có thể để trống hoặc thay đổi
                IdentityCard = "123456789",
                SocialInsuranceNumber = "987654321",
                FamilyPhoneNumber = "0987654321",
                NotificationAddress = "123 Đường ABC, Hà Nội"
            };

            return View(profile);
        }

     

    }

}
