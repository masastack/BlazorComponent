using System.Text.Json.Serialization;

namespace Microsoft.AspNetCore.Components.Web;

public class EventTarget
{
    public string Selector { get; set; }

    [JsonPropertyName("_bl_")]
    public string? ElementReferenceId { get; set; }

    public string Class { get; set; }

    public ElementReference ElementReference => ElementReferenceId is not null
        ? new ElementReference(ElementReferenceId[4..])
        : default;
}