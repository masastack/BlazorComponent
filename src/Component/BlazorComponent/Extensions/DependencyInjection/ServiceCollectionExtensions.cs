using BlazorComponent;
using BlazorComponent.Web;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorComponent(this IServiceCollection services)
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

            return services;
        }
    }
}
