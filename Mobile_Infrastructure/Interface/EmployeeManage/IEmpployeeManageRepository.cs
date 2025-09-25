using Mobile_Core.CommonClass;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.Employee;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.EmpployeeManage
{
    public interface IEmployeeManageRepository
    {
        Task<vmGetEmployeeById?> GetEmployeeById(int Id);
        Task<SP_Response> UpdateEmployeeProfile(EmployeeProfile model);
        Task<APIResponse> AddAppVersion(AppVersionModel model);
    }
}
