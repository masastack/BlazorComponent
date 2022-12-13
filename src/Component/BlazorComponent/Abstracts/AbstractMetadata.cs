namespace BlazorComponent
{
    public class AbstractMetadata
    {
        public AbstractMetadata(Type type, Dictionary<string, object>? attributes=null) 
        {
            Type = type;
            Attributes = attributes ?? new();
        }

        public Type Type { get; }

        public Dictionary<string, object> Attributes { get; }
    }
}
