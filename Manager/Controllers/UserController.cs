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
        private EquipmentData _equipmentData;
        private RoleData _roleData;
        private RegistrationData _registrationData;
        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _staffData = new StaffData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
            _equipmentData = new EquipmentData(httpContextAccessor);
            _roleData = new RoleData(httpContextAccessor);
            _registrationData = new RegistrationData(httpContextAccessor);
        }

        [HttpGet]
        public IActionResult Student()
        {
            try
            {
                var students = _studentData.GetAllStudentAsyn().Result;
                List<RegistrationVM> registrations = _registrationData.GetAllRegistration().Result;
                List<RegistrationVM> registrations1 = registrations.Where(r=>r.ApplicationStatus != "Approved").ToList();
                List<Student> filteredStudents = students
                .Where(student => !registrations1.Any(registration => registration.StudentID == student.StudentID))
                .ToList();
                return View(filteredStudents);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
        }
        public IActionResult SearchStudents(string searchText, string searchType)
        {
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            searchText = searchText?.ToLower(); // Chuyển từ khóa tìm kiếm sang chữ thường

            // Thực hiện tìm kiếm dựa trên searchType
            switch (searchType)
            {
                case "msv":
                    students = students.Where(s => s.StudentID.ToLower().Contains(searchText)).ToList();
                    break;
                case "name":
                    students = students.Where(s => s.FullName.ToLower().Contains(searchText)).ToList();
                    break;
                case "room":
                    students = students.Where(s => s.RoomName != null && s.RoomName.ToLower().Contains(searchText)).ToList();
                    break;
                default:
                    break;
            }

            return View("Student", students); // Trả về view "Student" với kết quả tìm kiếm
        }
        public IActionResult SortStudents(int sortOption)
        {
            List<Student> students = _studentData.GetAllStudentAsyn().Result;

            // Sắp xếp dựa trên sortOption
            switch (sortOption)
            {
                case 1:
                    students = students.OrderBy(s => s.FullName).ToList();
                    break;
                case 2:
                    students = students.OrderByDescending(s => s.FullName).ToList();
                    break;
                case 3:
                    students = students.OrderBy(s => s.StudentID).ToList();
                    break;
                case 4:
                    students = students.OrderByDescending(s => s.StudentID).ToList();
                    break;
                default:
                    break;
            }

            return View("Student", students); // Giả sử View để hiển thị danh sách là "StudentList"
        }
        [HttpGet]
        public IActionResult StudentDetail(string id)
        {
            try
            {
                var students = _studentData.GetAllStudentAsyn().Result;
                var student = students.Find(s => s.StudentID == id);
                return View(student);
            }
            catch (Exception ex)
            {
                return View("Error401");
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
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
            
        }
        [HttpGet]
        public IActionResult SortStaff(string sortOption)
        {
            List<Staff> staff = _staffData.GetAllStaffAsync().Result;

            // Sắp xếp dựa trên sortOption
            switch (sortOption)
            {
                case "name_asc":
                    staff = staff.OrderBy(s => s.FullName == null ? "" : s.FullName.ToLower()).ToList();
                    break;
                case "name_desc":
                    staff = staff.OrderByDescending(s => s.FullName == null ? "" : s.FullName.ToLower()).ToList();
                    break;
                case "id_asc":
                    staff = staff.OrderBy(s => s.StaffID).ToList();
                    break;
                case "id_desc":
                    staff = staff.OrderByDescending(s => s.StaffID).ToList();
                    break;
                default:
                    break;
            }

            return View("Staff", staff); // Trả về view "Staff" với danh sách đã được sắp xếp
        }
        [HttpGet]
        public IActionResult SearchStaff(string searchTerm, string searchBy)
        {
            List<Staff> staff = _staffData.GetAllStaffAsync().Result;

            // Kiểm tra và thực hiện tìm kiếm theo tiêu chí
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchBy)
                {
                    case "id":
                        staff = staff.Where(s => s.StaffID.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "name":
                        staff = staff.Where(s => s.FullName != null && s.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "role":
                        staff = staff.Where(s => s.RoleName != null && s.RoleName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    default:
                        break;
                }
            }
            return View("Staff", staff); // Trả về view "Staff" với danh sách đã được lọc
        }
        [HttpGet]
        public IActionResult StaffDetail(string id)
        {
            try
            {
                List<Role> roles = _roleData.GetAllRole().Result;
                ViewBag.roles = roles;
                List<Staff> staffs = _staffData.GetAllStaffAsync().Result;
                var staff = staffs.Find(s => s.StaffID == id);
                return View(staff);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error401");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStudentAsync(Student updatedStudent)
        {
            if (updatedStudent != null)
            {
                await _studentData.UpdateStudent(updatedStudent);
                return RedirectToAction("Student", "User");
            }

            return RedirectToAction("TTCN", "User");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStaffAsync(Staff updatedStaff)
        {
            if (updatedStaff != null)
            {
                await _staffData.UpdateStaff(updatedStaff);
                return RedirectToAction("Staff", "User");
            }

            return RedirectToAction("TTCN", "User");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfileAsync(Staff updatedStaff)
        {
            if (updatedStaff != null)
            {
                await _staffData.UpdateProfile(updatedStaff);
                return RedirectToAction("TTCN", "User");
            }

            return RedirectToAction("TTCN", "User");
        }
        [HttpGet]
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
        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }
    }
}
