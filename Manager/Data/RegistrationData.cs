using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class RegistrationData
    {
        private readonly HttpClient _httpClient;
        string Keygetallregistration = "https://localhost:7249/api/Registration/getallregistration";

        public RegistrationData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<RegistrationVM>> GetAllRegistration()
        {
            List<RegistrationVM> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(Keygetallregistration);
            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RegistrationVM>>(reponseData);
            return roomInvoices;
        }
    }
}
