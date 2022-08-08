using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public partial class BMobilePicker<TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired] public List<List<TColumnItem>> Columns { get; set; }

    [Parameter] public Func<TColumnItem, string> ColumnItemText { get; set; }

    [Parameter] public Func<TColumnItem, TColumnItemValue> ColumnItemValue { get; set; }

    [Parameter] public StringNumber ItemHeight { get; set; } = 44;

    [Parameter] public StringNumber ItemCount { get; set; } = 6;

    [Parameter] public string Title { get; set; }

    [Parameter] public string CancelText { get; set; }

    [Parameter] public string OkText { get; set; }

    protected List<List<TColumnItem>> FormattedColumns { get; set; } = new();

    protected int ItemPxHeight => ItemHeight.ToInt32();

    protected int WrapHeight
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
            return (WrapHeight / 2) - itemHeight / 2;
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // TODO: how to watch Columns
        FormattedColumns = new List<List<TColumnItem>>(Columns);
    }

    protected string GetColumnItemText(TColumnItem columnItem)
    {
        return ColumnItemText?.Invoke(columnItem) ?? columnItem.ToString();
    }

    protected TColumnItemValue GetItemValue(TColumnItem columnItem)
    {
        if (ColumnItemValue is not null)
        {
            return ColumnItemValue(columnItem);
        }

        if (columnItem is TColumnItemValue value)
        {
            return value;
        }

        return default;
    }
}
