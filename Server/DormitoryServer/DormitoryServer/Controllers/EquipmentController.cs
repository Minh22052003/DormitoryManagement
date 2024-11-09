using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public EquipmentController(DataDormitoryContext context)
        {
            _context = context;
        }

        [HttpGet("getallequipment")]
        public IActionResult GetAllEquipment()
        {
            var equipments = _context.Equipment.ToList();
            List<EquipmentDTO> equipment = new List<EquipmentDTO>() { };
            foreach (var item in equipments)
            {
                equipment.Add(new EquipmentDTO
                {
                    EquipmentID = item.EquipmentId,
                    EquipmentName = item.EquipmentName,
                    Price = item.Price
                });
            }
            return Ok(equipment);
        }

        [HttpGet("getequipmentbyid")]
        public IActionResult GetEquipment(int id)
        {
            var equipment = _context.Equipment.FirstOrDefault(e => e.EquipmentId == id);

            return Ok(equipment);
        }
        [HttpGet("getequipmentbyroom")]
        public IActionResult GetEquipmentbyRoom(string idroom)
        {

            var equipments = _context.RoomEquipments.Include(re => re.Equipment).Where(re => re.RoomId == idroom).ToList();
            List<EquipmentDTO> equipment = new List<EquipmentDTO>() { };
            foreach (var item in equipments)
            {
                equipment.Add(new EquipmentDTO
                {
                    EquipmentID = item.EquipmentId,
                    RoomID = item.RoomId,
                    EquipmentName = item.Equipment?.EquipmentName,
                    Price = item.Equipment?.Price,
                    Quantity = item.Quantity,
                    Condition = item.Condition
                });
            }

            return Ok(equipment);
        }

        //[HttpPost("addequipment")]
        //public IActionResult AddEquipment([FromBody] Equipment equipment)
        //{
        //    _context.Equipments.Add(equipment);
        //    _context.SaveChanges();
        //    return Ok();
        //}


        [HttpPut("addequipmenttoroom")]
        public IActionResult AddEquipmentToRoom(EquipmentDTO equipmentDTO)
        {
            var equipmentRoom = new RoomEquipment
            {
                RoomId = equipmentDTO.RoomID,
                EquipmentId = equipmentDTO.EquipmentID,
                Quantity = equipmentDTO.Quantity,
                Condition = "New"
            };
            _context.RoomEquipments.Add(equipmentRoom);
            _context.SaveChanges();
            return Ok();
        }
    }
}
