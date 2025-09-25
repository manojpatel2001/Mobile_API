using Microsoft.AspNetCore.Http;

namespace Mobile_Core.ViewModel.Employee
{
    public class AppVersionModel
    {
        public string? AppVersion { get; set; }
        public string? AppType { get; set; }
        public string? Description { get; set; }
        public DateTime? FromDate { get; set; }
        public string? DocumentName { get; set; }
        public int? Id { get; set; }
        public string? Type { get; set; }
        public decimal? FileSize { get; set; }
        public string? DocumentPath { get; set; }
        public IFormFile? DocumentFile { get; set; }
    }

}
