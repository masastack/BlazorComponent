using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BWindowItem : BGroupItem<ItemGroupBase>
    {
        public BWindowItem() : base(GroupType.Window)
        {
        }
    }
}