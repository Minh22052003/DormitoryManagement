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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationVM profile)
        {
            var error = await _registrationdata.CreateRequest(profile);
            if(error == null)
            {
                return RedirectToAction("Index", "Introduce");
            }
            else
            {
                ViewBag.ErrorMessage = error;
                return View();
            }
            
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
