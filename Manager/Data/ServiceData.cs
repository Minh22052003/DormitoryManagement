using Manager.Models;
using Newtonsoft.Json;
using System.Text;

namespace Manager.Data
{
    public class ServiceData
    {
        private readonly HttpClient _httpClient;
        string Keygetallservice = "https://localhost:7249/api/Service/getallservice";

        public ServiceData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Service>> GetAllService()
        {
            List<Service> services;
            HttpResponseMessage response = await _httpClient.GetAsync(Keygetallservice);
            string reponseData = await response.Content.ReadAsStringAsync();
            services = JsonConvert.DeserializeObject<List<Service>>(reponseData);
            return services;
        }

        //public async Task<HttpResponseMessage> Post_AddBuildingAsync(Service services)
        //{
        //    try
        //    {
        //        string json = JsonConvert.SerializeObject(services);
        //        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(apiKey, data);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
    }
}
