using DormitoryUser.Data;
using DormitoryUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryUser.Controllers
{
    public class RequestDetailController : Controller
    {
        private RequestData requestData;
        List<Request_Sent> requests = new List<Request_Sent>();

        public RequestDetailController( IHttpContextAccessor httpContextAccessor)
        {
            requestData = new RequestData(httpContextAccessor);
            requests = requestData.GetRequestStudent().Result;
        }

        public IActionResult Request_Sent()
        {
            return View(requests);
        }


        public IActionResult Index(int id)
        {
            var request = requests.Where(r=>r.RequestID==id).FirstOrDefault();
            if (request == null) {
                return NotFound();
            }
            return View(request);
            
        }
    }
}
