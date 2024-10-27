using Manager.Models;
using Manager.Models.SelectList;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Views.Shared.Components.RoomStatusSelectList
{
    public class RoomStatusSelectList : ViewComponent
    {
        public IViewComponentResult Invoke(Room room)
        {
            var roomStatuses = new List<RoomStatus>
            {
                new RoomStatus
                {
                    RoomStatusID = 1,
                    RoomStatusName = "Available"
                },
                new RoomStatus
                {
                    RoomStatusID = 2,
                    RoomStatusName = "Occupied"
                },
                new RoomStatus
                {
                    RoomStatusID = 3,
                    RoomStatusName = "Maintenance"
                }
            };

            var a = new RoomStatusSelectListVM
            {
                RoomStatuses = roomStatuses,
                Room = room
            };
            return View(a);
        }
    }
}
