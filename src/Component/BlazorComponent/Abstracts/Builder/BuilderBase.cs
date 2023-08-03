namespace BlazorComponent
{
    public class BuilderBase
    {
        internal readonly Dictionary<Func<string?>, Func<bool>> Mapper = new();

        internal string? Prefix { get; private set; }

        public object? Data { get; internal set; }

        public void SetPrefix(string prefix)
        {
            Prefix = prefix;
        }
    }
}
