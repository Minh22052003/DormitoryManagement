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
        string keygetallregistration = "/api/StaffRegistration/getalllregistration";

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

    }
}
