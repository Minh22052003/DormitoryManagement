﻿using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceData _invoiceData;
        private readonly RoomInvoiceData _roominvoiceData;
        public InvoiceController(IHttpContextAccessor httpContextAccessor)
        {
            _invoiceData = new InvoiceData(httpContextAccessor);
            _roominvoiceData = new RoomInvoiceData(httpContextAccessor);
        }
        public IActionResult DormInvoice()
        {
            try
            {
                List<DormInvoice> dormInvoices = _invoiceData.GetAllDormInvoice().Result;
                return View(dormInvoices);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
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
            try
            {
                List<RoomInvoice> roomInvoices = _invoiceData.GetAllRoomInvoice().Result;
                var notpendingInvoices = roomInvoices.Where(ri => ri.Status != "Pending Approval").ToList();
                return View(notpendingInvoices);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error401");
            }
            
        }

        public IActionResult AddRoomInvoice()
        {
            List<RoomInvoice> roomInvoices = _invoiceData.GetAllRoomInvoice().Result;

            var pendingInvoices = roomInvoices.Where(ri => ri.Status == "Pending Approval").ToList();

            if (pendingInvoices.Count == 0)
            {
                List<RoomInvoice> roomInvoicescd = _roominvoiceData.CreateRoomInvoice().Result;
                return View(roomInvoicescd);
            }
            return View(pendingInvoices);
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
        [HttpGet]
        public IActionResult RoomInvoicePendingApprovalDetail(string id)
        {
            List<RoomInvoice> roomInvoices = _invoiceData.GetAllRoomInvoice().Result;
            RoomInvoice roomInvoice = roomInvoices.Find(r => r.InvoiceID == int.Parse(id));
            ViewBag.listService = roomInvoice?.Services;
            return View(roomInvoice);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveInvoices()
        {
            await _roominvoiceData.Approveroominvoicenew();
            List<RoomInvoice> roomInvoices = _invoiceData.GetAllRoomInvoice().Result;

            var pendingInvoices = roomInvoices.Where(ri => ri.Status == "Pending Approval").ToList();

            if (pendingInvoices.Count == 0)
            {
                List<RoomInvoice> roomInvoicescd = _roominvoiceData.CreateRoomInvoice().Result;
                return View(roomInvoicescd);
            }
            return View("RoomInvoice", pendingInvoices);
        }

        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }
    }
}
