using Microsoft.AspNetCore.Http;

namespace BlazorComponent.I18n
{
    public class I18nConfig
    {
        CookieStorage _cookieStorage;
        I18n _i18n;

        public static string LanguageCookieKey { get; set; } = "Masa_I18nConfig_Language";

        public string? Language
        {
            get => _i18n.CurrentLanguage;
            set
            {
                _i18n.SetLang(value);
                _cookieStorage?.SetItemAsync(LanguageCookieKey, value);
            }
        }

        public I18nConfig(CookieStorage cookieStorage, IHttpContextAccessor httpContextAccessor, I18n i18n)
        {
            _cookieStorage = cookieStorage;
            _i18n = i18n;
            if (httpContextAccessor.HttpContext is not null)
            {
                var language = httpContextAccessor.HttpContext.Request.Cookies[LanguageCookieKey];
                if (language is null)
                {
                    var acceptLanguage = httpContextAccessor.HttpContext.Request.Headers["accept-language"].FirstOrDefault();
                    if (acceptLanguage is not null)
                    {
                        language = acceptLanguage.Split(",").Select(lang =>
                        {
                            var arr = lang.Split(';');
                            if (arr.Length == 1) return (arr[0], 1);
                            else return (arr[0], Convert.ToDecimal(arr[1].Split("=")[1]));
                        }).OrderByDescending(lang => lang.Item2).FirstOrDefault(lang => I18nCache.ContainsLang(lang.Item1)).Item1;
                    }
                }
                i18n.SetLang(language);
            }
            else i18n.SetLang(_cookieStorage.GetCookie(LanguageCookieKey));
        }
    }
}
