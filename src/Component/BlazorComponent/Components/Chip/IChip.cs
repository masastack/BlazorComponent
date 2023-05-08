namespace BlazorComponent
{
    public interface IChip : IHasProviderComponent
    {
        RenderFragment? ChildContent { get; }

        bool Filter => default;

        string? FilterIcon => default;

        bool InternalIsActive => default;

        bool Close => default;

        string? CloseIcon => default;
    }
}
