using Manager.Models;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace Manager.Data
{
    public class RoomData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public RoomData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Room>> GetAllUtilityMeter()
        {
            List<Room> utilityMeters;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            utilityMeters = JsonConvert.DeserializeObject<List<Room>>(reponseData);
            return utilityMeters;
        }

        public async Task<HttpResponseMessage> UpdateRoomAsync(Room room)
        {
            string json = JsonConvert.SerializeObject(room);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(apiKey, data);
            return response;
        }
    }
}
