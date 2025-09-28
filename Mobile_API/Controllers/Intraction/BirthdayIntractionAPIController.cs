using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_Core.CommonClass;
using Mobile_Core.ViewModel.Intraction;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.Intraction
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BirthdayIntractionAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BirthdayIntractionAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddBirthdayInteraction")]
        public async Task<APIResponse> AddBirthdayInteraction(BirthdayInteraction model)
        {
            try
            {
                var data = await _unitOfWork.BirthdayInteractionRepository.AddBirthdayInteraction(model);

                return data;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve records. Please try again later." };
            }
        }

        [HttpDelete("DeleteBirthdayComment")]
        public async Task<APIResponse> DeleteBirthdayComment(DeleteViewModel model)
        {
            try
            {
                var data = await _unitOfWork.BirthdayInteractionRepository.DeleteBirthdayComment(model);

                return data;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to Delete. Please try again later." };
            }
        }
        [HttpGet("GetBirthdayCommentsWithReplies/{PostId}")]
        public async Task<APIResponse> GetBirthdayCommentsWithReplies(int PostId)
        {
            try
            {
                var data = await _unitOfWork.BirthdayInteractionRepository.GetBirthdayCommentsWithReplies(PostId);

                return data;
            }
            catch
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to retrieve . Please try again later." };
            }
        }
    }
}
