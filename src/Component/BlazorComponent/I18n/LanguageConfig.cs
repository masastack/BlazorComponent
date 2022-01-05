namespace BlazorComponent.I18n
{
    public class LanguageConfig
    {
        public string? DefaultLanguage { get; set; }

        public string LanguageFileDirectoryForServer { get; set; }

        public string LanguageFileDirectoryForWasm { get; set; }

        public List<string> Languages { get; set; }

        public LanguageConfig(string? defaultLanguage, string languageFileDirectoryForServer, string languageFileDirectoryForWasm, List<string> languages)
        {
            DefaultLanguage = defaultLanguage;
            LanguageFileDirectoryForServer = languageFileDirectoryForServer;
            LanguageFileDirectoryForWasm = languageFileDirectoryForWasm;
            Languages = languages;
        }
    }
}
