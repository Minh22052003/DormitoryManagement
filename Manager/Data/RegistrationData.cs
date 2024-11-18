using Manager.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Manager.Data
{
    public class RegistrationData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetallregistration = "/api/Registration/getallregistration";
        string ketUpdateRegistration = "/api/Registration/updatestatus";

        public RegistrationData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<RegistrationVM>> GetAllRegistration()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallregistration;
            List<RegistrationVM> roomInvoices;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            roomInvoices = JsonConvert.DeserializeObject<List<RegistrationVM>>(reponseData);
            return roomInvoices;
        }

        public async Task<bool> UpdateRegistration(RegistrationVM registrationVM)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + ketUpdateRegistration;
            var json = JsonConvert.SerializeObject(registrationVM);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return true;
        }
    }
}
