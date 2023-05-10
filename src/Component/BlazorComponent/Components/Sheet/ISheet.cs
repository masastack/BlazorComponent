namespace BlazorComponent
{
    public interface ISheet : IHasProviderComponent
    {
        RenderFragment? ChildContent { get; }
    }
}
