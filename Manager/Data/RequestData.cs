using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class RequestData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public RequestData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Request>> GetAllUtilityMeter()
        {
            List<Request> utilityMeters;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            utilityMeters = JsonConvert.DeserializeObject<List<Request>>(reponseData);
            return utilityMeters;
        }
    }
}
