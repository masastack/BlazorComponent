namespace BlazorComponent
{
    public interface IAbstractComponentTypeMapper
    {
        Type Map(Type keyType);
    }
}
