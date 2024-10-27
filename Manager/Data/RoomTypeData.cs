using Manager.Models;
using Newtonsoft.Json;
using System.Text;

namespace Manager.Data
{
    public class RoomTypeData
    {
        private readonly HttpClient _httpClient;
        string Keygetallroomtype = "https://localhost:7249/api/Room/getallroomtype";

        public RoomTypeData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<RoomType>> GetAllRoomType()
        {
            List<RoomType> roomTypes;
            HttpResponseMessage response = await _httpClient.GetAsync(Keygetallroomtype);
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
