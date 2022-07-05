using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace BlazorComponent
{
    public partial class BSelectList<TItem, TItemValue, TValue> : BDomComponentBase
    {
        [Parameter]
        public bool HideSelected { get; set; }

        [EditorRequired]
        [Parameter]
        public IList<TItem> Items { get; set; }

        [Parameter]
        public Func<TItem, bool> ItemDisabled { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, string> ItemText { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, TItemValue> ItemValue { get; set; }

        [Parameter]
        public Func<TItem, bool> ItemDivider { get; set; }

        [Parameter] 
        public Func<TItem, string> ItemHeader { get; set; }

        [Parameter]
        public string NoDataText { get; set; }

        [Parameter]
        public IEnumerable<TItem> SelectedItems { get; set; }

        [Parameter]
        public RenderFragment NoDataContent { get; set; }

        [Parameter]
        public RenderFragment PrependItemContent { get; set; }

        [Parameter]
        public RenderFragment AppendItemContent { get; set; }

        [Parameter]
        public int SelectedIndex { get; set; }

        protected IList<TItemValue> ParsedItems => SelectedItems.Select(ItemValue).ToList();

        protected bool HasItem(TItem item)
        {
            return ParsedItems.IndexOf(ItemValue(item)) > -1;
        }
    }
}
