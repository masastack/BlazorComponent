namespace BlazorComponent;

public class Locale
{
    public string? Current { get; }

    public string? Fallback { get; }

    public Locale(string current, string fallback)
    {
        Current = current;
        Fallback = fallback;
    }
}
