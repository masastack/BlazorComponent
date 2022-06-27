using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorComponent.I18n;

internal static class EmbeddedLocales
{
    private static Dictionary<string, string> _availableResources = new();
    private static Dictionary<string, Dictionary<string, string>> _localeCaches = new();

    private static readonly Assembly ResourcesAssembly = typeof(I18n).Assembly;

    public static void Init()
    {
        _availableResources = ResourcesAssembly
                              .GetManifestResourceNames()
                              .Select(s => Regex.Match(s, @"^.*Locales\.(.+)\.json"))
                              .Where(s => s.Success)
                              .ToDictionary(s => s.Groups[1].Value, s => s.Value);
    }

    public static IReadOnlyDictionary<string, string> GetSpecifiedLocale(string cultureName)
    {
        if (!_availableResources.ContainsKey(cultureName))
            return I18nCache.GetLocale(cultureName);

        if (_localeCaches.ContainsKey(cultureName))
            return _localeCaches[cultureName];

        string fileName = _availableResources[cultureName];
        using var fileStream = ResourcesAssembly.GetManifestResourceStream(fileName);
        if (fileStream == null) return null;
        using var streamReader = new StreamReader(fileStream);
        var content = streamReader.ReadToEnd();

        var locale = I18nReader.Read(content);
        
        _localeCaches.Add(cultureName, locale);
        
        I18nCache.AddLocale(cultureName, locale);

        return locale;
    }

    public static bool ContainsLocale(string culture)
    {
        return _localeCaches.ContainsKey(culture);
    }
}
