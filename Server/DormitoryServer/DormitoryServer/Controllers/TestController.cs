using DormitoryServer.DTOs;
using DormitoryServer.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private Test test = new Test();
        private FormFileHelper _formFileHelper = new FormFileHelper();


        [HttpGet("getimage")]
        public IActionResult Test()
        {
            var image = _formFileHelper.GetImage("4ea8ca80-49e9-49cd-8b32-59195050033b.jpd");
            return Ok(image);
        }

        [HttpPost("postimage")]
        public IActionResult PostImage([FromForm] FileUploadDto t1)
        {
            var rootPath = "D:\\Project1\\Server\\DormitoryServer\\DormitoryServer\\Uploads";
            var subFolder = "images";
            string Name = _formFileHelper.SaveImageAsync(t1,rootPath,subFolder);
            return Ok(Name);
        }
    }
}
