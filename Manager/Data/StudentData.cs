﻿using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace Manager.Data
{
    public class StudentData
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Hosting _hosting = new Hosting();
        private string nameURL;
        string keygetAllStudent = "/api/Student/getallstudent";
        string keygetAllStudentbyRoom = "/api/Student/getallstudentbyroom";

        public StudentData(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<Student>> GetAllStudentAsyn()
        {
            List<Student> students;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keygetAllStudent;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                students = JsonConvert.DeserializeObject<List<Student>>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }

            return students;
        }





        public async Task<Student> GetStudentByIDAsyn()
        {
            Student student;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keygetAllStudentbyRoom;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<Student>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return student;
        }
        public async Task<List<Student>> GetStudentByRoomAsyn(string id)
        {
            var urll = nameURL + keygetAllStudentbyRoom;
            List<Student> student;
            string url = $"{urll}?idRoom={id}";

            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responsData = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<List<Student>>(responsData);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return student;
        }

    }
}