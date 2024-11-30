using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class RoomData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetallroom = "/api/Room/getallroom";
        string keygetroombyid = "/api/Room/getroombyid";
        string keygetallroomstatus = "/api/Room/getallroomstatus";
        string keyeditroom = "/api/Room/editroom";
        string keyaddroom = "/api/Room/addroom";
        string keydonphong = "/api/Room/donphong";


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


        public async Task<Room> GetRoomByID(string idRoom)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetroombyid;
            Room room;
            string urll = $"{url}?idRoom={idRoom}";
            HttpResponseMessage response = await _httpClient.GetAsync(urll);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            room = JsonConvert.DeserializeObject<Room>(reponseData);
            return room;
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

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            if (!response.IsSuccessStatusCode)
            {
                string errorDetail = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorDetail}");
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }

        public async Task DonPhong(List<Student> students)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keydonphong;
            string json = JsonConvert.SerializeObject(students);
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
    }
}
