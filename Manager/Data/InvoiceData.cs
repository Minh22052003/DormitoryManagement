using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class InvoiceData
    {
        private readonly HttpClient _httpClient;
        string keygetalldorminvoice = "https://localhost:7249/api/Invoice/getalldorminvoice";
        string keygetallroominvoice = "https://localhost:7249/api/Invoice/getallroominvoice";

        public InvoiceData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DormInvoice>> GetAllDormInvoice()
        {
            List<DormInvoice> dorminvoice;
            HttpResponseMessage response = await _httpClient.GetAsync(keygetalldorminvoice);
            string reponseData = await response.Content.ReadAsStringAsync();
            dorminvoice = JsonConvert.DeserializeObject<List<DormInvoice>>(reponseData);
            return dorminvoice;
        }
        public async Task<List<RoomInvoice>> GetAllRoomInvoice()
        {
            List<RoomInvoice> roominvoice;
            HttpResponseMessage response = await _httpClient.GetAsync(keygetallroominvoice);
            string reponseData = await response.Content.ReadAsStringAsync();
            roominvoice = JsonConvert.DeserializeObject<List<RoomInvoice>>(reponseData);
            return roominvoice;
        }
    }
}
