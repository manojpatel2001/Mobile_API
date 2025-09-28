using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_API.Services;
using Mobile_Core.CommonClass;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.EmployeeReport
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class EmployeeReportAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeReportAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetSalarySalaryDetails")]
        public async Task<APIResponse> GetSalarySalaryDetails(SalarysDetailsParameter vm)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetSalarySalaryDetails(vm);
                if (data == null || !data.Any())
                {
                    return new APIResponse()
                    {
                        Status = false,
                        ResponseMessage = "No record found"
                    };
                }

                return new APIResponse()
                {
                    Status = true,
                    Data = data,
                    ResponseMessage = "Record fetched successfully"
                };
            }
            catch (Exception err)
            {
                return new APIResponse
                {
                    Status = false,
                    Data = null,
                    ResponseMessage = $"Error: {err.Message}"
                };
            }
        }

        [HttpPost("GetTodayBirthdaysByCompany")]
        public async Task<APIResponse> GetTodayBirthdaysByCompany(Common_Parameter model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetTodayBirthdaysByCompany(model);
                
                return data;
            }
            catch (Exception err)
            {
                return new APIResponse
                {
                    Status = false,
                    Data = err.Message,
                    ResponseMessage = "Unable to retrieve records, Please try again later!"
                };
            }
        }

        [HttpPost("GetUpcomingHolidays")]
        public async Task<APIResponse> GetUpcomingHolidays(Common_Parameter parameter)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetUpcomingHolidays(parameter);

                return data;
            }
            catch (Exception err)
            {
                return new APIResponse
                {
                    Status = false,
                    Data = err.Message,
                    ResponseMessage = "Unable to retrieve records, Please try again later!"
                };
            }
        }


        [HttpPost("GetAttendanceCalender")]
        public async Task<APIResponse> GetAttendanceCalender(Common_Parameter commonParameter)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetAttendanceCalender(commonParameter);

                return data;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }
        [HttpPost("GetMonthlyAttendanceDetails")]
        public async Task<APIResponse> GetMonthlyAttendanceDetails(Common_Parameter parameter)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetMonthlyAttendanceDetails(parameter);
                if (data == null || !data.Any())
                    return new APIResponse { Status = false, ResponseMessage = "No records found." };

                return new APIResponse { Status = true, Data = data, ResponseMessage = "Records fetched successfully." };
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, Data = ex.Message, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }

      
        [HttpPost("GetAllAprovalApplication")]
        public async Task<APIResponse> GetAllAprovalApplication(Common_Parameter model)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetAllAprovalApplication(model);

                return data;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }

        [HttpGet("GetEmployeeDashboardCountDetails/{EmployeeId}")]
        public async Task<APIResponse> GetEmployeeCurrentStatus(int EmployeeId)
        {
            try
            {
                var data = await _unitOfWork.EmployeeReportRepository.GetEmployeeDashboardCountDetails(EmployeeId);

                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve status. Please try again later." };
            }
        }

       
    }
}
