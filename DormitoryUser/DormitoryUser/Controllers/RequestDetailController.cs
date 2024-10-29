using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class RequestDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
