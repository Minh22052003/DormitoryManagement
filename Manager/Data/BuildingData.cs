using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace Manager.Data
{
    public class BuildingData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _hosting = new Hosting();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string nameURL;
        string keygetallbuilding = "/api/Building/getallbuilding";

        public BuildingData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<Building>> GetAllBuilding()
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = nameURL + keygetallbuilding;
            List<Building> buildings;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            string reponseData = await response.Content.ReadAsStringAsync();
            buildings = JsonConvert.DeserializeObject<List<Building>>(reponseData);
            return buildings;
        }

        //public async Task<HttpResponseMessage> Post_AddBuildingAsync(Building building)
        //{
        //    try
        //    {
        //        string json = JsonConvert.SerializeObject(building);
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
