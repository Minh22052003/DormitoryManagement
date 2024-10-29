using System;

namespace DormitoryUser.Models
{
    public class ParkingHistory
    {
        public int TicketId { get; set; }               // Mã vé
        public string TransactionType { get; set; }     // Loại giao dịch ("Vào" hoặc "Ra")
        public DateTime TransactionTime { get; set; }   // Thời gian giao dịch
        public string ImageUrl { get; set; }            // URL hình ảnh giao dịch
    }
}

