using Microsoft.AspNetCore.Mvc;
using Manager.Models;
using Manager.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Manager.Controllers
{
    public class RegistrationController : Controller
    {
        private RegistrationData _registrationData;
        public RegistrationController(IHttpContextAccessor httpContextAccessor)
        {
            _registrationData = new RegistrationData(httpContextAccessor);
        }
        public IActionResult Registrations()
        {
            List<RegistrationVM> registrations = _registrationData.GetAllRegistration().Result;
            var registrationT = registrations.Where(r => r.ApplicationStatus == "Approved" || r.ApplicationStatus == "Refuse");
            return View(registrationT);
        }
        public IActionResult nRegistrations()
        {
            List<RegistrationVM> registrations = _registrationData.GetAllRegistration().Result;
            var registrationF = registrations.Where(r => r.ApplicationStatus == "Pending");
            return View(registrationF);
        }
        [HttpGet]
        public IActionResult RegistrationDetail(string id)
        {
            List<RegistrationVM> registrations = _registrationData.GetAllRegistration().Result;
            RegistrationVM registrationVM = registrations.Find(x => x.StudentID == id);
            return View(registrationVM);
        }
        [HttpGet]
        public IActionResult RegistrationTDetail(string id)
        {
            List<RegistrationVM> registrations = _registrationData.GetAllRegistration().Result;
            RegistrationVM registrationVM = registrations.Find(x => x.StudentID == id);
            return View(registrationVM);
        }
        [HttpPost]
        public IActionResult AcceptRegistrations(int id)
        {
            RegistrationVM registrationVM = new RegistrationVM();
            registrationVM.RegistrationID = id;
            registrationVM.ApplicationStatus = "Approved";
            _registrationData.UpdateRegistration(registrationVM);
            return RedirectToAction("Registrations");
        }
        [HttpPost]
        public IActionResult RejectRegistrations(int id)
        {
            RegistrationVM registrationVM = new RegistrationVM();
            registrationVM.RegistrationID = id;
            registrationVM.ApplicationStatus = "Refuse";
            _registrationData.UpdateRegistration(registrationVM);
            return View("Registrations");
        }
    }
}
