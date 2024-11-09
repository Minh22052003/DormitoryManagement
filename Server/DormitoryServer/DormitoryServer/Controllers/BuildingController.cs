using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("addbuilding")]
        public IActionResult AddBuilding(BuildingDTO buildingDTO)
        {
            Building building = new Building();
            building.BuildingName = buildingDTO.BuildingName;
            _context.Buildings.Add(building);
            _context.SaveChanges();
            return Ok();
        }
    }
}
