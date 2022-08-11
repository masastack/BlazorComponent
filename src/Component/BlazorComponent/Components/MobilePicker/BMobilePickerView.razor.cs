using System.Text.Json;

namespace BlazorComponent;

public partial class BMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired] public List<TColumn> Columns { get; set; } = new();

    [Parameter] public Func<TColumnItem, string> ItemText { get; set; }

    [Parameter] public Func<TColumnItem, TColumnItemValue> ItemValue { get; set; }

    [Parameter] public Func<TColumnItem, List<TColumnItem>> ItemChildren { get; set; }

    [Parameter] public Func<TColumnItem, bool> ItemDisabled { get; set; }

    [Parameter] public StringNumber ItemHeight { get; set; } = 44;

    [Parameter] public int SwipeDuration { get; set; } = 1000;

    [Parameter] public StringNumber VisibleItemCount { get; set; } = 6;

    [Parameter] public List<TColumnItemValue> Value { get; set; } = new();

    [Parameter] public EventCallback<List<TColumnItemValue>> ValueChanged { get; set; }

    protected readonly List<BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue>> Children = new();

    protected List<MobilePickerColumn<TColumnItem>> FormattedColumns { get; set; } = new();

    private string _dataType;
    private string _prevColumnsStr;
    private string _prevValue;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ItemDisabled ??= _ => false;

        ComputeDataType();

        bool isChanged = false;

        var columnsStr = JsonSerializer.Serialize(Columns);
        if (_prevColumnsStr != columnsStr)
        {
            _prevColumnsStr = columnsStr;
            isChanged = true;
        }

        if (!isChanged)
        {
            var valueStr = JsonSerializer.Serialize(Value);
            if (_prevValue != valueStr)
            {
                _prevValue = valueStr;
                isChanged = true;
            }
        }

        if (isChanged)
        {
            Format();

            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Format()");
        }
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
        if (firstColumn is null)
        {
            return;
        }

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

    private class Cursor
    {
        public List<TColumnItem> Children { get; set; }

        public TColumnItemValue? Value { get; set; }

        public int? Index { get; set; }
    }

    private void FormatCascade()
    {
        if (Columns is not List<TColumnItem> columns)
        {
            return;
        }

        List<MobilePickerColumn<TColumnItem>> formatted = new();

        int columnIndex = 0;

        Cursor cursor = new() { Children = columns };
        if (Value.Count > columnIndex)
        {
            cursor.Value = Value[columnIndex];
        }

        while (cursor?.Children is not null)
        {
            var children = cursor.Children;

            var index = children.FindIndex(c => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(c), cursor.Value));
            if (index == -1)
            {
                index = 0;
            }

            cursor.Index = index;
            var defaultIndex = index;

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

            formatted.Add(new MobilePickerColumn<TColumnItem>(cursor.Children, cursor.Index));

            var columnItem = children[defaultIndex];
            var columnItemChildren = ItemChildren(columnItem);

            columnIndex++;
            cursor = new() { Children = columnItemChildren };
            if (Value.Count > columnIndex)
            {
                cursor.Value = Value[columnIndex];
            }
        }

        FormattedColumns = formatted;
    }

    private void OnCascadeChange(int selectedIndex, int columnIndex)
    {
        var columns = Columns as List<TColumnItem>;

        var cursor = new Cursor { Children = columns };
        var indexes = GetSelectedIndexes();

        for (int i = 0; i <= columnIndex; i++)
        {
            var index = indexes[i];
        
            if (cursor.Children.Count > index)
            {
                cursor = new Cursor { Children = ItemChildren(cursor.Children[index]) };
            }
        }

        while (cursor.Children is not null)
        {
            if (cursor.Children.Count == 0)
            {
                break;
            }
            
            columnIndex++;
            SetColumnValues(columnIndex, cursor.Children);
            cursor = new Cursor { Children = ItemChildren(cursor.Children[0]) };
        }
    }

    protected async Task HandleOnChange(int selectedIndex, int columnIndex)
    {
        if (_dataType == "cascade")
        {
            OnCascadeChange(selectedIndex, columnIndex);
            StateHasChanged();
        }

        var items = GetItems();
        var values = items.Select(ItemValue).ToList();
        var indexes = GetSelectedIndexes();

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(values);
        }
        else
        {
            Value = values;
        }
    }

    public void SetColumnValues(int columnIndex, List<TColumnItem> items)
    {
        if (Children.Count > columnIndex)
        {
            FormattedColumns[columnIndex].Values = items;
            FormattedColumns[columnIndex].Index = 0;

            var child = Children[columnIndex];
            child.SetIndex(0);
        }
    }

    private List<TColumnItem> GetItems()
    {
        return Children.Select(child => child.GetItem()).ToList();
    }

    private List<int> GetSelectedIndexes()
    {
        return Children.Select(child => child.CurrentIndex).ToList();
    }

    // private int GetDefaultIndex(List<TColumnItem> columnItems, int columnIndex)
    // {
    //     if (Value.Count <= columnIndex)
    //     {
    //         return 0;
    //     }
    //
    //     var columnValue = Value[columnIndex];
    //     var index = columnItems.FindIndex(item => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(item), columnValue));
    //
    //     return index > -1 ? index : 0;
    // }

    public void Register(BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue> column)
    {
        Children.Add(column);
    }
}
