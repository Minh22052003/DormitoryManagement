using Manager.Models;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace Manager.Data
{
    public class AccountData
    {
        private readonly HttpClient _httpClient;
        string keylogin = $"https://localhost:7249/api/Auth/loginnv";

        public AccountData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> Post_LoginUserAsync(LoginAcc loginuser)
        {
            try
            {
                string json = JsonConvert.SerializeObject(loginuser);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(keylogin, data);
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
