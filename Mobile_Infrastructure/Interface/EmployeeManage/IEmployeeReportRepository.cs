using Mobile_Core.CommonClass;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.EmployeeManage
{
    public interface IEmployeeReportRepository
    {
        Task<List<vmGetTodayBirthdaysByCompany>> GetTodayBirthdaysByCompany(int CompanyId);
        Task<List<vmGetUpcomingHolidays>> GetUpcomingHolidays(Common_Parameter parameter);
        Task<APIResponse> GetAttendanceCalender(Common_Parameter commonParameter);
        Task<List<vmGetSalarySalaryDetails>> GetSalarySalaryDetails(SalarysDetailsParameter vm);
        Task<List<vmGetMonthlyAttendanceDetails>> GetMonthlyAttendanceDetails(Common_Parameter parameter);
    }
}
