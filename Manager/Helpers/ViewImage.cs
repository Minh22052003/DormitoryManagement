using Microsoft.AspNetCore.Mvc;
using static Manager.Controllers.ImageController;
using System.Net.Http;

namespace Manager.Helpers
{
    public class ViewImage
    {
        public async Task<string> DisplayImage(FileContentResult fileContentResult)
        {
            
            var base64Image = $"data:{fileContentResult.ContentType};base64,{fileContentResult.FileContents}";

            return base64Image;
        }
    }
}
