using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Room()
        {
            var rooms = new List<Room>
{
    new Room
    {
        RoomID = "R101",
        RoomTypeID = 1,
        BuildingID = "B01",
        BuildingName = "IT Building",
        LeaderID = "S001",
        LeaderName = "Nguyen Van A",
        RoomName = "Lab A",
        NumberOfStudent = 4,
        Capacity = 6,
        RoomStatusID = 1,
        RoomStatusName = "Available",
        RoomNote = "Room equipped with air conditioning and projectors."
    },
    new Room
    {
        RoomID = "R202",
        RoomTypeID = 2,
        BuildingID = "B02",
        BuildingName = "Science Building",
        LeaderID = "S002",
        LeaderName = "Tran Thi B",
        RoomName = "Room 202",
        NumberOfStudent = 6,
        Capacity = 6,
        RoomStatusID = 2,
        RoomStatusName = "Occupied",
        RoomNote = "Shared room for Chemistry students."
    },
    new Room
    {
        RoomID = "R303",
        RoomTypeID = 3,
        BuildingID = "B03",
        BuildingName = "Science Complex",
        LeaderID = "S003",
        LeaderName = "Le Van D",
        RoomName = "Physics Lab",
        NumberOfStudent = 3,
        Capacity = 5,
        RoomStatusID = 3,
        RoomStatusName = "Maintenance",
        RoomNote = "Under maintenance for equipment upgrades."
    }
};

            return View(rooms);
        }
        public IActionResult RoomDetail()
        {
            Room room = new Room
            {
                RoomID = "R303",
                RoomTypeID = 3,
                BuildingID = "B03",
                BuildingName = "Science Complex",
                LeaderID = "S003",
                LeaderName = "Le Van D",
                RoomName = "Physics Lab",
                NumberOfStudent = 3,
                Capacity = 5,
                RoomStatusID = 1,
                RoomStatusName = "Maintenance",
                RoomNote = "Under maintenance for equipment upgrades."
            };
            ViewBag.listStudent = new List<Student>
            {
                new Student
                {
                    StudentID = "211212355",
                    FullName = "Mao Minh",
                    Gender = true,
                    PhoneNumber = "0988335327"
                },
                new Student
                {
                    StudentID = "211434355",
                    FullName = "Hong Minh",
                    Gender = true,
                    PhoneNumber = "0999335327"
                },
                new Student
                {
                    StudentID = "211678455",
                    FullName = "Nho Tinh",
                    Gender = false,
                    PhoneNumber = "0123854327"
                }
            };
            ViewBag.listEquipment = new List<Equipment>
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
