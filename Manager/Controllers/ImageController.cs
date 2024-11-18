using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class ImageController : Controller
    {
        private readonly HttpClient _httpClient;

        public ImageController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> DisplayImage()
        {
            var response = await _httpClient.GetAsync("https://localhost:7249/api/Test/getimage");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error"); // Hiển thị trang lỗi nếu gọi API không thành công
            }

            var data = await response.Content.ReadFromJsonAsync<ImageResponse>();

            // Tạo URL Data URI từ dữ liệu Base64
            var base64Image = $"data:{data.ContentType};base64,{data.FileContents}";

            // Gửi URL Data URI đến View
            ViewBag.ImageData = base64Image;
            return View();
        }

        public class ImageResponse
        {
            public string FileContents { get; set; }
            public string ContentType { get; set; }
        }
    }
}
