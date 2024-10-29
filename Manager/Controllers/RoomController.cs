using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class RoomController : Controller
    {
        private RoomData _roomData = new RoomData();
        private StudentData _studentData = new StudentData();
        private EquipmentData _equipmentData = new EquipmentData();
        List<Room> rooms = new List<Room>();
        public RoomController()
        {
            rooms = _roomData.GetAllRoom().Result;
        }
        public IActionResult Room()
        {
            return View(rooms);
        }

        [HttpGet]
        public IActionResult RoomDetail(string id)
        {
            Room room = rooms.Find(r => r.RoomID == id);

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
