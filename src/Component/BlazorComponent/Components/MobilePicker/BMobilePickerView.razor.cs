namespace BlazorComponent;

public partial class BMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired] public List<TColumn> Columns { get; set; } = new();

    [Parameter] public Func<TColumnItem, string> ItemText { get; set; }

    [Parameter] public Func<TColumnItem, TColumnItemValue> ItemValue { get; set; }

    [Parameter] public Func<TColumnItem, List<TColumnItem>> ItemChildren { get; set; }

    [Parameter] public Func<TColumnItem, bool> ItemDisabled { get; set; }

    [Parameter] public StringNumber ItemHeight { get; set; } = 44;

    [Parameter] public StringNumber DefaultIndex { get; set; }

    [Parameter] public int SwipeDuration { get; set; } = 1000;

    [Parameter] public StringNumber VisibleItemCount { get; set; } = 6;

    [Parameter] public List<TColumnItemValue> Value { get; set; }

    [Parameter] public EventCallback<List<TColumnItemValue>> ValueChanged { get; set; }

    protected readonly List<BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue>> Children = new();

    protected List<MobilePickerColumn<TColumnItem>> FormattedColumns { get; set; } = new();

    private string _dataType;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ItemDisabled ??= _ => false;
        DefaultIndex ??= 0;

        ComputeDataType();
        
        Format();
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
            FormatList();
        }
        else if (_dataType == "cascade")
        {
            FormatCascade();
        }
    }

    private void FormatList()
    {
        if (Columns is not List<List<TColumnItem>> columns)
        {
            return;
        }

        FormattedColumns = columns.Select(c => new MobilePickerColumn<TColumnItem>(c)).ToList();
    }

    private void FormatCascade()
    {
        if (Columns is not List<TColumnItem> columns)
        {
            return;
        }

        List<MobilePickerColumn<TColumnItem>> formatted = new();

        var cursor = new { Children = columns, DefaultIndex = (int?)null };

        while (cursor?.Children is not null)
        {
            var children = cursor.Children;
            var defaultIndex = cursor.DefaultIndex ?? DefaultIndex.ToInt32();

            while (children.Count > defaultIndex  && ItemDisabled(children[defaultIndex]))
            {
                if (defaultIndex < children.Count - 1)
                {
                    defaultIndex++;
                }
                else
                {
                    defaultIndex = 0;
                    break;
                }
            }

            formatted.Add(new MobilePickerColumn<TColumnItem>(cursor.Children));
        }

        FormattedColumns = formatted;
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

    public void Register(BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue> column)
    {
        Children.Add(column);
    }
}
