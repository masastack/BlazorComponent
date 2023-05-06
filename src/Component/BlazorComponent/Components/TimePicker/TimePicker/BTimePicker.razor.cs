namespace BlazorComponent
{
    public partial class BTimePicker
    {
        [Parameter]
        public bool NoTitle { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
