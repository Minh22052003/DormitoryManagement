using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UtilityMeterController : Controller
    {
        private readonly UtilityMeterData _utilityMeterData = new UtilityMeterData();
        private readonly RoomData roomData = new RoomData();
        private readonly BuildingData buildingData = new BuildingData();
        List<UtilityMeter> utilityMeters = new List<UtilityMeter>();
        List<Room> rooms = new List<Room>();
        List<Building> buildings = new List<Building>();
        public UtilityMeterController()
        {
            utilityMeters = _utilityMeterData.GetAllUtilityMeter().Result;
            rooms = roomData.GetAllRoom().Result;
            buildings = buildingData.GetAllBuilding().Result;
        }
        public IActionResult Record()
        {

            ViewBag.buildings = buildings;
            return View();
        }


        [HttpGet]
        public JsonResult GetRoomsByBuilding(string buildingId)
        {
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
            return View(utilityMeters);
        }
    }
}
