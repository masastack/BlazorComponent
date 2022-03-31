namespace BlazorComponent
{
    public interface IComponentIdGenerator
    {
        string Generate(BDomComponentBase component);
    }
}
