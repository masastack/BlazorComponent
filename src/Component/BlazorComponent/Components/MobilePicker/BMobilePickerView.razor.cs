using System.Text.Json;

namespace BlazorComponent;

public partial class BMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired]
    public virtual List<TColumn> Columns { get; set; } = new();

    [Parameter]
    public virtual Func<TColumnItem, string> ItemText { get; set; }

    [Parameter]
    public virtual Func<TColumnItem, TColumnItemValue> ItemValue { get; set; }

    [Parameter]
    public virtual Func<TColumnItem, List<TColumnItem>> ItemChildren { get; set; }

    [Parameter]
    public Func<TColumnItem, bool> ItemDisabled { get; set; }

    // TODO: change to StringNumber, support px, vh, vw, rem
    [Parameter]
    public int ItemHeight { get; set; } = 44;

    [Parameter]
    public int SwipeDuration { get; set; } = 1000;

    [Parameter]
    public int VisibleItemCount { get; set; } = 6;

    [Parameter]
    public List<TColumnItemValue> Value
    {
        get => _value;
        set
        {
            _value = value;
            InternalValue = value;
        }
    }

    [Parameter]
    public EventCallback<List<TColumnItemValue>> ValueChanged { get; set; }

    protected readonly List<BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue>> Children = new();

    protected List<MobilePickerColumn<TColumnItem>> FormattedColumns { get; set; } = new();

    private string _dataType;
    private string _prevColumnsStr;
    private string _prevValue;
    private List<TColumnItemValue> _value;

    private List<TColumnItemValue> InternalValue { get; set; } = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ItemDisabled ??= _ => false;

        ComputeDataType();

        var isChanged = false;

        var valueStr = JsonSerializer.Serialize(Value);
        if (_prevValue != valueStr)
        {
            _prevValue = valueStr;
            isChanged = true;
        }

        if (!isChanged || _prevColumnsStr is null)
        {
            var columnsStr = JsonSerializer.Serialize(Columns);
            if (_prevColumnsStr != columnsStr)
            {
                _prevColumnsStr = columnsStr;
                isChanged = true;
            }
        }

        if (isChanged)
        {
            Format();
        }
    }

    // TODO: use StringNumber support(px vh vw rem)
    protected int ItemPxHeight => ItemHeight;

    protected int WrapHeight
    {
        get
        {
            var itemHeight = ItemPxHeight;
            var itemCount = VisibleItemCount;

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

        FormattedColumns.Clear();

        for (int i = 0; i < columns.Count; i++)
        {
            var column = columns[i];
            var index = 0;

            if (Value.Count > i)
            {
                var val = Value[i];
                var itemIndex = column.FindIndex(c => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(c), val));
                if (itemIndex > 0)
                    index = itemIndex;
            }

            FormattedColumns.Add(new MobilePickerColumn<TColumnItem>(column, index));
        }
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
        if (InternalValue.Count > columnIndex)
        {
            cursor.Value = InternalValue[columnIndex];
        }

        while (cursor.Children is not null && cursor.Children.Any())
        {
            var children = cursor.Children;

            var index = children.FindIndex(c => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(c), cursor.Value));
            if (index == -1)
            {
                index = 0;
            }

            cursor.Index = index;
            var defaultIndex = index;

            while (children.Count > defaultIndex && ItemDisabled(children[defaultIndex]))
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
            if (InternalValue.Count > columnIndex)
            {
                cursor.Value = InternalValue[columnIndex];
            }
        }

        FormattedColumns = formatted;
    }

    private void OnCascadeChange(int columnIndex)
    {
        var columns = Columns as List<TColumnItem>;
        columns ??= new List<TColumnItem>();

        var cursor = new Cursor { Children = columns };
        var indexes = GetSelectedIndexes();

        for (int i = 0; i <= columnIndex; i++)
        {
            var index = indexes[i];

            if (cursor.Children.Count > index)
            {
                // TODO: refactor with a function?
                // FormattedColumns[columnIndex].Index = index;

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

            int index = 0;

            // TODO: 用一个变量是决定是否之前的index.
            
            if (indexes.Count() > columnIndex)
            {
                index = indexes[columnIndex];
            }

            if (index > cursor.Children.Count - 1)
            {
                index = cursor.Children.Count - 1;
            }

            SetColumnValues(columnIndex, cursor.Children, index);
            cursor = new Cursor { Children = ItemChildren(cursor.Children[0]) };
        }
    }

    private async Task HandleOnChange(int columnIndex)
    {
        if (_dataType == "cascade")
        {
            OnCascadeChange(columnIndex);
        }

        var items = GetItems();
        var values = items.Select(ItemValue).ToList();

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(values);
        }
        else
        {
            InternalValue = values;
        }
    }

    // TODO: rename this
    public void SetColumnValues(int columnIndex, List<TColumnItem> items, int index)
    {
        if (Children.Count > columnIndex)
        {
            FormattedColumns[columnIndex].Values = items;
            FormattedColumns[columnIndex].Index = index;

            var child = Children[columnIndex];
            child.SetIndex(index);
        }
    }

    private List<TColumnItem> GetItems()
    {
        return Children.Select(child => child.GetItem()).ToList();
    }

    private List<int> GetSelectedIndexes()
    {
        return Children.Select(child => child.SelectedIndex).ToList();
    }

    public void Register(BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue> column)
    {
        Children.Add(column);
    }
}
