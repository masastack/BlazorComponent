using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSlideGroupPrev<TSlideGroup> : ComponentAbstractBase<TSlideGroup>
        where TSlideGroup : ISlideGroup
    {
        protected bool Active => Component.HasPrev;

        protected string Icon => Component.PrevIcon;

        protected bool Visible => !(Component.ShowArrows == null && !Active);

        protected void HandleOnClick(MouseEventArgs args)
        {
            Component.OnAffixClick("prev");
        }
    }
}