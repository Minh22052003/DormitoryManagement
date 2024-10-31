using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountData _accountData;
        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _accountData = new AccountData();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            Login loginRequest = new Login()
            {
                User_Name = username,
                //Password = HashPassword(username, password)
                Password = password
            };
            HttpResponseMessage response = await _accountData.Post_LoginUserAsync(loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("jwt1", token);
                return RedirectToAction("Index", "Notification");

            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorMessage}");
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Registration(int id)
        {
            return View();
        }

    }
}
