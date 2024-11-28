using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Reflection;

namespace Manager.Controllers
{
    public class RoomController : Controller
    {
        private RoomData _roomData;
        private StudentData _studentData;
        private EquipmentData _equipmentData;
        private BuildingData _buildingData;
        private RoomTypeData _roomtypeData;
        public RoomController(IHttpContextAccessor httpContextAccessor)
        {
            _roomData = new RoomData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
            _equipmentData = new EquipmentData(httpContextAccessor);
            _buildingData = new BuildingData(httpContextAccessor);
            _roomtypeData = new RoomTypeData(httpContextAccessor);

        }
        public IActionResult Room()
        {
            try
            {
                List<Room> rooms = _roomData.GetAllRoom().Result;
                return View(rooms);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return RedirectToAction("Error401");
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
            var student = students.Where(s => s.RoomID == null).ToList();
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
            var roomConsolidationList = new List<RoomConsolidation>();
            List<Room> rooms = _roomData.GetAllRoom().Result;
            List<Building> buildings = _buildingData.GetAllBuilding().Result;
            List<RoomType> roomTypes = _roomtypeData.GetAllRoomType().Result;
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            var roomsSorted = rooms
             .OrderBy(r =>
             {
                 if (int.TryParse(r.RoomName.Substring(r.RoomName.Length - 3), out int roomNumber))
                     return roomNumber;
                 return int.MaxValue; // Nếu không parse được, đưa về cuối danh sách
             })
             .ToList();

            // Dồn sinh viên
            foreach (var room in roomsSorted)
            {
                var roomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == room.RoomTypeID);
                if (roomType == null || room.NumberOfStudent >= roomType.Capacity)
                    continue;

                foreach (var donorRoom in roomsSorted.Skip(roomsSorted.IndexOf(room) + 1))
                {
                    var donorRoomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == donorRoom.RoomTypeID);

                    if (donorRoomType == null || donorRoom.NumberOfStudent <= 0)
                        continue;

                    var studentsFromDonorRoom = students
                        .Where(s => s.RoomID == donorRoom.RoomID)
                        .OrderBy(s => int.Parse(s.StudentID.Substring(2)))
                        .ToList();

                    while (room.NumberOfStudent < roomType.Capacity && studentsFromDonorRoom.Count > 0)
                    {
                        var studentToMove = studentsFromDonorRoom.First();
                        studentsFromDonorRoom.Remove(studentToMove);

                        roomConsolidationList.Add(new RoomConsolidation
                        {
                            StudentID = studentToMove.StudentID,
                            FullName = studentToMove.FullName,
                            OldRoomID = studentToMove.RoomID,
                            OldRoomName = studentToMove.RoomName,
                            OldBuildingID = studentToMove.BuildingID,
                            OldBuildingName = buildings.FirstOrDefault(b => b.BuildingID == studentToMove.BuildingID)?.BuildingName,
                            NewRoomID = room.RoomID,
                            NewRoomName = room.RoomName,
                            NewBuildingID = room.BuildingID,
                            NewBuildingName = buildings.FirstOrDefault(b => b.BuildingID == room.BuildingID)?.BuildingName
                        });

                        studentToMove.RoomID = room.RoomID;
                        room.NumberOfStudent++;
                        donorRoom.NumberOfStudent--;
                    }

                    if (room.NumberOfStudent >= roomType.Capacity)
                        break;
                }
            }

            return View(roomConsolidationList);
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var roomConsolidationList = new List<RoomConsolidation>();
            List<Room> rooms = _roomData.GetAllRoom().Result;
            List<Building> buildings = _buildingData.GetAllBuilding().Result;
            List<RoomType> roomTypes = _roomtypeData.GetAllRoomType().Result;
            List<Student> students = _studentData.GetAllStudentAsyn().Result;
            var roomsSorted = rooms
             .OrderBy(r =>
             {
                 if (int.TryParse(r.RoomName.Substring(r.RoomName.Length - 3), out int roomNumber))
                     return roomNumber;
                 return int.MaxValue;
             })
             .ToList();

            foreach (var room in roomsSorted)
            {
                var roomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == room.RoomTypeID);
                if (roomType == null || room.NumberOfStudent >= roomType.Capacity)
                    continue;

                foreach (var donorRoom in roomsSorted.Skip(roomsSorted.IndexOf(room) + 1))
                {
                    var donorRoomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == donorRoom.RoomTypeID);

                    if (donorRoomType == null || donorRoom.NumberOfStudent <= 0)
                        continue;

                    var studentsFromDonorRoom = students
                        .Where(s => s.RoomID == donorRoom.RoomID)
                        .OrderBy(s => int.Parse(s.StudentID.Substring(2)))
                        .ToList();

                    while (room.NumberOfStudent < roomType.Capacity && studentsFromDonorRoom.Count > 0)
                    {
                        var studentToMove = studentsFromDonorRoom.First();
                        studentsFromDonorRoom.Remove(studentToMove);

                        roomConsolidationList.Add(new RoomConsolidation
                        {
                            StudentID = studentToMove.StudentID,
                            FullName = studentToMove.FullName,
                            OldRoomID = studentToMove.RoomID,
                            OldRoomName = studentToMove.RoomName,
                            OldBuildingID = studentToMove.BuildingID,
                            OldBuildingName = buildings.FirstOrDefault(b => b.BuildingID == studentToMove.BuildingID)?.BuildingName,
                            NewRoomID = room.RoomID,
                            NewRoomName = room.RoomName,
                            NewBuildingID = room.BuildingID,
                            NewBuildingName = buildings.FirstOrDefault(b => b.BuildingID == room.BuildingID)?.BuildingName
                        });

                        studentToMove.RoomID = room.RoomID;
                        room.NumberOfStudent++;
                        donorRoom.NumberOfStudent--;
                    }

                    if (room.NumberOfStudent >= roomType.Capacity)
                        break;
                }
            }
            var data = roomConsolidationList; // Thay bằng dữ liệu thực tế của bạn, ví dụ từ cơ sở dữ liệu.

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách chuyển phòng "+DateTime.Now);

                // Tạo tiêu đề cột
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Mã sinh viên";
                worksheet.Cells[1, 3].Value = "Tên sinh viên";
                worksheet.Cells[1, 4].Value = "Tòa nhà cũ";
                worksheet.Cells[1, 5].Value = "Phòng cũ";
                worksheet.Cells[1, 6].Value = "Tòa nhà mới";
                worksheet.Cells[1, 7].Value = "Phòng mới";

                // Ghi dữ liệu
                int row = 2;
                int index = 1;
                foreach (var r in data)
                {
                    worksheet.Cells[row, 1].Value = index;
                    worksheet.Cells[row, 2].Value = r.StudentID;
                    worksheet.Cells[row, 3].Value = r.FullName;
                    worksheet.Cells[row, 4].Value = r.OldBuildingName;
                    worksheet.Cells[row, 5].Value = r.OldRoomName;
                    worksheet.Cells[row, 6].Value = r.NewBuildingName;
                    worksheet.Cells[row, 7].Value = r.NewRoomName;

                    row++;
                    index++;
                }

                // Lưu vào stream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Trả về file Excel
                string fileName = "DanhSachChuyenPhong.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileName);
            }
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
        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }
    }
}
