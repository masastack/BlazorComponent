using System.Text.Json;

namespace BlazorComponent;

public class LocalStorage
{
    private const string SET_ITEM_SCRIPT = """
function(key,value) {
  localStorage.setItem(key, value);
}
""";

    private const string GET_ITEM_SCRIPT = """
function(key) {
  return localStorage.getItem(key);
}
""";

    private const string REMOVE_ITEM_SCRIPT = """
function(key) {
  localStorage.removeItem(key);
}
""";

    private const string CLEAR_SCRIPT = """
function() {
  localStorage.clear();
}
""";

    private readonly IJSRuntime _jsRuntime;

    public LocalStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("eval", $"({SET_ITEM_SCRIPT})('{key}', '{value}')");
    }

    public async Task<string> GetItemAsync(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("eval", $"({GET_ITEM_SCRIPT})('{key}')");
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var value = await GetItemAsync(key);
        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task RemoveItemAsync(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("eval", $"({REMOVE_ITEM_SCRIPT})('{key}', '{value}')");
    }

    public async Task ClearAsync()
    {
        await _jsRuntime.InvokeVoidAsync("eval", $"({CLEAR_SCRIPT})()");
    }
}
