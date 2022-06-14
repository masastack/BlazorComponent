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
            services.AddFluentValidation();

            return services;
        }

        private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            try
            {
                var referenceAssemblys = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var referenceAssembly in referenceAssemblys)
                {
                    if (referenceAssembly.FullName.StartsWith("Microsoft.") || referenceAssembly.FullName.StartsWith("System."))
                        continue;

                    var types = referenceAssembly.GetTypes().Where(t => t.BaseType?.IsGenericType == true && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)).ToArray();
                    foreach (var type in types)
                    {
                        services.AddScoped(type);
                    }
                }
            }
            catch
            {
            }
            return services;
        }
    }
}
