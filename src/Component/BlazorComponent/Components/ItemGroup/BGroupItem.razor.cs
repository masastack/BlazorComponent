using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract class BGroupItem<TGroup> : BGroupable<TGroup>
        where TGroup : ItemGroupBase
    {
        protected BGroupItem(GroupType groupType) : base(groupType)
        {
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}