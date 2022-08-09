namespace BlazorComponent;

public partial class BMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired] public List<TColumn> Columns { get; set; } = new();

    [Parameter] public Func<TColumnItem, string> ItemText { get; set; }

    [Parameter] public Func<TColumnItem, TColumnItemValue> ItemValue { get; set; }

    [Parameter] public Func<TColumnItem, List<TColumnItem>> ItemChildren { get; set; }

    [Parameter] public StringNumber ItemHeight { get; set; } = 44;

    [Parameter] public int SwipeDuration { get; set; } = 1000;

    [Parameter] public StringNumber VisibleItemCount { get; set; } = 6;

    [Parameter] public List<TColumnItemValue> Value { get; set; }

    [Parameter] public EventCallback<List<TColumnItemValue>> ValueChanged { get; set; }

    protected readonly List<BMobilePickerColumn<TColumnItem, TColumnItemValue>> Children = new();

    protected List<List<TColumnItem>> FormattedColumns { get; set; } = new();

    private string _dataType;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ComputeDataType();

        // TODO: how to watch Columns
        FormattedColumns = new List<List<TColumnItem>>(Columns);
    }

    protected int ItemPxHeight => ItemHeight.ToInt32();

    protected int WrapHeight
    {
        get
        {
            var itemHeight = ItemHeight.ToInt32();
            var itemCount = VisibleItemCount.ToInt32();

            return itemHeight * itemCount;
        }
    }

    private void ComputeDataType()
    {
        var firstColumn = Columns.FirstOrDefault();
        if (ItemChildren is not null)
        {
            if (firstColumn is TColumnItem)
            {
                _dataType = "cascade";
            }
        }
        else
        {
            if (firstColumn is IEnumerable<TColumnItem>)
            {
                _dataType = "list";
            }
        }
    }

    private void Format()
    {
        if (_dataType == "list")
        {
            // FormattedColumns = new List<List<TColumnItem>>(Columns);
        }
        else if (_dataType == "cascade")
        {
        }
    }

    private void FormatCascade()
    {
        List<TColumnItem> formatted = new();

        foreach (TColumn column in Columns)
        {
            if (column is TColumnItem columnItem)
            {
                var children = ItemChildren(columnItem);


                formatted.Add(columnItem);
            }
        }

        FormattedColumns.Add(formatted);

        // (int, List<TColumnItem>) format(TColumnItem item, int index)
        // {
        //     var res = new List<TColumnItem>();
        //
        //     var children = ItemChildren(item);
        //     if (children is null || children.Count == 0)
        //     {
        //         res.Add(item);
        //     }
        //
        //     index++;
        //     foreach (var child in children)
        //     {
        //        var (i, list) = format(child, index);
        //        
        //     }
        // }
    }

    protected async Task HandleOnChange(int index)
    {
        var items = GetItems();
        var values = items.Select(ItemValue).ToList();
        var indexes = GetIndexes();

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(values);
        }
    }

    private List<TColumnItem> GetItems()
    {
        return Children.Select(child => child.GetItem()).ToList();
    }

    private List<int> GetIndexes()
    {
        return Children.Select(child => child.CurrentIndex).ToList();
    }

    private int GetDefaultIndex(List<TColumnItem> columnItems, int columnIndex)
    {
        if (Value.Count <= columnIndex)
        {
            return 0;
        }

        var columnValue = Value[columnIndex];
        var index = columnItems.FindIndex(item => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(item), columnValue));

        return index > -1 ? index : 0;
    }

    public void Register(BMobilePickerColumn<TColumnItem, TColumnItemValue> column)
    {
        Children.Add(column);
    }
}
