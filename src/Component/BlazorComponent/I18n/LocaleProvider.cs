using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BlazorComponent.Components
{
    public class LocaleProvider
    {
        private static readonly ConcurrentDictionary<string, Locale> _localeCache = new ConcurrentDictionary<string, Locale>();
        private static Assembly _resourceAssembly = typeof(LocaleProvider).Assembly;
        private static readonly IDictionary<string, string> _resources = _resourceAssembly
            .GetManifestResourceNames()
            .Select(resource => Regex.Match(resource, @"^.*Locales\.(.+)\.json"))
            .Where(match => match.Success)
            .ToDictionary(match => match.Groups[1].Value, x => x.Value);

        public static Locale CurrentLocale => GetCurrentLocale();

        public static string DefaultLanguage { get; set; } = "en-US";

        public static Locale GetCurrentLocale()
        {
            var currentCulture = CultureInfo.CurrentUICulture?.Name;
            if (string.IsNullOrWhiteSpace(currentCulture) || !_resources.ContainsKey(currentCulture))
            {
                currentCulture = DefaultLanguage;
            }

            return GetLocale(currentCulture);
        }

        public static Locale GetLocale(string cultureName)
        {
            return _localeCache.GetOrAdd(cultureName, key =>
            {
                //fallback to default language if not found
                if (!_resources.TryGetValue(key, out var fileName))
                    fileName = _resources[DefaultLanguage];

                using var fileStream = _resourceAssembly.GetManifestResourceStream(fileName);
                if (fileStream == null) return null;
                using var streamReader = new StreamReader(fileStream);
                var content = streamReader.ReadToEnd();

                var serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };
                serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                var result = JsonSerializer.Deserialize<Locale>(content, serializerOptions);

                return result;
            });
        }

        public static void SetLocale(string cultureName, Locale locale = null)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);

            if (culture != null)
            {
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            if (locale != null)
            {
                _localeCache.AddOrUpdate(cultureName, locale, (name, original) => locale);
            }
        }
    }
}
