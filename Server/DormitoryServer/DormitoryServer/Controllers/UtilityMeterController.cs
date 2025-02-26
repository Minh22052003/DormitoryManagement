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
    public class UtilityMeterController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public UtilityMeterController(DataDormitoryContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Staff,Manager, Admin")]
        [HttpGet("getallutilitymeter")]
        public ActionResult<List<UtilityMeterDTO>> GetAllUtilityMeter()
        {
            var utilityMeters = _context.UtilityMeters
                .Include(u => u.Room)
                    .ThenInclude(r => r.Building)
                .Include(u => u.Staff)
                .ToList();
            List<UtilityMeterDTO> result = new List<UtilityMeterDTO>();
            foreach (var utilityMeter in utilityMeters)
            {
                result.Add(new UtilityMeterDTO
                {
                    UtilityMeterID = utilityMeter.UtilityMeterId,
                    RoomID = utilityMeter.RoomId,
                    RoomName = utilityMeter.Room?.RoomName,
                    BuildingID = utilityMeter.Room?.BuildingId,
                    BuildingName = utilityMeter.Room?.Building?.BuildingName,
                    StaffID = utilityMeter.StaffId,
                    StaffName = utilityMeter.Staff?.FullName,
                    Electricity = utilityMeter.Electricity,
                    Water = utilityMeter.Water,
                    RecordingDate = utilityMeter.RecordingDate
                });
            }
            return result;
        }


        [Authorize(Roles = "Staff,Manager, Admin")]
        [HttpPost("addutilitymeter")]
        public ActionResult AddUtilityMeter(UtilityMeterDTO utilityMeterDTO)
        {
            var staffid = User.FindFirst("UserID").Value;
            var utilityMeter = new UtilityMeter
            {
                RoomId = utilityMeterDTO.RoomID,
                StaffId = staffid,
                Electricity = utilityMeterDTO.Electricity,
                Water = utilityMeterDTO.Water,
                RecordingDate = DateTime.Now,
            };
            _context.UtilityMeters.Add(utilityMeter);
            _context.SaveChanges();
            return Ok();
        }


        [Authorize(Roles = "Staff,Manager, Admin")]
        [HttpPut("updateutilitymeter")]
        public ActionResult EditUtilityMeter(UtilityMeterDTO utilityMeterDTO)
        {
            var utilityMeter = _context.UtilityMeters.Find(utilityMeterDTO.UtilityMeterID);
            utilityMeter.Electricity = utilityMeterDTO.Electricity;
            utilityMeter.Water = utilityMeterDTO.Water;
            _context.UtilityMeters.Update(utilityMeter);
            _context.SaveChanges();
            return Ok();
        }
    }
}
