using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class Window
    {
        [JsonPropertyName("innerHeight")]
        public double InnerHeight { get; set; }

        [JsonPropertyName("innerWidth")]
        public double InnerWidth { get; set; }

        [JsonPropertyName("pageXOffset")]
        public double PageXOffset { get; set; }

        [JsonPropertyName("pageYOffset")]
        public double PageYOffset { get; set; }

        public bool IsTop { get; set; }

        public bool IsBottom { get; set; }
    }
}
