using Manager.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Manager.Data
{
    public class UtilityMeterData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keyGetAllUtilityMeter = "/api/UtilityMeter/getallutilitymeter";

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
    }
}
