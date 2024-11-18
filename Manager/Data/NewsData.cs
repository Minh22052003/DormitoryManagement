using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Manager.Data
{
    public class NewsData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetallnews = "/api/News/getallnews";

        public NewsData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<New>> GetAllNews()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallnews;
            List<New> news;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            news = JsonConvert.DeserializeObject<List<New>>(reponseData);
            return news;
        }
    }
}
