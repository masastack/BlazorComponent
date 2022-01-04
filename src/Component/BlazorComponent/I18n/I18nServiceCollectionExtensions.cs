using BlazorComponent.I18n;
using System.Text.Json;
using System.IO;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        static HttpClient _httpClient;

        static string _baseUrl;

        static I18nServiceCollectionExtensions()
        {
            _httpClient = new HttpClient();
            _baseUrl = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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
            var files = Directory.GetFiles(Path.Combine(_baseUrl, languageDirectory));
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var filePath in files)
            {
                var language = Path.GetFileNameWithoutExtension(filePath);
                var map = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(Path.Combine(_baseUrl, filePath)));
                languageMap.Add((language, map));
            }
            services.AddMasaI18n(languageMap, defaultLanguage);

            return services;
        }

        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, LanguageConfig languageConfig)
        {
            var languageMap = languageConfig.Languages.Select(language => (language.Value, JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(language.FilePath))));
            services.AddMasaI18n(languageMap, languageConfig.DefaultLanguage);

            return services;
        }

        public static IServiceCollection AddMasaI18nForServer(this IServiceCollection services, string languageConfigFilePath)
        {
            var languageConfig = JsonSerializer.Deserialize<LanguageConfig>(File.ReadAllText(Path.Combine(_baseUrl, languageConfigFilePath))) ?? throw new Exception("Failed to read i18n jsonn file data!");
            services.AddMasaI18nForServer(languageConfig);

            return services;
        }

        public static async Task AddMasaI18nForWasm(this IServiceCollection services, string baseUri, LanguageConfig languageConfig)
        {
            SetHttpClientBaseUri(baseUri);
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var language in languageConfig.Languages)
            {
                var map = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(language.FilePath);
                languageMap.Add((language.Value, map));
            }
            services.AddMasaI18n(languageMap, languageConfig.DefaultLanguage);
        }

        public static async Task AddMasaI18nForWasm(this IServiceCollection services, string baseUri, string uri)
        {
            SetHttpClientBaseUri(baseUri);
            var languageConfig = await _httpClient.GetFromJsonAsync<LanguageConfig>(uri) ?? throw new Exception("Failed to read i18n json file data!");
            await services.AddMasaI18nForWasm(baseUri, languageConfig);
        }

        public static ParameterView GetMasaI18nParameter(this IServiceProvider servicesProvider)
        {
            var i18nConfig = servicesProvider.GetRequiredService<I18nConfig>();
            return ParameterView.FromDictionary(new Dictionary<string, object?> { [nameof(I18nConfig)] = i18nConfig });
        }

        static void SetHttpClientBaseUri(string baseUri)
        {
            if (_httpClient.BaseAddress is null) _httpClient.BaseAddress = new Uri(baseUri);
        }
    }

    public class LanguageConfig
    {

        public string? DefaultLanguage { get; set; }

        public List<Language> Languages { get; set; }

        public LanguageConfig(string? defaultLanguage, List<Language> languages)
        {
            DefaultLanguage = defaultLanguage;
            Languages = languages;
        }
    }

    public class Language
    {
        public Language(string value, string filePath)
        {
            Value = value;
            FilePath = filePath;
        }
        public string Value { get; set; }

        public string FilePath { get; set; }
    }
}
