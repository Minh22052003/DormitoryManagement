using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Manager.Data
{
    public class InvoiceData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetalldorminvoice = "/api/Invoice/getalldorminvoice";
        string keygetallroominvoice = "/api/Invoice/getallroominvoice";

        public InvoiceData(IHttpContextAccessor httpContextAccessor)
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
            List<DormInvoice> dorminvoice;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            dorminvoice = JsonConvert.DeserializeObject<List<DormInvoice>>(reponseData);
            return dorminvoice;
        }
        public async Task<List<RoomInvoice>> GetAllRoomInvoice()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallroominvoice;
            List<RoomInvoice> roominvoice;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            roominvoice = JsonConvert.DeserializeObject<List<RoomInvoice>>(reponseData);
            return roominvoice;
        }
    }
}
