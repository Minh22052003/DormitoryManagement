using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
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
                newsDTO.Status = item.Status;
                newsDTO.CreationDate = item.CreationDate;
                newsDTOs.Add(newsDTO);
            }
            return Ok(newsDTOs);
        }


        [HttpGet("getallnewsforstudent")]
        public IActionResult GetAllNewsForStudent()
        {
            var news = _context.News.Include("Staff").Where(n=>n.Status== "Active").ToList();
            List<NewsDTO> newsDTOs = new List<NewsDTO>();
            foreach (var item in news)
            {
                NewsDTO newsDTO = new NewsDTO();
                newsDTO.NewsID = item.NewsId;
                newsDTO.StaffID = item.StaffId;
                newsDTO.StaffName = item.Staff?.FullName;
                newsDTO.Title = item.Title;
                newsDTO.Content = item.Content;
                newsDTO.Status = item.Status;
                newsDTO.CreationDate = item.CreationDate;
                newsDTOs.Add(newsDTO);
            }
            return Ok(newsDTOs);
        }


        [Authorize(Roles = "Admin, Staff")]
        [HttpPost("addnews")]
        public IActionResult AddNews([FromBody] NewsDTO newsDTO)
        {
            News news = new News();
            news.StaffId = newsDTO.StaffID;
            news.Title = newsDTO.Title;
            news.Content = newsDTO.Content;
            news.Status = newsDTO.Status;
            news.CreationDate = newsDTO.CreationDate;
            _context.News.Add(news);
            _context.SaveChanges();
            return Ok();
        }

        //Update news
        [Authorize(Roles = "Admin, Staff")]
        [HttpPut("updatestatusnews")]
        public IActionResult UpdateNews([FromBody] NewsDTO newsDTO)
        {
            var news = _context.News.Where(n => n.NewsId == newsDTO.NewsID).SingleOrDefault();
            if (news == null)
            {
                return NotFound();
            }
            news.Status = newsDTO.Status;
            _context.SaveChanges();
            return Ok();
        }

    }
}
