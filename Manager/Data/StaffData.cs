using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class StaffData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public StaffData() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            List<Staff> staffs;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            staffs = JsonConvert.DeserializeObject<List<Staff>>(reponseData);
            return staffs;
        }
    }
}
