using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class SupportRequestController : Controller
    {
        public IActionResult List()
        {
            var requests = new List<Request>
{
    new Request
    {
        RequestID = 1,
        StudentID = "S001",
        StudentName = "Nguyen Van A",
        RoomID = "R101",
        RoomName = "Lab A",
        BuildingID = "B01",
        BuildingName = "IT Building",
        StaffID = "ST001",
        StaffName = "Tran Thi B",
        RequestTypeID = 1,
        RequestTypeName = "Maintenance",
        Description = "Request for air conditioner repair in room R101.",
        RequestSentDate = new DateTime(2024, 10, 5),
        RequestProcessDate = new DateTime(2024, 10, 7),
        Image = null,
        Reply = "Maintenance completed on 7th October.",
        Status = "Processed"
    },
    new Request
    {
        RequestID = 2,
        StudentID = "S002",
        StudentName = "Tran Thi C",
        RoomID = "R202",
        RoomName = "Room 202",
        BuildingID = "B02",
        BuildingName = "Science Building",
        StaffID = "ST002",
        StaffName = "Le Quoc C",
        RequestTypeID = 2,
        RequestTypeName = "Room Change",
        Description = "Request to change room due to noise issues.",
        RequestSentDate = new DateTime(2024, 10, 6),
        RequestProcessDate = null, // Not processed yet
        Image = null,
        Reply = null,
        Status = "Pending"
    },
    new Request
    {
        RequestID = 3,
        StudentID = "S003",
        StudentName = "Le Van D",
        RoomID = "R303",
        RoomName = "Physics Lab",
        BuildingID = "B03",
        BuildingName = "Science Complex",
        StaffID = "ST003",
        StaffName = "Nguyen Van E",
        RequestTypeID = 3,
        RequestTypeName = "Permission to Stay Late",
        Description = "Request to stay late for study purposes until 10 PM.",
        RequestSentDate = new DateTime(2024, 10, 8),
        RequestProcessDate = new DateTime(2024, 10, 9),
        Image = null,
        Reply = "Approved for late stay until 10 PM.",
        Status = "Approved"
    }
};

            return View(requests);
        }
        public IActionResult Detail()
        {
            Request rq = new Request
            {
                RequestID = 3,
                StudentID = "S003",
                StudentName = "Le Van D",
                RoomID = "R303",
                RoomName = "Physics Lab",
                BuildingID = "B03",
                BuildingName = "Science Complex",
                StaffID = "ST003",
                StaffName = "Nguyen Van E",
                RequestTypeID = 3,
                RequestTypeName = "Permission to Stay Late",
                Description = "Request to stay late for study purposes until 10 PM.",
                RequestSentDate = new DateTime(2024, 10, 8),
                RequestProcessDate = new DateTime(2024, 10, 9),
                Image = null,
                Reply = "Approved for late stay until 10 PM.",
                Status = "Approved"
            };
            return View(rq);
        }
    }
}
