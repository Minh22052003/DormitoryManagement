using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class RoomInvoiceData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public RoomInvoiceData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<RoomInvoice>> GetAllUtilityMeter()
        {
            List<RoomInvoice> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RoomInvoice>>(reponseData);
            return roomInvoices;
        }
    }
}
