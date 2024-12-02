using Manager.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class UtilityMeterData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keyGetAllUtilityMeter = "/api/UtilityMeter/getallutilitymeter";
        string keyCreateUtilityMeter = "/api/UtilityMeter/addutilitymeter";
        string keyUpdateUtilityMeter = "/api/UtilityMeter/updateutilitymeter";

        public UtilityMeterData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<UtilityMeter>> GetAllUtilityMeter()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keyGetAllUtilityMeter;
            List<UtilityMeter> utilityMeters;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            utilityMeters = JsonConvert.DeserializeObject<List<UtilityMeter>>(reponseData);
            return utilityMeters;
        }

        public async Task<bool> CreateUtilityMeter(UtilityMeter utilityMeter)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keyCreateUtilityMeter;
            var json = JsonConvert.SerializeObject(utilityMeter);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return true;
        }

        public async Task<bool> UpdateUtilityMeter(UtilityMeter utilityMeter)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keyUpdateUtilityMeter;
            var json = JsonConvert.SerializeObject(utilityMeter);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                string errorDetail = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorDetail}");
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
            return true;
        }

    }
}
