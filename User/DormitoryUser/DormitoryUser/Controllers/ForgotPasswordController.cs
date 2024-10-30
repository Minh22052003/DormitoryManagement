using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
