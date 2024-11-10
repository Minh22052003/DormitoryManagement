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
            List<Request> requests = _requestData.GetAllRequest().Result;
            return View(requests);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            List<Request> requests = _requestData.GetAllRequest().Result;
            var rq = requests.Find(r => r.RequestID == id);
            return View(rq);
        }

        [HttpPost]
        public async Task<IActionResult> ResponseAsync(Request request)
        {
            request.Status = "Đã xử lý";
            await _requestData.Response(request);
            return RedirectToAction("List");
        }
    }
}
