namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class HolidaySummary
    {
        public string? TimePeriod { get; set; }
        public int? Count { get; set; }
        public List<vmGetUpcomingHolidays>? Holidays { get; set; }
    }
}
