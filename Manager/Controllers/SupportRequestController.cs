using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class SupportRequestController : Controller
    {
        private readonly RequestData _requestData = new RequestData();
        List<Request> requests = new List<Request>();
        public SupportRequestController()
        {
            requests = _requestData.GetAllUtilityMeter().Result;
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
