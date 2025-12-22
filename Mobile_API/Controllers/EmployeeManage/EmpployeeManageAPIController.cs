using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobile_API.Controllers.AuthManage;
using Mobile_API.Services;
using Mobile_Core.CommonClass;
using Mobile_Core.ViewModel;
using Mobile_Core.ViewModel.Employee;
using Mobile_Infrastructure.Interface;
using Mobile_Utility;

namespace Mobile_API.Controllers.EmployeeManage
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize]
    public class EmpployeeManageAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FileUploadService _fileUploadService;

        public EmpployeeManageAPIController (IUnitOfWork unitOfWork,
                                  FileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
        }
        [HttpPost("UpdateProfilePic")]
        public async Task<APIResponse> UpdateProfilePic(EmployeeProfile model)
        {
            try
            {
                if (model == null || model.EmployeeId == null)
                    return new APIResponse { Status = false, ResponseMessage = "Profile details cannot be null." };
                var check = await _unitOfWork.EmpployeeManageRepository.GetEmployeeById((int)model.EmployeeId);
                if (check == null)
                    return new APIResponse { Status = false, ResponseMessage = "Please select a valid  record." };


                if (model.EmployeeProfileFile != null)
                {
                    if (model.EmployeeProfileFile.Length > 0)
                    {

                        var folder = $"uploads/employeeprofile";
                        var fileUrl = await _fileUploadService.UploadAndReplaceDocumentAsync(model.EmployeeProfileFile, folder, check.EmployeeProfileUrl);
                        if (string.IsNullOrEmpty(fileUrl))
                        {
                            return new APIResponse { Status = false, ResponseMessage = "Some thing went wrong. Please try again later." };

                        }
                        model.EmployeeProfileUrl = fileUrl;
                    }

                }
                var result = await _unitOfWork.EmpployeeManageRepository.UpdateEmployeeProfile(model);

                if (result.Success)
                {
                    var updatedProfile = await _unitOfWork.EmpployeeManageRepository.GetEmployeeById((int)model.EmployeeId);
                    return new APIResponse { Status = true, Data = updatedProfile, ResponseMessage = result.Message };
                }
                return new APIResponse { Status = false, ResponseMessage = result.Message };
            }
            catch (Exception ex)
            {
                return new APIResponse { Status = false, Data = ex.Message, ResponseMessage = "Unable to  update employee profile. Please try again later." };
            }
        }

      
       
    }
}
