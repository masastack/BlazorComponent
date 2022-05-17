using System.Collections.Concurrent;

namespace BlazorComponent.I18n;

public static class I18nCache
{
    private static ConcurrentDictionary<string, IReadOnlyDictionary<string, string>> _i18nCache;

    static I18nCache()
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
}

