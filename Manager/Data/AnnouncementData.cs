using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class AnnouncementData
    {
        private readonly HttpClient _httpClient;
        string keygetallannouncement = $"https://localhost:7249/api/announcement/getallannouncement";

        public AnnouncementData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Announcement>> GetAllAnnouncement()
        {
            List<Announcement> announcements;
            HttpResponseMessage response = await _httpClient.GetAsync(keygetallannouncement);
            string reponseData = await response.Content.ReadAsStringAsync();
            announcements = JsonConvert.DeserializeObject<List<Announcement>>(reponseData);
            return announcements;
        }

    }
}
