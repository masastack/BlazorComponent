using BlazorComponent;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorComponent(this IServiceCollection services)
        {
            services.TryAddScoped<DomEventJsInterop>();
            //services.TryAddScoped(sp => new HtmlRenderService(new HtmlRenderer(sp, sp.GetRequiredService<ILoggerFactory>(),
            //            s => HtmlEncoder.Default.Encode(s)))
            //);

            services.TryAddSingleton<IComponentIdGenerator, GuidComponentIdGenerator>();
            //services.TryAddScoped<IconService>();
            //services.TryAddScoped<InteropService>();
            //services.TryAddScoped<NotificationService>();
            //services.TryAddScoped<MessageService>();
            //services.TryAddScoped<ModalService>();
            //services.TryAddScoped<DrawerService>();
            //services.TryAddScoped<ConfirmService>();
            //services.TryAddScoped<ImageService>();

            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;

            return services;
        }
    }
}
