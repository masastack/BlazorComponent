using BlazorComponent.I18n;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        const string SupportedCulturesJson = "supportedCultures.json";
        const string DefaultCultureKey = "$DefaultCulture";

        public static IServiceCollection AddMasaI18n(this IServiceCollection services,
            IEnumerable<(string culture, Dictionary<string, string>)> locales)
        {
            foreach (var (locale, map) in locales)
            {
                I18nCache.AddLocale(locale, map);
                if (map.TryGetValue(DefaultCultureKey, out string defaultCulture) && defaultCulture == "true") I18nCache.DefaultCulture = locale;
            }

            services.TryAddScoped<I18n>();
            services.TryAddScoped<CookieStorage>();
            services.AddHttpContextAccessor();

            return services;
        }

        /// <summary>
        /// Add MasaI18n service according to the physical path of the folder where the i18n resource file is located
        /// </summary>
        /// <param name="services"></param>
        /// <param name="localeDirectory">i18n resource folder physical path,i18n resource file name will be used as culture name</param>
        /// <returns></returns>
        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, string localeDirectory)
        {
            if (Directory.Exists(localeDirectory))
            {
                AddMasaI18n(localeDirectory);
            }
            else
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var i18nPath = Path.Combine(assemblyPath, localeDirectory);
                if (Directory.Exists(i18nPath))
                {
                    AddMasaI18n(i18nPath);
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
                            AddMasaI18n(i18nPath);
                        }
                        else throw new Exception($"Can't find path：{localeDirectory}");
                    }
                }
                else throw new Exception($"Can't find path：{localeDirectory}");
            }

            return services;

            void AddMasaI18n(string path)
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

                services.AddMasaI18n(locales);
            }
        }

        public static async Task AddMasaI18nForWasmAsync(this IServiceCollection services, string localesDirectoryApi)
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

            services.AddMasaI18n(locales);
        }
    }
}
