using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DormitoryUser.Data
{
    public class RegistrationData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private string NameUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string keyaddregistration = "/api/Registration/addregistration";
        public RegistrationData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            NameUrl = _serverURL.nameurl;
        }
        public async Task CreateRequest(RegistrationVM registration)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = NameUrl + keyaddregistration;
            string json = JsonConvert.SerializeObject(registration);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                string errorDetail = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorDetail}");
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }
    }
}
