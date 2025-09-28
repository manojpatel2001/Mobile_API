namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class BirthdayGroup
    {
        public string? TimePeriod { get; set; } 
        public int? Count { get; set; }
        public List<vmGetTodayBirthdaysByCompany>? Birthdays { get; set; }
    }

}
