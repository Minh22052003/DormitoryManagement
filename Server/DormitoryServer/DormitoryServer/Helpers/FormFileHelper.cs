using DormitoryServer.DTOs;
using DormitoryServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryServer.Helpers
{
    public class FormFileHelper : FormFileHelperServices
    {
        public string GetImage(string fileName)
        {
            var filePath = Path.Combine(fileName);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(fileBytes);
            return base64String; 
        }

        public string SaveImage(FileUploadDto fileUploadDto, string rootPath, string subFolder)
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
            return Path.Combine(rootPath, subFolder, fileUploadDto.FileName);
        }

        

    }

}
