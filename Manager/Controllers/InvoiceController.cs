using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceData _invoiceData;
        public InvoiceController(IHttpContextAccessor httpContextAccessor)
        {
            _invoiceData = new InvoiceData(httpContextAccessor);
        }
        public IActionResult DormInvoice()
        {
            List<DormInvoice> dormInvoices = _invoiceData.GetAllDormInvoice().Result;
            return View(dormInvoices);
        }
        [HttpGet]
        public IActionResult DormInvoiceDetail(string id)
        {
            List<DormInvoice> dormInvoices = _invoiceData.GetAllDormInvoice().Result;
            DormInvoice dormInvoice = dormInvoices.Find(d => d.InvoiceID == int.Parse(id));
            return View(dormInvoice);
        }
        public IActionResult AddDormInvoice()
        {
            return View();
        }
        public IActionResult RoomInvoice()
        {
            List<RoomInvoice> roomInvoices = _invoiceData.GetAllRoomInvoice().Result;
            return View(roomInvoices);
        }
        [HttpGet]
        public IActionResult RoomInvoiceDetail(string id)
        {
            List<RoomInvoice> roomInvoices = _invoiceData.GetAllRoomInvoice().Result;
            RoomInvoice roomInvoice = roomInvoices.Find(r => r.InvoiceID == int.Parse(id));
            ViewBag.listService = roomInvoice?.Services;
            return View(roomInvoice);
        }

    }
}
