using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile_Core.ViewModel.Setting;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MobileNotificationAPIController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MobileNotificationAPIController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("Get/{userId}")]
    public async Task<APIResponse> Get(int userId)
    {
        try
        {
            var data = await _unitOfWork.MobileNotificationRepository.GetNotifications(userId);
            return data;
        }
        catch
        {
            return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve notifications. Please try again later." };
        }
    }

    [HttpPost("Insert")]
    public async Task<APIResponse> Insert(MobileNotificationVM notificationVM)
    {
        try
        {
            if (notificationVM.UserId == 0)
            {
                return new APIResponse { Status = false, ResponseMessage = "Please provide a valid user ID!" };
            }

            var data = await _unitOfWork.MobileNotificationRepository.InsertNotification(notificationVM);
            return data;
        }
        catch
        {
            return new APIResponse { Status = false, ResponseMessage = "Unable to insert notification. Please try again later." };
        }
    }

    [HttpPost("MarkAsRead")]
    public async Task<APIResponse> MarkAsRead([FromBody] MarkAsReadVM markAsReadVM)
    {
        try
        {
            if (markAsReadVM.UserId == 0 || markAsReadVM.CategoryDetailId == 0)
            {
                return new APIResponse { Status = false, ResponseMessage = "Please provide valid user ID and category detail ID!" };
            }

            var data = await _unitOfWork.MobileNotificationRepository.MarkAsRead(markAsReadVM);
            return data;
        }
        catch
        {
            return new APIResponse { Status = false, ResponseMessage = "Unable to mark notification as read. Please try again later." };
        }
    }
}

