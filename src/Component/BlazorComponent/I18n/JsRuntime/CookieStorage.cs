using Microsoft.JSInterop;

namespace BlazorComponent.I18n;

public class CookieStorage
{
    private readonly IJSRuntime _jsRuntime;

    public CookieStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetCookieAsync(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("getCookie", key);
    }

    public string? GetCookie(string key)
    {
        if (_jsRuntime is IJSInProcessRuntime jsInProcess)
        {
            return jsInProcess.Invoke<string>("getCookie", key);
        }

        // TODO: how to read config in MAUI?

        return null;
    }

    public async void SetItemAsync<T>(string key, T? value)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("setCookie", key, value?.ToString());
        }
        catch
        {
        }
    }
}
