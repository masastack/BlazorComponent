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
            services.AddValidators();
            services.AddI18n();

            return new BlazorComponentBuilder(services);
        }

        internal static IServiceCollection AddValidators(this IServiceCollection services)
        {
            var referenceAssembles = AppDomain.CurrentDomain.GetAssemblies();
            services.AddValidatorsFromAssemblies(referenceAssembles, ServiceLifetime.Scoped, includeInternalTypes: true);
            foreach (var referenceAssembly in referenceAssembles)
            {
                if (referenceAssembly!.FullName!.StartsWith("Microsoft.") || referenceAssembly.FullName.StartsWith("System."))
                    continue;

                var types = referenceAssembly
                            .GetTypes()
                            .Where(t => t.IsClass)
                            .Where(t => !t.IsAbstract)
                            .Where(t => typeof(IValidator).IsAssignableFrom(t))
                            .ToArray();

                foreach (var type in types)
                {
                    var modelType = type!.BaseType!.GenericTypeArguments[0];
                    if (modelType.IsGenericParameter && modelType.BaseType is not null)
                    {
                        var modelTypes = referenceAssembly.GetTypes().Where(t => modelType.BaseType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericTypeDefinition);
                        foreach (var item in modelTypes)
                        {
                            var validatorType = typeof(IValidator<>).MakeGenericType(item);
                            var implementationType = type.MakeGenericType(item);
                            services.AddScoped(validatorType, implementationType);
                        }
                    }
                }
            }

            return services;
        }
    }
}
