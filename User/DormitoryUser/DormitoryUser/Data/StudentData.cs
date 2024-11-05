using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DormitoryUser.Data
{
    public class StudentData
    {
        private readonly HttpClient _httpClient;
        private readonly Hosting _serverURL = new Hosting();
        private string NameUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string GetRoomMate = "/api/Student/getroommate";
        string GetProfileStudent = "/api/Student/getprofilestudent";

        public StudentData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            NameUrl = _serverURL.nameurl;
        }
        public async Task<Profile> GetProfileStudentAsyn()
        {
            Profile student;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            string url = NameUrl + GetProfileStudent;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<Profile>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return student;
        }
        public async Task<List<Profile>> GetStudentByRoomAsyn()
        {
            List<Profile> student;
            string url = NameUrl + GetRoomMate;

            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt1");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<List<Profile>>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return student;
        }
       
    }
}
