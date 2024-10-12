using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Announcement()
        {
            return View();
        }
        public IActionResult Create_Announcement()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Create_News()
        {
            return View();
        }
    }
}
