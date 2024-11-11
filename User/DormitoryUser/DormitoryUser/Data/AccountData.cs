using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace DormitoryUser.Data
{
    public class AccountData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string NameUrl;
        string keylogin = $"/api/Auth/loginsv";
        string keychangepassword = "/api/Auth/changepasswordsv";
        string keySign0ut = "/api/Auth/logout";

        public AccountData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            NameUrl = _serverURL.nameurl;
        }

        public async Task<HttpResponseMessage> Post_LoginUserAsync(Login loginuser)
        {
            try
            {
                string json = JsonConvert.SerializeObject(loginuser);
                string url = NameUrl + keylogin;
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, data);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task ChangePassword(ChangePassword changePassword)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = NameUrl + keychangepassword;
            string json = JsonConvert.SerializeObject(changePassword);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }

        public async Task SignOut()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = NameUrl + keySign0ut;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Không thể đăng xuất: " + response.StatusCode);
            }
        }
    }
}
