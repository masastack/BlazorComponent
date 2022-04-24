using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace BlazorComponent.I18n
{
    public class I18n
    {
        private static ConcurrentDictionary<string, IReadOnlyDictionary<string, string>> _i18nCache;

        static I18n()
        {
            _i18nCache = new ConcurrentDictionary<string, IReadOnlyDictionary<string, string>>();
        }

        private static string? _defaultLanguage;

        public static string DefaultLanguage
        {
            get
            {
                return _defaultLanguage ?? _i18nCache.Keys.FirstOrDefault() ?? throw new Exception("Please add Language !");
            }
            set
            {
                _defaultLanguage = value;
            }
        }

        public static void AddLang(string language, IReadOnlyDictionary<string, string>? langMap, bool isDefaultLanguage = false)
        {
            if (langMap is null) return;

            if (isDefaultLanguage) DefaultLanguage = language;

            _i18nCache.AddOrUpdate(language, langMap, (name, original) => langMap);
        }

        public static IReadOnlyDictionary<string, string>? GetLang(string language)
        {
            return _i18nCache.GetValueOrDefault(language);
        }

        public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> ToDictionary() => _i18nCache;

        public static bool ContainsLang(string lang) => _i18nCache.ContainsKey(lang);

        public I18n(string? language = null) => SetLang(language ?? DefaultLanguage);

        private string? _currentLanguage;

        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage ?? DefaultLanguage;
            }
            private set
            {
                _currentLanguage = value ?? DefaultLanguage;
                _languageMap = GetLang(_currentLanguage);
            }
        }

        public IReadOnlyDictionary<string, string>? _languageMap;

        public IReadOnlyDictionary<string, string> LanguageMap
        {
            get
            {
                return _languageMap ?? (_languageMap = GetLang(CurrentLanguage)) ?? throw new Exception($"Not has {CurrentLanguage} language !");
            }
            private set
            {
                _languageMap = value;
            }
        }

        public void SetLang(string language) => CurrentLanguage = language;

        public string? T(string key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            return LanguageMap.GetValueOrDefault(key) ?? (whenNullReturnKey ? key : null);
        }
    }
}
