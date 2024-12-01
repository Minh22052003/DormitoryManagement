namespace DormitoryUser.Models
{
    public class RoomMate
    {
        public string Id { get; set; }             
        public string? FullName { get; set; }      
        public DateTime BirthDate { get; set; }    
        public bool? Gender { get; set; }         
        public string? PhoneNumber { get; set; }
        public bool? IsLeader { get; set; }
        public string? Hometown { get; set; }      
    }
}
