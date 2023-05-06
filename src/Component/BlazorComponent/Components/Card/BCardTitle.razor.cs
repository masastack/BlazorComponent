namespace BlazorComponent
{
    public partial class BCardTitle : BDomComponentBase
    {
        [Parameter]
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }
    }
}