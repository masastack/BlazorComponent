using Microsoft.JSInterop;

namespace BlazorComponent.I18n
{
    public class CookieStorage
    {
        IJSRuntime _jsRuntime;

        public CookieStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        const string GetCookieJs = "(function(name){const reg = new RegExp(`(^| )${name}=([^;]*)(;|$)`);const arr = document.cookie.match(reg);if (arr) {return unescape(arr[2]);}return null;})";

        const string SetCookieJs = "(function(name,value){ var Days = 30;var exp = new Date();exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);document.cookie = `${name}=${escape(value.toString())};path=/;expires=${exp.toUTCString()}`;})";

        public async Task<string> GetCookie(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("eval", $"{GetCookieJs}('{key}')");
        }

        public async void SetItemAsync<T>(string key, T? value)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("eval", $"{SetCookieJs}('{key}','{value}')");
            }
            catch
            {

            }
        }
    }
}
