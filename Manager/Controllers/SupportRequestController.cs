using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class SupportRequestController : Controller
    {
        private readonly RequestData _requestData;
        List<Request> requests = new List<Request>();
        public SupportRequestController(IHttpContextAccessor httpContextAccessor)
        {
            _requestData = new RequestData(httpContextAccessor);
        }
        public IActionResult List()
        {
            return View(requests);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var rq = requests.Find(r => r.RequestID == id);
            return View(rq);
        }
    }
}
