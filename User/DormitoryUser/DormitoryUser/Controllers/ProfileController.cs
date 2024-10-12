using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateStudentInfo(string address, string contact, string email)
        {
            //// Giả sử Model.Student chứa thông tin sinh viên
            //var student = _context.Students.FirstOrDefault(s => s.Id == /* Id của sinh viên */);

            //if (student != null)
            //{
            //    student.Address = address;
            //    student.Contact = contact;
            //    student.Email = email;

            //    // Lưu thay đổi vào cơ sở dữ liệu
            //    _context.SaveChanges();
            //}

            return Json(new { success = true });
        }

    }

}
