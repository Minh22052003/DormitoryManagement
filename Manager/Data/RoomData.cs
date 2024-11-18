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
        string keygetallroomstatus = "/api/Room/getallroomstatus";
        string keyeditroom = "/api/Room/editroom";
        string keyaddroom = "/api/Room/addroom";


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
            List<Room> rooms;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            rooms = JsonConvert.DeserializeObject<List<Room>>(reponseData);
            return rooms;
        }

        public async Task<List<RoomStatus>> GetAllRoomStatus()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallroomstatus;
            List<RoomStatus> roomStatuses;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            roomStatuses = JsonConvert.DeserializeObject<List<RoomStatus>>(reponseData);
            return roomStatuses;
        }
        

        public async Task UpdateRoom(Room room)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyeditroom;
            string json = JsonConvert.SerializeObject(room);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }

        public async Task AddRoom(Room room)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyaddroom;
            string json = JsonConvert.SerializeObject(room);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, data);

            //if (response.IsSuccessStatusCode)
            //{
            //    string responseData = await response.Content.ReadAsStringAsync();

            //}
            //else
            //{
            //    throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            //}
        }



    }
}
