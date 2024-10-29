namespace DormitoryUser.Models
{
    public class Request_Sent
    {
        public int Id { get; set; }                  // ID yêu cầu
        public string RequestId { get; set; }        // ID yêu cầu
        public DateTime SentDate { get; set; }       // Ngày gửi yêu cầu
        public string Summary { get; set; }           // Nội dung tóm tắt
        public string Status { get; set; }            // Trạng thái yêu cầu
        public string Type { get; set; }              // Loại yêu cầu (mạng, sửa, khác)
    }
}
