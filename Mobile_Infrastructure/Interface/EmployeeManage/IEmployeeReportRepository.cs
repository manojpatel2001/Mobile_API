using Mobile_Core.CommonClass;
using Mobile_Core.EmployeeAttedance;
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
        Task<APIResponse> GetTodayBirthdaysByCompany(Common_Parameter model);
        Task<APIResponse> GetUpcomingHolidays(Common_Parameter model);
        Task<APIResponse> GetAttendanceCalender(Common_Parameter commonParameter);
        Task<List<vmGetSalarySalaryDetails>> GetSalarySalaryDetails(SalarysDetailsParameter vm);
        Task<List<vmGetMonthlyAttendanceDetails>> GetMonthlyAttendanceDetails(Common_Parameter parameter);
       
        Task<APIResponse> GetAllAprovalApplication(Common_Parameter commonParameter);
        Task<APIResponse> GetEmployeeDashboardCountDetails(int EmployeeId);
        Task<APIResponse> GetEmployeeDashboardDetails(Common_Parameter model);

    }
}
