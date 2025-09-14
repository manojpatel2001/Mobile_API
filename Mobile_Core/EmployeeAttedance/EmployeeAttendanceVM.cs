namespace Mobile_Core.EmployeeAttedance
{
    public class EmployeeAttendanceVM
    {
        public int? CompanyId { get; set; }
        public int? EmployeeId { get; set; }
        public string? Lat { get; set; }
        public string? Long { get; set; }
        public string? LocationName { get; set; }
        public int? PunchTypeId { get; set; }
    }
}
