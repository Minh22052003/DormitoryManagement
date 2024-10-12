using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Student()
        {
            return View();
        }
        public IActionResult Student_Detail(string msv)
        {
            return View();
        }
        public IActionResult Staff()
        {
            return View();
        }
        public IActionResult Staff_Detail()
        {
            return View();
        }
        public IActionResult TTCN()
        {
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }
    }
}
