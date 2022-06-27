using System.Collections.Concurrent;
using System.Globalization;

namespace BlazorComponent.I18n;

internal static class I18nCache
{
    private static readonly ConcurrentDictionary<CultureInfo, IReadOnlyDictionary<string, string>> _i18nCache;

    private static CultureInfo? _defaultCulture;

    static I18nCache()
    {
        _i18nCache = new ConcurrentDictionary<CultureInfo, IReadOnlyDictionary<string, string>>();
    }

    public static CultureInfo DefaultCulture
    {
        get => _defaultCulture ?? _i18nCache.Keys.FirstOrDefault() ?? throw new Exception("Please add Language !");
        set => _defaultCulture = value;
    }

    public static void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale, bool isDefault = false)
    {
        if (locale is null) return;

        if (isDefault) DefaultCulture = culture;

        _i18nCache.AddOrUpdate(culture, locale, (_,  dictionary) => Merge(dictionary, locale));
    }

    public static IReadOnlyDictionary<string, string>? GetLocale(CultureInfo culture)
    {
        return _i18nCache.GetValueOrDefault(culture);
    }

    public static bool ContainsCulture(CultureInfo culture) => _i18nCache.ContainsKey(culture);

    public static bool ContainsCulture(string cultureName) => _i18nCache.Keys.Any(c => c.Name == cultureName);

    public static IEnumerable<CultureInfo> GetCultures()
    {
        return _i18nCache.Keys;
    }

    private static IReadOnlyDictionary<TK, TV> Merge<TK, TV>(params IReadOnlyDictionary<TK, TV>[] dictionaries)
    {
        var result = new Dictionary<TK, TV>();

        foreach (var dict in dictionaries)
        {
            dict.ToList().ForEach(pair => result[pair.Key] = pair.Value);
        }

        return result;
    }
}
