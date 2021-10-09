using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BWindowItem : BGroupItem<BItemGroup>
    {
        public BWindowItem() : base(GroupType.Window)
        {
        }

        [Parameter]
        public bool Disabled { get; set; }
    }
}