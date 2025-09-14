using Microsoft.AspNetCore.Http;

namespace Mobile_Core.EmployeeAttedance
{
    public class EmployeeAttendanceVM
    {
        public int? CompanyId { get; set; }
        public int? EmployeeId { get; set; }
        public string? Lat { get; set; }
        public string? Long { get; set; }
        public string? LocationName { get; set; }
        public int? PunchTypeId { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
        public decimal? @FileSize { get; set; }
        public IFormFile? DocumentFile { get; set; }
    }
}
