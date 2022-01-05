using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        static HttpClient _httpClient;

        static string _basePath;

        static I18nServiceCollectionExtensions()
        {
            _httpClient = new HttpClient();
            _basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, IEnumerable<(string language, Dictionary<string, string>)> languageMap, string? defaultLanguage = null)
        {
            foreach (var (language, map) in languageMap)
            {
                I18n.AddLang(language, map, language == defaultLanguage);
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
        /// <param name="defaultLanguage"></param>
        /// <returns></returns>
        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, string languageDirectory, string? defaultLanguage = null)
        {
            var files = Directory.GetFiles(Path.Combine(_basePath, languageDirectory));
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var filePath in files)
            {
                var language = Path.GetFileNameWithoutExtension(filePath);
                var map = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(filePath));
                languageMap.Add((language, map));
            }
            services.AddMasaI18n(languageMap, defaultLanguage);

            return services;
        }

        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, LanguageConfig languageConfig)
        {
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var language in languageConfig.Languages)
            {
                var map = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(Path.Combine(_basePath, languageConfig.LanguageFileDirectoryForServer, $"{language}.json")));
                languageMap.Add((language, map));
            }
            services.AddMasaI18n(languageMap, languageConfig.DefaultLanguage);

            return services;
        }

        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, string languageConfigFilePath)
        {
            var languageConfig = JsonSerializer.Deserialize<LanguageConfig>(File.ReadAllText(Path.Combine(_basePath, languageConfigFilePath))) ?? throw new Exception("Failed to read i18n json file data!");
            services.AddMasaI18nForServer(languageConfig);

            return services;
        }

        public static async Task AddMasaI18nForWasm(this IServiceCollection services, string baseUri, LanguageConfig languageConfig)
        {
            SetHttpClientBaseUri(baseUri);
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var language in languageConfig.Languages)
            {
                var map = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(Path.Combine(languageConfig.LanguageFileDirectoryForWasm, $"{language}.json"));
                languageMap.Add((language, map));
            }
            services.AddMasaI18n(languageMap, languageConfig.DefaultLanguage);
        }

        public static async Task AddMasaI18nForWasm(this IServiceCollection services, string baseUri, string uri)
        {
            SetHttpClientBaseUri(baseUri);
            var languageConfig = await _httpClient.GetFromJsonAsync<LanguageConfig>(uri) ?? throw new Exception("Failed to read i18n json file data!");
            await services.AddMasaI18nForWasm(baseUri, languageConfig);
        }

        public static async Task<ParameterView> GetMasaI18nParameter(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var i18nConfig = provider.GetRequiredService<I18nConfig>();
            await i18nConfig.Initialization();
            return ParameterView.FromDictionary(new Dictionary<string, object?> { [nameof(I18nConfig)] = i18nConfig });
        }

        static void SetHttpClientBaseUri(string baseUri)
        {
            if (_httpClient.BaseAddress is null) _httpClient.BaseAddress = new Uri(baseUri);
        }
    }
}
