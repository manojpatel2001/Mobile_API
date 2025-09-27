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
    public class vmGetLeaveBalance
    {
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? CompanyName { get; set; }
        public string? LeaveType { get; set; }
        public Decimal? LeaveBalance { get; set; }
        public DateTime? LastTransactionDate { get; set; }
      
    }
    public class vmGetLeaveType
    {
        public int? LeaveTypeId { get; set; }
        public string? LeaveName { get; set; }
       
    }
    public class vmGetHalfDayType
    {
        public string? Day { get; set; }
       
    }
    public class vmGetResponsibleperson
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
       
    }

}
