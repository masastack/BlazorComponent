namespace BlazorComponent.Web;

public static class ElementReferenceExtensions
{
    public static string? GetSelector(this ElementReference? elementReference)
    {
        return elementReference?.GetSelector();
    }

    public static string? GetSelector(this ElementReference elementReference)
    {
        if (elementReference.Context is null)
        {
            return null;
        }

        return $"[_bl_{elementReference.Id}]";
    }
}
