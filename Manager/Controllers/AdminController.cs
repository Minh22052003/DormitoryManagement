﻿using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                try
                {
                    await _buildingData.AddBuilding(building);
                    return RedirectToAction("ListBuilding");
                }catch (Exception e)
                {
                    return RedirectToAction("Error401");
                }

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
            try
            {
                await _roomTypeData.AddRoomtype(roomType);
                return RedirectToAction("ListRoomType");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error401", e);
            }
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
            try
            {
                await _roleData.AddRole(role);
                return RedirectToAction("ListRole");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error401", e);
            }
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
            try
            {
                await _equipmentData.AddEquipment(equipment);
                return RedirectToAction("ListEquipment");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error401", e);
            }
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
            try
            {
                await _serviceData.AddService(service);
                return RedirectToAction("ListService");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error401", e);
            }
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
            try
            {
                await _roomData.AddRoom(room);
                return RedirectToAction("ListBuilding");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error401", e);
            }
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
            try
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
            catch (Exception e)
            {
                return RedirectToAction("Fail", e);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RejectStaffAsync([FromForm] StaffRegistration staffRegistration)
        {
            var accountStaff = new AccountStaff()
            {
                Email = staffRegistration.Email,
            };
            await _accountData.RejectAccount(accountStaff);
            return RedirectToAction("StaffRegistration");
        }

        public IActionResult Fail()
        {
            return View();
        }

        public IActionResult ServiceDetail(int id)
        {
            Service service = _serviceData.GetServiceById(id).Result;
            return View(service);
        }

        public async Task<IActionResult> UpdateServiceAsync([FromForm] Service service)
        {
            if (service == null) {
                Console.WriteLine("Service is null");
                return RedirectToAction("ListService");
            }
            await _serviceData.UpdateService(service);
            return RedirectToAction("ListService");
        }





        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }




    }
}
