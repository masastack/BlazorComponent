using System.Reflection;
using BlazorComponent.Doc.Highlight;
using BlazorComponent.Doc.Localization;
using BlazorComponent.Doc.Routing;
using BlazorComponent.Doc.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntDesignDocs(this IServiceCollection services)
        {
            // Replace it with your own library
            services.AddAntDesign();

            services.AddSingleton<RouteManager>();
            services.AddScoped<DemoService>();
            services.AddScoped<IconListService>();
            services.AddSingleton<ILanguageService>(new InAssemblyLanguageService(Assembly.GetExecutingAssembly()));
            services.AddScoped<IPrismHighlighter, PrismHighlighter>();

            return services;
        }
    }
}
