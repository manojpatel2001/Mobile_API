using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.Setting
{
    public class AnnouncementVM
    {
        public int? AnnouncementId { get; set; }
        public int? UserId { get; set; }
        public int? CategoryDetailsId { get; set; }
        public string? Message { get; set; }
        public DateTime? ValidTill { get; set; }
        public string? PostedBy { get; set; }
        
    }

    public class MarkAnnouncementAsReadVM
    {
        public int? UserId { get; set; }
        public int? AnnouncementId { get; set; }


    }

}
