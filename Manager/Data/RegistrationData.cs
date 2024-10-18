using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class RegistrationData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public RegistrationData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<RegistrationVM>> GetAllUtilityMeter()
        {
            List<RegistrationVM> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RegistrationVM>>(reponseData);
            return roomInvoices;
        }
    }
}
