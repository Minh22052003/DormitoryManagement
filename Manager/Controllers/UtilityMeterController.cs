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
        List<UtilityMeter> utilityMeters = new List<UtilityMeter>();
        List<Room> rooms = new List<Room>();
        List<Building> buildings = new List<Building>();
        public UtilityMeterController(IHttpContextAccessor httpContextAccessor)
        {
            _utilityMeterData = new UtilityMeterData(httpContextAccessor);
            _roomData = new RoomData(httpContextAccessor);
            _buildingData = new BuildingData(httpContextAccessor);
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
