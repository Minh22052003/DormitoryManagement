using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace Manager.Data
{
    public class RoomData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetallroom = "/api/Room/getallroom";

        public RoomData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<Room>> GetAllRoom()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallroom;
            List<Room> utilityMeters;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            utilityMeters = JsonConvert.DeserializeObject<List<Room>>(reponseData);
            return utilityMeters;
        }

        public async Task<HttpResponseMessage> UpdateRoomAsync(Room room)
        {
            string json = JsonConvert.SerializeObject(room);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(keygetallroom, data);
            return response;
        }
    }
}
