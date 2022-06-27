using System.Globalization;
using BlazorComponent.I18n;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class I18nServiceCollectionExtensions
{
    const string SupportedCulturesJson = "supportedCultures.json";
    const string DefaultCultureKey = "$DefaultCulture";

    internal static IServiceCollection AddI18n(this IServiceCollection services)
    {
        services.TryAddScoped<I18n>();
        services.TryAddScoped<CookieStorage>();
        services.AddHttpContextAccessor();

        return services;
    }

    /// <summary>
    /// Add MasaI18n service according to the physical path of the folder where the i18n resource file is located
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="localeDirectory">i18n resource folder physical path,i18n resource file name will be used as culture name</param>
    /// <returns></returns>
    public static IBlazorComponentBuilder AddI18nForServer(this IBlazorComponentBuilder builder, string localeDirectory)
    {
        if (Directory.Exists(localeDirectory))
        {
            CacheLocalesFromPath(localeDirectory);
        }
        else
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var i18nPath = Path.Combine(assemblyPath, localeDirectory);
            if (Directory.Exists(i18nPath))
            {
                CacheLocalesFromPath(i18nPath);
            }
            else if (localeDirectory.StartsWith("wwwroot"))
            {
                var wwwrootPath = Path.Combine(Path.Combine(assemblyPath, "wwwroot"));
                if (Directory.Exists(wwwrootPath))
                {
                    var i18nDirectory = localeDirectory.Split('/').Last();
                    i18nPath = Directory.GetDirectories(wwwrootPath, i18nDirectory, SearchOption.AllDirectories).FirstOrDefault();
                    if (i18nPath is not null)
                    {
                        CacheLocalesFromPath(i18nPath);
                    }
                    else throw new Exception($"Can't find path：{localeDirectory}");
                }
            }
            else throw new Exception($"Can't find path：{localeDirectory}");
        }

        return builder;
    }

    public static async Task<IBlazorComponentBuilder> AddI18nForWasmAsync(this IBlazorComponentBuilder builder, string localesDirectoryApi)
    {
        using var httpclient = new HttpClient();

        string supportedCulturesApi = Path.Combine(localesDirectoryApi, SupportedCulturesJson);

        var cultures = await httpclient.GetFromJsonAsync<string[]>(supportedCulturesApi) ??
                       throw new Exception("Failed to read supportedCultures json file data!");

        var locales = new List<(string culture, Dictionary<string, string>)>();

        foreach (var culture in cultures)
        {
            await using var stream = await httpclient.GetStreamAsync(Path.Combine(localesDirectoryApi, $"{culture}.json"));
            using StreamReader reader = new StreamReader(stream);
            var map = I18nReader.Read(reader.ReadToEnd());
            locales.Add((culture, map));
        }

        CacheLocales(locales);

        return builder;
    }

    private static void CacheLocalesFromPath(string path)
    {
        var files = new List<string>();
        var locales = new List<(string culture, Dictionary<string, string>)>();
        var supportedCulturesPath = Path.Combine(path, SupportedCulturesJson);
        if (File.Exists(supportedCulturesPath))
        {
            var cultures = JsonSerializer.Deserialize<string[]>(File.ReadAllText(supportedCulturesPath));
            files.AddRange(cultures.Select(culture => Path.Combine(path, $"{culture}.json")));
        }
        else
        {
            files.AddRange(Directory.GetFiles(path));
        }

        foreach (var filePath in files)
        {
            var culture = Path.GetFileNameWithoutExtension(filePath);
            var json = File.ReadAllText(filePath);
            var locale = I18nReader.Read(json);
            locales.Add((culture, locale));
        }

        CacheLocales(locales);
    }

    private static void CacheLocales(IEnumerable<(string culture, Dictionary<string, string>)> locales)
    {
        foreach (var (cultureName, map) in locales)
        {
            var culture = CultureInfo.CreateSpecificCulture(cultureName);
            if (string.IsNullOrEmpty(culture.Name))
            {
                continue;
            }

            I18nCache.AddLocale(culture, map);

            if (map.TryGetValue(DefaultCultureKey, out string defaultCulture) && defaultCulture == "true")
                I18nCache.DefaultCulture = culture;
        }
    }
}
