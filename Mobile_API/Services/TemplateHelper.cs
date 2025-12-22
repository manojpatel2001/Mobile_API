namespace Mobile_API.Services
{
    public static class TemplateHelper
    {
        /// <summary>
        /// Reads HTML template from path and replaces placeholders
        /// </summary>
        /// <param name="templatePath">Physical or relative template path</param>
        /// <param name="placeholders">Key-value placeholders</param>
        /// <returns>Processed HTML string</returns>
        public static string ReadAndReplace(
            string templatePath,
            Dictionary<string, string>? placeholders)
        {
            if (string.IsNullOrWhiteSpace(templatePath))
                throw new ArgumentException("Template path is required.");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException("Template not found.", templatePath);

            // 1️⃣ Read template
            string template = File.ReadAllText(templatePath);

            // 2️⃣ Replace placeholders
            if (placeholders != null)
            {
                foreach (var kvp in placeholders)
                {
                    template = template.Replace(
                        $"{{{{{kvp.Key}}}}}",
                        kvp.Value ?? string.Empty,
                        StringComparison.OrdinalIgnoreCase
                    );
                }
            }

            return template;
        }


    }
}
