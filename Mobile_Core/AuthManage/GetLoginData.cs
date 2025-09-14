using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Core.AuthManage
{
    public class GetLoginData
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? DesignationId { get; set; }
        public string? UserName { get; set; }
        public string? MobileNo { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
        public string? BranchName { get; set; }
        public Boolean? IsReset { get; set; }
        public int? IsGeofencing { get; set; }
        public int? IsSelfiRequired { get; set; }
        public string? Designation { get; set; }
        public string? GeoLocationData { get; set; }
        public string? EmployeeProfile { get; set; }

    }

}
