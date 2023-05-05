namespace BlazorComponent
{
    public class BuilderBase
    {
        internal readonly Dictionary<Func<string?>, Func<bool>> _mapper = new();

        public int Index { get; internal set; }

        public object? Data { get; internal set; }
    }
}
