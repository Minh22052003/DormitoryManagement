using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class Invoice : Controller
    {
        public IActionResult DormInvoice()
        {
            return View();
        }
        public IActionResult DormInvoiceDetail()
        {
            return View();
        }
        public IActionResult RoomInvoice()
        {
            return View();
        }
        public IActionResult RoomInvoiceDetail()
        {
            return View();
        }

    }
}
