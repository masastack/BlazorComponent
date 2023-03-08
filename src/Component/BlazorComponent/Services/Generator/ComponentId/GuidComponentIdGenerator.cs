namespace BlazorComponent
{
    public class GuidComponentIdGenerator : IComponentIdGenerator
    {
        public string? Prefix { get; set; }

        public string Generate(BDomComponentBase component) => $"{Prefix ?? "B-" }{Guid.NewGuid()}";
    }
}
