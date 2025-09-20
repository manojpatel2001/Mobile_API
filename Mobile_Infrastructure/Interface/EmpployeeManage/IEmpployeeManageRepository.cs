using Mobile_Core.CommonClass;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.EmpployeeManage
{
    public interface IEmpployeeManageRepository
    {
        Task<vmGetEmployeeById?> GetEmployeeById(int Id);
        Task<SP_Response> UpdateEmployeeProfile(EmployeeProfile model);
        Task<List<vmGetSalarySalaryDetails>> GetSalarySalaryDetails(SalarysDetailsParameter vm);
    }
}
