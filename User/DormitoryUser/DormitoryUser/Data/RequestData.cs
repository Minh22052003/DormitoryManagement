using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DormitoryUser.Data
{
    public class RequestData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private string NameUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string GetRequest = "/api/SupportRequest/getsupportrequestbystudent";
        string ketcreaterequest = "/api/SupportRequest/createsupportrequest";
        string ketgetrequesttype = "/api/SupportRequest/getrequesttype";
        public RequestData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            NameUrl = _serverURL.nameurl;
        }
        public async Task<List<Request_Sent>> GetRequestStudent()
        {
            List<Request_Sent> facilities = new List<Request_Sent>();
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            string url = NameUrl + GetRequest;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                facilities = JsonConvert.DeserializeObject<List<Request_Sent>>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return facilities;
        }


        public async Task CreateRequest(Request_Sent requestDTO)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = NameUrl + ketcreaterequest;
            string json = JsonConvert.SerializeObject(requestDTO);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                string errorDetail = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorDetail}");
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }

        public async Task<List<RequestType>> GetRequestTypesAsync()
        {
            List<RequestType> requestTypes = new List<RequestType>();
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            string url = NameUrl + ketgetrequesttype;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                requestTypes = JsonConvert.DeserializeObject<List<RequestType>>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return requestTypes;
        }
    }
}
