using System.Text.RegularExpressions;

namespace BlazorComponent;

public interface IRoutable
{
    IDictionary<string, object?> Attributes { get; }

    bool Disabled { get; }

    string? Href { get; }

    bool Link { get; }

    EventCallback<MouseEventArgs> OnClick { get; }

    string? Tag { get; }

    string? Target { get; }

    bool Exact { get; }

    NavigationManager NavigationManager { get; }

    public bool IsClickable => !Disabled && (IsLink || OnClick.HasDelegate || Tabindex > 0);

    public bool IsLink => Href != null || Link;

    public int Tabindex => Attributes.TryGetValue("tabindex", out var tabindex) ? Convert.ToInt32(tabindex) : 0;

    public(string tag, Dictionary<string, object?>) GenerateRouteLink()
    {
        string tag;
        Dictionary<string, object?> attrs = new(Attributes);

        if (Href != null)
        {
            tag = "a";
            attrs["href"] = Href;
        }
        else
        {
            tag = Tag ?? "div";
        }

        if (Target != null)
        {
            attrs["target"] = Target;
        }

        return (tag, attrs);
    }

    // TODO: rename
    public bool MatchRoute()
    {
        if (Href is null) return false;

        var baseRelativePath = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        return MatchRoute(Href, baseRelativePath, Exact);
    }

    public static bool MatchRoute(string href, string relativePath, bool exact)
    {
        href = FormatUrl(href);

        relativePath = relativePath.Split('#', '?')[0];
        relativePath = FormatUrl(relativePath);

        if (exact || href == "/")
        {
            href += "$";
        }

        href = "^" + href;

        return Regex.Match(relativePath, href, RegexOptions.IgnoreCase).Success;
    }

    private static string FormatUrl(string url)
    {
        if (!url.StartsWith("/"))
        {
            return "/" + url;
        }

        return url;
    }
}
