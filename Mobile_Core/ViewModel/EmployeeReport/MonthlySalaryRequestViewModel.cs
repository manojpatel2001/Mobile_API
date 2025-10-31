using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class MonthlySalaryRequestViewModel
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? EmployeeCodes { get; set; }
        public string? BranchIds { get; set; }
        public int? CompanyId { get; set; }

    }



}
