namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class EmployeeDashboardViewModel
    {
        public List<HolidayViewModel>? Holidays { get; set; } = new List<HolidayViewModel>();
        public List<BirthdayViewModel>? Birthdays { get; set; } = new List<BirthdayViewModel>();
        public DashboardCountsViewModel? DashboardCounts { get; set; } = new DashboardCountsViewModel();
    }

    public class HolidayViewModel
    {
        public string? HolidayName { get; set; }
        public string? HolidayDate { get; set; }
        public string? HolidayCategory { get; set; }
    }

    public class BirthdayViewModel
    {
        public string? FullName { get; set; }
        public string? EmployeeProfileUrl { get; set; }
        public string? CompanyName { get; set; }
        public string? BranchName { get; set; }
        public string? DesignationName { get; set; }
        public string? DepartmentName { get; set; }
        public string? BirthDate { get; set; }
        public int? AgeInYears { get; set; }
        public string? TimePeriod { get; set; }
        public int? DaysUntilBirthday { get; set; }
    }

    public class DashboardCountsViewModel
    {
        public int? TotalPresent { get; set; }
        public decimal? TotalBalance { get; set; }
        public int? TotalPendingApproval { get; set; }
        public decimal? NetSalary { get; set; }
    }

}
