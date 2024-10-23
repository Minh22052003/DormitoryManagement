using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult ListBuilding()
        {
            var buildings = new List<Building>
{
    new Building
    {
        BuildingID = "B01",
        BuildingName = "IT Building",
        RoomCount = 10
    },
    new Building
    {
        BuildingID = "B02",
        BuildingName = "Science Building",
        RoomCount = 15
    },
    new Building
    {
        BuildingID = "B03",
        BuildingName = "Science Complex",
        RoomCount = 20
    }
};
            return View(buildings);
        }
        [HttpGet]
        public IActionResult ListRoomType()
        {
            var roomTypes = new List<RoomType>
            {
                new RoomType
                {
                    RoomTypeID = 1,
                    RoomTypeName = "Single Room",
                    Capacity = 1,
                    RoomPrice = 3000000m
                },
                new RoomType
                {
                    RoomTypeID = 2,
                    RoomTypeName = "Double Room",
                    Capacity = 2,
                    RoomPrice = 4500000m
                },
                new RoomType
                {
                    RoomTypeID = 3,
                    RoomTypeName = "Shared Room",
                    Capacity = 4,
                    RoomPrice = 6000000m
                }
            };
            return View(roomTypes);
        }
        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    RoleId = 1,
                    RoleName = "Administrator"
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Staff"
                },
                new Role
                {
                    RoleId = 3,
                    RoleName = "Student"
                }
            };
            return View(roles);
        }
        [HttpGet]
        public IActionResult ListEquipment()
        {
            var equipments = new List<Equipment>
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
                    EquipmentName = "Whiteboard",
                    Price = 100000m,
                    Quantity = 10,
                    Condition = "Good"
                },
                new Equipment
                {
                    EquipmentID = 3,
                    EquipmentName = "Laptop",
                    Price = 20000000m,
                    Quantity = 3,
                    Condition = "Used"
                }
            };
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
            var services = new List<Service>
            {
                new Service
                {
                    ServiceID = 1,
                    ServiceName = "Laundry",
                    Unit = "Per kg",
                    Price = 20000m
                },
                new Service
                {
                    ServiceID = 2,
                    ServiceName = "Internet",
                    Unit = "Per month",
                    Price = 100000m
                },
                new Service
                {
                    ServiceID = 3,
                    ServiceName = "Electricity",
                    Unit = "Per kWh",
                    Price = 3500m
                }
            };
            return View(services);
        }
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        public IActionResult AddRoom()
        {
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
