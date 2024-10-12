using Microsoft.AspNetCore.Mvc;

namespace DormitoryManager.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
