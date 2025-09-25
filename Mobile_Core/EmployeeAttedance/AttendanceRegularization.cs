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

        public int AttendanceRegularizationId { get; set; } = 0;
        public int? EmpId { get; set; }
        public string? FullName { get; set; }
        public string? BranchName { get; set; }
        public DateTime? ForDate { get; set; }
        public string? ShiftTime { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public decimal? Duration { get; set; }
        public string? Day { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsPending { get; set; } = true;  // Default to pending
        public bool IsRejected { get; set; } = false;
        public bool IsLocked { get; set; } = false;

  
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }
}
