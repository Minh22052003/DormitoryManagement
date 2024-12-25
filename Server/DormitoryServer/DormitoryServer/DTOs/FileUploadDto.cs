namespace DormitoryServer.DTOs
{
    public class FileUploadDto
    {
        public string? FileName { get; set; }
        public IFormFile File { get; set; }
        public string? Description { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}
