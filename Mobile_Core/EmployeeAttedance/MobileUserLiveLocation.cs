namespace Mobile_Core.EmployeeAttedance
{
    public class MobileUserLiveLocation
    {
        public int? UserId { get; set; }
        public DateTime? LocationDatetime { get; set; }
        public string? Lat { get; set; }
        public string? Long { get; set; }
        public string? DeviceName { get; set; }
    }
}
