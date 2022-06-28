using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorComponent.I18n;

internal static class EmbeddedLocales
{
    private static readonly Dictionary<string, string> AvailableResources;
    private static readonly Dictionary<CultureInfo, Dictionary<string, string>> LocaleCaches = new();

    private static readonly Assembly ResourcesAssembly = typeof(I18n).Assembly;

    static EmbeddedLocales()
    {
        AvailableResources = ResourcesAssembly
                             .GetManifestResourceNames()
                             .Select(s => Regex.Match(s, @"^.*Locales\.(.+)\.json"))
                             .Where(s => s.Success)
                             .ToDictionary(s => s.Groups[1].Value, s => s.Value);
    }

    public static IReadOnlyDictionary<string, string> GetSpecifiedLocale(CultureInfo culture)
    {
        if (!AvailableResources.ContainsKey(culture.Name))
            return I18nCache.GetLocale(culture);

        if (LocaleCaches.ContainsKey(culture))
            return LocaleCaches[culture];

        string fileName = AvailableResources[culture.Name];
        using var fileStream = ResourcesAssembly.GetManifestResourceStream(fileName);
        if (fileStream == null) return null;
        using var streamReader = new StreamReader(fileStream);
        var content = streamReader.ReadToEnd();

        var locale = I18nReader.Read(content);

        LocaleCaches.Add(culture, locale);

        I18nCache.AddLocale(culture, locale);

        return locale;
    }

    public static bool ContainsLocale(CultureInfo culture)
    {
        return LocaleCaches.ContainsKey(culture);
    }
}
