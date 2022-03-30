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

        if (Exact || baseRelativePath == string.Empty)
        {
            href += "$";
        }

        var relativePath = "/" + baseRelativePath;

        return Regex.Match(relativePath, href, RegexOptions.IgnoreCase).Success;
    }
}