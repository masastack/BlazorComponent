using BlazorComponent.JSInterop;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BCascaderColumn<TItem, TValue>
    {
        [CascadingParameter]
        protected ICascader<TItem, TValue>? Cascader { get; set; }
        
        [Parameter]
        [NotNull]
        [EditorRequired]
        public IList<TItem>? Items { get; set; }

        [Parameter]
        [NotNull]
        [EditorRequired]
        public Func<TItem, string>? ItemText { get; set; }

        [Parameter]
        public Func<TItem, TValue>? ItemValue { get; set; }

        [Parameter]
        public IList<TItem>? SelectedItems { get; set; }

        [Parameter]
        [NotNull]
        [EditorRequired]
        public Func<TItem, List<TItem>?> ItemChildren { get; set; }

        [Parameter]
        public Func<TItem, Task>? LoadChildren { get; set; }

        [Parameter]
        public EventCallback<(TItem item, bool closeOnSelect, int columnIndex)> OnSelect { get; set; }

        [Parameter]
        public int ColumnIndex { get; set; }

        private BCascaderColumn<TItem, TValue>? NextCascaderColumn { get; set; }

        protected virtual string? Icon { get; }

        protected BItemGroup? ItemGroup { get; set; }

        protected TItem? LoadingItem { get; set; }

        protected IList<TItem>? Children { get; set; }

        protected TItem? SelectedItem { get; set; }

        protected int SelectedItemIndex { get; set; }

        private bool IsLast => Children == null || Children.Count == 0;

        private bool IsSelectedItemDefault => EqualityComparer<TItem>.Default.Equals(SelectedItem, default);

        private bool HasChildren => Children is { Count: > 0 } && !IsSelectedItemDefault;

        protected virtual string GetSelectedItemSelector(int index) => string.Empty;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (SelectedItems is not null)
            {
                var selectedItem = SelectedItems.ElementAtOrDefault(ColumnIndex);
                if (selectedItem is not null && Items.Contains(selectedItem))
                {
                    SelectedItem = selectedItem;
                    SelectedItemIndex = Items.IndexOf(selectedItem);
                    Children = ItemChildren(SelectedItem);
                }
            }

            Cascader.Register(this);
        }

        public async Task ScrollToSelected()
        {
            await Js.ScrollIntoParentView(GetSelectedItemSelector(SelectedItemIndex), level: 2);
        }

        /// <summary>
        /// Clear the selection.
        /// </summary>
        internal void Clear()
        {
            SelectedItem = default;
        }

        protected async Task SelectItemAsync(TItem item)
        {
            // clear the child cascader's selection if the item is equal to SelectedItem
            if (EqualityComparer<TItem>.Default.Equals(SelectedItem, item))
            {
                NextCascaderColumn?.Clear();
            }

            SelectedItem = item;
            SelectedItemIndex = Items.IndexOf(SelectedItem);
            Children = ItemChildren(item);

            if (LoadChildren != null && Children != null && Children.Count == 0)
            {
                LoadingItem = item;
                await LoadChildren(item);
                LoadingItem = default;

                Children = ItemChildren(item);
            }

            if (OnSelect.HasDelegate)
            {
                await OnSelect.InvokeAsync((item, IsLast, ColumnIndex));
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            ArgumentNullException.ThrowIfNull(Items);
            ArgumentNullException.ThrowIfNull(ItemChildren);
            ArgumentNullException.ThrowIfNull(ItemText);
        }
    }
}
