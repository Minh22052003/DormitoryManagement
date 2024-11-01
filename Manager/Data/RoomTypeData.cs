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

        //public async Task<HttpResponseMessage> Post_AddRoomTypeAsync(RoomType roomTypes)
        //{
        //    try
        //    {
        //        string json = JsonConvert.SerializeObject(roomTypes);
        //        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(apiKey, data);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
    }
}
