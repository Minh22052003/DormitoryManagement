using Manager.Models;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<List<DormInvoice>> GetAllDormInvoice()
        {
            List<DormInvoice> dormInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(apiKey);
            string reponseData = await response.Content.ReadAsStringAsync();
            dormInvoices = JsonConvert.DeserializeObject<List<DormInvoice>>(reponseData);
            return dormInvoices;
        }
        public async Task<HttpResponseMessage> Post_AddBuildingAsync(DormInvoice dormInvoice)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dormInvoice);
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
