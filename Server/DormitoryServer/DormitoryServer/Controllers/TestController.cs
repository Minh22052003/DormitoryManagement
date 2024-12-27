using DormitoryServer.DTOs;
using DormitoryServer.Helpers;
using DormitoryServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private Test test = new Test();
        private FormFileHelperServices _formFileHelper;
        public TestController(FormFileHelperServices formFileHelperServices)
        {
            _formFileHelper = formFileHelperServices;
        }

        [HttpGet("getimage")]
        public IActionResult Test()
        {
            decimal a = 0;
            var image = _formFileHelper.GetImage("D:\\Project1\\Server\\DormitoryServer\\DormitoryServer\\Uploads\\images\\minhdz.jpg");

            return Ok(image);
            
        }

        [HttpPost("postimage")]
        public IActionResult PostImage([FromForm] FileUploadDto t1)
        {
            var rootPath = "D:\\Project1\\Server\\DormitoryServer\\DormitoryServer\\Uploads";
            var subFolder = "images";
            string Name = _formFileHelper.SaveImage(t1,rootPath,subFolder);
            return Ok(Name);
        }
    }
}
