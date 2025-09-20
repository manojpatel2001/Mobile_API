using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.Employee;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using Mobile_Infrastructure.Interface.EmpployeeManage;
using Mobile_Infrastructure.Repository.AuthManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository.EmpployeeManage
{
    public class EmpployeeManageRepository : IEmpployeeManageRepository
    {
        private readonly MobileDbcontext _db;
        
        public EmpployeeManageRepository(MobileDbcontext db)
        {
            _db = db; 
        }
        public async Task<vmGetEmployeeById?> GetEmployeeById(int Id)
        {
            try
            {
                var result = await _db.Set<vmGetEmployeeById>()
                                      .FromSqlInterpolated($"EXEC GetEmployeeById @Id = {Id}")
                                      .ToListAsync();

                return result.FirstOrDefault() ?? null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<SP_Response> UpdateEmployeeProfile(EmployeeProfile model)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                    EXEC Mobile_UpdateEmployeeProfile
                        @EmployeeId = {model.EmployeeId},
                        @EmployeeProfileUrl = {model.EmployeeProfileUrl}
                        
                ").ToListAsync();

                return result.FirstOrDefault() ?? new SP_Response { Success = false, Message = "Something went wrong!" };
            }
            catch (Exception)
            {
                return new SP_Response { Success = false, Message = "Something went wrong!" };
            }
        }
        public async Task<List<vmGetSalarySalaryDetails>> GetSalarySalaryDetails(SalarysDetailsParameter vm)
        {
            try
            {

                var result = await _db.Set<vmGetSalarySalaryDetails>().FromSqlInterpolated($"EXEC SP_Mobile_GetSalarySalaryDetails @EmployeeId={vm.EmployeeId},@Month={vm.Month},@Year={vm.Year}").ToListAsync();
                return result;
            }
            catch
            {
                return new List<vmGetSalarySalaryDetails>();
            }
        }
    }
}
