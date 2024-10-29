using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class StaffData
    {
        private readonly HttpClient _httpClient;
        string keygetallstaff = "https://localhost:7249/api/Staff/getallstaff";

        public StaffData() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            List<Staff> staffs;
            HttpResponseMessage response = await _httpClient.GetAsync(keygetallstaff);
            string reponseData = await response.Content.ReadAsStringAsync();
            staffs = JsonConvert.DeserializeObject<List<Staff>>(reponseData);
            return staffs;
        }
    }
}
