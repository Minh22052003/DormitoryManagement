using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UtilityMeter : Controller
    {
        public IActionResult Record()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
