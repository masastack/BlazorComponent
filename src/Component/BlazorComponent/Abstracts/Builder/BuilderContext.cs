namespace BlazorComponent
{
    public class BuilderContext
    {
        public BuilderContext(int index, object data)
        {
            Index = index;
            Data = data;
        }

        public int Index { get; }

        public object Data { get; }
    }
}
