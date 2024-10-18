using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class DormInvoiceData
    {
        private readonly HttpClient _httpClient;
        string apiKey = "https://localhost:7249/api/Student/getallstudent";

        public DormInvoiceData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DormInvoice>> GetAllUtilityMeter()
        {
            List<DormInvoice> dormInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            dormInvoices = JsonConvert.DeserializeObject<List<DormInvoice>>(reponseData);
            return dormInvoices;
        }
    }
}
