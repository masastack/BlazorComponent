namespace BlazorComponent
{
    public partial class BMessagesMessage<TMessages> where TMessages : IMessages
    {
        [Parameter]
        public string Message { get; set; } = null!;

        public RenderFragment<string>? ComponentChildContent => Component.ChildContent;
    }
}
