using BlazorComponent.I18n;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        const string LanguageConfigJson = "languageConfig.json";
        const string DefaultLanguageKey = "$DefaultLanguage";

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, IEnumerable<(string language, Dictionary<string, string>)> languageMap)
        {
            foreach (var (language, map) in languageMap)
            {
                I18nCache.AddLocale(language, map);
                if (map.TryGetValue(DefaultLanguageKey, out string defaultLanguage) && defaultLanguage == "true") I18nCache.DefaultCulture = language;
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
        /// <param name="languageDirectory">i18n resource folder physical path,i18n resource file name will be used as language name</param>
        /// <returns></returns>
        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, string languageDirectory)
        {
            if (Directory.Exists(languageDirectory))
            {
                AddMasaI18n(languageDirectory);
            }
            else
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var i18nPath = Path.Combine(assemblyPath, languageDirectory);
                if (Directory.Exists(i18nPath))
                {
                    AddMasaI18n(i18nPath);
                }
                else if (languageDirectory.StartsWith("wwwroot"))
                {
                    var wwwrootPath = Path.Combine(Path.Combine(assemblyPath, "wwwroot"));
                    if (Directory.Exists(wwwrootPath))
                    {
                        var i18nDirectory = languageDirectory.Split('/').Last();
                        i18nPath = Directory.GetDirectories(wwwrootPath, i18nDirectory, SearchOption.AllDirectories).FirstOrDefault();
                        if (i18nPath is not null)
                        {
                            AddMasaI18n(i18nPath);
                        }
                        else throw new Exception($"Can't find path：{languageDirectory}");
                    }
                }
                else throw new Exception($"Can't find path：{languageDirectory}");
            }

            return services;

            void AddMasaI18n(string path)
            {
                var files = new List<string>();
                var languageMap = new List<(string language, Dictionary<string, string>)>();
                var languageConfigPath = Path.Combine(path, LanguageConfigJson);
                if (File.Exists(languageConfigPath))
                {
                    var languages = JsonSerializer.Deserialize<string[]>(File.ReadAllText(languageConfigPath));
                    files.AddRange(languages.Select(language => Path.Combine(path, $"{language}.json")));
                }
                else
                {
                    files.AddRange(Directory.GetFiles(path));
                }
                foreach (var filePath in files)
                {
                    var language = Path.GetFileNameWithoutExtension(filePath);
                    var json = File.ReadAllText(filePath);
                    var map = I18nReader.Read(json);
                    languageMap.Add((language, map));
                }
                services.AddMasaI18n(languageMap);
            }
        }

        public static async Task AddMasaI18nForWasmAsync(this IServiceCollection services, string languageDirectoryApi)
        {
            using var httpclient = new HttpClient();
            string languageConfigApi = Path.Combine(languageDirectoryApi, LanguageConfigJson);
            var languages = await httpclient.GetFromJsonAsync<string[]>(languageConfigApi) ?? throw new Exception("Failed to read languageConfig json file data!");
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var language in languages)
            {
                using var stream = await httpclient.GetStreamAsync(Path.Combine(languageDirectoryApi, $"{language}.json"));
                using StreamReader reader = new StreamReader(stream);
                var map = I18nReader.Read(reader.ReadToEnd());
                languageMap.Add((language, map));
            }
            services.AddMasaI18n(languageMap);
        }
    }
}
