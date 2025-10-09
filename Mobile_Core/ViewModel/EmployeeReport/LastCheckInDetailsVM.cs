namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class LastCheckInDetailsVM
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? PunchDateTime { get; set; }
        public string? Mode { get; set; }
        public string? LocationName { get; set; }
        public string? Punch { get; set; } // 'CheckIn', 'CheckOut', or 'Not Available'
    }

}
