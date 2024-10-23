using Manager.Models;
using Newtonsoft.Json;
using System.Text;

namespace Manager.Data
{
    public class EquipmentData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public EquipmentData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Equipment>> GetAllEquipment()
        {
            List<Equipment> equipment;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            equipment = JsonConvert.DeserializeObject<List<Equipment>>(reponseData);
            return equipment;
        }

        public async Task<HttpResponseMessage> Post_AddEquipmentAsync(Equipment equipment)
        {
            try
            {
                string json = JsonConvert.SerializeObject(equipment);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiKey, data);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
