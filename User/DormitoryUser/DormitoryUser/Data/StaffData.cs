using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class StaffData
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string keygetallstaff = "https://localhost:7249/api/Staff/getallstaff";
        string keygetstaff = "https://localhost:7249/api/Staff/getprofilestaff";

        public StaffData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<InfoStaff>> GetAllStaffAsync()
        {
            List<InfoStaff> staffs;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(keygetallstaff);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                staffs = JsonConvert.DeserializeObject<List<InfoStaff>>(responseData);
            }
            else
            {
                // Xử lý lỗi
                // Có thể ném ngoại lệ hoặc trả về null
                throw new Exception("Không thể lấy thông tin nhân viên: " + response.StatusCode);
            }
            return staffs;
        }

    }
}
