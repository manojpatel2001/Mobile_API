using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.EmployeeAttedance
{

    public class EmployeeAttendanceStatusVM
    {
        public int? EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? TypeId { get; set; }
        public int? TotalWorkingHours { get; set; }
        public int? TotalWorkingMinutes { get; set; }
        public long? TotalWorkingMilliseconds { get; set; }
    }
}
