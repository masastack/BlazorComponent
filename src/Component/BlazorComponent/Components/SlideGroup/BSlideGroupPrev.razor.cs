using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSlideGroupPrev<TSlideGroup> : ComponentAbstractBase<TSlideGroup>
        where TSlideGroup : ISlideGroup
    {
        protected string Icon => Component.PrevIcon;
        
        protected bool Active => Component.HasPrev;

        protected void HandleOnClick(MouseEventArgs args)
        {
            Component.OnAffixClick("prev");
        }
    }
}