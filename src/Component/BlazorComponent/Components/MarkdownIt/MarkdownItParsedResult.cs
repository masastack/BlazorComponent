using System.Text.Json.Serialization;

namespace BlazorComponent;

public class MarkdownItParsedResult
{
    public string? FrontMatter { get; set; }

    [JsonPropertyName("markup")]
    public string MarkupContent { get; set; }

    public List<MarkdownItHeading> Toc { get; set; }
}
