using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class DormInvoiceData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetalldorminvoice = "/api/Student/getallstudent";

        public DormInvoiceData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<DormInvoice>> GetAllDormInvoice()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetalldorminvoice;
            List<DormInvoice> dormInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
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
                HttpResponseMessage response = await _httpClient.PostAsync(keygetalldorminvoice, data);
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
