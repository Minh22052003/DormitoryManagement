using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Manager.Controllers
{
    public class UserController : Controller
    {
        private StudentData _studentData;
        private StaffData _staffData;
        private EquipmentData _equipmentData = new EquipmentData();
        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _staffData = new StaffData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
        }

        [HttpGet]
        public IActionResult Student()
        {
            try
            {
                List<Student> students = _studentData.GetAllStudentAsyn().Result;
                return View(students);
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
        [HttpGet]
        public IActionResult StudentDetail(string id)
        {
            try
            {
                List<Student> students = _studentData.GetAllStudentAsyn().Result;
                var student = students.Find(s => s.StudentID == id);
                return View(student);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào thông tin sinh viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
            
        }



        [HttpGet]
        public IActionResult Staff()
        {
            try
            {
                List<Staff> staffs = _staffData.GetAllStaffAsync().Result;
                return View(staffs);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách nhân viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
            
        }
        [HttpGet]
        public IActionResult StaffDetail(string id)
        {
            try
            {
                List<Staff> staffs = _staffData.GetAllStaffAsync().Result;
                var staff = staffs.Find(s => s.StaffID == id);
                return View(staff);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách nhân viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
            
        }
        public IActionResult TTCN()
        {
            var token = HttpContext.Session.GetString("jwt");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("SignIn", "Account");
            }
            var staff = _staffData.GetStaffAsync().Result;
            return View(staff);
        }

        public IActionResult Error(string message)
        {
            ViewData["Message"] = message; // Truyền thông điệp lỗi vào ViewData
            return View("Error"); // Trả về View lỗi
        }  


    }
}
