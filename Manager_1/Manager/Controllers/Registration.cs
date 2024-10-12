using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class Registration : Controller
    {
        public IActionResult nRegistrations()
        {
            return View();
        }
        public IActionResult Registrations()
        {
            return View();
        }
        public IActionResult RegistrationDetail()
        {
            return View();
        }
        public IActionResult AcceptRegistrations()
        {
            return View();
        }
        public IActionResult RejectRegistrations()
        {
            return View();
        }
    }
}
