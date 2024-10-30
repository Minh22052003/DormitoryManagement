using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class QRCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
