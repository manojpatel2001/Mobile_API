using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.ViewModel.EmployeeReport
{
    public class vmGetTodayBirthdaysByCompany
    {
        public int? PostId { get; set; }
        public string? FullName { get; set; }
        public string? EmployeeProfileUrl { get; set; }
        public string? CompanyName { get; set; }
        public string? BranchName { get; set; }
        public string? DesignationName { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? AgeInYears { get; set; }
        public int? TotalComments { get; set; }
        public int? TotalLikes { get; set; }
        public bool? IsLiked { get; set; }
        public int? DaysUntilBirthday { get; set; }


    }

}
