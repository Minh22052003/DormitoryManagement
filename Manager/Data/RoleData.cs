using Manager.Models;
using Newtonsoft.Json;
using System.Text;

namespace Manager.Data
{
    public class RoleData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public RoleData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Role>> GetAllRole()
        {
            List<Role> roles;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            roles = JsonConvert.DeserializeObject<List<Role>>(reponseData);
            return roles;
        }

        public async Task<HttpResponseMessage> Post_AddRoleAsync(Role roles)
        {
            try
            {
                string json = JsonConvert.SerializeObject(roles);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiKey, data);
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
