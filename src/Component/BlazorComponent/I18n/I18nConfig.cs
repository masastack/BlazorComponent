using Microsoft.AspNetCore.Http;

namespace BlazorComponent.I18n
{
    public class I18nConfig
    {
        CookieStorage? _cookieStorage;

        public static string LanguageCookieKey { get; set; } = "Masa_I18nConfig_Language";

        private string? _language;

        public string? Language
        {
            get => _language;
            set
            {
                _language = value;
                _cookieStorage?.SetItemAsync(LanguageCookieKey, value);
            }
        }

        public I18nConfig() { }

        public I18nConfig(CookieStorage cookieStorage)
        {
            _cookieStorage = cookieStorage;
        }

        public void Initialization(IRequestCookieCollection cookies)
        {
            _language = cookies[LanguageCookieKey];
        }

        public async Task Initialization()
        {
            if (_cookieStorage is not null)
            {
                _language = await _cookieStorage.GetCookie(LanguageCookieKey);
            }
        }

        public void Bind(I18nConfig i18NConfig)
        {
            _language = i18NConfig.Language;
        }
    }
}
