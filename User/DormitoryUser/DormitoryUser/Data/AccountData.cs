using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace DormitoryUser.Data
{
    public class AccountData
    {
        private readonly HttpClient _httpClient;
        private readonly ServerURL _serverURL = new ServerURL();
        private string NameUrl;
        string keylogin = $"/api/Auth/loginsv";

        public AccountData()
        {
            _httpClient = new HttpClient();
            NameUrl = _serverURL.URL;
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
    }
}
