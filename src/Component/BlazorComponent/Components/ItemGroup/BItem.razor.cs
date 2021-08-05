using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BItem : BDomComponentBase
    {
        [Parameter]
        public RenderFragment<ItemContext> ChildContent { get; set; }

        [CascadingParameter]
        public BItemGroup ItemGroup { get; set; }

        public bool IsActive => ItemGroup != null && ItemGroup.Values.Contains(Value);

        [Parameter]
        public StringNumber Value { get; set; }

        public async Task Toggle()
        {
            if (ItemGroup != null)
            {
                await ItemGroup.TogglePanel(Value);
            }
        }

        protected override void OnInitialized()
        {
            if (ItemGroup == null) return;

            if (Value == null)
                Value = ItemGroup.AllKeys.Count;

            ItemGroup.AllKeys.Add(Value);
        }
    }
}
