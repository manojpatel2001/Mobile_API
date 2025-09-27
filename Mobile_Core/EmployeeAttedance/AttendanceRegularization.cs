using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.EmployeeAttedance
{
    public class AttendanceRegularization 
    {
        public int? EmpId { get; set; }
        public DateTime? ForDate { get; set; }
        public string? Day { get; set; }
        public string? Reason { get; set; }
        public string? CreatedBy { get; set; }
    
    }

}
