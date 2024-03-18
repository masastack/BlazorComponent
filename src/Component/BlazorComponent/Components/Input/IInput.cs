namespace BlazorComponent
{
    public interface IInput<TValue> : IHasProviderComponent
    {
        TValue? Value { get; }

        RenderFragment? AppendContent
        {
            get
            {
                return default;
            }
        }

        string? AppendIcon
        {
            get
            {
                return default;
            }
        }

        RenderFragment? ChildContent { get; }

        string? Id { get; }

        string? Label => default;

        RenderFragment? LabelContent => default;

        bool HasLabel => default;

        bool ShowDetails => default;

        RenderFragment? PrependContent
        {
            get
            {
                return default;
            }
        }

        string? PrependIcon
        {
            get
            {
                return default;
            }
        }

        ElementReference InputSlotElement
        {
            get
            {
                return default;
            }
            set
            {
                //default todo nothing
            }
        }

        TValue InternalValue { get; }

        bool HasPrependClick { get; }

        bool HasAppendClick { get; }

        Task HandleOnPrependClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnAppendClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnMouseDownAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
