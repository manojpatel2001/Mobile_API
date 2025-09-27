using Microsoft.EntityFrameworkCore;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using Mobile_Utility;

namespace Mobile_Infrastructure.Repository.EmployeeAttedance
{
    public class EmployeeLeaveManageRepository : IEmployeeLeaveManageRepository
    {
        private readonly MobileDbcontext _db;

        public EmployeeLeaveManageRepository(MobileDbcontext db)
        {
            _db = db;
        }

        public async Task<APIResponse> CreateLeaveApplication(LeaveApplication model)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                EXEC SP_Mobile_LeaveApplication
                    @Action={"INSERT"}, 
                    @EmployeeId = {model.EmployeeId},
                    @LeaveTypeId = {model.LeaveTypeId},
                     @FromDate= {model.FromDate},
                    @No_Of_Date = {model.No_Of_Date},
                     @Todate= {model.Todate},
                     @Reason= {model.Reason},
                     @Responsibleperson= {model.Responsibleperson},
                     @HalfDayType= {model.HalfDayType},
                     @CreatedBy= {model.CreatedBy}

            ").ToListAsync();

                var data = result?.FirstOrDefault() ?? null;
                if (data != null)
                {
                    return new APIResponse { Status = data.Success, ResponseMessage = data.Message };
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }
        public async Task<APIResponse> LeaveApproveOrReject(ApprovalModel model)
        {
            try
            {
                var result = await _db.Set<SP_Response>().FromSqlInterpolated($@"
                EXEC SP_Mobile_LeaveApproveReject
                    @UserId={model.UserId}, 
                    @LeaveApplicationId = {model.ApplicationId},
                    @LeaveStatus = {model.Status}
            ").ToListAsync();

                var data = result?.FirstOrDefault() ?? null;
                if (data != null)
                {
                    return new APIResponse { Status = data.Success, ResponseMessage = data.Message };
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }

        public async Task<APIResponse> GetLeaveBalance(LeaveApplication model)
        {
            try
            {
                var result = await _db.Set<vmGetLeaveBalance>().FromSqlInterpolated($@"
                 EXEC SP_Mobile_LeaveApplication
                    @Action={"GetLeaveBalance"}, 
                    @EmployeeId = {model.EmployeeId},
                    @LeaveTypeId = {model.LeaveTypeId}
            ").ToListAsync();

               
                if (result.Any())
                {
                    return new APIResponse { Status = true, Data=result, ResponseMessage = "Record fetch sucessfully!"};
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "No record found" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }
        public async Task<APIResponse> GetLeaveType()
        {
            try
            {
                var result = await _db.Set<vmGetLeaveType>().FromSqlInterpolated($@"
                 EXEC SP_Mobile_LeaveApplication
                    @Action={"GetLeaveType"}
            ").ToListAsync();

               
                if (result.Any())
                {
                    return new APIResponse { Status = true, Data=result, ResponseMessage = "Record fetch sucessfully!"};
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "No record found" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }
        public async Task<APIResponse> GetHalfDayType()
        {
            try
            {
                var result = await _db.Set<vmGetHalfDayType>().FromSqlInterpolated($@"
                 EXEC SP_Mobile_LeaveApplication
                    @Action={"GetHalfDayType"}
            ").ToListAsync();

               
                if (result.Any())
                {
                    return new APIResponse { Status = true, Data=result, ResponseMessage = "Record fetch sucessfully!"};
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "No record found" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }
        public async Task<APIResponse> GetResponsibleperson()
        {
            try
            {
                var result = await _db.Set<vmGetResponsibleperson>().FromSqlInterpolated($@"
                 EXEC SP_Mobile_LeaveApplication
                    @Action={"GetResponsibleperson"}
            ").ToListAsync();

               
                if (result.Any())
                {
                    return new APIResponse { Status = true, Data=result, ResponseMessage = "Record fetch sucessfully!"};
                }
                else
                {
                    return new APIResponse { Status = false, ResponseMessage = "No record found" };
                }

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Something went wrong" };
            }
        }


    }
}
