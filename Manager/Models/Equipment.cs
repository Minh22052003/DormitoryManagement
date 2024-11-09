namespace Manager.Models
{
    public class Equipment
    {
        public int? EquipmentID { get; set; }
        public string? RoomID { get; set; }
        public string? EquipmentName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Condition { get; set; }
    }
}
