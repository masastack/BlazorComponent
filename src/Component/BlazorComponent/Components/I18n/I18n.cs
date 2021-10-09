using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;

namespace BlazorComponent.Components
{
    public class I18n
    {
        private static readonly ConcurrentDictionary<string, Dictionary<string, string>> _i18nCache = new ConcurrentDictionary<string, Dictionary<string, string>>();

        public static string CurrentCulture => GetCurrentCulture();

        public static Dictionary<string, string> CurrentLang => _i18nCache.GetValueOrDefault(CurrentCulture);

        public static string DefaultLanguage { get; set; } = "en-US";

        public static string GetCurrentCulture()
        {
            var currentCulture = CultureInfo.CurrentUICulture?.Name;
            if (string.IsNullOrWhiteSpace(currentCulture) || !_i18nCache.ContainsKey(currentCulture))
            {
                currentCulture = DefaultLanguage;
            }

            return currentCulture;
        }

        public static void SetLang(string cultureName)
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }

        public static void AddLang(string cultureName, Dictionary<string, string>[] langs)
        {
            if (langs is null) return;

            foreach (var lang in langs)
            {
                _i18nCache.AddOrUpdate(cultureName, lang, (name, original) => lang);
            }
        }

        public static string T(string key)
        {
            return _i18nCache.GetValueOrDefault(CurrentCulture).GetValueOrDefault(key);
        }
    }
}
