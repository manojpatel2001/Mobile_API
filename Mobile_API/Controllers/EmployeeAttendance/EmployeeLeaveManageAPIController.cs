using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_API.Services;
using Mobile_Core.EmployeeAttedance;
using Mobile_Core.ViewModel.EmployeeReport;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.EmployeeAttendance
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeLeaveManageAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeLeaveManageAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("CreateLeaveApplication")]
        public async Task<APIResponse> LeaveApproveOrReject(LeaveApplication model)
        {
            try
            {

                var data = await _unitOfWork.EmployeeLeaveManageRepository.CreateLeaveApplication(model);
                return data;

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to approved/reject. Please try again later." };
            }
        }
       
        [HttpPost("LeaveApproveOrReject")]
        public async Task<APIResponse> LeaveApproveOrReject(ApprovalModel model)
        {
            try
            {

                var data = await _unitOfWork.EmployeeLeaveManageRepository.LeaveApproveOrReject(model);
                return data;

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to approved/reject. Please try again later." };
            }
        }

        [HttpPost("GetLeaveBalance")]
        public async Task<APIResponse> GetLeaveBalance(LeaveApplication model)
        {
            try
            {

                var data = await _unitOfWork.EmployeeLeaveManageRepository.GetLeaveBalance(model);
                return data;

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to approved/reject. Please try again later." };
            }
        }


        [HttpGet("GetLeaveType")]
        public async Task<APIResponse> GetLeaveType()
        {
            try
            {

                var data = await _unitOfWork.EmployeeLeaveManageRepository.GetLeaveType();
                return data;

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to fetch record. Please try again later." };
            }
        }

        [HttpGet("GetHalfDayType")]
        public async Task<APIResponse> GetHalfDayType()
        {
            try
            {

                var data = await _unitOfWork.EmployeeLeaveManageRepository.GetHalfDayType();
                return data;

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to fetch record. Please try again later." };
            }
        }
        [HttpGet("GetResponsibleperson")]
        public async Task<APIResponse> GetResponsibleperson()
        {
            try
            {

                var data = await _unitOfWork.EmployeeLeaveManageRepository.GetResponsibleperson();
                return data;

            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to fetch record. Please try again later." };
            }
        }

    }
}
