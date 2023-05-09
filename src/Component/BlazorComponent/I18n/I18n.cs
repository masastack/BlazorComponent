using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BlazorComponent.I18n;

public class I18n
{
    private const string CULTURE_COOKIE_KEY = "Masa_I18nConfig_Culture";

    private readonly CookieStorage _cookieStorage;

    public I18n(IOptions<BlazorComponentOptions> options, CookieStorage cookieStorage, IHttpContextAccessor httpContextAccessor)
    {
        _cookieStorage = cookieStorage;

        var cultureName = _cookieStorage.GetCookie(CULTURE_COOKIE_KEY);

        if (cultureName is null && httpContextAccessor.HttpContext is not null)
        {
            cultureName = httpContextAccessor.HttpContext.Request.Cookies[CULTURE_COOKIE_KEY];

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
                                      return arr.Length == 1 ? (arr[0], 1) : (arr[0], Convert.ToDecimal(arr[1].Split("=")[1]));
                                  })
                                  .OrderByDescending(lang => lang.Item2)
                                  .FirstOrDefault().Item1;
                }
            }
        }

        cultureName ??= options.Value.Locale?.Current;

        var culture = GetValidCulture(cultureName, options.Value.Locale?.Fallback ?? "en-us");

        SetCulture(culture);
    }

    [NotNull]
    public CultureInfo? Culture { get; private set; }

    [NotNull]
    public IReadOnlyDictionary<string, string>? Locale { get; private set; }

    public void SetCulture(CultureInfo culture)
    {
        _cookieStorage?.SetItemAsync(CULTURE_COOKIE_KEY, culture);

        SetCultureAndLocale(culture);

        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    public void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale) => I18nCache.AddLocale(culture, locale);

    public IEnumerable<CultureInfo> SupportedCultures => I18nCache.GetCultures();

    [return: NotNullIfNotNull("defaultValue")]
    public string? T(string? key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true, string? defaultValue = null, params object[] args)
    {
        return T(null, key, whenNullReturnKey, defaultValue, args);
    }

    [return: NotNullIfNotNull("defaultValue")]
    public string? T(string? scope, string? key, [DoesNotReturnIf(true)] bool whenNullReturnKey = true, string? defaultValue = null,
        params object[] args)
    {
        string? value;

        if (key is null)
        {
            value = null;
        }
        else
        {
            var scopeKey = scope is null ? key : $"{scope}.{key}";

            value = Locale.GetValueOrDefault(scopeKey);

            if (value is null && whenNullReturnKey)
            {
                if (key.Trim().Contains(' ') || key.StartsWith(".") || key.EndsWith("."))
                {
                    value = key;
                }
                else
                {
                    value = key.Split('.').Last();
                }
            }
        }

        if (value is null)
        {
            return defaultValue;
        }

        if (args.Length == 0)
        {
            return value;
        }

        try
        {
            return string.Format(value, args);
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

    private static CultureInfo GetValidCulture(string? cultureName, string fallbackCultureName)
    {
        CultureInfo? culture = null;

        try
        {
            culture = CultureInfo.CreateSpecificCulture(cultureName ?? fallbackCultureName);
        }
        catch (Exception)
        {
            // ignored
        }

        if (culture is null && cultureName is not null)
        {
            try
            {
                culture = CultureInfo.CreateSpecificCulture(fallbackCultureName);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        culture ??= CultureInfo.CurrentUICulture;

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
