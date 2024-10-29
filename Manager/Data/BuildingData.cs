using Manager.Models;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace Manager.Data
{
    public class BuildingData
    {
        private readonly HttpClient _httpClient;
        string Keygetallbuilding = "https://localhost:7249/api/Building/getallbuilding";

        public BuildingData()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Building>> GetAllBuilding()
        {
            List<Building> buildings;
            HttpResponseMessage response = await _httpClient.GetAsync(Keygetallbuilding);
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
