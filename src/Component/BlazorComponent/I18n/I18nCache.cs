﻿using System.Collections.Concurrent;
using System.Globalization;

namespace BlazorComponent.I18n;

internal static class I18nCache
{
    private static readonly ConcurrentDictionary<CultureInfo, IReadOnlyDictionary<string, string>> Cache;

    static I18nCache()
    {
        Cache = new ConcurrentDictionary<CultureInfo, IReadOnlyDictionary<string, string>>();
    }

    public static void AddLocale(CultureInfo culture, IReadOnlyDictionary<string, string>? locale)
    {
        if (locale is null) return;

        Cache.AddOrUpdate(culture, locale, (_,  dictionary) => Merge(dictionary, locale));
    }

    public static IReadOnlyDictionary<string, string>? GetLocale(CultureInfo culture)
    {
        return Cache.GetValueOrDefault(culture);
    }

    public static bool ContainsCulture(CultureInfo culture) => Cache.ContainsKey(culture);

    public static bool ContainsCulture(string cultureName) => Cache.Keys.Any(c => c.Name == cultureName);

    public static IEnumerable<CultureInfo> GetCultures() => Cache.Keys;

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
