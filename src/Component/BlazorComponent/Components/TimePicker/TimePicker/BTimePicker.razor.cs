namespace BlazorComponent
{
    public partial class BTimePicker: BDomComponentBase
    {
        [Parameter]
        public bool NoTitle { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
