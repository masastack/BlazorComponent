namespace BlazorComponent
{
    public partial class BSlideGroupPrev<TSlideGroup> : ComponentPartBase<TSlideGroup>
        where TSlideGroup : ISlideGroup
    {
        protected bool Active => Component.HasPrev;

        protected string? Icon => Component.PrevIcon;

        protected RenderFragment? IconContent => Component.PrevContent;

        protected bool Visible => !(Component.ShowArrows == null && !Active);

        protected Task HandleOnClick(MouseEventArgs args)
        {
            return Component.OnAffixClick("prev");
        }
    }
}