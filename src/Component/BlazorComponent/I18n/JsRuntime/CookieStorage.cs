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
        return await _jsRuntime.InvokeAsync<string>(JsInteropConstants.GetCookie, key);
    }

    public string? GetCookie(string key)
    {
        if (_jsRuntime is IJSInProcessRuntime jsInProcess)
        {
            return jsInProcess.Invoke<string>(JsInteropConstants.GetCookie, key);
        }

        // TODO: how to read config in MAUI?

        return null;
    }

    public async void SetItemAsync<T>(string key, T? value)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync(JsInteropConstants.SetCookie, key, value?.ToString());
        }
        catch
        {
        }
    }
}
