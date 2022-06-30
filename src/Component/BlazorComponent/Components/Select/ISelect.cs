using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ISelect<TItem, TItemValue, TValue> : ITextField<TValue>
    {
        bool Multiple { get; }
        IList<TItemValue> InternalValues { get; }
        IList<TItem> ComputedItems { get; }
        IList<TItem> SelectedItems { get; }
        int SelectedIndex { get; set; }
        object Menu { set; }
        bool HasChips { get; }
        RenderFragment PrependItemContent { get; }
        RenderFragment AppendItemContent { get; }
        RenderFragment<SelectSelectionProps<TItem>> SelectionContent { get; }
        RenderFragment NoDataContent { get; }
        string GetText(TItem item);
        TItemValue GetValue(TItem item);
        bool GetDisabled(TItem item);
    }
}
