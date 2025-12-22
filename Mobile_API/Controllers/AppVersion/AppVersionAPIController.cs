using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_API.Services;
using Mobile_Core.ViewModel.AppVersion;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.AppVersion
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppVersionAPIController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly FileUploadService _fileUploadService;

        public AppVersionAPIController(IUnitOfWork unitOfWork,
                                  FileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("UploadAppVersion")]
        public async Task<APIResponse> UploadAppVersion(AppVersionModel model)
        {
            try
            {
                if (model == null)
                    return new APIResponse { Status = false, ResponseMessage = "Version details cannot be null." };


                if (model.DocumentFile != null)
                {
                    if (model.DocumentFile.Length > 0)
                    {

                        var folder = $"uploads/app_version_file";
                        var fileUrl = await _fileUploadService.UploadAndReplaceDocumentAsync(model.DocumentFile, folder, null);
                        if (string.IsNullOrEmpty(fileUrl))
                        {
                            return new APIResponse { Status = false, ResponseMessage = "Some thing went wrong. Please try again later." };

                        }
                        model.DocumentPath = fileUrl;
                        model.FileSize = Math.Round((decimal)model.DocumentFile.Length / (1024 * 1024), 2);
                        model.DocumentName = model.DocumentFile.FileName;
                    }

                }
                var result = await _unitOfWork.MobileAppVersionRepository.UploadAppVersion(model);

                return result;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to  add app version . Please try again later." };
            }
        }

        [HttpPost("GetLatestVersion")]
        public async Task<APIResponse> GetLatestVersion(MobileAppVersionPara model)
        {
            try
            {
                if (model == null||string.IsNullOrEmpty(model.AppVersion)|| string.IsNullOrEmpty(model.AppType))
                    return new APIResponse { Status = false, ResponseMessage = "Version details cannot be null." };


                var result = await _unitOfWork.MobileAppVersionRepository.GetLatestVersion(model);

                return result;
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, ResponseMessage = "Unable to  fetch app version . Please try again later." };
            }
        }


    }
}
