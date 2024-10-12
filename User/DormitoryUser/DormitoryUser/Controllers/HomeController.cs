using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DormitoryUser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

<<<<<<< HEAD
=======
        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult Room()
        {
            return View();
        }

        public IActionResult Facilities()
        {
            return View();
        }

        public IActionResult Track_Rent()
        {
            return View();
        }

        public IActionResult Sent_Request()
        {
            return View();
        }

        public IActionResult Request_Sent()
        {
            return View();
        }

        public IActionResult Officer_Information()
        {
            return View();
        }

>>>>>>> c9c66a1 (update1210)
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
