using Microsoft.JSInterop;

namespace BlazorComponent
{
    public static class IJSObjectReferenceExtensions
    {
        public static async ValueTask TryInvokeVoidAsync(this IJSObjectReference? jsObjectReference, string identifier, params object?[]? args)
        {
            if (jsObjectReference is not null)
                await jsObjectReference.InvokeVoidAsync(identifier, args);
        }

        public static async ValueTask TryInvokeVoidAsync(this IJSObjectReference? jsObjectReference, Func<bool> conditions, string identifier, params object?[]? args)
        {
            if (jsObjectReference is not null && conditions())
                await jsObjectReference.InvokeVoidAsync(identifier, args);
        }
    }
}
