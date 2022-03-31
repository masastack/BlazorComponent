namespace BlazorComponent
{
    public interface ICard : IHasProviderComponent, ILoadable, ISheet
    {
        string Tag { get; }
    }
}
