using Mobile_Core.ViewModel.Setting;
using Mobile_Utility;

namespace Mobile_Infrastructure.Interface.Settings
{
    public interface IAnnouncementRepository
    {
        Task<APIResponse> GetAnnouncements(int userId);
        Task<APIResponse> InsertAnnouncement(AnnouncementVM announcementVM);
        Task<APIResponse> UpdateAnnouncement(AnnouncementVM announcementVM);
        Task<APIResponse> MarkAsRead(MarkAnnouncementAsReadVM markAsReadVM);
    }

}
