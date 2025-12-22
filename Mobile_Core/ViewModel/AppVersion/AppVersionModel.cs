using Microsoft.AspNetCore.Http;

namespace Mobile_Core.ViewModel.AppVersion
{
    public class AppVersionModel
    {
        public string? AppVersion { get; set; }
        public string? AppType { get; set; }
        public string? Description { get; set; }
        public DateTime? FromDate { get; set; }
        public string? DocumentName { get; set; }
        public int? Id { get; set; }
        public decimal? FileSize { get; set; }
        public string? DocumentPath { get; set; }
        public IFormFile? DocumentFile { get; set; }
    }
    public class MobileAppVersionPara
    {
        public string AppType { get; set; }
        public string AppVersion { get; set; }
    }
    public class MobileAppVersionResult
    {
        public string Message { get; set; }
        public string DownloadLink { get; set; }
    }
}
