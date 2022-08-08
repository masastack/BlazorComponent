using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public partial class BMobilePicker<TItem, TItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired] public List<List<TItem>> Items { get; set; }

    [Parameter] public Func<TItem, string> ItemText { get; set; }

    [Parameter] public Func<TItem, TItemValue> ItemValue { get; set; }

    [Parameter] public StringNumber ItemHeight { get; set; } = 44;

    [Parameter] public StringNumber ItemCount { get; set; } = 6;

    [Parameter] public string Title { get; set; }

    [Parameter] public string CancelText { get; set; }

    [Parameter] public string OkText { get; set; }

    protected int ComputedItemHeight => ItemHeight.ToInt32();

    protected int ComputedHeight
    {
        get
        {
            var itemHeight = ItemHeight.ToInt32();
            var itemCount = ItemCount.ToInt32();

            return itemHeight * itemCount;
        }
    }

    protected int ComputedTop
    {
        get
        {
            var itemHeight = ItemHeight.ToInt32();
            return (ComputedHeight / 2) - itemHeight / 2;
        }
    }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NormalizeNormalColumns(Items);
    }

    protected string GetItemText(TItem item)
    {
        return ItemText?.Invoke(item) ?? item.ToString();
    }

    protected TItemValue GetItemValue(TItem item)
    {
        if (ItemValue is not null)
        {
            return ItemValue(item);
        }

        if (item is TItemValue value)
        {
            return value;
        }

        return default;
    }

    private int sid = 0;
    private void NormalizeNormalColumns(List<List<TItem>> items)
    {
        foreach (var (column, index) in items.Select((column, index) => (column, index)))
        {
            var normalColumn = column;
            var scrollColumn = new
            {
                id = sid++,
                prevY = -1,
                momentumPrevY = -1,
                touching = false,
                translate = ComputedTop,
                // index = normalColumn.initialIndex ?? 0,
                columnIndex = index,
                duration = 0,
                momentumTime = 0,
                column = normalColumn,
            };
            
            
        }
    }

    private void HandleTouchstart(TouchEventArgs args)
    {
        
    }

    private void HandleTouchmove(TouchEventArgs args)
    {
        
    }

    private void HandleTouchend(TouchEventArgs args)
    {
        
    }
}
