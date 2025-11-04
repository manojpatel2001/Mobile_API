using Mobile_Core.ViewModel.Setting;
using Mobile_Utility;

namespace Mobile_Infrastructure.Interface.Settings
{
    public interface IMobileNotificationRepository
    {
        Task<APIResponse> GetNotifications(int userId);
        Task<APIResponse> InsertNotification(MobileNotificationVM notificationVM);
        Task<APIResponse> MarkAsRead(MarkAsReadVM model);
    }

}
