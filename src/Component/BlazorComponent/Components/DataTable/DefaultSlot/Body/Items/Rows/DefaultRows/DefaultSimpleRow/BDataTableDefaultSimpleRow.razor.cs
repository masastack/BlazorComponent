namespace BlazorComponent
{
    public partial class BDataTableDefaultSimpleRow<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public TItem Item { get; set; } = default!;

        public bool IsMobile => Component.IsMobile;

        public Func<TItem, string>? ItemKey => Component.ItemKey;

        public bool ShowExpand => Component.ShowExpand;

        public bool ShowSelect => Component.ShowSelect;

        public string ExpandIcon => Component.ExpandIcon;
        
        public RenderFragment<DataTableHeader>? HeaderColContent => Component.HeaderColContent;

        public RenderFragment<ItemColProps<TItem>>? ItemColContent => Component.ItemColContent;

        public RenderFragment? ItemDataTableExpandContent => Component.ItemDataTableExpandContent;

        public RenderFragment? ItemDataTableSelectContent => Component.ItemDataTableSelectContent;

        private bool IsSelected => Component.IsSelected(Item);

        private bool IsExpanded => Component.IsExpanded(Item);

        public bool OnRowContextmenuPreventDefault => Component.OnRowContextmenuPreventDefault;

        private async Task HandleOnRowClickAsync(MouseEventArgs args)
        {
            var rowMouseEventArgs = new DataTableRowMouseEventArgs<TItem>(Item, IsMobile, IsSelected, IsExpanded, args);
            await Component.HandleOnRowClickAsync(rowMouseEventArgs);
        }

        private async Task HandleOnRowContextmenuAsync(MouseEventArgs args)
        {
            var rowMouseEventArgs = new DataTableRowMouseEventArgs<TItem>(Item, IsMobile, IsSelected, IsExpanded, args);
            await Component.HandleOnRowContextmenuAsync(rowMouseEventArgs);
        }

        private async Task HandleOnRowDblClickAsync(MouseEventArgs args)
        {
            var rowMouseEventArgs = new DataTableRowMouseEventArgs<TItem>(Item, IsMobile, IsSelected, IsExpanded, args);
            await Component.HandleOnRowDblClickAsync(rowMouseEventArgs);
        }

        private bool HasExpand(ItemColProps<TItem> props) => props.Header.Value == "data-table-expand" && ShowExpand;

        private bool HasSelect(ItemColProps<TItem> props) => props.Header.Value == "data-table-select" && ShowSelect;

        private bool HasItemColContent() => ItemColContent is not null;

        private bool HasSlot(ItemColProps<TItem> props)
        {
            return HasExpand(props) || HasSelect(props) || HasItemColContent();
        }
    }
}
