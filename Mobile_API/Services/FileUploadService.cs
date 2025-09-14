using Mobile_Utility;
using Serilog;


namespace Mobile_API.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileUploadService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> UploadAndReplaceDocumentAsync(IFormFile document, string folderName, string? existingFileUrl = null)
        {
            try
            {
                MobileLog.LogToFile($"Starting upload in folder: {folderName}");

                if (document == null || document.Length == 0)
                {
                    MobileLog.LogToFile("Upload failed: Document is null or empty.");
                    return null;
                }

                var uploadDir = Path.Combine(_env.WebRootPath, folderName);
                MobileLog.LogToFile($"Upload directory resolved to: {uploadDir}");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                    MobileLog.LogToFile($"Directory created: {uploadDir}");
                }

                // Delete existing file if any
                if (!string.IsNullOrEmpty(existingFileUrl))
                {
                    string fileName = Path.GetFileName(new Uri(existingFileUrl).LocalPath);
                    var existingPath = Path.Combine(uploadDir, fileName);
                    MobileLog.LogToFile($"Attempting to delete existing file: {existingPath}");

                    if (File.Exists(existingPath))
                    {
                        File.Delete(existingPath);
                        MobileLog.LogToFile($"Existing file deleted: {existingPath}");
                    }
                    else
                    {
                        MobileLog.LogToFile($"Existing file not found: {existingPath}");
                    }
                }

                // Create new unique file name
                var extension = Path.GetExtension(document.FileName);
                var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var originalName = Path.GetFileNameWithoutExtension(document.FileName);
                var newFileName = $"{originalName}_{timestamp}{extension}";
                var fullPath = Path.Combine(uploadDir, newFileName);

                MobileLog.LogToFile($"Saving file to path: {fullPath}");

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }

                // Build dynamic base URL
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                var fileUrl = $"{baseUrl}/{folderName.Replace("\\", "/")}/{newFileName}";
                MobileLog.LogToFile($"File uploaded successfully. URL: {fileUrl}");

                return fileUrl;
            }
            catch (Exception ex)
            {
                MobileLog.LogToFile($"Exception in UploadAndReplaceDocumentAsync: {ex}");
                return null;
            }
        }

        public bool DeleteUploadedFile(string folderName, string? fileUrl)
        {
            try
            {
                MobileLog.LogToFile($"Attempting delete in folder: {folderName}");

                var uploadDir = Path.Combine(_env.WebRootPath, folderName);
                MobileLog.LogToFile($"Delete directory resolved to: {uploadDir}");

                if (!Directory.Exists(uploadDir))
                {
                    MobileLog.LogToFile("Delete skipped: Directory does not exist.");
                    return false;
                }

                if (!string.IsNullOrEmpty(fileUrl))
                {
                    string fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);
                    var existingPath = Path.Combine(uploadDir, fileName);
                    MobileLog.LogToFile($"Attempting to delete file: {existingPath}");

                    if (File.Exists(existingPath))
                    {
                        File.Delete(existingPath);
                        MobileLog.LogToFile($"File deleted: {existingPath}");
                    }
                    else
                    {
                        MobileLog.LogToFile($"Delete skipped: File not found - {existingPath}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MobileLog.LogToFile($"Exception in DeleteUploadedFile: {ex}");
                return false;
            }
        }
    }
}
