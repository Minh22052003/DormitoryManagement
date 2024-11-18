using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class AdminController : Controller
    {
        private readonly BuildingData _buildingData;
        private readonly RoomTypeData _roomTypeData;
        private readonly RoleData _roleData;
        private readonly EquipmentData _equipmentData;
        private readonly ServiceData _serviceData;
        private readonly RoomData _roomData;
        private readonly AccountData _accountData;
        public AdminController(IHttpContextAccessor httpContextAccessor)
        {
            _buildingData = new BuildingData(httpContextAccessor);
            _equipmentData = new EquipmentData(httpContextAccessor);
            _roomTypeData = new RoomTypeData(httpContextAccessor);
            _roleData = new RoleData(httpContextAccessor);
            _serviceData = new ServiceData(httpContextAccessor);
            _roomData = new RoomData(httpContextAccessor);
            _accountData = new AccountData(httpContextAccessor);
        }
        public IActionResult ListBuilding()
        {
            List<Building> buildings = _buildingData.GetAllBuilding().Result;
            return View(buildings);
        }
        [HttpGet]
        public IActionResult AddBuilding()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBuilding(Building building)
        {
            if (building != null)
            {
                await _buildingData.AddBuilding(building);
                return RedirectToAction("ListBuilding");
            }
            return RedirectToAction("ListBuilding");
        }




        [HttpGet]
        public IActionResult ListRoomType()
        {
            List<RoomType> roomTypes = _roomTypeData.GetAllRoomType().Result;
            return View(roomTypes);
        }
        public IActionResult AddRoomType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRoomType(RoomType roomType)
        {
            if(roomType == null)
            {
                return RedirectToAction("ListRoomType");
            }
            await _roomTypeData.AddRoomtype(roomType);
            return RedirectToAction("ListRoomType");
        }




        [HttpGet]
        public IActionResult ListRole()
        {
            List<Role> roles = _roleData.GetAllRole().Result;
            return View(roles);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(Role role)
        {
            if (role == null)
            {
                return RedirectToAction("ListRole");
            }
            await _roleData.AddRole(role);
            return RedirectToAction("ListRole");
        }





        [HttpGet]
        public IActionResult ListEquipment()
        {
            List<Equipment> equipments = _equipmentData.GetAllEquipment().Result;
            return View(equipments);
        }
        [HttpGet]
        public IActionResult AddEquipment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEquipment(Equipment equipment)
        {
            if (equipment == null)
            {
                return RedirectToAction("ListEquipment");
            }
            await _equipmentData.AddEquipment(equipment);
            return RedirectToAction("ListEquipment");
        }





        [HttpGet]
        public IActionResult ListService()
        {
            List<Service> services = _serviceData.GetAllService().Result;
            return View(services);
        }
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService(Service service)
        {
            if (service == null)
            {
                return RedirectToAction("ListService");
            }
            await _serviceData.AddService(service);
            return RedirectToAction("ListService");
        }





        [HttpGet]
        public IActionResult AddRoom()
        {
            List<Building> buildings = _buildingData.GetAllBuilding().Result;
            List<RoomType> roomTypes = _roomTypeData.GetAllRoomType().Result;
            List<RoomStatus> roomStatuses = _roomData.GetAllRoomStatus().Result;
            ViewBag.Buildings = buildings;
            ViewBag.RoomTypes = roomTypes;
            ViewBag.RoomStatuses = roomStatuses;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRoom(Room room)
        {
            if(room == null)
            {
                return RedirectToAction("ListBuilding");
            }
            await _roomData.AddRoom(room);
            return RedirectToAction("ListBuilding");
        }


        
        
        [HttpGet]
        public IActionResult StaffRegistration()
        {
            List<StaffRegistration> staffRegistrations = _accountData.GetAllStaffRegistration().Result;
            var roles = _roleData.GetAllRole().Result;
            ViewBag.Roles = roles;
            return View(staffRegistrations);
        }
        [HttpPost]
        public async Task<IActionResult> AcceptStaffAsync(string Email, int RoleId)
        {
            Console.WriteLine(Email);
            Console.WriteLine(RoleId);
            List<StaffRegistration> staffRegistrations = _accountData.GetAllStaffRegistration().Result;
            StaffRegistration staffRegistration = staffRegistrations.Find(x => x.Email == Email);
            var accountStaff = new AccountStaff()
            {
                AccountStaff1 = staffRegistration?.AccountStaffId,
                Username = staffRegistration.UserName,
                RoleId = RoleId,
                Email = staffRegistration.Email,
            };
            await _accountData.AcceptAccount(accountStaff);
            return RedirectToAction("StaffRegistration");
        }
        [HttpDelete]
        public IActionResult RejectStaff(int id)
        {
            return View();
        }
    }
}
