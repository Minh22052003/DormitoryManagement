using Manager.ModelRequest;
using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace Manager.Data
{
    public class AccountData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keylogin = "/api/Auth/loginnv";
        string keysignup = "/api/Auth/signupnv";
        string keysignout = "/api/Auth/logout";
        string keygetallregistration = "/api/StaffRegistration/getalllregistration";
        string keyacceptregistration = "/api/Auth/acceptaccount";
        string keyrejectregistration = "/api/Auth/rejectaccount";
        string keychangepassword = "/api/Auth/changepassword";

        public AccountData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<HttpResponseMessage> Post_LoginUserAsync(LoginAcc loginuser)
        {
            try
            {
                string json = JsonConvert.SerializeObject(loginuser);
                var url = nameURL + keylogin;
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
        public async Task<HttpResponseMessage> Post_SignUpUserAsync(StaffRegistration staffRegistration)
        {
            try
            {
                string json = JsonConvert.SerializeObject(staffRegistration);
                var url = nameURL + keysignup;
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

        public async Task<List<StaffRegistration>> GetAllStaffRegistration()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallregistration;
            List<StaffRegistration> registrations;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            registrations = JsonConvert.DeserializeObject<List<StaffRegistration>>(reponseData);
            return registrations;
        }

        public async Task AcceptAccount(AccountStaff accountStaff)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyacceptregistration;
            string json = JsonConvert.SerializeObject(accountStaff);
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

        public async Task RejectAccount(AccountStaff accountStaff)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyrejectregistration;
            string json = JsonConvert.SerializeObject(accountStaff);
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

        public async Task ChangePassword(ChangePassword changePassword)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keychangepassword;
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
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keysignout;
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
