using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class UtilityMeterController : Controller
    {
        public IActionResult Record()
        {
            return View();
        }
        public IActionResult List()
        {
            var utilityMeters = new List<UtilityMeter>
{
    new UtilityMeter
    {
        UtilityMeterID = 1,
        RoomID = "R101",
        RoomName = "Lab A",
        BuildingID = "B01",
        BuildingName = "IT Building",
        StaffID = "ST001",
        StaffName = "Nguyen Van A",
        Electricity = 350,
        Water = 120,
        RecordingDate = new DateTime(2024, 10, 10)
    },
    new UtilityMeter
    {
        UtilityMeterID = 2,
        RoomID = "R202",
        RoomName = "Room 202",
        BuildingID = "B02",
        BuildingName = "Science Building",
        StaffID = "ST002",
        StaffName = "Tran Thi B",
        Electricity = 420,
        Water = 150,
        RecordingDate = new DateTime(2024, 10, 11)
    },
    new UtilityMeter
    {
        UtilityMeterID = 3,
        RoomID = "R303",
        RoomName = "Physics Lab",
        BuildingID = "B03",
        BuildingName = "Science Complex",
        StaffID = "ST003",
        StaffName = "Le Quoc C",
        Electricity = 390,
        Water = 130,
        RecordingDate = new DateTime(2024, 10, 12)
    }
};

            return View(utilityMeters);
        }
    }
}
