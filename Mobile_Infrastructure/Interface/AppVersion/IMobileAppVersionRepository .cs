using Mobile_Core.ViewModel.AppVersion;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.AppVersion
{
    public interface IMobileAppVersionRepository
    {
        Task<APIResponse> UploadAppVersion(AppVersionModel model);
        Task<APIResponse> GetLatestVersion(MobileAppVersionPara model);

    }
}
