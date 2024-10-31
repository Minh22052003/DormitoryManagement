using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class ProfileController : Controller
    {
        private StudentData _studentData;
        public ProfileController(IHttpContextAccessor httpContextAccessor)
        {
            _studentData = new StudentData(httpContextAccessor);
        }
        public IActionResult Index()
        {
            try
            {
                Profile student = _studentData.GetProfileStudentAsyn().Result;
                return View(student);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách sinh viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = ex });
            }
        }


        public IActionResult Error(string message)
        {
            ViewData["Message"] = message; // Truyền thông điệp lỗi vào ViewData
            return View("Error"); // Trả về View lỗi
        }


    }

}
