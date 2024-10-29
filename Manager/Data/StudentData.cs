using Manager.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Manager.Data
{
    public class StudentData
    {
        private readonly HttpClient _httpClient;
        string GetAllStudent = "https://localhost:7249/api/Student/getallstudent";
        string GetAllStudentbyRoom = "https://localhost:7249/api/Student/getallstudentbyroom";

        public StudentData()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<Student>> GetAllStudentAsyn()
        {
            List<Student> students;
            HttpResponseMessage response = await _httpClient.GetAsync(GetAllStudent);
            string responsData = await response.Content.ReadAsStringAsync();
            students = JsonConvert.DeserializeObject<List<Student>>(responsData);
            return students;
        }
        public async Task<Student> GetStudentByIDAsyn()
        {
            Student student;
            HttpResponseMessage response = await _httpClient.GetAsync(GetAllStudent);
            string responsData = await response.Content.ReadAsStringAsync();
            student = JsonConvert.DeserializeObject<Student>(responsData);
            return student;
        }
        public async Task<List<Student>> GetStudentByRoomAsyn(string id)
        {
            List<Student> student;
            string url = $"{GetAllStudentbyRoom}?idRoom={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responsData = await response.Content.ReadAsStringAsync();
            student = JsonConvert.DeserializeObject<List<Student>>(responsData);
            return student;
        }

    }
}
