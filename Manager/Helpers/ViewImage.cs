using Microsoft.AspNetCore.Mvc;
using static Manager.Controllers.ImageController;
using System.Net.Http;

namespace Manager.Helpers
{
    public class ViewImage
    {
        public async Task<string> ConvertFormFileToBase64Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                byte[] fileBytes = memoryStream.ToArray();

                string base64String = Convert.ToBase64String(fileBytes);

                return base64String;
            }
        }
    }
}
