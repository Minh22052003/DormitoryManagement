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
        public IActionResult Index()
        {
            var newsList = _newsdata.GetAllNews().Result;
            return View(newsList);
        }
    }
}
