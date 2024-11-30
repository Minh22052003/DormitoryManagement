using Microsoft.AspNetCore.Mvc;
using DormitoryUser.Models;
using System;
using System.Collections.Generic;
using DormitoryUser.Data;

namespace DormitoryUser.Controllers
{
    public class NewsController : Controller
    {
        private NewsData _newsdata;
        public NewsController(IHttpContextAccessor httpContextAccessor)
        {
            _newsdata = new NewsData(httpContextAccessor);
        }
        public IActionResult Index(int? id =1)
        {
            var news = _newsdata.GetAllNews().Result.FirstOrDefault(n => n.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }

            var newsList = _newsdata.GetAllNews().Result;
            ViewBag.SelectedNews = news;
            return View(newsList);
        }
    }
}
