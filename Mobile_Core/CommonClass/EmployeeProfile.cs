using Microsoft.AspNetCore.Http;

namespace Mobile_Core.CommonClass
{
    public class EmployeeProfile
    {
        public int? EmployeeId { get; set; }
        public IFormFile? EmployeeProfileFile { get; set; }
        public string? EmployeeProfileUrl { get; set; }
    }
}
