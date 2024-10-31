﻿using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Authorize(Policy = "Manager")]
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
                    NumberOfStudent = room.NumberOfStudent,
                    Capacity = room.RoomType?.Capacity,
                    RoomStatusID = room.RoomStatusId,
                    RoomStatusName = room.RoomStatus?.RoomStatusName,
                    RoomNote = room.RoomNote
                });
            }
            return roomDTOs;
        }

        [Authorize(Policy = "Manager")]
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


        [Authorize(Policy = "Student")]
        [HttpGet("getroombysytudent")]
        public async Task<ActionResult<RoomDTO>> GetRoomByStudent()
        {
            var studentId = User.FindFirst("UserId")?.Value;
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound("Không tìm thấy sinh viên");
            }
            var room = await _context.Rooms.FindAsync(student.RoomId);
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
                NumberOfStudent = room.NumberOfStudent,
                Capacity = room.RoomType?.Capacity,
                RoomStatusID = room.RoomStatusId,
                RoomStatusName = room.RoomStatus?.RoomStatusName,
                RoomNote = room.RoomNote
            };
            return roomDTO;
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
