using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BItem : BGroupable<ItemGroupBase>
    {
        public BItem(GroupType groupType) : base(groupType)
        {
        }

        [Parameter]
        public RenderFragment<ItemContext> ChildContent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Ref = RefBack.Current;
            }
        }
    }
}