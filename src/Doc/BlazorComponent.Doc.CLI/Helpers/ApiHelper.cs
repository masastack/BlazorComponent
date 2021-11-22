using System.Collections.Generic;
using System.Linq;
using BlazorComponent.Doc.Models;

namespace BlazorComponent.Doc.CLI.Helpers;

public static class ApiHelper
{
    public static List<ApiItem> GetApiDoc(string apiDoc)
    {
        if (string.IsNullOrEmpty(apiDoc)) return null;

        var lis = apiDoc.Split("</li>");

        return lis.Where(markup => markup.Contains("<li>"))
            .Select(item =>
            {
                var name = GetApiName(item);
                var href = GetApiHref(item);

                return new ApiItem(name, href);
            }).ToList();
    }

    private static string GetApiHref(string markup)
    {
        var href = "href=\"";
        var from = markup.IndexOf(href) + href.Length;
        var to = markup.LastIndexOf("\">");

        return markup.Substring(from, to - from);
    }

    private static string GetApiName(string markup)
    {
        var from = markup.IndexOf("\">") + "\">".Length;
        var to = markup.LastIndexOf("</a>");

        return markup.Substring(from, to - from);
    }
}