using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class AnnouncementData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetallannouncement = "/api/announcement/getallannouncement";

        public AnnouncementData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<Announcement>> GetAllAnnouncement()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallannouncement;
            List<Announcement> announcements;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            announcements = JsonConvert.DeserializeObject<List<Announcement>>(reponseData);
            return announcements;
        }
    }
}
