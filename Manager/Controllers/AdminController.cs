﻿using Manager.Data;
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
        List<Building> buildings = new List<Building>();
        List<RoomType> roomTypes = new List<RoomType>();
        List<Role> roles = new List<Role>();
        List<Equipment> equipments = new List<Equipment>();
        List<Service> services = new List<Service>();
        public AdminController(IHttpContextAccessor httpContextAccessor)
        {
            _buildingData = new BuildingData(httpContextAccessor);
            _equipmentData = new EquipmentData(httpContextAccessor);
            _roomTypeData = new RoomTypeData(httpContextAccessor);
            _roleData = new RoleData(httpContextAccessor);
            _serviceData = new ServiceData(httpContextAccessor);
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