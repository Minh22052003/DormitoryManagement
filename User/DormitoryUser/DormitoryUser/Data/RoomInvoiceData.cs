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
        string apiKey = "/api/Invoice/getallroominvoice";//Loại bỏ đường dẫn cứng

        public RoomInvoiceData(IHttpContextAccessor httpContextAccessor)//Thêm thằng này
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;//Thêm thằng này
            nameurl = _hosting.nameurl;//Thêm thằng này
        }

        public async Task<List<RoomInvoice>> GetAllRoomInvoice()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");//Thêm thằng này
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);//Thêm thằng này
            var url = nameurl + apiKey;//Thêm thằng này


            List<RoomInvoice> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(url);//thay key


            if (!response.IsSuccessStatusCode)//Thêm thằng này
            {//Thêm thằng này
                throw new Exception(response.StatusCode.ToString());//Thêm thằng này
            }//Thêm thằng này

            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RoomInvoice>>(reponseData);
            return roomInvoices;
        }
    }
}
