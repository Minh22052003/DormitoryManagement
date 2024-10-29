using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Views.Shared.Components.BuildingSelectList
{
    public class BuildingSelectList : ViewComponent
    {
        public IViewComponentResult Invoke(Room room)
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
            var a = new BuildingSelectListVM
            {
                Buildings = buildings,
                Room = room
            };
            return View(buildings);
        }
    }
}
