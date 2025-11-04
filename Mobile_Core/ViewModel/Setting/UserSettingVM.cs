using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.Setting
{
    public class UserSettingVM
    {
        public int? UserId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsActive { get; set; }
    }

}
