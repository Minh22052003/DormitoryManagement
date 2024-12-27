using DormitoryServer.DTOs;

namespace DormitoryServer.Services
{
    public interface FormFileHelperServices
    {
        string GetImage(string fileName);
        string SaveImage(FileUploadDto fileUploadDto, string rootPath, string subFolder);
    }
}
