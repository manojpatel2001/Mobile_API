namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class AttendanceRegularizationViewModel
    {
        public string? ApplicationType { get; set; }
        public int? AttendanceRegularizationId { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FullName { get; set; }
        public string? Type { get; set; } // Reason
        public string? Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Duration { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

   

}
