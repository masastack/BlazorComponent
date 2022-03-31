using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDataTableDefaultSimpleRow<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public TItem Item { get; set; }

        public Func<TItem, string> ItemKey => Component.ItemKey;

        public bool ShowExpand => Component.ShowExpand;

        public bool ShowSelect => Component.ShowSelect;

        public string ExpandIcon => Component.ExpandIcon;

        public RenderFragment<ItemColProps<TItem>> ItemColContent => Component.ItemColContent;

        public RenderFragment ItemDataTableExpandContent => Component.ItemDataTableExpandContent;

        public RenderFragment ItemDataTableSelectContent => Component.ItemDataTableSelectContent;

        public EventCallback<MouseEventArgs> HandleOnRowClickAsync => CreateEventCallback<MouseEventArgs>(Component.HandleOnRowClickAsync);

        public EventCallback<MouseEventArgs> HandleOnRowContextMenuAsync => CreateEventCallback<MouseEventArgs>(Component.HandleOnRowContextMenuAsync);

        public EventCallback<MouseEventArgs> HandleOnRowDbClickAsync => CreateEventCallback<MouseEventArgs>(Component.HandleOnRowDbClickAsync);
    }
}
