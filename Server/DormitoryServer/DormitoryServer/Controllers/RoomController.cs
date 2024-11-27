using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public RoomController(DataDormitoryContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("getallroom")]
        public async Task<ActionResult<List<RoomDTO>>> GetRooms()
        {
            var rooms = await _context.Rooms.Include("Building").Include("RoomType").Include("RoomStatus").ToListAsync();
            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach (var room in rooms)
            {
                roomDTOs.Add(new RoomDTO
                {
                    RoomID = room.RoomId,
                    RoomTypeID = room.RoomTypeId,
                    BuildingID = room.BuildingId,
                    BuildingName = room.Building?.BuildingName,
                    LeaderID = getLeader(room.RoomId)?.StudentId ?? "Unknown",
                    LeaderName = getLeader(room.RoomId)?.FullName ?? "Unknown",
                    RoomName = room.RoomName,
                    RoomPrice = room.RoomType?.RoomPrice,
                    NumberOfStudent = room.NumberOfStudent,
                    Capacity = room.RoomType?.Capacity,
                    RoomStatusID = room.RoomStatusId,
                    RoomStatusName = room.RoomStatus?.RoomStatusName,
                    RoomNote = room.RoomNote
                });
            }
            return roomDTOs;
        }

        [Authorize]
        [HttpGet("getallroomtype")]
        public async Task<ActionResult<List<RoomTypeDTO>>> GetRoomTypes()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();
            List<RoomTypeDTO> roomTypeDTOs = new List<RoomTypeDTO>();
            foreach (var roomType in roomTypes)
            {
                roomTypeDTOs.Add(new RoomTypeDTO
                {
                    RoomTypeID = roomType.RoomTypeId,
                    RoomTypeName = roomType.RoomTypeName,
                    Capacity = roomType.Capacity,
                    RoomPrice = roomType.RoomPrice
                });
            }
            return roomTypeDTOs;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addroomtype")]
        public async Task<ActionResult<RoomTypeDTO>> AddRoomType(RoomTypeDTO roomTypeDTO)
        {
            var roomType = new RoomType
            {
                RoomTypeName = roomTypeDTO.RoomTypeName,
                Capacity = roomTypeDTO.Capacity,
                RoomPrice = roomTypeDTO.RoomPrice
            };
            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRoomTypes", new { id = roomType.RoomTypeId }, roomTypeDTO);
        }


        [Authorize(Roles = "Student")]
        [HttpGet("getroombysytudent")]
        public async Task<ActionResult<RoomDTO>> GetRoomByStudent()
        {
            var studentId = User.FindFirst("UserId")?.Value;
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound("Không tìm thấy sinh viên");
            }
            var room = _context.Rooms.Include("Building").Include("RoomType").Include("RoomStatus").Where(r=>r.RoomId==student.RoomId).FirstOrDefault();
            if (room == null)
            {
                return NotFound("Không tìm thấy phòng");
            }
            var roomDTO = new RoomDTO
            {
                RoomID = room.RoomId,
                RoomTypeID = room.RoomTypeId,
                BuildingID = room.BuildingId,
                BuildingName = room.Building?.BuildingName,
                LeaderID = getLeader(room.RoomId)?.StudentId ?? "Unknown",
                LeaderName = getLeader(room.RoomId)?.FullName ?? "Unknown",
                RoomName = room.RoomName,
                RoomPrice = room.RoomType?.RoomPrice,
                NumberOfStudent = room.NumberOfStudent,
                Capacity = room.RoomType?.Capacity,
                RoomStatusID = room.RoomStatusId,
                RoomStatusName = room.RoomStatus?.RoomStatusName,
                RoomNote = room.RoomNote
            };
            return roomDTO;
        }

        [Authorize]
        [HttpGet("getallroomstatus")]
        public async Task<ActionResult<List<RoomStatusDTO>>> GetRoomStatus()
        {
            var roomStatuses = await _context.RoomStatuses.ToListAsync();
            List<RoomStatusDTO> roomStatusDTOs = new List<RoomStatusDTO>();
            foreach (var roomStatus in roomStatuses)
            {
                roomStatusDTOs.Add(new RoomStatusDTO
                {
                    RoomStatusID = roomStatus.RoomStatusId,
                    RoomStatusName = roomStatus.RoomStatusName
                });
            }
            return roomStatusDTOs;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("addroom")]
        public async Task<IActionResult> AddRoom(RoomDTO roomDTO)
        {
            var room = new Room
            {
                RoomId = GenerateUniqueIdMD5(roomDTO.RoomName),
                RoomTypeId = roomDTO.RoomTypeID,
                BuildingId = roomDTO.BuildingID,
                RoomName = roomDTO.RoomName,
                RoomStatusId = roomDTO.RoomStatusID,
                NumberOfStudent = 0,
                RoomNote = roomDTO.RoomNote
            };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return Ok();
        }
        private string GenerateUniqueIdMD5(string name)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(name);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString().Substring(0, 10);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("editroom")]
        public async Task<IActionResult> EditRoom(RoomDTO roomDTO)
        {
            var room = await _context.Rooms.FindAsync(roomDTO.RoomID);
            if (room == null)
            {
                return NotFound("Không tìm thấy phòng");
            }
            room.RoomName = roomDTO.RoomName;
            room.RoomStatusId = roomDTO.RoomStatusID;
            room.RoomNote = roomDTO.RoomNote;
            _context.SaveChanges();
            return NoContent();
        }


        private Student getLeader(string idroom)
        {
            var leader = _context.Students.Where(s => s.IsLeader == true && s.RoomId == idroom).FirstOrDefault();
            if (leader == null)
            {
                return null;
            }
            return leader;
        }


    }
}
