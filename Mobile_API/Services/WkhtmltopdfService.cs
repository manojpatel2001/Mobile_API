using System.Diagnostics;

namespace Mobile_API.Services
{
    public static class WkhtmltopdfService
    {
        public static void GeneratePdf(string wkhtmlPath, string htmlContent, string outputPath)
        {
            try
            {
                // Ensure output directory exists
                var directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create a temporary HTML file
                var tempHtmlPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.html");
                File.WriteAllText(tempHtmlPath, htmlContent);

                // Build the arguments - page-size MUST come before input file
                var arguments = $"--page-size A4 --margin-top 10mm --margin-right 10mm --margin-bottom 10mm --margin-left 10mm --enable-local-file-access \"{tempHtmlPath}\" \"{outputPath}\"";

                var processInfo = new ProcessStartInfo
                {
                    FileName = wkhtmlPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processInfo))
                {
                    // Capture output and error streams
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    // Clean up temp file
                    if (File.Exists(tempHtmlPath))
                    {
                        File.Delete(tempHtmlPath);
                    }

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"wkhtmltopdf error: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"PDF generation failed: {ex.Message}", ex);
            }
        }
    }
}
