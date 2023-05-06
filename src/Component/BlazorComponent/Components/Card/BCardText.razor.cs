namespace BlazorComponent
{
    public partial class BCardText : BDomComponentBase
    {
        [Parameter]
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }
    }
}