using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class EquipmentData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private string NameUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string GetEquipment = "/api/Equipment/getequipmentbyroom";
        public EquipmentData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            NameUrl = _serverURL.nameurl;
        }
        public async Task<List<Facility>> GetEquipmentInRoom(string roomid)
        {
            List<Facility> facilities = new List<Facility>();
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            string url = $"{NameUrl}{GetEquipment}?idroom={roomid}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                facilities = JsonConvert.DeserializeObject<List<Facility>>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return facilities;
        }
    }
}
