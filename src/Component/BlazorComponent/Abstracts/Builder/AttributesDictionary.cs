namespace BlazorComponent
{
    public class AttributesDictionary : Dictionary<string, object?>
    {
        public AttributesDictionary()
        {
        }

        public AttributesDictionary(int index)
        {
            Index = index;
        }

        public AttributesDictionary(object? data)
        {
            Data = data;
        }

        public int Index { get; }

        public object? Data { get; }
    }
}
