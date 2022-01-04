namespace BlazorComponent.I18n
{
    public class LanguageConfig
    {
        public string? DefaultLanguage { get; set; }

        public List<Language> Languages { get; set; }

        public LanguageConfig(string? defaultLanguage, List<Language> languages)
        {
            DefaultLanguage = defaultLanguage;
            Languages = languages;
        }
    }

    public class Language
    {
        public string Value { get; set; }

        public string FilePath { get; set; }

        public Language(string value, string filePath)
        {
            Value = value;
            FilePath = filePath;
        }
    }
}
