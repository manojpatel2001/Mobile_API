using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Utility;

namespace Mobile_Infrastructure.Interface.EmployeeAttedance
{
    public interface IEmployeeLeaveManageRepository
    {
        Task<APIResponse> LeaveApproveOrReject(ApprovalModel model);
        Task<APIResponse> CreateLeaveApplication(LeaveApplication model);

    }

}
