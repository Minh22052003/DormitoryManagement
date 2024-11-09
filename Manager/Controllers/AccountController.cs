using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Manager.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;
        private readonly AccountData _accountData;
        private readonly StaffData _staffData;
        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _accountData = new AccountData(httpContextAccessor);
            _staffData = new StaffData(httpContextAccessor);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(string username, string password)
        {
            LoginAcc loginRequest = new LoginAcc()
            {
                User_Name = username,
                //Password = HashPassword(username, password)
                Password = password
            };
            HttpResponseMessage response = await _accountData.Post_LoginUserAsync(loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("jwt", token);
                return RedirectToAction("TTCN", "User");

            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorMessage}");
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult SignOut()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }


        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(StaffRegistration staffRegistration)
        {
            _accountData.Post_SignUpUserAsync(staffRegistration);
            return View();
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
