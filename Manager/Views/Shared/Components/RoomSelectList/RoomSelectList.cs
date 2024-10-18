using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Views.Shared.Components.RoomSelectList
{
    public class RoomSelectList : ViewComponent
    {
        public IViewComponentResult Invoke(string buildingID)
        {
            List<Room> rooms = new List<Room>
{
    new Room
    {
        RoomID = "R101",
        RoomTypeID = 1,
        BuildingID = "B01",
        BuildingName = "IT Building",
        LeaderID = "S001",
        LeaderName = "Nguyen Van A",
        RoomName = "Lab A",
        NumberOfStudent = 4,
        Capacity = 6,
        RoomStatusID = 1,
        RoomStatusName = "Available",
        RoomNote = "Room equipped with air conditioning and projectors."
    },
    new Room
    {
        RoomID = "R202",
        RoomTypeID = 2,
        BuildingID = "B02",
        BuildingName = "Science Building",
        LeaderID = "S002",
        LeaderName = "Tran Thi B",
        RoomName = "Room 202",
        NumberOfStudent = 6,
        Capacity = 6,
        RoomStatusID = 2,
        RoomStatusName = "Occupied",
        RoomNote = "Shared room for Chemistry students."
    },
    new Room
    {
        RoomID = "R303",
        RoomTypeID = 3,
        BuildingID = "B03",
        BuildingName = "Science Complex",
        LeaderID = "S003",
        LeaderName = "Le Van D",
        RoomName = "Physics Lab",
        NumberOfStudent = 3,
        Capacity = 5,
        RoomStatusID = 3,
        RoomStatusName = "Maintenance",
        RoomNote = "Under maintenance for equipment upgrades."
    }
};
            return View(rooms);
        }
    }
}
