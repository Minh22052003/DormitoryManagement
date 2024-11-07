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
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            // Đường dẫn thư mục lưu ảnh
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "images");

            // Tạo tên file duy nhất
            var uniqueFileName = Guid.NewGuid().ToString()+".jpg";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối để lưu vào cơ sở dữ liệu
            var relativePath = Path.Combine(uniqueFileName);
            return relativePath;
        }
    }
}
