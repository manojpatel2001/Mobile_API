using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_Core.EmployeeAttedance;
using Mobile_Infrastructure.Interface;
using Mobile_Infrastructure.Interface.EmployeeAttedance;
using Mobile_Infrastructure.Repository;
using Mobile_Utility;

namespace Mobile_API.Controllers.EmployeeAttendance
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeAttendanceAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAttendanceAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("InsertAttendance")]
        public async Task<APIResponse> InsertAttendance([FromBody] EmployeeAttendanceVM model)
        {
            try
            {
                if (model == null || model.EmployeeId <= 0)
                    return new APIResponse { Status = false, ResponseMessage = "Attendance details cannot be null." };
                
                var result = await _unitOfWork.EmployeeAttendanceRepository.InsertAttendance(model);
                return new APIResponse
                {
                    Status = result.Success, 
                    ResponseMessage = result.Message
                };
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to insert attendance. Please try again later." };
            }
        }

        [HttpGet("GetEmployeeCurrentStatus/{employeeId}")]
        public async Task<APIResponse> GetEmployeeCurrentStatus(int employeeId)
        {
            try
            {
                var data = await _unitOfWork.EmployeeAttendanceRepository.GetEmployeeCurrentStatus(employeeId);
                if (data == null)
                    return new APIResponse { Status = false, ResponseMessage = "No status found for the employee." };

                return new APIResponse { Status = true, Data = data, ResponseMessage = "Status fetched successfully." };
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve status. Please try again later." };
            }
        }

        [HttpGet("GetAutoList")]
        public async Task<APIResponse> GetAutoList()
        {
            try
            {
                var data = await _unitOfWork.EmployeeAttendanceRepository.GetAutoList();
                if (data == null || !data.Any())
                    return new APIResponse { Status = false, ResponseMessage = "No types found." };

                return new APIResponse { Status = true, Data = data, ResponseMessage = "Types fetched successfully." };
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve types. Please try again later." };
            }
        }

        [HttpPost("InsertLiveLocation")]
        public async Task<APIResponse> InsertLiveLocation([FromBody] MobileUserLiveLocation model)
        {
            try
            {
                if (model == null || model.UserId <= 0)
                    return new APIResponse { Status = false, ResponseMessage = "Live location details cannot be null." };

                var result = await _unitOfWork.EmployeeAttendanceRepository.InsertLiveLocation(model);
                return new APIResponse
                {
                    Status = result.Success,
                    ResponseMessage = result.Message
                };
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to insert live location. Please try again later." };
            }
        }
    }
}
