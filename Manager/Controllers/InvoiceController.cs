using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceData _invoiceData = new InvoiceData();
        List<DormInvoice> dormInvoices = new List<DormInvoice>();
        List<RoomInvoice> roomInvoices = new List<RoomInvoice>();
        public InvoiceController()
        {
            dormInvoices = _invoiceData.GetAllDormInvoice().Result;
            roomInvoices = _invoiceData.GetAllRoomInvoice().Result;
        }
        public IActionResult DormInvoice()
        {
            return View(dormInvoices);
        }
        [HttpGet]
        public IActionResult DormInvoiceDetail(string id)
        {
            DormInvoice dormInvoice = dormInvoices.Find(d => d.InvoiceID == int.Parse(id));
            return View(dormInvoice);
        }
        public IActionResult AddDormInvoice()
        {
            return View();
        }
        public IActionResult RoomInvoice()
        {
            return View(roomInvoices);
        }
        [HttpGet]
        public IActionResult RoomInvoiceDetail(string id)
        {
            RoomInvoice roomInvoice = roomInvoices.Find(r => r.InvoiceID == int.Parse(id));
            ViewBag.listService = new List<Service>
            {
                new Service
                {
                    ServiceID = 1,
                    ServiceName = "Laundry",
                    Unit = "Per kg",
                    Price = 20000m,
                    Quantity = 30 // Example usage: 30 kg
                },
                new Service
                {
                    ServiceID = 2,
                    ServiceName = "Internet",
                    Unit = "Per month",
                    Price = 100000m,
                    Quantity = 1 // Example usage: 1 month
                },
                new Service
                {
                    ServiceID = 3,
                    ServiceName = "Electricity",
                    Unit = "Per kWh",
                    Price = 3500m,
                    Quantity = 100 // Example usage: 100 kWh
                }
            };
            return View(roomInvoice);
        }

    }
}
