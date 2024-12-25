using DormitoryServer.DTOs;
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

        public string SaveImageAsync(FileUploadDto fileUploadDto, string rootPath, string subFolder)
        {
            var targetPath = Path.Combine(rootPath, subFolder);
            if(!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            if(fileUploadDto.FileName== null) {
                fileUploadDto.FileName = fileUploadDto.File.FileName;   
            }
            var fullPath = Path.Combine(targetPath, fileUploadDto.FileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                fileUploadDto.File.CopyTo(stream);
            }
            return Path.Combine(subFolder, fileUploadDto.FileName);
        }



    }

}
