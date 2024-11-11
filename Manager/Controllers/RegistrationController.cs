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
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            List<RegistrationVM> registrations = _registrationData.GetAllRegistration().Result;
            List<RegistrationVM> searchResults;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchResults = registrations.Where(r =>
                    r.StudentID.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    r.StudentName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            else
            {
                searchResults = registrations.ToList();
            }

            return View("Registrations", searchResults); // Trả về View với danh sách đơn đăng ký sau khi tìm kiếm
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
