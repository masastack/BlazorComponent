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
        public decimal innerHeight { get; set; }

        public decimal innerWidth { get; set; }

        [JsonPropertyName("pageXOffset")]
        public double PageXOffset { get; set; }

        [JsonPropertyName("pageYOffset")]
        public double PageYOffset { get; set; }

        public bool IsTop { get; set; }

        public bool IsBottom { get; set; }
    }
}
