using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract class BGroupItem<TGroup> : BDomComponentBase
        where TGroup : BItemGroup
    {
        /// <summary>
        /// Set by [Parameter]IsActive if has value.
        /// </summary>
        private bool? _isActive;

        [CascadingParameter]
        public TGroup ItemGroup { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public StringNumber Value { get; set; }

        [Parameter]
        public bool NoGroup { get; set; }

        public bool Groupable => !NoGroup && ItemGroup != null;

        [Parameter]
        public bool IsActive
        {
            get => _isActive.HasValue ? _isActive.Value : Groupable && ItemGroup.Values.Contains(Value);
            set => _isActive = value;
        }

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
