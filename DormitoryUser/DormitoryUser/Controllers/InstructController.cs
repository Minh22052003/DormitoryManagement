using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class InstructController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
