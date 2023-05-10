namespace BlazorComponent
{
    public partial class BCardSubtitle : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}