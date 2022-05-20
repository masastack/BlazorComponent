namespace BlazorComponent.I18n
{
    public class I18n
    {
        public I18n(string? language = null) => SetLang(language ?? I18nCache.DefaultLanguage);

        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Languages => I18nCache.ToDictionary();

        private string? _currentLanguage;

        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage ?? I18nCache.DefaultLanguage;
            }
            private set
            {
                _currentLanguage = value ?? I18nCache.DefaultLanguage;
                _languageMap = I18nCache.GetLang(_currentLanguage);
            }
        }

        public IReadOnlyDictionary<string, string>? _languageMap;

        public IReadOnlyDictionary<string, string> LanguageMap
        {
            get
            {
                return _languageMap ?? (_languageMap = I18nCache.GetLang(CurrentLanguage)) ?? throw new Exception($"Not has {CurrentLanguage} language !");
            }
            private set
            {
                _languageMap = value;
            }
        }

        public void SetLang(string language) => CurrentLanguage = language;

        public void AddLang(string language, IReadOnlyDictionary<string, string>? langMap, bool isDefaultLanguage = false)
        {
            I18nCache.AddLang(language, langMap, isDefaultLanguage);
        }

        public string? T(string key, bool matchLastLevel = false, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            if(matchLastLevel is true) return LanguageMap.FirstOrDefault(kv => kv.Key.EndsWith($".{key}") || kv.Key == key).Value ?? (whenNullReturnKey ? key : null);
            return LanguageMap.GetValueOrDefault(key) ?? (whenNullReturnKey ? key : null);
        }

        public string? T(string scope, string key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            return LanguageMap.GetValueOrDefault($"{scope}.{key}") ?? (whenNullReturnKey ? key : null);
        }
    }
}
