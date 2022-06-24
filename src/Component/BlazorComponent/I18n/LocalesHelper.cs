using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorComponent.I18n;

internal static class LocalesHelper
{
    private static Dictionary<string, string> _availableResources = new();

    private static readonly Assembly ResourcesAssembly = typeof(I18n).Assembly;

    public static void Init()
    {
        _availableResources = ResourcesAssembly
                              .GetManifestResourceNames()
                              .Select(s => Regex.Match(s, @"^.*Locales\.(.+)\.json"))
                              .Where(s => s.Success)
                              .ToDictionary(s => s.Groups[1].Value, s => s.Value);

        TryGetSpecifiedLocale(CultureInfo.CurrentCulture.Name);
    }

    public static IReadOnlyDictionary<string, string> TryGetSpecifiedLocale(string cultureName)
    {
        if (!_availableResources.ContainsKey(cultureName))
            return I18nCache.GetLang(cultureName);

        string fileName = _availableResources[cultureName];
        using var fileStream = ResourcesAssembly.GetManifestResourceStream(fileName);
        if (fileStream == null) return null;
        using var streamReader = new StreamReader(fileStream);
        var content = streamReader.ReadToEnd();

        var map = I18nReader.Read(content);
        I18nCache.AddLang(cultureName, map);

        return map;
    }
}
