namespace DormitoryUser.Models
{
    public class Facility
    {
        public int Id { get; set; }               // ID cơ sở vật chất
        public string FacilityName { get; set; }  // Tên cơ sở vật chất
        public string Description { get; set; }    // Mô tả
        public bool IsAvailable { get; set; }      // Trạng thái có sẵn
    }
}
