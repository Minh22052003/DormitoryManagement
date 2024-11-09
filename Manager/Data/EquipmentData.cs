using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class EquipmentData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string getallEquipment = "/api/Equipment/getallequipment";
        string getEquipmentbyRoom = "/api/Equipment/getequipmentbyroom";
        string addEquipmenttoRoom = "/api/Equipment/addequipmenttoroom";

        public EquipmentData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }
        public async Task<List<Equipment>> GetAllEquipment()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + getallEquipment;
            List<Equipment> equipment;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string responsData = await response.Content.ReadAsStringAsync();
            equipment = JsonConvert.DeserializeObject<List<Equipment>>(responsData);
            return equipment;
        }

        public async Task<List<Equipment>> GetEquipmentbyRoomAsyn(string id)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var urll = nameURL + getEquipmentbyRoom;
            List<Equipment> equipment;
            string url = $"{urll}?idroom={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responsData = await response.Content.ReadAsStringAsync();
            equipment = JsonConvert.DeserializeObject<List<Equipment>>(responsData);
            return equipment;
        }

        public async Task AddEquipmentWithRoom(Equipment equipment)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + addEquipmenttoRoom;
            string json = JsonConvert.SerializeObject(equipment);
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






        //public async Task<HttpResponseMessage> Post_AddEquipmentAsync(Equipment equipment)
        //{
        //    try
        //    {
        //        string json = JsonConvert.SerializeObject(equipment);
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
