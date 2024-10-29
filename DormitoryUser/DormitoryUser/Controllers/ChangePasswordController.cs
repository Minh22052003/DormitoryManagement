using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class ChangePasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
