using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class SupportRequestController : Controller
    {
        private readonly RequestData _requestData;
        public SupportRequestController(IHttpContextAccessor httpContextAccessor)
        {
            _requestData = new RequestData(httpContextAccessor);
        }
        public IActionResult List()
        {
            try
            {
                List<Request> requests = _requestData.GetAllRequest().Result;
                return View(requests);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            try
            {
                List<Request> requests = _requestData.GetAllRequest().Result;
                var rq = requests.Find(r => r.RequestID == id);
                return View(rq);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
        }
        public async Task<IActionResult> Method(string? searchString, string? filterStatus, string? sortOrder)
        {
            List<Request> requests = _requestData.GetAllRequest().Result;

            // Tìm kiếm theo tên sinh viên
            if (!String.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(r => r.StudentName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Lọc theo trạng thái
            if (!String.IsNullOrEmpty(filterStatus))
            {
                requests = requests.Where(r => r.Status == filterStatus).ToList();
            }

            // Sắp xếp
            requests = sortOrder switch
            {
                "name_asc" => requests.OrderBy(r => r.StudentName).ToList(),
                "name_desc" => requests.OrderByDescending(r => r.StudentName).ToList(),
                "msv_asc" => requests.OrderBy(r => r.RequestID).ToList(),
                "msv_desc" => requests.OrderByDescending(r => r.RequestID).ToList(),
                _ => requests
            };

            return View("List", requests);
        }


        [HttpPost]
        public async Task<IActionResult> ResponseAsync(Request request)
        {
            try
            {
                request.Status = "Đã xử lý";
                await _requestData.Response(request);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
        }
        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }
    }
}
