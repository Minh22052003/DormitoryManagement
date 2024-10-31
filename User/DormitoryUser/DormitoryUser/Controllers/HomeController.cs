using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DormitoryUser.Controllers
{
    public class HomeController : Controller
    {
        private StudentData studentData;
        private RoomData roomData;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            studentData = new StudentData(httpContextAccessor);
            roomData = new RoomData(httpContextAccessor);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Notification()
        {
            return View();
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
            var facilities = new List<Facility>
            {
                new Facility { Id = 1, FacilityName = "Wi-Fi", Description = "Kết nối Internet không dây", IsAvailable = true },
                new Facility { Id = 2, FacilityName = "Máy giặt", Description = "Dịch vụ giặt ủi", IsAvailable = false },
                new Facility { Id = 3, FacilityName = "Nhà bếp", Description = "Khu vực nấu ăn chung", IsAvailable = true },
                new Facility { Id = 4, FacilityName = "Thang máy", Description = "Thang máy phục vụ các tầng", IsAvailable = true },
            };

            return View(facilities);
        }

        public IActionResult Track_Rent()
        {
            return View();
        }

        public IActionResult Sent_Request()
        {
            return View();
        }

        private List<Request_Sent> requests = new List<Request_Sent>
        {
            new Request_Sent { Id = 1, RequestId = "REQ12345", SentDate = new DateTime(2024, 10, 14), Summary = "Xin hỗ trợ vấn đề mạng", Status = "Đã Xử Lý", Type = "mang" },
            new Request_Sent { Id = 2, RequestId = "REQ12346", SentDate = new DateTime(2024, 10, 10), Summary = "Yêu cầu sửa phòng", Status = "Đang Chờ", Type = "sua" }
        };

        public IActionResult Request_Sent()
        {
            return View(requests);
        }
        // Action tìm kiếm yêu cầu
        public IActionResult Search(DateTime? searchDate, string requestType)
        {
            var filteredRequests = requests.AsQueryable();

            if (searchDate.HasValue)
            {
                filteredRequests = filteredRequests.Where(r => r.SentDate.Date == searchDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(requestType))
            {
                filteredRequests = filteredRequests.Where(r => r.Type.Equals(requestType, StringComparison.OrdinalIgnoreCase));
            }

            return View("Index", filteredRequests.ToList());
        }

        public IActionResult Officer_Information()
        {
            var officers = new List<Officer_Information>
            {
                new Officer_Information
                {
                    Id = 1,
                    Name = "Nguyễn Văn A",
                    Position = "Trưởng Ban",
                    Room = "203 - B3",
                    Email = "daylamotcaimail@hust.edu.vn",
                    Phone = "0382212381",
                    AvatarUrl = "~/img/avatar/avatar-1.png"
                },
                new Officer_Information
                {
                    Id = 2,
                    Name = "Nguyễn Văn B",
                    Position = "Phó Ban",
                    Room = "201 - B3",
                    Email = "daylamotcaimail5@hust.edu.vn",
                    Phone = "0382214432",
                    AvatarUrl = "~/img/avatar/avatar-1.png"
                },
                new Officer_Information
                {
                    Id = 3,
                    Name = "Nguyễn Văn C",
                    Position = "Phó Ban",
                    Room = "201 - B3",
                    Email = "daylamotcaimail1@hust.edu.vn",
                    Phone = "038222233333",
                    AvatarUrl = "~/img/avatar/avatar-1.png"
                },
                new Officer_Information
                {
                    Id = 4,
                    Name = "Trần Quang A",
                    Position = "Trưởng Ban",
                    Room = "202 - B3",
                    Email = "daylamotcaimail@hust.edu.vn",
                    Phone = "0333221234",
                    AvatarUrl = "~/img/avatar/avatar-1.png"
                },
                // Bạn có thể thêm nhiều cán bộ khác ở đây
            };

            return View(officers);
        }

        public IActionResult Parking_History()
        {
            // Initialize VehicleInfo with sample data (replace this with actual data as needed)
            var vehicleInfo = new VehicleInfo
            {
                VehicleType = "Xe máy",
                LicensePlate = "29A1-12345",
                VehicleImageUrl = "~/img/wave-do.jpg",
                ParkingHistories = new List<ParkingHistory>
                {
                    new ParkingHistory
                {
                    TicketId = 1001,
                    TransactionType = "Vào",
                    TransactionTime = new DateTime(2024, 10, 16, 8, 30, 0),
                    ImageUrl = "~/img/car-entry.jpg"
                },
                new ParkingHistory
                {
                    TicketId = 1002,
                    TransactionType = "Ra",
                    TransactionTime = new DateTime(2024, 10, 16, 17, 30, 0),
                    ImageUrl = "~/img/car-exit.jpg"
                }
                }
            };
            // Pass the viewModel to the view
            return View(vehicleInfo);
        }

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
