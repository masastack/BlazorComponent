namespace BlazorComponent.I18n
{
    public class I18n
    {
        public I18n(string? language = null) => SetLang(language ?? I18nCache.DefaultLanguage);

        private string? _currentLanguage;

        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage ?? I18nCache.DefaultLanguage;
            }
            private set
            {
                _currentLanguage = value;
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

        public string? T(string key, bool onlyMatchLastLevel = false, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            if(onlyMatchLastLevel is true) return LanguageMap.FirstOrDefault(kv => kv.Key.EndsWith($".{key}") || kv.Key == key).Value ?? (whenNullReturnKey ? key : null);
            return LanguageMap.GetValueOrDefault(key) ?? (whenNullReturnKey ? key : null);
        }

        public string? T(string scope, string key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            return LanguageMap.GetValueOrDefault($"{scope}.{key}") ?? (whenNullReturnKey ? key : null);
        }
    }
}
