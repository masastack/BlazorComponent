using BlazorComponent;
using BlazorComponent.Web;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using BlazorComponent.I18n;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static async Task<IServiceCollection> AddBlazorComponent(this IServiceCollection services)
        {
            services.TryAddScoped<DomEventJsInterop>();
            services.TryAddScoped<HeadJsInterop>();
            services.TryAddScoped<Document>();
            services.TryAddScoped(serviceProvider => new Window(serviceProvider.GetService<Document>()));
            services.TryAddScoped<IPopupProvider, PopupProvider>();
            services.TryAddSingleton<IComponentIdGenerator, GuidComponentIdGenerator>();
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;
            services.AddSingleton<IComponentActivator, AbstractComponentActivator>();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped, includeInternalTypes: true);

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyPath is not null)
            {
                var i18nLocalesPath = Path.Combine(assemblyPath, "I18n/Locales");
                if (Directory.Exists(i18nLocalesPath))
                {
                    var files = new List<string>();
                    var languageMap = new List<(string language, Dictionary<string, string>)>();
                    files.AddRange(Directory.GetFiles(i18nLocalesPath));
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
            else
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var languages = await httpClient.GetStringAsync("http://localhost:5000/_content/Masa.Blazor.Doc/I18n/Locales/en-US.json");
                
                // TODO: only wwwroot ?
            }



            return services;
        }
    }
}
