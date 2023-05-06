namespace BlazorComponent
{
    public partial class BCardActions : BDomComponentBase
    {
        [Parameter]
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }
    }
}