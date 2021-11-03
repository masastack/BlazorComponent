using System.Diagnostics;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSlideGroupNext<TSlideGroup> : ComponentAbstractBase<TSlideGroup>
        where TSlideGroup : ISlideGroup
    {
        protected bool Active => Component.HasNext;

        protected string Icon => Component.NextIcon;

        protected bool Visible => !(Component.ShowArrows == null && !Active);

        protected void HandleOnClick(MouseEventArgs args)
        {
            Component.OnAffixClick("next");
        }
    }
}