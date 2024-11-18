using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public NewsController(DataDormitoryContext context)
        {
            _context = context;
        }

        [HttpGet("getallnews")]
        public IActionResult GetAllNews()
        {
            var news = _context.News.Include("Staff").ToList();
            List<NewsDTO> newsDTOs = new List<NewsDTO>();
            foreach (var item in news)
            {
                NewsDTO newsDTO = new NewsDTO();
                newsDTO.NewsID = item.NewsId;
                newsDTO.StaffID = item.StaffId;
                newsDTO.StaffName = item.Staff?.FullName;
                newsDTO.Title = item.Title;
                newsDTO.Content = item.Content;
                newsDTO.Tag = item.Tag;
                newsDTO.Status = item.Status;
                newsDTO.CreationDate = item.CreationDate;
                newsDTOs.Add(newsDTO);
            }
            return Ok(newsDTOs);
        }

    }
}
