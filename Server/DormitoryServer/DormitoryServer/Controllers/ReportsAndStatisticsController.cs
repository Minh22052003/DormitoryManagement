using DormitoryServer.DTOs;
using DormitoryServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Linq;

namespace DormitoryServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportsAndStatisticsController : ControllerBase
    {
        private readonly DataDormitoryContext _context;

        public ReportsAndStatisticsController(DataDormitoryContext context)
        {
            _context = context;
        }
        [HttpGet("reportsandstatistics")]
        public IActionResult GetReportAndStatistics(DateTime firstTime, DateTime lastTime)
        {
            var students = _context.Students.ToList();
            var requests = _context.SupportRequests.ToList();
            var revenue = _context.SupportRequests.ToList();
            var registrations = _context.Registrations.ToList();

            var dailyStats = new Dictionary<DateTime, (int TotalRequests, int ProcessedRequests, decimal TotalRevenue, int StudentRegistrations)>();

            for (DateTime date = firstTime; date <= lastTime; date = date.AddDays(1))
            {

                var dailyRequests = requests
                    .Where(r => r.RequestSentDate.HasValue && r.RequestSentDate.Value.Date == date.Date)
                    .ToList();
                var dailyProcessedRequests = requests
                    .Where(r => r.Status == "Completed" && r.RequestProcessDate.HasValue && r.RequestProcessDate.Value.Date == date.Date)
                    .ToList();
                decimal dailyRevenue = _context.Invoices
                    .Join(_context.InvoiceDetails,
                    invoice => invoice.InvoiceId,
                    invoiceDetail => invoiceDetail.InvoiceId,
                    (invoice, invoiceDetail) => new { invoice, invoiceDetail })
                    .Where(x => x.invoice.PaymentDate.HasValue && x.invoice.PaymentDate.Value.Date == date.Date && x.invoice.Status=="Paid")
                    .Join(_context.Services,
                    combined => combined.invoiceDetail.ServiceId,
                    service => service.ServiceId,
                    (combined, service) => new { combined.invoiceDetail.Quantity, service.Price })
                    .Sum(x => (decimal?)x.Quantity * x.Price) ?? 0;
                int studentRegistrations = registrations
           .Where(reg =>
           {
               DateTime registrationDate = reg.Semester == "1"
                   ? new DateTime(int.Parse(reg.AcademicYear), 8, 10)  
                   : new DateTime(int.Parse(reg.AcademicYear), 1, 10); 

               return registrationDate == date; 
           })
           .Count();
                dailyStats[date] = (dailyRequests.Count, dailyProcessedRequests.Count, dailyRevenue, studentRegistrations);


            }

            int totalProcessedRequests = dailyStats.Sum(stat => stat.Value.ProcessedRequests);
            int totalRequests = dailyStats.Sum(stat => stat.Value.TotalRequests);
            decimal totalRevenue = dailyStats.Sum(stat => stat.Value.TotalRevenue);
            int totalStudentRegistrations = dailyStats.Sum(stat => stat.Value.StudentRegistrations);

            return Ok(new
            {
                TotalProcessedRequests = totalProcessedRequests,
                TotalRequests = totalRequests,
                TotalRevenue= totalRevenue,
                StudentRegistrations= totalStudentRegistrations,
                DailyStatistics = dailyStats.Select(stat => new
                {
                    Date = stat.Key,
                    TotalRequests = stat.Value.TotalRequests,
                    ProcessedRequests = stat.Value.ProcessedRequests,
                    Revenue = stat.Value.TotalRevenue,
                    StudentRegistrations = stat.Value.StudentRegistrations
                })
            });
        }
    }
}
