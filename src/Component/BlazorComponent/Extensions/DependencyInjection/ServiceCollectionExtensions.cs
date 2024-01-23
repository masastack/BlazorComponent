using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using BlazorComponent;
using BlazorComponent.Components.OtpInput;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

            return new BlazorComponentBuilder(services);
        }

        /// <summary>
        /// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation?pivots=dotnet-6-0
        /// net7 add TypeInfoResolver
        /// jsonSerializerOptions.TypeInfoResolver = JsonTypeInfoResolver.Combine(jsonSerializerOptions.TypeInfoResolver, your TypeInfoResolver, ...);
        /// net 8 add TypeInfoResolver
        /// jsonSerializerOptions.TypeInfoResolverChain.Add(your TypeInfoResolver);
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IBlazorComponentBuilder AddJsonOptions(this IBlazorComponentBuilder builder, Action<JsonSerializerOptions>? optionsAction = null)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
#if NET6_0
            jsonSerializerOptions.AddContext<OtpJsResultJsonContext>();
#elif NET7_0_OR_GREATER
            jsonSerializerOptions.TypeInfoResolver = OtpJsResultJsonContext.Default;
#endif


            if (optionsAction is not null)
            {
                optionsAction.Invoke(jsonSerializerOptions);
            }
            
            builder.Services.AddSingleton(jsonSerializerOptions);


            return builder;
        }
    }
}
