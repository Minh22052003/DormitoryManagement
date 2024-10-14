using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Room()
        {
            return View();
        }
        public IActionResult RoomDetail()
        {
            return View();
        }
    }
}
