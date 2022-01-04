using BlazorComponent.I18n;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class I18nApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMasaI18n(this IApplicationBuilder app)
        {
            app.UseMiddleware<CookieMiddleware>();

            return app;
        }
    }
}
