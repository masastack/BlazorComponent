using System.Linq;

namespace BlazorComponent.Doc.Extensions;

public static class StringExtensions
{
    public static string HashSection(this string title)
    {
        title = title.ToLower();

        if (new[] { "api", "caveats" }.Contains(title))
            return title;

        return "section-" + HashHelper.Hash(title);
    }
}