namespace BlazorComponent
{
    public class BuilderBase
    {
        internal readonly Dictionary<Func<string?>, Func<bool>> Mapper = new();

        public int Index { get; internal set; }

        public object? Data { get; internal set; }
    }
}
