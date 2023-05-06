namespace BlazorComponent
{
    public interface ISimpleTable : IHasProviderComponent
    {
        RenderFragment? WrapperContent { get; }

        RenderFragment? ChildContent { get; }

        Task HandleOnScrollAsync(EventArgs args);

        ElementReference WrapperElement { set; }
    }
}
