using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class AnnouncementData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public AnnouncementData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Announcement>> GetAllNews()
        {
            List<Announcement> announcements;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            announcements = JsonConvert.DeserializeObject<List<Announcement>>(reponseData);
            return announcements;
        }
    }
}
