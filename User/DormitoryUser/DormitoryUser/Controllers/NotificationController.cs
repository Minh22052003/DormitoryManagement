using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
