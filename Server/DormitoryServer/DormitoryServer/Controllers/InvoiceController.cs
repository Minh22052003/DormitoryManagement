    using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormitoryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DataDormitoryContext _context;
        public InvoiceController()
        {
            _context = new DataDormitoryContext();
        }

        [HttpGet("getalldorminvoice")]
        public ActionResult GetAllDormInvoice()
        {
            var invoices = _context.DormInvoices.ToList();
            List<DormInvoiceDTO> result = new List<DormInvoiceDTO>();
            foreach (var invoice in invoices)
            {
                var nv = _context.staff.Where(r => r.StaffId == invoice.StaffIdPay).SingleOrDefault();
                result.Add(new DormInvoiceDTO
                {
                    InvoiceID = invoice.InvoiceId,
                    StaffID_Create = invoice.StaffIdCreate,
                    StaffName_Create = invoice.StaffIdCreate,
                    StaffID_Pay = invoice.StaffIdPay,
                    StaffName_Pay = nv?.FullName,
                    InvoiceTypeName = invoice.InvoiceType,
                    Description = invoice.Description,
                    Note = invoice.Note,
                    TotalAmount = invoice.TotalAmount,
                    IssueDate = invoice.IssueDate,
                    PayDate = invoice.PayDate,
                    Status = invoice.Status
                });
            }

            return Ok(result);
        }

        [HttpGet("getallroominvoice")]
        public ActionResult GetAllRoomInvoice()
        {
            var invoices = _context.Invoices
                .Include(i => i.Room)
                    .ThenInclude(r => r.Building)
                .ToList();
            List<RoomInvoiceDTO> result = new List<RoomInvoiceDTO>();
            foreach (var invoice in invoices)
            {

                var dsdv = _context.InvoiceDetails.Include(i => i.Service).Where(i => i.InvoiceId == invoice.InvoiceId).ToList();
                List<ServiceDTO> service = new List<ServiceDTO>();



                decimal totalAmount = 0;
                foreach (var dv in dsdv)
                {
                    totalAmount += dv.Quantity.Value * dv.Service.Price.Value;
                    service.Add(new ServiceDTO
                    {
                        ServiceID = dv.ServiceId,
                        ServiceName = dv.Service.ServiceName,
                        Price = dv.Service.Price,
                        Unit = dv.Service.Unit,
                        Quantity = dv.Quantity,
                    });
                }
                result.Add(new RoomInvoiceDTO
                {
                    InvoiceID = invoice.InvoiceId,
                    StaffID = invoice.StaffId,
                    RoomID = invoice.RoomId,
                    RoomName = invoice.Room?.RoomName,
                    BuildingID = invoice.Room?.BuildingId,
                    BuildingName = invoice.Room?.Building?.BuildingName,
                    PayerID = invoice.Payer,
                    PayerName = invoice.Payer,
                    IssueDate = invoice.IssueDate,
                    Description = invoice.Description,
                    PaymentDate = invoice.PaymentDate,
                    Status = invoice.Status,
                    Note = invoice.Note,
                    TotalAmount = totalAmount,
                    Services = service
                });
            }

            return Ok(result);
        }

        [Authorize(Policy = "Manager")]
        [HttpGet("getallroominvoicenew")]
        public ActionResult GetAllRoomInvoiceNew()
        {
            var staffid = User.FindFirst("UserId")?.Value;
            List<Room> rooms = _context.Rooms.ToList();
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            foreach (var room in rooms)
            {
                bool invoiceExists = _context.Invoices.Any(i => i.RoomId == room.RoomId &&
                                                i.IssueDate.HasValue &&
                                                i.IssueDate.Value.Month == currentMonth &&
                                                i.IssueDate.Value.Year == currentYear);
                if (invoiceExists)
                {
                    continue;
                }

                var latestUtility = _context.UtilityMeters
                                    .Where(u => u.RoomId == room.RoomId)
                                    .OrderByDescending(u => u.RecordingDate)
                                    .FirstOrDefault();
                bool isCurrentMonthUtility = latestUtility != null &&
                             latestUtility.RecordingDate.HasValue &&
                             latestUtility.RecordingDate.Value.Month == currentMonth &&
                             latestUtility.RecordingDate.Value.Year == currentYear;

                var invoice = new Invoice
                {
                    RoomId = room.RoomId,
                    StaffId = staffid,
                    IssueDate = DateTime.Now,
                    Description = "Hóa đơn tháng " + currentMonth,
                    Status = "Pending Approval"
                };

                _context.Invoices.Add(invoice);
                _context.SaveChanges();

                var services = _context.Services.ToList();
                foreach (var service in services)
                {
                    var invoiceDetail = new InvoiceDetail
                    {
                        InvoiceId = invoice.InvoiceId,
                        ServiceId = service.ServiceId
                    };

                    if (service.ServiceName == "Điện" && latestUtility != null)
                    {
                        if (isCurrentMonthUtility)
                        {
                            invoiceDetail.Quantity = latestUtility.Electricity;
                        }
                        invoiceDetail.Quantity = 0;
                    }
                    else if (service.ServiceName == "Nước" && latestUtility != null)
                    {
                        if (isCurrentMonthUtility)
                        {
                            invoiceDetail.Quantity = latestUtility.Water;
                        }
                        invoiceDetail.Quantity = 0;
                    }
                    else
                    {
                        invoiceDetail.Quantity = 1;
                    }

                    _context.InvoiceDetails.Add(invoiceDetail);
                    _context.SaveChanges();
                }
            }

            var invoices = _context.Invoices
                .Include(i => i.Room)
                    .ThenInclude(r => r.Building)
                .Where(i => i.Status == "Pending Approval")
                .ToList();
            List<RoomInvoiceDTO> result = new List<RoomInvoiceDTO>();
            foreach (var invoice in invoices)
            {
                var dsdv = _context.InvoiceDetails.Include(i => i.Service).Where(i => i.InvoiceId == invoice.InvoiceId).ToList();
                List<ServiceDTO> service = new List<ServiceDTO>();
                decimal totalAmount = 0;
                foreach (var dv in dsdv)
                {
                    totalAmount += dv.Quantity.Value * dv.Service.Price.Value;
                    service.Add(new ServiceDTO
                    {
                        ServiceID = dv.ServiceId,
                        ServiceName = dv.Service.ServiceName,
                        Price = dv.Service.Price,
                        Unit = dv.Service.Unit,
                        Quantity = dv.Quantity,
                    });
                }
                result.Add(new RoomInvoiceDTO
                {
                    InvoiceID = invoice.InvoiceId,
                    StaffID = invoice.StaffId,
                    RoomID = invoice.RoomId,
                    RoomName = invoice.Room?.RoomName,
                    BuildingID = invoice.Room?.BuildingId,
                    BuildingName = invoice.Room?.Building?.BuildingName,
                    PayerID = invoice.Payer,
                    PayerName = invoice.Payer,
                    IssueDate = invoice.IssueDate,
                    Description = invoice.Description,
                    PaymentDate = invoice.PaymentDate,
                    Status = invoice.Status,
                    Note = invoice.Note,
                    TotalAmount = totalAmount,
                    Services = service
                });
            }

            return Ok(result);
        }

        [Authorize(Policy = "Manager")]
        [HttpGet("approveroominvoicenew")]
        public ActionResult ApproveRoomInvoiceNew()
        {
            _context.Invoices
                .Where(i => i.Status == "Pending Approval")
                .ToList()
                .ForEach(i => i.Status = "Not Paid");
            _context.SaveChanges();
            return Ok("Success");
        }
    }
}
