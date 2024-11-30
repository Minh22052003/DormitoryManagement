using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class RoomTypeData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string Keygetallroomtype = "/api/Room/getallroomtype";
        string Keygetroomtypebyid = "/api/Room/getroomtypebyid";
        string Keyaddroomtype = "/api/Room/addroomtype";

        public RoomTypeData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<RoomType>> GetAllRoomType()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + Keygetallroomtype;
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

        public async Task<RoomType> GetRoomTypeByID(int? id)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + Keygetroomtypebyid;
            RoomType roomType;
            string urll = $"{url}?id={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(urll);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            roomType = JsonConvert.DeserializeObject<RoomType>(reponseData);
            return roomType;
        }

        public async Task AddRoomtype(RoomType roomType)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + Keyaddroomtype;
            string json = JsonConvert.SerializeObject(roomType);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, data);

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
