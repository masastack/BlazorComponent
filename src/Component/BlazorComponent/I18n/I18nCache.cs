using System.Collections.Concurrent;

namespace BlazorComponent.I18n;

internal static class I18nCache
{
    private static readonly ConcurrentDictionary<string, IReadOnlyDictionary<string, string>> _i18nCache;

    private static string? _defaultCulture;

    static I18nCache()
    {
        _i18nCache = new ConcurrentDictionary<string, IReadOnlyDictionary<string, string>>();
    }

    public static string DefaultCulture
    {
        get => _defaultCulture ?? _i18nCache.Keys.FirstOrDefault() ?? throw new Exception("Please add Language !");
        set => _defaultCulture = value;
    }

    public static void AddLocale(string culture, IReadOnlyDictionary<string, string>? langMap, bool isDefaultLanguage = false)
    {
        if (langMap is null) return;

        if (isDefaultLanguage) DefaultCulture = culture;

        _i18nCache.AddOrUpdate(culture, langMap, (_,  dictionary) => Merge(dictionary, langMap));
    }

    public static IReadOnlyDictionary<string, string>? GetLocale(string culture)
    {
        return _i18nCache.GetValueOrDefault(culture);
    }

    public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> ToDictionary() => _i18nCache;

    public static bool ContainsCulture(string culture) => _i18nCache.ContainsKey(culture);

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
