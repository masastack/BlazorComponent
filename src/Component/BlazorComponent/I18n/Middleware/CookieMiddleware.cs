using Microsoft.AspNetCore.Http;

namespace BlazorComponent.I18n
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, I18nConfig i18nConfig)
        {
            var cookies = context.Request.Cookies;
            i18nConfig.Initialization(cookies);

            await _next(context);
        }
    }
}
