using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class AdminController : Controller
    {
        private readonly BuildingData buildingData = new BuildingData();
        private readonly RoomTypeData roomTypeData = new RoomTypeData();
        private readonly RoleData roleData = new RoleData();
        private readonly EquipmentData equipmentData = new EquipmentData();
        private readonly ServiceData serviceData = new ServiceData();
        List<Building> buildings = new List<Building>();
        List<RoomType> roomTypes = new List<RoomType>();
        List<Role> roles = new List<Role>();
        List<Equipment> equipments = new List<Equipment>();
        List<Service> services = new List<Service>();
        public AdminController()
        {
            buildings = buildingData.GetAllBuilding().Result;
            roomTypes = roomTypeData.GetAllRoomType().Result;
            roles = roleData.GetAllRole().Result;
            equipments = equipmentData.GetAllEquipment().Result;
            services = serviceData.GetAllService().Result;
        }
        public IActionResult ListBuilding()
        {
            return View(buildings);
        }
        [HttpGet]
        public IActionResult ListRoomType()
        {
            return View(roomTypes);
        }
        [HttpGet]
        public IActionResult ListRole()
        {
            return View(roles);
        }
        [HttpGet]
        public IActionResult ListEquipment()
        {
            return View(equipments);
        }
        [HttpGet]
        public IActionResult AddEquipment()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ListService()
        {
            return View(services);
        }
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddRoom()
        {
            ViewBag.buildings = buildings;
            ViewBag.roomtype = roomTypes;
            return View();
        }
        public IActionResult AddRoomType()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddBuilding()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBuilding(Building building)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult StaffRegistration()
        {
            var staffRegistrations = new List<StaffRegistration>
            {
                new StaffRegistration
                {
                    AccountStaffId = 101,
                    UserName = "jdoe",
                    Password = "password123",
                    Email = "jdoe@example.com"
                },
                new StaffRegistration
                {
                    AccountStaffId = 102,
                    UserName = "asmith",
                    Password = "smithPass456",
                    Email = "asmith@example.com"
                },
                new StaffRegistration
                {
                    AccountStaffId = 103,
                    UserName = "bnguyen",
                    Password = "nguyenPass789",
                    Email = "bnguyen@example.com"
                }
            };
            return View(staffRegistrations);
        }
        [HttpPost]
        public IActionResult AcceptStaff()
        {
            return View();
        }
        [HttpDelete]
        public IActionResult RejectStaff(int id)
        {
            return View();
        }
    }
}
