using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Helpers
{
    public class FormFileHelper
    {
        public string GetImage(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "images", fileName);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(fileBytes);
            return base64String; 
        }

        // Lưu ảnh vào thư mục
        public async Task<string> SaveImageAsync(string base64File)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "images");

            var uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var base64Data = base64File;
            if (base64File.Contains(","))
            {
                base64Data = base64File.Substring(base64File.IndexOf(",") + 1);
            }

            byte[] imageBytes = Convert.FromBase64String(base64Data);

            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            var relativePath = uniqueFileName;
            return relativePath;
        }

    }
}
