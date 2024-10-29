using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class UtilityMeterData
    {
        private readonly HttpClient _httpClient;
        string KeyGetAllUtilityMeter = "https://localhost:7249/api/UtilityMeter/getallutilitymeter";

        public UtilityMeterData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<UtilityMeter>> GetAllUtilityMeter()
        {
            List<UtilityMeter> utilityMeters;
            HttpResponseMessage response = await _httpClient.GetAsync(KeyGetAllUtilityMeter);
            string reponseData = await response.Content.ReadAsStringAsync();
            utilityMeters = JsonConvert.DeserializeObject<List<UtilityMeter>>(reponseData);
            return utilityMeters;
        }
    }
}
