using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class SupportRequestController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}
