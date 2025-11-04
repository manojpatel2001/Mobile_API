using Mobile_Core.ViewModel.Setting;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Interface.Settings
{
    public interface IUserSettingRepository
    {
        Task<APIResponse> GetUserSettings(int userId);
        Task<APIResponse> UpdateUserSetting(UserSettingVM userSettingVM);
    }
}
