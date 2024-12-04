using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class Track_RentController : Controller
    {
        private RoomInvoiceData roomInvoiceData;
        private StudentData studentData;
        private RoomData roomData;
        public Track_RentController(IHttpContextAccessor httpContextAccessor)
        {
            studentData = new StudentData(httpContextAccessor);
            roomData = new RoomData(httpContextAccessor);
            roomInvoiceData = new RoomInvoiceData(httpContextAccessor);
        }
        public IActionResult Index()
        {
            List<RoomInvoice> roomInvoices = roomInvoiceData.GetAllRoomInvoice().Result;
            var profile = studentData.GetProfileStudentAsyn().Result;
            var room = roomData.GetRoomIn().Result;
            var RoomInvoiceF = roomInvoices.Where(r => r.Status == "Not Paid" && r.RoomID == room.RoomID).FirstOrDefault();
            ViewBag.RoomPrice = room.RoomPrice;
            ViewBag.Profile = profile;
            return View(RoomInvoiceF);
        }
    }
}
