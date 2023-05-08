using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace BlazorComponent
{
    public partial class BSelectList<TItem, TItemValue, TValue> : BDomComponentBase
    {
        [Parameter]
        public bool HideSelected { get; set; }

        [Parameter, EditorRequired]
        public IList<TItem> Items { get; set; } = null!;

        [Parameter]
        public Func<TItem, bool>? ItemDisabled { get; set; }

        [Parameter, EditorRequired]
        public Func<TItem, string> ItemText { get; set; } = null!;

        [Parameter, EditorRequired]
        public Func<TItem, TItemValue?> ItemValue { get; set; } = null!;

        [Parameter]
        public Func<TItem, bool>? ItemDivider { get; set; }

        [Parameter]
        public Func<TItem, string>? ItemHeader { get; set; }

        [Parameter]
        public string? NoDataText { get; set; }

        [Parameter]
        public IEnumerable<TItem> SelectedItems { get; set; }  = null!;

        [Parameter]
        public RenderFragment? NoDataContent { get; set; }

        [Parameter]
        public RenderFragment? PrependItemContent { get; set; }

        [Parameter]
        public RenderFragment? AppendItemContent { get; set; }

        [Parameter]
        public int SelectedIndex { get; set; }

        private IList<TItemValue?> ParsedItems => SelectedItems.Select(item => ItemValue(item)).ToList();

        protected bool HasItem(TItem item)
        {
            return ParsedItems.IndexOf(ItemValue.Invoke(item)) > -1;
        }
    }
}
