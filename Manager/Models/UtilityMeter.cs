namespace Manager.Models
{
    public class UtilityMeter
    {
        public int? UtilityMeterID { get; set; }
        public string? RoomID { get; set; }
        public string? RoomName { get; set; }
        public string? BuildingID { get; set; }
        public string? BuildingName { get; set; }
        public string? StaffID { get; set; }
        public string? StaffName { get; set; }
        public int? Electricity { get; set; }
        public int? Water { get; set; }
        public DateTime? RecordingDate { get; set; }
    }
}
