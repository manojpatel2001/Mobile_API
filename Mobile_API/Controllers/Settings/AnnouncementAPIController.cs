using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_Core.ViewModel.Setting;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnnouncementAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get/{userId}")]
        public async Task<APIResponse> Get(int userId)
        {
            try
            {
                var data = await _unitOfWork.AnnouncementRepository.GetAnnouncements(userId);
                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve announcements. Please try again later." };
            }
        }

        [HttpPost("Insert")]
        public async Task<APIResponse> Insert(AnnouncementVM announcementVM)
        {
            try
            {
                if (announcementVM.CategoryDetailsId == 0)
                {
                    return new APIResponse { Status = false, ResponseMessage = "Please provide a valid category detail ID!" };
                }

                var data = await _unitOfWork.AnnouncementRepository.InsertAnnouncement(announcementVM);
                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to insert announcement. Please try again later." };
            }
        }

        [HttpPost("Update")]
        public async Task<APIResponse> Update(AnnouncementVM announcementVM)
        {
            try
            {
                if (announcementVM.AnnouncementId == 0)
                {
                    return new APIResponse { Status = false, ResponseMessage = "Please provide a valid announcement ID!" };
                }

                var data = await _unitOfWork.AnnouncementRepository.UpdateAnnouncement(announcementVM);
                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to update announcement. Please try again later." };
            }
        }

        [HttpPost("MarkAsRead")]
        public async Task<APIResponse> MarkAsRead(MarkAnnouncementAsReadVM markAsReadVM)
        {
            try
            {
                if (markAsReadVM.AnnouncementId == 0 || markAsReadVM.UserId == 0)
                {
                    return new APIResponse { Status = false, ResponseMessage = "Please provide valid announcement ID and user ID!" };
                }

                var data = await _unitOfWork.AnnouncementRepository.MarkAsRead(markAsReadVM);
                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to mark announcement as read. Please try again later." };
            }
        }
    }
}
