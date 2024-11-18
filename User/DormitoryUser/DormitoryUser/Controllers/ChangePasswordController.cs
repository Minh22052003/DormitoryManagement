using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DormitoryUser.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly AccountData _accountData;
        public ChangePasswordController(IHttpContextAccessor httpContextAccessor)
        {
            _accountData = new AccountData(httpContextAccessor);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword change)
        {
            string Username = HttpContext.Session.GetString("MSV");
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
            return RedirectToAction("Index", "Home");
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
