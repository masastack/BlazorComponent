using System.Text.Json.Serialization;

namespace BlazorComponent
{
    public class Window
    {
        [JsonPropertyName("innerHeight")]
        public double InnerHeight { get; set; }

        [JsonPropertyName("innerWidth")]
        public double InnerWidth { get; set; }
    }
}
