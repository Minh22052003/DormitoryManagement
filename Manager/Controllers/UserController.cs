using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UserController : Controller
    {
        private StudentData _studentData = new StudentData();
        private StaffData _staffData = new StaffData();
        private EquipmentData _equipmentData = new EquipmentData();
        List<Student> students = new List<Student>();
        List<Staff> staffs = new List<Staff>();
        public UserController()
        {
            students = _studentData.GetAllStudentAsyn().Result;
            staffs = _staffData.GetAllStaffAsync().Result;
        }

        [HttpGet]
        public IActionResult Student()
        {
            return View(students);
        }
        [HttpGet]
        public IActionResult StudentDetail(string id)
        {
            var student = students.Find(s => s.StudentID == id);
            return View(student);
        }



        [HttpGet]
        public IActionResult Staff()
        {
            return View(staffs);
        }
        [HttpGet]
        public IActionResult StaffDetail(string id)
        {
            var staff = staffs.Find(s => s.StaffID == id);
            return View(staff);
        }
        public IActionResult TTCN()
        {
            Staff staff = new Staff
            {
                StaffID = "ST003",
                FullName = "Le Quoc C",
                BirthDate = new DateTime(1982, 12, 20),
                Gender = true,
                PhoneNumber = "0934567890",
                Email = "lequocc@example.com",
                Hometown = "Da Nang",
                IDCard = "456789123",
                InsuranceNumber = "INS456789",
                Ethnicity = "Kinh",
                Religion = "None",
                Nationality = "Vietnamese",
                Office = "IT Department",
                WorkSchedule = "Full-time",
                RoleName = "Bao ve"
            };
            return View(staff);
        }

    }
}
