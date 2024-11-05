using DormitoryUser.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class RequestData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private string NameUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string GetRequest = "/api/SupportRequest/getsupportrequestbystudent";
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
            string url = NameUrl+GetRequest;
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
    }
}
