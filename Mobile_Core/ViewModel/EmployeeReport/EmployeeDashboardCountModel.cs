using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class EmployeeDashboardCountModel
    {
        public int? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeCode { get; set; }
        public decimal? TotalPresent { get; set; }
        public decimal? TotalBalance { get; set; }
        public int? TotalPendingApproval { get; set; }
        public decimal? NetSalary { get; set; }
    }

}
