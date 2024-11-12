using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Manager.Controllers
{
    public class RoomController : Controller
    {
        private RoomData _roomData;
        private StudentData _studentData;
        private EquipmentData _equipmentData;
        private BuildingData _buildingData;
        public RoomController(IHttpContextAccessor httpContextAccessor)
        {
            _roomData = new RoomData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
            _equipmentData = new EquipmentData(httpContextAccessor);
            _buildingData = new BuildingData(httpContextAccessor);
        }
        public IActionResult Room()
        {
            try
            {
                List<Room> rooms = _roomData.GetAllRoom().Result;
                return View(rooms);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Nếu người dùng không có quyền truy cập, chuyển hướng đến trang lỗi
                return RedirectToAction("Error", new { message = "Bạn không có quyền truy cập vào danh sách sinh viên." });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error", new { message = ex });
            }
        }
        [HttpGet]
        public async Task<IActionResult> RoomMethod(string sortOrder, string filterStatus)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;

            // Lọc trạng thái phòng
            if (!String.IsNullOrEmpty(filterStatus))
            {
                rooms = rooms.Where(r => r.RoomStatusName == filterStatus).ToList();
            }

            // Sắp xếp
            rooms = sortOrder switch
            {
                "room_asc" => rooms.OrderBy(r => r.RoomName).ToList(),
                "room_desc" => rooms.OrderByDescending(r => r.RoomName).ToList(),
                _ => rooms
            };

            return View("Room", rooms);
        }
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;
            List<Room> searchResults;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchResults = rooms.Where(r =>
                    r.RoomName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    r.BuildingName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    r.RoomStatusName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            else
            {
                searchResults = rooms.ToList();
            }

            return View("Room", searchResults); // Trả về View với kết quả tìm kiếm
        }

        [HttpGet]
        public IActionResult RoomDetail(string id)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;
            Room room = rooms.Find(r => r.RoomID == id);

            List<Student> students = _studentData.GetStudentByRoomAsyn(id).Result;
            ViewBag.listStudent = students;

            List<Equipment> equipments = _equipmentData.GetEquipmentbyRoomAsyn(id).Result;
            List<Equipment> equipmentadd = _equipmentData.GetEquipmentbyRoomAsyn(id).Result;
            List<RoomStatus> roomStatuses = _roomData.GetAllRoomStatus().Result;
            List<Building> buildings = _buildingData.GetAllBuilding().Result;

            ViewBag.listEquipment = equipments;
            ViewBag.listEquipment_Add = equipmentadd;
            ViewBag.listRoomStatus = roomStatuses;
            ViewBag.buildings = buildings;
            return View(room);
        }

        [HttpGet]
        public IActionResult ChangeStudent(string id)
        {
            List<Building> buildings = _buildingData.GetAllBuilding().Result;
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            var student = students.Find(s => s.StudentID == id);
            ViewBag.buildings = buildings;
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStudent1(Student student)
        {
            if (student != null)
            {
                await _studentData.UpdateStudentWithRoom(student);
                return RedirectToAction("Room");
            }
            return null;
        }


        public async Task<IActionResult> SetAsLeader(string id)
        {
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            var student = students.Find(s => s.StudentID == id);
            student.IsLeader = true;
            if (student != null)
            {
                await _studentData.UpdateStudentLeader(student);
                return RedirectToAction("Room");
            }
            return null;
        }




        [HttpGet]
        public async Task<IActionResult> RemoveStudentAsync(string id)
        {
            if (id != "")
            {
                List<Student> students = _studentData.GetAllStudentAsyn().Result;
                var student = students.Find(s => s.StudentID == id);
                await _studentData.DeleteStudentWithRoom(student);
                return RedirectToAction("Room");
            }
            return null;
        }
        public IActionResult RemoveEquipment(int equipmentID)
        {
            return View();
        }





        [HttpGet]
        public IActionResult AddStudentToRoom(string id)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;
            Room room = rooms.Find(r => r.RoomID == id);
            ViewBag.Room = room;
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            var student = students.Where(s=>s.RoomID==null).ToList();
            return View(student);
        }
        public async Task<IActionResult> AddStudentToRoom1(string idstudent, string idroom)
        {
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            var student = students.Find(s => s.StudentID == idstudent);
            student.RoomID = idroom;
            await _studentData.AddStudentWithRoom(student);
            return RedirectToAction("RoomDetail", new { id = idroom });
        }






        [HttpGet]
        public IActionResult AddEquipmentToRoom(string id)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;
            Room room = rooms.Find(r => r.RoomID == id);
            ViewBag.Room = room;
            List<Equipment> equipmentadd = _equipmentData.GetAllEquipment().Result;
            List<Equipment> equipments = _equipmentData.GetEquipmentbyRoomAsyn(id).Result;
            List<Equipment> equipmentNotInRoom = equipmentadd
                                                .Where(e => !equipments.Any(eq => eq.EquipmentID == e.EquipmentID))
                                                .ToList();
            return View(equipmentNotInRoom);
        }
        [HttpPost]
        public async Task<IActionResult> AddEquipmentToRoom1(Equipment equipment)
        {
            await _equipmentData.AddEquipmentWithRoom(equipment);
            return RedirectToAction("RoomDetail", new { id = equipment.RoomID });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeRoomInformationAsync(Room room)
        {
            await _roomData.UpdateRoom(room);
            return RedirectToAction("RoomDetail", new { id = room.RoomID });
        }

        public IActionResult RoomConsolidation()
        {
            var roomConsolidationList = new List<RoomConsolidation>
     {
         new RoomConsolidation
         {
             FullName = "Nguyễn Văn A",
             StudentID = "SV001",
             OldRoomID = "R101",
             OldRoomName = "Phòng 101",
             OldBuildingID = "B01",
             OldBuildingName = "Tòa nhà A",
             NewRoomID = "R201",
             NewRoomName = "Phòng 201",
             NewBuildingID = "B02",
             NewBuildingName = "Tòa nhà B"
         },
         new RoomConsolidation
         {
             FullName = "Trần Thị B",
             StudentID = "SV002",
             OldRoomID = "R102",
             OldRoomName = "Phòng 102",
             OldBuildingID = "B01",
             OldBuildingName = "Tòa nhà A",
             NewRoomID = "R202",
             NewRoomName = "Phòng 202",
             NewBuildingID = "B02",
             NewBuildingName = "Tòa nhà B"
         },
         new RoomConsolidation
         {
             FullName = "Lê Văn C",
             StudentID = "SV003",
             OldRoomID = "R103",
             OldRoomName = "Phòng 103",
             OldBuildingID = "B01",
             OldBuildingName = "Tòa nhà A",
             NewRoomID = "R203",
             NewRoomName = "Phòng 203",
             NewBuildingID = "B02",
             NewBuildingName = "Tòa nhà B"
         }
     };

            return View(roomConsolidationList);
        }




        [HttpGet]
        public JsonResult GetRoomsByBuilding(string buildingId)
        {
            List<Room> rooms = _roomData.GetAllRoom().Result;
            var rooms1 = rooms.Where(r => r.BuildingID == buildingId && r.NumberOfStudent < r.Capacity).Select(r => new
            {
                roomID = r.RoomID,
                roomName = r.RoomName
            }).ToList();
            return Json(rooms1);
        }
    }
}
