using System.Collections.Concurrent;

namespace BlazorComponent.I18n;

public static class I18nCache
{
    private static readonly ConcurrentDictionary<string, IReadOnlyDictionary<string, string>> _i18nCache;

    private static string? _defaultLanguage;

    static I18nCache()
    {
        _i18nCache = new ConcurrentDictionary<string, IReadOnlyDictionary<string, string>>();
    }

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

        _i18nCache.AddOrUpdate(language, langMap, (_,  dictionary) => Merge(dictionary, langMap));
    }

    public static IReadOnlyDictionary<string, string>? GetLang(string language)
    {
        return _i18nCache.GetValueOrDefault(language);
    }

    public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> ToDictionary() => _i18nCache;

    public static bool ContainsLang(string lang) => _i18nCache.ContainsKey(lang);
    
    private static IReadOnlyDictionary<TK, TV> Merge<TK, TV>(params IReadOnlyDictionary<TK, TV>[] dictionaries)
    {
        var result = new Dictionary<TK, TV>();
 
        foreach (var dict in dictionaries) {
            dict.ToList().ForEach(pair => result[pair.Key] = pair.Value);
        }
 
        return result;
    }
}

