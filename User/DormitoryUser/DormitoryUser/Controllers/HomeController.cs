using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace DormitoryUser.Controllers
{
    public class HomeController : Controller
    {
        private StudentData studentData;
        private RoomData roomData;
        private EquipmentData equipmentData;
        private RequestData requestData;
        private StaffData staffData;
        private RoomInvoiceData roomInvoiceData;
        private AnnouncementData announcementData;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            studentData = new StudentData(httpContextAccessor);
            roomData = new RoomData(httpContextAccessor);
            equipmentData = new EquipmentData(httpContextAccessor);
            requestData = new RequestData(httpContextAccessor);
            staffData = new StaffData(httpContextAccessor);
            roomInvoiceData = new RoomInvoiceData(httpContextAccessor);
            announcementData = new AnnouncementData(httpContextAccessor);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Notification()
        {
            List<Announcement> announcements = announcementData.GetAllAnnouncement().Result;
            List < Announcement > announcementsforstudent = announcements.FindAll(a => a.Target == "SinhVien" || a.Target== "TatCa");
            return View(announcementsforstudent);
        }

        public async Task<IActionResult> Room()
        {
            var roommates = studentData.GetStudentByRoomAsyn().Result;
            var room = roomData.GetRoomIn().Result;
            List<RoomMate> roomMates = new List<RoomMate>();
            foreach (var item in roommates)
            {
                RoomMate roomMate = new RoomMate
                {
                    Id = item.StudentID,
                    FullName = item.FullName,
                    BirthDate = (DateTime)item.BirthDate,
                    Gender = item.Gender,
                    IsLeader = item.IsLeader,
                    PhoneNumber = item.PhoneNumber,
                    Hometown = item.ProvinceName
                };
                roomMates.Add(roomMate);
            };
            var roominactive = new RoomIsActive
            {
                RoomId = room.RoomID,
                RoomName = room.RoomName,
                CurrentNumber = room.NumberOfStudent,
                MaxNumber = room.Capacity,
                Roommates = roomMates
            };
            return View(roominactive);
        }

        public IActionResult Facilities()
        {
            try
            {
                var room = roomData.GetRoomIn().Result;
                List<Facility> facilitiess = equipmentData.GetEquipmentInRoom(room.RoomID).Result;

                return View(facilitiess);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách nhân viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }

        }

        public IActionResult Track_Rent()
        {
            List<RoomInvoice> roomInvoices = roomInvoiceData.GetAllRoomInvoice().Result;
            var profile = studentData.GetProfileStudentAsyn().Result;
            var room = roomData.GetRoomIn().Result;
            ViewBag.RoomInvoiceF = roomInvoices.Where(r => r.Status == "Not Paid" && r.RoomID == room.RoomID).FirstOrDefault();
            ViewBag.RoomPrice = room.RoomPrice;
            ViewBag.Profile = profile;
            var roomInvoiceDTT = roomInvoices.Where(r => r.Status == "Paid" && r.RoomID == room.RoomID).ToList();
            return View(roomInvoiceDTT);
        }

        public IActionResult Sent_Request()
        {
            var listtype = requestData.GetRequestTypesAsync().Result;
            ViewBag.RequestTypes = listtype;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Sent_Request(Request_Sent request_Sent)
        {
            await requestData.CreateRequest(request_Sent);
            return RedirectToAction("Request_Sent", "RequestDetail");
        }


        // Action tìm kiếm yêu cầu
        //public IActionResult Search(DateTime? searchDate, string requestType)
        //{
        //    //var filteredRequests = requests.AsQueryable();

        //    //if (searchDate.HasValue)
        //    //{
        //    //    filteredRequests = filteredRequests.Where(r => r.SentDate.Date == searchDate.Value.Date);
        //    //}

        //    //if (!string.IsNullOrEmpty(requestType))
        //    //{
        //    //    filteredRequests = filteredRequests.Where(r => r.Type.Equals(requestType, StringComparison.OrdinalIgnoreCase));
        //    //}

        //    //return View("Index", filteredRequests.ToList());
        //}

        public IActionResult Officer_Information()
        {
            try
            {
                List<InfoStaff> staffs = staffData.GetAllStaffAsync().Result;

                return View(staffs);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách nhân viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
        }

        //public IActionResult Parking_History()
        //{
        //    // Initialize VehicleInfo with sample data (replace this with actual data as needed)
        //    var vehicleInfo = new VehicleInfo
        //    {
        //        VehicleType = "Xe máy",
        //        LicensePlate = "29A1-12345",
        //        VehicleImageUrl = "~/img/wave-do.jpg",
        //        ParkingHistories = new List<ParkingHistory>
        //        {
        //            new ParkingHistory
        //        {
        //            TicketId = 1001,
        //            TransactionType = "Vào",
        //            TransactionTime = new DateTime(2024, 10, 16, 8, 30, 0),
        //            ImageUrl = "~/img/car-entry.jpg"
        //        },
        //        new ParkingHistory
        //        {
        //            TicketId = 1002,
        //            TransactionType = "Ra",
        //            TransactionTime = new DateTime(2024, 10, 16, 17, 30, 0),
        //            ImageUrl = "~/img/car-exit.jpg"
        //        }
        //        }
        //    };
        //    // Pass the viewModel to the view
        //    return View(vehicleInfo);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
