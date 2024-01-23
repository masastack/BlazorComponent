using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorComponent.Components.OtpInput
{
    [JsonSourceGenerationOptions(WriteIndented = true, GenerationMode = JsonSourceGenerationMode.Serialization)]
    [JsonSerializable(typeof(OtpJsResult), GenerationMode = JsonSourceGenerationMode.Metadata)]
    internal partial class OtpJsResultJsonContext : JsonSerializerContext
    {
    }
}
