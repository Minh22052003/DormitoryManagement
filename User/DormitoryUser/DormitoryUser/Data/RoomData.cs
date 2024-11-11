using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class RoomData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private string NameUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string GetRoom = "/api/Room/getroombysytudent";
        string Keygetallroomtype = "/api/Room/getallroomtype";

        public RoomData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            NameUrl = _serverURL.nameurl;
        }

        public async Task<Room> GetRoomIn()
        {
            Room room;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            string url = NameUrl + GetRoom;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                room = JsonConvert.DeserializeObject<Room>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return room;
        }

        public async Task<List<RoomType>> GetAllRoomType()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = NameUrl + Keygetallroomtype;
            List<RoomType> roomTypes;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            roomTypes = JsonConvert.DeserializeObject<List<RoomType>>(reponseData);
            return roomTypes;
        }
    }
}
