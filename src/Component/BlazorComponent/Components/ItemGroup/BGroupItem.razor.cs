using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract class BGroupItem<TGroup> : BItemBase<TGroup>
        where TGroup : BItemGroup
    {
        protected BGroupItem(GroupType groupType) : base(groupType)
        {
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}