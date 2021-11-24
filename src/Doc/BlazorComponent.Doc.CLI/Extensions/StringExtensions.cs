namespace Microsoft.Extensions.CommandLineUtils;

public static class StringExtensions
{
    public static string RemoveTag(this string markup, string tag)
    {
        if (markup == null) return null;
        
        var start = $"<{tag}>";
        var end = $"</{tag}>";

        if (!markup.Contains(start) || !markup.Contains(end)) return markup;

        var from = markup.IndexOf(start);
        var to = markup.LastIndexOf(end) + end.Length;

        var tagContent = markup[from..to];

        return markup.Replace(tagContent, "");
    }
}