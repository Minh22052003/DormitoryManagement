using Microsoft.AspNetCore.Mvc;
using Manager.Models;
using Manager.Data;

namespace Manager.Controllers
{
    public class RegistrationController : Controller
    {
        private RegistrationData _registrationData = new RegistrationData();
        List<RegistrationVM> registrations = new List<RegistrationVM>();
        public RegistrationController()
        {
            registrations = _registrationData.GetAllRegistration().Result;
        }
        public IActionResult Registrations()
        {
            var registrationT = registrations.Where(r => r.ApplicationStatus == "Approved");
            return View(registrationT);
        }
        public IActionResult nRegistrations()
        {
            var registrationF = registrations.Where(r => r.ApplicationStatus == "Pending");
            return View(registrationF);
        }
        [HttpGet]
        public IActionResult RegistrationDetail(string id)
        {
            RegistrationVM registrationVM = registrations.Find(x => x.StudentID == id);
            return View(registrationVM);
        }
        [HttpGet]
        public IActionResult RegistrationTDetail(string id)
        {
            RegistrationVM registrationVM = registrations.Find(x => x.StudentID == id);
            return View(registrationVM);
        }
        public IActionResult AcceptRegistrations()
        {
            return View();
        }
        public IActionResult RejectRegistrations()
        {
            return View();
        }
    }
}
