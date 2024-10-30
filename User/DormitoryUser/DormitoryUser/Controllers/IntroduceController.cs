using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class IntroduceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
