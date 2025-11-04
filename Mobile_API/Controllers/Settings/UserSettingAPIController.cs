using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_Core.ViewModel.Setting;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace HRMS_API.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSettingAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSettingAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get/{userId}")]
        public async Task<APIResponse> Get(int userId)
        {
            try
            {
                var data = await _unitOfWork.UserSettingRepository.GetUserSettings(userId);
                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve user settings. Please try again later." };
            }
        }

        [HttpPost("Update")]
        public async Task<APIResponse> Update(UserSettingVM userSettingVM)
        {
            try
            {
                if (userSettingVM.UserId == 0)
                {
                    return new APIResponse { Status = false, ResponseMessage = "Please provide a valid user ID!" };
                }

                var data = await _unitOfWork.UserSettingRepository.UpdateUserSetting(userSettingVM);
                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to update user settings. Please try again later." };
            }
        }
    }
}
