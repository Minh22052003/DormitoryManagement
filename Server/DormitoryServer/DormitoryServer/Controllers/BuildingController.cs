using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public BuildingController(DataDormitoryContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("getallbuilding")]
        public IActionResult GetAllBuilding()
        {
            var buildings = _context.Buildings.ToList();
            List<BuildingDTO> buildingDTOs = new List<BuildingDTO>(); 
            foreach (var building in buildings)
            {
                var dsroom = _context.Rooms.Where(r => r.BuildingId == building.BuildingId).ToList();

                BuildingDTO buildingDTO = new BuildingDTO();
                buildingDTO.BuildingID = building.BuildingId;
                buildingDTO.BuildingName = building.BuildingName;
                buildingDTO.RoomCount = dsroom.Count;
                buildingDTOs.Add(buildingDTO);
            }
            return Ok(buildingDTOs);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("addbuilding")]
        public IActionResult AddBuilding(BuildingDTO buildingDTO)
        {
            Building building = new Building();
            building.BuildingId = GenerateUniqueIdMD5(buildingDTO.BuildingName);
            building.BuildingName = buildingDTO.BuildingName;
            _context.Buildings.Add(building);
            _context.SaveChanges();
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
    }
}
