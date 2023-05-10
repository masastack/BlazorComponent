namespace BlazorComponent.Web;

public static class ElementReferenceExtensions
{
    public static string? GetSelector(this ElementReference? elementReference)
    {
        return elementReference?.GetSelector();
    }

    public static string? GetSelector(this ElementReference elementReference)
    {
        elementReference.TryGetSelector(out var selector);

        return selector;
    }

    public static bool TryGetSelector(this ElementReference elementReference, [NotNullWhen(true)] out string? selector)
    {
        selector = null;

        if (elementReference.Id is null)
        {
            return false;
        }

        selector = $"[_bl_{elementReference.Id}]";

        return true;
    }
}
