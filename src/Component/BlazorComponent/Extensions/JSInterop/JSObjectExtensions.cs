using System.Text.Json;

namespace BlazorComponent.Web
{
    public static class JSObjectExtensions
    {
        public static Task<double?> GetScrollWidthAsync(this JSObject jsObject)
        {
            return GetNumberPropAsync(jsObject, "scrollWidth");
        }

        public static Task<double?> GetOffsetWidthAsync(this JSObject jsObject)
        {
            return GetNumberPropAsync(jsObject, "offsetWidth");
        }

        public static Task<double?> GetClientWidthAsync(this JSObject jsObject)
        {
            return GetNumberPropAsync(jsObject, "clientWidth");
        }

        public static Task<double?> GetClientHeightAsync(this JSObject jsObject)
        {
            return GetNumberPropAsync(jsObject, "clientHeight");
        }

        public static Task<double?> GetInnerWidthAsync(this JSObject jsObject)
        {
            return GetNumberPropAsync(jsObject, "innerWidth");
        }

        public static Task<double?> GetInnerHeightAsync(this JSObject jsObject)
        {
            return GetNumberPropAsync(jsObject, "innerHeight");
        }

        public static async Task<double?> GetNumberPropAsync(this JSObject jsObject, string name)
        {
            var jsonElement = await jsObject.JS.InvokeAsync<JsonElement>(JsInteropConstants.GetProp, jsObject.Selector, name);
            return jsonElement.ValueKind == JsonValueKind.Number ? jsonElement.GetDouble() : null;
        }
    }
}
