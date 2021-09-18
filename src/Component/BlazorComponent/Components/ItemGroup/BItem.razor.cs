using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public partial class BItem : BItemBase<BItemGroup>
    {
        public BItem(GroupType groupType) : base(groupType)
        {
        }

        [Parameter]
        public RenderFragment<ItemContext> ChildContent { get; set; }

        public override bool IsActive
        {
            get => _isActive ?? Groupable && ItemGroup.Values.Contains(Value);
            set => _isActive = value;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Ref = RefBack.Current;
            }
        }
    }
}