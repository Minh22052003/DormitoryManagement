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
        public IActionResult RoomInvoiceList(string searchTerm, string searchBy, string sortOption)
        {
            List<RoomInvoice> invoices = _invoiceData.GetAllRoomInvoice().Result;

            // Tìm kiếm theo nội dung
            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (searchBy == "room")
                {
                    invoices = invoices.Where(i => i.RoomName.Contains(searchTerm)).ToList();
                }
                else if (searchBy == "manager")
                {
                    invoices = invoices.Where(i => i.PayerName.Contains(searchTerm)).ToList();
                }
            }

            // Sắp xếp dựa trên tùy chọn
            switch (sortOption)
            {
                case "building":
                    invoices = invoices.OrderBy(i => i.BuildingName).ToList();
                    break;
                case "room":
                    invoices = invoices.OrderBy(i => i.RoomName).ToList();
                    break;
                case "manager_asc":
                    invoices = invoices.OrderBy(i => i.PayerName).ToList();
                    break;
                case "manager_desc":
                    invoices = invoices.OrderByDescending(i => i.PayerName).ToList();
                    break;
            }

            return View("RoomInvoice", invoices);
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
