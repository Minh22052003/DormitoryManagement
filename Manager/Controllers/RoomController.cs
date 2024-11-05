using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class RoomController : Controller
    {
        private RoomData _roomData;
        private StudentData _studentData;
        private EquipmentData _equipmentData;
        public RoomController(IHttpContextAccessor httpContextAccessor)
        {
            _roomData = new RoomData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
            _equipmentData = new EquipmentData(httpContextAccessor);
        }
        public IActionResult Room()
        {
            try
            {
                List<Room> rooms = _roomData.GetAllRoom().Result;
                return View(rooms);
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
        public IActionResult RoomDetail(string id)
        {
            Room room = new Room();

            List<Student> students = _studentData.GetStudentByRoomAsyn(id).Result;
            ViewBag.listStudent = students;

            List<Equipment> equipments = _equipmentData.GetEquipmentbyRoomAsyn(id).Result;

            ViewBag.listEquipment = equipments;
            ViewBag.listEquipment_Add = new List<Equipment>
            {
                new Equipment
                {
                    EquipmentID = 1,
                    EquipmentName = "Projector",
                    Price = 1500000m,
                    Quantity = 5,
                    Condition = "New"
                },
                new Equipment
                {
                    EquipmentID = 2,
                    EquipmentName = "Air Conditioner",
                    Price = 7000000m,
                    Quantity = 3,
                    Condition = "Good"
                },
                new Equipment
                {
                    EquipmentID = 3,
                    EquipmentName = "Desk",
                    Price = 500000m,
                    Quantity = 20,
                    Condition = "Used"
                }
            };
            return View(room);
        }
        [HttpPut]
        public IActionResult RemoveStudent(int studentID)
        {
            return View(RoomDetail);
        }
        public IActionResult RemoveEquipment(int equipmentID)
        {
            return View();
        }
        [HttpPut("{id}")]
        public IActionResult AddStudentToRoom(int studentID)
        { return View(); }
        [HttpPost]
        public IActionResult AddEquipmentToRoom(int studentID)
        { return View(); }
        [HttpPut]
        public IActionResult ChangeRoomInformation(Room r)
        {
            return View();
        }
    }
}
