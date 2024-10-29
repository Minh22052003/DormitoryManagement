using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class RequestData
    {
        private readonly HttpClient _httpClient;
        string Keygetallsupportrequest = "https://localhost:7249/api/SupportRequest/getallsupportrequest";

        public RequestData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Request>> GetAllUtilityMeter()
        {
            List<Request> utilityMeters;
            HttpResponseMessage response = await _httpClient.GetAsync(Keygetallsupportrequest);
            string reponseData = await response.Content.ReadAsStringAsync();
            utilityMeters = JsonConvert.DeserializeObject<List<Request>>(reponseData);
            return utilityMeters;
        }
    }
}
