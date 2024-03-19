using Microsoft.Extensions.DependencyInjection.Extensions;
﻿using BlazorComponent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IBlazorComponentBuilder AddBlazorComponent(this IServiceCollection services,
            Action<BlazorComponentOptions>? optionsAction = null)
        {
            if (optionsAction is not null)
            {
                services.AddOptions<BlazorComponentOptions>().Configure(optionsAction);
            }

            services.TryAddScoped<LocalStorage>();
            services.TryAddScoped<Document>();
            services.TryAddScoped(serviceProvider => new Window(serviceProvider.GetRequiredService<Document>()));
            services.TryAddScoped<IPopupProvider, PopupProvider>();
            services.TryAddSingleton<IComponentIdGenerator, GuidComponentIdGenerator>();
            services.AddScoped(typeof(BDragDropService));
            services.AddSingleton<IComponentActivator, AbstractComponentActivator>();
            services.AddI18n();

            services.TryAddTransient<ActivatableJsModule>();
            services.TryAddTransient<OutsideClickJSModule>();
            services.TryAddTransient<ScrollStrategyJSModule>();
            services.TryAddTransient<InputJSModule>();
            services.TryAddTransient<TransitionJSModule>();

            return new BlazorComponentBuilder(services);
        }
    }
}
