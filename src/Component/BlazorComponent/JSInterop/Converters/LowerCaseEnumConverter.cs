using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorComponent.JSInterop;

public class LowerCaseEnumConverter : JsonStringEnumConverter
{
    public LowerCaseEnumConverter() : base(new ToLowerNamingPolicy())
    {
    }

    private class ToLowerNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}
