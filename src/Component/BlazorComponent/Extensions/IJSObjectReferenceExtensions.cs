using Microsoft.JSInterop;

namespace BlazorComponent
{
    public static class IJSObjectReferenceExtensions
    {
        public static async ValueTask TryInvokeVoidAsync(this IJSObjectReference? jsObjectReference, string identifier, params object?[]? args)
        {
            if (jsObjectReference is null)
                return;
            
            await jsObjectReference.InvokeVoidAsync(identifier, args);
        }

        public static async ValueTask TryInvokeVoidAsync(this IJSObjectReference? jsObjectReference, Func<bool> conditions, string identifier, params object?[]? args)
        {
            if (jsObjectReference is null || !conditions())
                return;
                
            await jsObjectReference.InvokeVoidAsync(identifier, args);
        }
    }
}
