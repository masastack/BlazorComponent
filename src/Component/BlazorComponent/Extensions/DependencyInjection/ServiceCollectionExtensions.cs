using BlazorComponent;
using BlazorComponent.Web;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IBlazorComponentBuilder AddBlazorComponent(this IServiceCollection services)
        {
            services.TryAddScoped<DomEventJsInterop>();
            services.TryAddScoped<HeadJsInterop>();
            services.TryAddScoped<Document>();
            services.TryAddScoped(serviceProvider => new Window(serviceProvider.GetService<Document>()));
            services.TryAddScoped<IPopupProvider, PopupProvider>();
            services.TryAddSingleton<IComponentIdGenerator, GuidComponentIdGenerator>();
            services.AddScoped(typeof(BDragDropService));
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;
            services.AddSingleton<IComponentActivator, AbstractComponentActivator>();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped, includeInternalTypes: true);

            services.TryAddScoped<MarkdownItProxyModule>();
            services.TryAddScoped<GridstackProxyModule>();

            services.AddI18n();

            return new BlazorComponentBuilder(services);
        }
    }
}
