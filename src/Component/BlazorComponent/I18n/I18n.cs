using Microsoft.AspNetCore.Http;

namespace BlazorComponent.I18n
{
    public class I18n
    {
        private static string LanguageCookieKey { get; set; } = "Masa_I18nConfig_Language";

        private readonly CookieStorage _cookieStorage;
        
        private string? _currentLanguage;
        private IReadOnlyDictionary<string, string>? _languageMap;

        public I18n(CookieStorage cookieStorage, IHttpContextAccessor httpContextAccessor)
        {
            _cookieStorage = cookieStorage;

            if (httpContextAccessor.HttpContext is not null)
            {
                _currentLanguage = httpContextAccessor.HttpContext.Request.Cookies[LanguageCookieKey];
                if (_currentLanguage is not null) return;

                var acceptLanguage = httpContextAccessor.HttpContext.Request.Headers["accept-language"].FirstOrDefault();
                if (acceptLanguage is not null)
                {
                    _currentLanguage = acceptLanguage.Split(",").Select(lang =>
                    {
                        var arr = lang.Split(';');
                        if (arr.Length == 1) return (arr[0], 1);
                        else return (arr[0], Convert.ToDecimal(arr[1].Split("=")[1]));
                    }).OrderByDescending(lang => lang.Item2).FirstOrDefault(lang => I18nCache.ContainsLang(lang.Item1)).Item1;
                }
            }
            else
            {
                _currentLanguage = _cookieStorage.GetCookie(LanguageCookieKey);
            }

            SetLang(I18nCache.DefaultLanguage);
        }

        public string Language => CurrentLanguage;
        
        public string CurrentLanguage
        {
            get => _currentLanguage ?? I18nCache.DefaultLanguage;
            private set
            {
                _currentLanguage = value ?? I18nCache.DefaultLanguage;
                _languageMap = I18nCache.GetLang(_currentLanguage);
            }
        }

        public IReadOnlyDictionary<string, string> LanguageMap
        {
            get => _languageMap ?? (_languageMap = I18nCache.GetLang(CurrentLanguage)) ??
                throw new Exception($"Not has {CurrentLanguage} language !");
            private set => _languageMap = value;
        }

        public void SetLang(string language)
        {
            LanguageMap = LocalesHelper.TryGetSpecifiedLocale(language);
            _cookieStorage?.SetItemAsync(LanguageCookieKey, language);
            CurrentLanguage = language;
        }

        public void AddLang(string language, IReadOnlyDictionary<string, string>? langMap, bool isDefaultLanguage = false)
        {
            I18nCache.AddLang(language, langMap, isDefaultLanguage);
        }

        public string? T(string key, bool matchLastLevel = false, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            if (matchLastLevel)
            {
                return LanguageMap.FirstOrDefault(kv => kv.Key.EndsWith($".{key}") || kv.Key == key).Value ?? (whenNullReturnKey ? key : null);
            }

            return LanguageMap.GetValueOrDefault(key) ?? (whenNullReturnKey ? key : null);
        }

        public string? T(string scope, string key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
        {
            return LanguageMap.GetValueOrDefault($"{scope}.{key}") ?? (whenNullReturnKey ? key : null);
        }
    }
}
