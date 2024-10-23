using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult DormInvoice()
        {
            var dormInvoices = new List<DormInvoice>
{
    new DormInvoice
    {
        InvoiceID = 1,
        StaffID_Create = "ST001",
        StaffName_Create = "Nguyen Van A",
        StaffID_Pay = "ST002",
        StaffName_Pay = "Tran Thi B",
        InvoiceTypeName = "Electricity Bill",
        Description = "Electricity usage for October",
        Note = "Payment made via bank transfer",
        TotalAmount = 500000m,
        IssueDate = new DateTime(2024, 10, 1),
        PayDate = new DateTime(2024, 10, 10),
        Status = "Paid"
    },
    new DormInvoice
    {
        InvoiceID = 2,
        StaffID_Create = "ST003",
        StaffName_Create = "Le Quoc C",
        StaffID_Pay = "ST004",
        StaffName_Pay = "Pham Thi D",
        InvoiceTypeName = "Water Bill",
        Description = "Water usage for October",
        Note = "Late payment fee included",
        TotalAmount = 300000m,
        IssueDate = new DateTime(2024, 10, 1),
        PayDate = new DateTime(2024, 10, 15),
        Status = "Paid"
    },
    new DormInvoice
    {
        InvoiceID = 3,
        StaffID_Create = "ST005",
        StaffName_Create = "Do Van E",
        StaffID_Pay = "ST006",
        StaffName_Pay = "Hoang Thi F",
        InvoiceTypeName = "Dormitory Rent",
        Description = "Monthly dormitory rent for October",
        Note = "Payment pending",
        TotalAmount = 1500000m,
        IssueDate = new DateTime(2024, 10, 1),
        PayDate = null, // Not paid yet
        Status = "Unpaid"
    }
};

            return View(dormInvoices);
        }
        public IActionResult DormInvoiceDetail()
        {
            DormInvoice dormInvoice = new DormInvoice
            {
                InvoiceID = 3,
                StaffID_Create = "ST005",
                StaffName_Create = "Do Van E",
                StaffID_Pay = "ST006",
                StaffName_Pay = "Hoang Thi F",
                InvoiceTypeName = "Dormitory Rent",
                Description = "Monthly dormitory rent for October",
                Note = "Payment pending",
                TotalAmount = 1500000m,
                IssueDate = new DateTime(2024, 10, 1),
                PayDate = null, // Not paid yet
                Status = "Unpaid"
            };
            return View(dormInvoice);
        }
        public IActionResult AddDormInvoice()
        {
            return View();
        }
        public IActionResult RoomInvoice()
        {
            var roomInvoices = new List<RoomInvoice>
{
    new RoomInvoice
    {
        InvoiceID = 1,
        StaffID = "ST001",
        RoomID = "R101",
        RoomName = "Lab A",
        BuildingID = "B01",
        BuildingName = "IT Building",
        PayerID = "S001",
        PayerName = "Nguyen Van A",
        IssueDate = new DateTime(2024, 10, 5),
        Description = "Monthly room rent for October",
        PaymentDate = new DateTime(2024, 10, 10),
        Status = "Paid",
        Note = "Payment received in full."
    },
    new RoomInvoice
    {
        InvoiceID = 2,
        StaffID = "ST002",
        RoomID = "R202",
        RoomName = "Room 202",
        BuildingID = "B02",
        BuildingName = "Science Building",
        PayerID = "S002",
        PayerName = "Tran Thi B",
        IssueDate = new DateTime(2024, 10, 7),
        Description = "Monthly room rent for October",
        PaymentDate = null, // Not paid yet
        Status = "Unpaid",
        Note = "Payment due by end of October."
    },
    new RoomInvoice
    {
        InvoiceID = 3,
        StaffID = "ST003",
        RoomID = "R303",
        RoomName = "Physics Lab",
        BuildingID = "B03",
        BuildingName = "Science Complex",
        PayerID = "S003",
        PayerName = "Le Quoc C",
        IssueDate = new DateTime(2024, 10, 9),
        Description = "Monthly room rent for October",
        PaymentDate = new DateTime(2024, 10, 12),
        Status = "Paid",
        Note = "Late payment, extra fee applied."
    }
};

            return View(roomInvoices);
        }
        [HttpGet]
        public IActionResult RoomInvoiceDetail()
        {
            RoomInvoice roomInvoices = new RoomInvoice
            {
                InvoiceID = 3,
                StaffID = "ST003",
                RoomID = "R303",
                RoomName = "Physics Lab",
                BuildingID = "B03",
                BuildingName = "Science Complex",
                PayerID = "S003",
                PayerName = "Le Quoc C",
                IssueDate = new DateTime(2024, 10, 9),
                Description = "Monthly room rent for October",
                PaymentDate = new DateTime(2024, 10, 12),
                Status = "Paid",
                Note = "Late payment, extra fee applied.",
                TotalAmount = 500000
            };
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
            return View(roomInvoices);
        }

    }
}
