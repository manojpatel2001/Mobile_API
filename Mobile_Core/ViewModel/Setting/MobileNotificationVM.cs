namespace Mobile_Core.ViewModel.Setting
{
    public class MobileNotificationVM
    {
        public int? NotificationId { get; set; }
        public int? UserId { get; set; }
        public int? CategoryDetailId { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
       
    }

    public class MarkAsReadVM
    {
        public int? UserId { get; set; }
        public int? CategoryDetailId { get; set; }
    }

}
