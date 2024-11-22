using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UtilityMeterController : Controller
    {
        private readonly UtilityMeterData _utilityMeterData;
        private readonly RoomData _roomData;
        private readonly BuildingData _buildingData;
        public UtilityMeterController(IHttpContextAccessor httpContextAccessor)
        {
            _utilityMeterData = new UtilityMeterData(httpContextAccessor);
            _roomData = new RoomData(httpContextAccessor);
            _buildingData = new BuildingData(httpContextAccessor);
        }
        public IActionResult Record()
        {
            List<Building> buildings = _buildingData.GetAllBuilding().Result;
            ViewBag.buildings = buildings;
            return View();
        }

        public IActionResult ChangeRecord(int id)
        {
            List<UtilityMeter> utilityMeters = _utilityMeterData.GetAllUtilityMeter().Result;
            UtilityMeter utilityMeter = utilityMeters.FirstOrDefault(u => u.UtilityMeterID == id);
            return View(utilityMeter);
        }


        [HttpGet]
        public JsonResult GetRoomsByBuilding(string buildingId)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;
            var room = rooms.Where(r => r.BuildingID == buildingId)
                                .Select(r => new {
                                    roomID = r.RoomID,
                                    roomName = r.RoomName
                                })
                                .ToList();

            return Json(room);
        }


        public IActionResult List()
        {
            List<UtilityMeter> utilityMeters = _utilityMeterData.GetAllUtilityMeter().Result;
            return View(utilityMeters);
        }
        public IActionResult SearchOrSort(string searchBy, string searchValue, string sortOrder)
        {
            List<UtilityMeter> records = _utilityMeterData.GetAllUtilityMeter().Result;

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchValue))
            {
                switch (searchBy)
                {
                    case "room":
                        records = records.Where(r => r.RoomName != null && r.RoomName.ToLower().Contains(searchValue)).ToList();
                        break;
                    case "staff":
                        records = records.Where(r => r.RoomName != null && r.StaffName.ToLower().Contains(searchValue)).ToList();
                        break;
                    case "date":
                        if (DateTime.TryParse(searchValue, out DateTime searchDate))
                        {
                            records = records.Where(r => r.RoomName != null && r.RecordingDate == searchDate).ToList();
                        }
                        break;
                }
            }

            // Sắp xếp
            switch (sortOrder)
            {
                case "name_asc":
                    records = records.OrderBy(r => r.StaffName).ToList();
                    break;
                case "date_desc":
                    records = records.OrderByDescending(r => r.RecordingDate).ToList();
                    break;
                case "date_asc":
                    records = records.OrderBy(r => r.RecordingDate).ToList();
                    break;
            }

            return View("List", records);
        }

        [HttpPost]
        public IActionResult CreateUtilityMeter(UtilityMeter utilityMeter)
        {
            _utilityMeterData.CreateUtilityMeter(utilityMeter);
            return RedirectToAction("List");
        }
    }
}
