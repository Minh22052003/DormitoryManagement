using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
