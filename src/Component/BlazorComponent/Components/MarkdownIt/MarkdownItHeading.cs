using System.Text.Json.Serialization;

namespace BlazorComponent;

public class MarkdownItHeading
{
    public string Content { get; set; }

    [JsonPropertyName("link")]
    public string Hash { get; set; }

    public List<MarkdownItHeading> Children { get; set; }
    
    public int Level { get; set; }
}
