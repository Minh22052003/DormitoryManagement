using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class RoomInvoiceData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();//Thêm thằng này
        private readonly IHttpContextAccessor _httpContextAccessor;//Thêm thằng này
        private string nameurl;
        string apiKeygetallroominvoicenew = "/api/Invoice/getallroominvoicenew";
        string apiKeyapproveroominvoicenew = "/api/Invoice/approveroominvoicenew";
        string apiKeyupdateroominvoice = "/api/Invoice/updateroominvoice";
        string apiKeyupdatestatusroominvoice = "/api/Invoice/updatestatusroominvoice";

        public RoomInvoiceData(IHttpContextAccessor httpContextAccessor)//Thêm thằng này
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;//Thêm thằng này
            nameurl = _hosting.nameurl;//Thêm thằng này
        }


        public async Task<List<RoomInvoice>> CreateRoomInvoice()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameurl + apiKeygetallroominvoicenew;


            List<RoomInvoice> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(url);


            if (!response.IsSuccessStatusCode)
            {//Thêm thằng này
                throw new Exception(response.StatusCode.ToString());
            }//Thêm thằng này

            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RoomInvoice>>(reponseData);
            return roomInvoices;
        }
        public async Task Approveroominvoicenew()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameurl + apiKeyapproveroominvoicenew;


            List<RoomInvoice> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(url);


            if (!response.IsSuccessStatusCode)
            {//Thêm thằng này
                throw new Exception(response.StatusCode.ToString());
            }//Thêm thằng này

            string reponseData = await response.Content.ReadAsStringAsync();
            
        }

        public async Task UpdateRoomInvoice(RoomInvoice roomInvoice)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameurl + apiKeyupdateroominvoice;
            var json = JsonConvert.SerializeObject(roomInvoice);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public async Task UpdateStatusRoomInvoice(RoomInvoice roomInvoice)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameurl + apiKeyupdatestatusroominvoice;
            var json = JsonConvert.SerializeObject(roomInvoice);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
