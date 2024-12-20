﻿using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceData _invoiceData;
        private readonly RoomInvoiceData _roominvoiceData;
        private readonly StudentData _studentData;
        private readonly StaffData _staffData;
        private object _httpContextAccessor;

        public InvoiceController(IHttpContextAccessor httpContextAccessor)
        {
            _invoiceData = new InvoiceData(httpContextAccessor);
            _roominvoiceData = new RoomInvoiceData(httpContextAccessor);
            _studentData = new StudentData(httpContextAccessor);
            _staffData = new StaffData(httpContextAccessor);
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

        [HttpPost]
        public async Task<IActionResult> UpdateDormInvoice(DormInvoice dormInvoice)
        {
            await _invoiceData.UpdateDormInvoice(dormInvoice);
            return RedirectToAction("DormInvoice");
        }


        public IActionResult AddDormInvoice()
        {
            var listStaff = _staffData.GetAllStaffAsync().Result;
            ViewBag.Staff = listStaff;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDormInvoiceAsync([FromForm] DormInvoice dormInvoice)
        {
            var staffid = HttpContext.Session.GetString("staffid");
            dormInvoice.PayDate = DateTime.Now;
            dormInvoice.IssueDate = DateTime.Now;
            dormInvoice.Status = "Paid";
            dormInvoice.StaffID_Create = staffid;
            await _invoiceData.AddDormInvoice(dormInvoice);
            return RedirectToAction("DormInvoice");
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
            List<Student> students = _studentData.GetStudentByRoomAsyn(roomInvoice.RoomID).Result;
            ViewBag.listStudent = students;
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
            return RedirectToAction("RoomInvoice");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoomInvoiceAsync(RoomInvoice roomInvoice)
        {
            await _roominvoiceData.UpdateRoomInvoice(roomInvoice);
            Console.WriteLine(roomInvoice.InvoiceID);
            return RedirectToAction("RoomInvoice");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(RoomInvoice roomInvoice)
        {
            roomInvoice.Status = "Paid";
            roomInvoice.PaymentDate = DateTime.Now;
            await _roominvoiceData.UpdateRoomInvoice(roomInvoice);
            Console.WriteLine(roomInvoice.InvoiceID);
            return RedirectToAction("RoomInvoice");
        }



        public IActionResult Error401()
        {
            ViewBag.Error = "Bạn không có quyền sử dụng chức năng này";
            return View("Error401");
        }
    }
}
