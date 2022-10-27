using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace BlazorComponent;

public class Linker : ILinkable
{
    public bool Linkage { get; set; }

    public bool Exact { get; set; }

    public NavigationManager NavigationManager { get; set; }

    public Linker(ILinkable linkable)
    {
        Linkage = linkable.Linkage;
        Exact = linkable.Exact;
        NavigationManager = linkable.NavigationManager;
    }

    public bool MatchRoute(string href)
    {
        var baseRelativePath = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        return MatchRoute(href, baseRelativePath, Exact);
    }

    public static bool MatchRoute(string href, string relativePath, bool exact)
    {
        if (!href.StartsWith("/"))
        {
            href = "/" + href;
        }

        if (!relativePath.StartsWith("/"))
        {
            relativePath = "/" + relativePath;
        }

        if (exact || href == "/")
        {
            href += "$";
        }

        href = "^" + href;

        return Regex.Match(relativePath, href, RegexOptions.IgnoreCase).Success;
    }
}