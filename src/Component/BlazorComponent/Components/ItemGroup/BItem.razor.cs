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

        protected RenderFragment ComputedChildContent => ChildContent(GenItemContext());

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Ref = RefBack.Current;
            }
        }

        private ItemContext GenItemContext()
        {
            return new ItemContext()
            {
                Active = IsActive,
                ActiveClass = IsActive ? ComputedActiveClass : "",
                Toggle = ToggleItem,
                Ref = RefBack,
                Value = Value
            };
        }
    }
}