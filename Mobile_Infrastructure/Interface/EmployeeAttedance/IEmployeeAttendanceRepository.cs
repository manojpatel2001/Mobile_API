using Mobile_Core.CommonClass;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.EmployeeAttedance
{
    public interface IEmployeeAttendanceRepository
    {
        Task<SP_Response> InsertAttendance(EmployeeAttendanceVM attendance);
        Task<EmployeeAttendanceStatusVM> GetEmployeeCurrentStatus(int employeeId);
        Task<List<AutoListVM>> GetAutoList();
        Task<SP_Response> InsertLiveLocation(MobileUserLiveLocation location);

        Task<APIResponse> CreateAttendanceRegularization(AttendanceRegularization model);
        Task<APIResponse> GetDay();
        Task<APIResponse> GetAttendanceByDate(AttendanceRegularization model);
        Task<APIResponse> AttedanceApproveOrReject(ApprovalModel model);

    }

}
