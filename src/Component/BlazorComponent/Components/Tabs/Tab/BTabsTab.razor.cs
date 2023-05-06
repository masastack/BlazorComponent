namespace BlazorComponent
{
    public partial class BTabsTab<TTabs> : ComponentPartBase<TTabs>
        where TTabs : ITabs
    {
        protected RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
