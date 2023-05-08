namespace BlazorComponent
{
    public partial class BCardTitle : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}