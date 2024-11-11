using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DormitoryUser.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountData _accountData;
        private readonly StudentData _studentData;
        private readonly RoomData _roomData;
        private readonly RegistrationData _registrationdata;
        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _accountData = new AccountData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
            _roomData = new RoomData(httpContextAccessor);
            _registrationdata = new RegistrationData(httpContextAccessor);
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
                Password = HashPassword(username, password)
            };
            HttpResponseMessage response = await _accountData.Post_LoginUserAsync(loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("jwt1", token);

                Profile student = _studentData.GetProfileStudentAsyn().Result;
                HttpContext.Session.SetString("StudentName", student.FullName);
                HttpContext.Session.SetString("MSV", student.StudentID);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorMessage}");
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.RoomTypes = _roomData.GetAllRoomType().Result;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationVM profile)
        {
            await _registrationdata.CreateRequest(profile);
            return RedirectToAction("Index", "Introduce");
        }



        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("jwt1");
            HttpContext.Session.Remove("StudentName");
            HttpContext.Session.Remove("MSV");
            return RedirectToAction("Index", "News");
        }








        public static string HashPassword(string username, string password)
        {
            string input = username + password;

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
