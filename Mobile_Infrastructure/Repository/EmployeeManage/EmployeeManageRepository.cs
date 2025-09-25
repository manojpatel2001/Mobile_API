using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.Employee;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using Mobile_Infrastructure.Interface.EmpployeeManage;
using Mobile_Infrastructure.Repository.AuthManage;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository.EmpployeeManage
{
    public class EmployeeManageRepository : IEmployeeManageRepository
    {
        private readonly MobileDbcontext _db;
        
        public EmployeeManageRepository(MobileDbcontext db)
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

        public async Task<APIResponse> AddAppVersion(AppVersionModel model)
        {
            try
            {
                // Use FromSqlInterpolated to call the stored procedure with parameters
                var result = await _db.Set<SP_Response>()
                    .FromSqlInterpolated($@"
                EXEC USP_Mobile_AppVersion
                    @Action = 'Insert',
                    @AppVersion = {model.AppVersion},
                    @Description = {model.Description},
                    @FromDate = {model.FromDate},
                    @AppType = {model.AppType},
                    @DocumentName = {model.DocumentName},
                    @FileSize = {model.FileSize},
                    @DocumentPath = {model.DocumentPath}
            ")
                    .ToListAsync();

                var data = result.FirstOrDefault();

                if (data == null)
                {
                    return new APIResponse { Status = false, ResponseMessage = "Some thing went wrong!" };
                }
                else
                {
                    return new APIResponse { Status = data.Success, ResponseMessage = data.Message };

                }
            }
            catch (Exception ex)
            {
                // Log the exception (recommended for debugging)
                // _logger.LogError(ex, "Error in ManageAppVersion");
                return new APIResponse { Status = false, ResponseMessage = "Some thing went wrong!"};
            }
        }


    }
}
