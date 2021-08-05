using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract class BGroupItem<TGroup> : BDomComponentBase
        where TGroup : BItemGroup
    {
        [CascadingParameter]
        public TGroup ItemGroup { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public StringNumber Value { get; set; }

        [Parameter]
        public bool NoGroup { get; set; }

        public bool Groupable => !NoGroup && ItemGroup != null;

        public bool IsActive => Groupable && ItemGroup.Values.Contains(Value);

        protected override void OnInitialized()
        {
            if (!Groupable) return;

            if (Value == null)
                Value = ItemGroup.AllKeys.Count;

            ItemGroup.AllKeys.Add(Value);
        }

        protected virtual async Task ToggleItem()
        {
            if (Groupable)
            {
                await ItemGroup.Toggle(Value);
            }
        }
    }
}
