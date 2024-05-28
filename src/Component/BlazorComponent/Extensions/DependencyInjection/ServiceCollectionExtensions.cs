using Microsoft.Extensions.DependencyInjection.Extensions;
﻿using BlazorComponent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IBlazorComponentBuilder AddBlazorComponent(this IServiceCollection services,
            Action<BlazorComponentOptions>? optionsAction = null,
            ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
        {
            if (optionsAction is not null)
            {
                services.AddOptions<BlazorComponentOptions>().Configure(optionsAction);
            }

            services.TryAddScoped<LocalStorage>();
            services.TryAddScoped<Document>();
            services.TryAddScoped(serviceProvider => new Window(serviceProvider.GetRequiredService<Document>()));
            services.AddI18n();

            return new BlazorComponentBuilder(services);
        }
    }
}
