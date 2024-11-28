using Manager.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
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
        string keygetStudentByID = "/api/Student/getstudentbyid";
        string keygetAllStudentbyRoom = "/api/Student/getallstudentbyroom";
        string keyEditStudent = "/api/Student/editstudent";
        string keyAddStudentWithRoom = "/api/Student/addstudentwithroom";
        string keyEditStudentWithRoom = "/api/Student/editstudentwithroom";
        string keyEditStudentLeader = "/api/Student/editstudenleader";
        string keyDeleteStudentWithRoom = "/api/Student/deletestudentwithroom";

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

        public async Task<Student> GetStudentByIDAsyn(string id)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không tồn tại trong session.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"{nameURL}{keygetAllStudent}?id={id}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                throw new HttpRequestException($"Yêu cầu thất bại với mã trạng thái: {response.StatusCode}");
            }

            string responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Student>(responseData);
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

        public async Task UpdateStudent(Student student)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyEditStudent;
            string json = JsonConvert.SerializeObject(student);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }


        public async Task UpdateStudentLeader(Student student)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyEditStudentLeader;
            string json = JsonConvert.SerializeObject(student);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }


        public async Task AddStudentWithRoom(Student student)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyAddStudentWithRoom;
            string json = JsonConvert.SerializeObject(student);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }

        public async Task UpdateStudentWithRoom(Student student)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyEditStudentWithRoom;
            string json = JsonConvert.SerializeObject(student);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }

        public async Task DeleteStudentWithRoom(Student student)
        {
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keyDeleteStudentWithRoom;
            string json = JsonConvert.SerializeObject(student);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

            }
            else
            {
                throw new Exception("Không cập nhật thành công: " + response.StatusCode);
            }
        }
    }
}
