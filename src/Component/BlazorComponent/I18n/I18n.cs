using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace BlazorComponent.I18n;

public class I18n
{
    private const string CultureCookieKey = "Masa_I18nConfig_Culture";

    private readonly CookieStorage _cookieStorage;

    private CultureInfo? _culture;
    private IReadOnlyDictionary<string, string>? _locale;

    public I18n(CookieStorage cookieStorage, IHttpContextAccessor httpContextAccessor)
    {
        _cookieStorage = cookieStorage;

        string cultureName;
        if (httpContextAccessor.HttpContext is not null)
        {
            cultureName = httpContextAccessor.HttpContext.Request.Cookies[CultureCookieKey];

            if (cultureName is null)
            {
                var acceptLanguage = httpContextAccessor.HttpContext.Request.Headers["accept-language"].FirstOrDefault();
                if (acceptLanguage is not null)
                {
                    cultureName = acceptLanguage
                                  .Split(",")
                                  .Select(lang =>
                                  {
                                      var arr = lang.Split(';');
                                      if (arr.Length == 1) return (arr[0], 1);
                                      else return (arr[0], Convert.ToDecimal(arr[1].Split("=")[1]));
                                  })
                                  .OrderByDescending(lang => lang.Item2)
                                  .FirstOrDefault(lang => I18nCache.ContainsCulture(lang.Item1)).Item1;
                }
            }
        }
        else
        {
            cultureName = _cookieStorage.GetCookie(CultureCookieKey);
        }

        var culture = !string.IsNullOrEmpty(cultureName) ? new CultureInfo(cultureName) : CultureInfo.CurrentCulture;

        if (!EmbeddedLocales.ContainsLocale(culture))
        {
            AddLocale(culture, EmbeddedLocales.GetSpecifiedLocale(culture));
        }

        _culture = culture;
    }

    public CultureInfo Culture
    {
        get => _culture ?? I18nCache.DefaultCulture;
        private set
        {
            _culture = value ?? I18nCache.DefaultCulture;
            _locale = I18nCache.GetLocale(_culture);
        }
    }

    public IReadOnlyDictionary<string, string> Locale =>
        _locale ?? (_locale = I18nCache.GetLocale(Culture)) ?? throw new Exception($"Not has {Culture} language !");

    public void SetCulture(CultureInfo culture)
    {
        if (!EmbeddedLocales.ContainsLocale(culture))
        {
            AddLocale(culture, EmbeddedLocales.GetSpecifiedLocale(culture));
        }

        _cookieStorage?.SetItemAsync(CultureCookieKey, culture);

        Culture = culture;

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    public void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale, bool isDefault = false)
    {
        I18nCache.AddLocale(culture, locale, isDefault);
    }

    public IEnumerable<CultureInfo> SupportedCultures => I18nCache.GetCultures();

    public string? T(string? key, bool matchLastLevel = false, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
    {
        if (key is null)
        {
            return null;
        }

        if (matchLastLevel)
        {
            return Locale.FirstOrDefault(kv => kv.Key.EndsWith($".{key}") || kv.Key == key).Value ?? (whenNullReturnKey ? key : null);
        }

        var value = Locale.GetValueOrDefault(key);
        if (value is not null)
        {
            return value;
        }

        return whenNullReturnKey ? key.Split('.').Last() : null;
    }

    public string? T(string? scope, string? key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true)
    {
        if (key is null)
        {
            return null;
        }

        var value = Locale.GetValueOrDefault($"{scope}.{key}");
        if (value is not null)
        {
            return value;
        }

        return whenNullReturnKey ? key.Split('.').Last() : null;
    }
}
