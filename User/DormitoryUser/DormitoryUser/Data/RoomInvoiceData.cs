using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class RoomInvoiceData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();//Thêm thằng này
        private readonly IHttpContextAccessor _httpContextAccessor;//Thêm thằng này
        private string nameurl;//Thêm thằng này
        string apiKey = "/api/Invoice/getallroominvoicebystudent";//Loại bỏ đường dẫn cứng

        public RoomInvoiceData(IHttpContextAccessor httpContextAccessor)//Thêm thằng này
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;//Thêm thằng này
            nameurl = _hosting.nameurl;//Thêm thằng này
        }

        public async Task<List<RoomInvoice>> GetAllRoomInvoice()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameurl + apiKey;


            List<RoomInvoice> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(url);


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RoomInvoice>>(reponseData);
            return roomInvoices;
        }
    }
}
