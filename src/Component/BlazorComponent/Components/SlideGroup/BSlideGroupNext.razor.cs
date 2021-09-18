using System.Diagnostics;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSlideGroupNext<TSlideGroup> : ComponentAbstractBase<TSlideGroup>
        where TSlideGroup : ISlideGroup
    {
        protected string Icon => Component.NextIcon;

        protected bool Active => Component.HasNext;

        protected void HandleOnClick(MouseEventArgs args)
        {
            Component.OnAffixClick("next");
        }
    }
}