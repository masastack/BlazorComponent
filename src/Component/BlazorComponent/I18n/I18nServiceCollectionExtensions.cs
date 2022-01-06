using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

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
                I18n.AddLang(language, map);
                if (map.TryGetValue(DefaultLanguageKey, out string defaultLanguage) && defaultLanguage == "true") I18n.DefaultLanguage = language;
            }

            services.AddScoped<I18n>();
            services.AddScoped<I18nConfig>();
            services.AddScoped<CookieStorage>();

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
                var absolutePath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), languageDirectory));
                if (Directory.Exists(absolutePath))
                {
                    AddMasaI18n(absolutePath);
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
                    var map = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(filePath));
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
                var map = await httpclient.GetFromJsonAsync<Dictionary<string, string>>(Path.Combine(languageDirectoryApi, $"{language}.json"));
                languageMap.Add((language, map));
            }
            services.AddMasaI18n(languageMap);
        }

        public static async Task<ParameterView> GetMasaI18nParameterAsync(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var i18nConfig = provider.GetRequiredService<I18nConfig>();
            await i18nConfig.Initialization();
            return ParameterView.FromDictionary(new Dictionary<string, object?> { [nameof(I18nConfig)] = i18nConfig });
        }
    }
}
