namespace BlazorComponent
{
    public class AbstractMetadata
    {
        public AbstractMetadata(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public AbstractMetadata(Type type, Dictionary<string, object> attributes)
            : this(type)
        {
            Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }

        public Type Type { get; }

        public Dictionary<string, object> Attributes { get; }
    }
}
