namespace BlazorComponent
{
    public partial class BMessagesMessage<TMessages> where TMessages : IMessages
    {
        [Parameter]
        public string Message { get; set; }

        public RenderFragment<string> ComponentChildContent => Component.ChildContent;
    }
}
