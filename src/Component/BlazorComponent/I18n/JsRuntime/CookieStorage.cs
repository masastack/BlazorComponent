using Microsoft.JSInterop;

namespace BlazorComponent.I18n;

public class CookieStorage
{
    private readonly IJSRuntime _jsRuntime;

    public CookieStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    const string GetCookieJs =
        "(function(name){const reg = new RegExp(`(^| )${name}=([^;]*)(;|$)`);const arr = document.cookie.match(reg);if (arr) {return unescape(arr[2]);}return null;})";

    const string SetCookieJs =
        @"(function(name,value){var host=document.domain;var domain=host;var domainRegexStr='([a-z0-9--]{1,200})\\.(ac.cn|bj.cn|sh.cn|tj.cn|cq.cn|he.cn|sn.cn|sx.cn|nm.cn|ln.cn|jl.cn|hl.cn|js.cn|zj.cn|ah.cn|fj.cn|jx.cn|sd.cn|ha.cn|hb.cn|hn.cn|gd.cn|gx.cn|hi.cn|sc.cn|gz.cn|yn.cn|gs.cn|qh.cn|nx.cn|xj.cn|tw.cn|hk.cn|mo.cn|xz.cn|com.cn|net.cn|org.cn|gov.cn|.com.hk|我爱你|在线|中国|网址|网店|中文网|公司|网络|集团|com|cn|cc|org|net|xin|xyz|vip|shop|top|club|wang|fun|info|online|tech|store|site|ltd|ink|biz|group|link|work|pro|mobi|ren|kim|name|tv|red|cool|team|live|pub|company|zone|today|video|art|chat|gold|guru|show|life|love|email|fund|city|plus|design|social|center|world|auto|.rip|.ceo|.sale|.hk|.io|.gg|.tm|.gs|.us)$';var domainRegex=new RegExp(domainRegexStr);if(domainRegex.test(domain)){domain=`.${host.match(domainRegex)[0]}`}var Days=30;var exp=new Date();exp.setTime(exp.getTime()+Days*24*60*60*1000);document.cookie=`${name}=${escape(value.toString())};path=/;expires=${exp.toUTCString()};domain=${domain}`})";

    public async Task<string> GetCookieAsync(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("eval", $"{GetCookieJs}('{key}')");
    }

    public string? GetCookie(string key)
    {
        if (_jsRuntime is IJSInProcessRuntime jsInProcess)
        {
            return jsInProcess.Invoke<string>("eval", $"{GetCookieJs}('{key}')");
        }

        // TODO: how to read config in MAUI?

        return null;
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
