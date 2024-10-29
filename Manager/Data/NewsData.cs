using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Data
{
    public class NewsData
    {
        private readonly HttpClient _httpClient;
        string keygetallnews = "https://localhost:7249/api/News/getallnews";

        public NewsData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<New>> GetAllNews()
        {
            List<New> news;
            HttpResponseMessage response = await _httpClient.GetAsync(keygetallnews);
            string reponseData = await response.Content.ReadAsStringAsync();
            news = JsonConvert.DeserializeObject<List<New>>(reponseData);
            return news;
        }
    }
}
