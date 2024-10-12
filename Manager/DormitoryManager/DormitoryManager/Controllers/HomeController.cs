using DormitoryManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DormitoryManager.Controllers
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
        public IActionResult Student_Management()
        {
            return View();
        }

        public IActionResult Room_Management()
        {
            return View();
        }

        public IActionResult Parking_Management()
        {
            return View();
        }

        public IActionResult Handle_Requests()
        {
            return View();
        }

        public IActionResult Notification_Management()
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
