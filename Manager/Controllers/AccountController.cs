using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

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
                Password = password
            };
            HttpContext.Session.SetString("username", username);
            HttpResponseMessage response = await _accountData.Post_LoginUserAsync(loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("jwt", token);
                var staff = _staffData.GetStaffAsync().Result;
                if(staff.FullName == "")
                {
                    HttpContext.Session.SetString("fullname", "Chưa cập nhật");
                }
                HttpContext.Session.SetString("fullname", staff.FullName);
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
        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("jwt");
            HttpContext.Session.Remove("fullname");
            await _accountData.SignOut();
            return RedirectToAction("SignIn");
        }



        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassword change)
        {
            string Username  = HttpContext.Session.GetString("username");
            if (change == null)
            {
                Console.WriteLine("Change is null");
                return View();
            }
            if (change.MkCu == change.MkMoi)
            {
                Console.WriteLine("Password and old Password do not match");
                return View();
            }
            if (change.MkMoi != change.XnMk)
            {
                Console.WriteLine("New Password and Confirm Password do not match");
                return View();
            }
            change.MkCu = HashPassword(Username, change.MkCu);
            change.MkMoi = HashPassword(Username, change.MkMoi);
            await _accountData.ChangePassword(change);
            return RedirectToAction("SignIn");
        }


        






        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(StaffRegistration staffRegistration)
        {
            if(staffRegistration == null)
            {
                return View();
            }
            if(staffRegistration.Password != staffRegistration.ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Password and Confirm Password do not match";
                return View();
            }
            staffRegistration.Password = HashPassword(staffRegistration.UserName, staffRegistration.Password);
            _accountData.Post_SignUpUserAsync(staffRegistration);
            return RedirectToAction("SignIn");
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
