namespace Mobile_Core.EmployeeAttedance
{
    public class LeaveApplication
    {
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime? FromDate { get; set; }
        public decimal? No_Of_Date { get; set; }
        public DateTime? Todate { get; set; }
        public string? Reason { get; set; }
        public int? Responsibleperson { get; set; }
        public string? HalfDayType { get; set; }
        public string? CreatedBy { get; set; }
    }
}
