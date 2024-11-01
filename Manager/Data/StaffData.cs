﻿using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Manager.Data;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Manager.Data
{
    public class StaffData
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Hosting _hosting = new Hosting();
        private string nameURL;
        string keygetallstaff = "/api/Staff/getallstaff";
        string keygetstaff = "/api/Staff/getprofilestaff";

        public StaffData(IHttpContextAccessor httpContextAccessor) 
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            nameURL = _hosting.nameurl;
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            List<Staff> staffs;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keygetallstaff;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                staffs = JsonConvert.DeserializeObject<List<Staff>>(responseData);
            }
            else
            {
                // Xử lý lỗi
                // Có thể ném ngoại lệ hoặc trả về null
                throw new Exception("Không thể lấy thông tin nhân viên: " + response.StatusCode);
            }
            return staffs;
        }

        public async Task<Staff> GetStaffAsync()
        {
            Staff staff = null;
            string token = _httpContextAccessor.HttpContext.Session.GetString("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = nameURL + keygetstaff;
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                staff = JsonConvert.DeserializeObject<Staff>(responseData);
            }
            else
            {
                // Xử lý lỗi
                // Có thể ném ngoại lệ hoặc trả về null
                throw new Exception("Không thể lấy thông tin nhân viên: " + response.StatusCode);
            }

            return staff;
        }

    }
}