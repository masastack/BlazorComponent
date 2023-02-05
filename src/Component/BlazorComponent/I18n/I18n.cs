using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace BlazorComponent.I18n;

public class I18n
{
    private const string CultureCookieKey = "Masa_I18nConfig_Culture";

    private readonly CookieStorage _cookieStorage;

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
            cultureName = _cookieStorage.GetCookie(CultureCookieKey) ?? "";
        }

        var culture = GetValidCulture(cultureName);

        SetCultureAndLocale(culture);
    }

    [NotNull]
    public CultureInfo? Culture { get; private set; }

    [NotNull]
    public IReadOnlyDictionary<string, string>? Locale { get; private set; }

    public void SetCulture(CultureInfo culture)
    {
        _cookieStorage?.SetItemAsync(CultureCookieKey, culture);

        SetCultureAndLocale(culture);

        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    public void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale, bool isDefault = false)
    {
        I18nCache.AddLocale(culture, locale, isDefault);
    }

    public IEnumerable<CultureInfo> SupportedCultures => I18nCache.GetCultures();

    public string? T(string? key, bool matchLastLevel = false, bool whenNullReturnKey = true, params object[] args)
    {
        if (key is null)
        {
            return null;
        }

        var value = Locale.GetValueOrDefault(key);

        if (value is null && matchLastLevel)
        {
            var matchKey = Locale.Keys.FirstOrDefault(k => k.EndsWith($".{key}"));

            if (matchKey is not null)
            {
                value = Locale[matchKey];
            }
        }

        if (value is null && whenNullReturnKey)
        {
            return key.Split('.').Last();
        }

        try
        {
            return value is null ? null : string.Format(value, args);
        }
        catch (FormatException)
        {
            return value;
        }
    }

    public string? T(string? scope, string? key, bool whenNullReturnKey = true, params object[] args)
    {
        if (key is null)
        {
            return null;
        }

        var value = Locale.GetValueOrDefault($"{scope}.{key}");

        if (value is null && whenNullReturnKey)
        {
            return key.Split('.').Last();
        }

        try
        {
            return value is null ? null : string.Format(value, args);
        }
        catch (FormatException)
        {
            return value;
        }
    }

    private void SetCultureAndLocale(CultureInfo culture)
    {
        if (!EmbeddedLocales.ContainsLocale(culture))
        {
            AddLocale(culture, EmbeddedLocales.GetSpecifiedLocale(culture));
        }

        Culture = culture;
        Locale = I18nCache.GetLocale(culture);
    }

    private static CultureInfo GetValidCulture(string cultureName)
    {
        CultureInfo culture;

        try
        {
            culture = CultureInfo.CreateSpecificCulture(cultureName);
        }
        catch (Exception e)
        {
            culture = CultureInfo.CurrentUICulture;
        }

        if (culture.Name == string.Empty)
        {
            culture = I18nCache.DefaultCulture;
        }

        // https://github.com/dotnet/runtime/issues/18998#issuecomment-254565364
        // `CultureInfo.CreateSpecificCulture` has the different behavior in different OS,
        // so need to standardize the culture.
        return culture.Name switch
        {
            "zh-Hans-CN" => new CultureInfo("zh-CN"),
            "zh-Hant-CN" => new CultureInfo("zh-TW"),
            _ => culture
        };
    }
}
