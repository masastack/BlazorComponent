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

    // TODO: implement
    [Parameter]
    public Func<TColumnItem, bool> ItemDisabled { get; set; }

    // TODO: change to StringNumber, support px, vh, vw, rem
    [Parameter]
    public int ItemHeight { get; set; } = 44;

    [Parameter]
    public EventCallback<List<TColumnItem>> OnSelect { get; set; }

    [Parameter]
    public int SwipeDuration { get; set; } = 1000;

    [Parameter]
    public int VisibleItemCount { get; set; } = 6;

    [Parameter]
    public List<TColumnItemValue> Value
    {
        get => _value;
        set => _value = value ?? new List<TColumnItemValue>();
    }

    [Parameter]
    public EventCallback<List<TColumnItemValue>> ValueChanged { get; set; }

    protected List<MobilePickerColumn<TColumnItem>> FormattedColumns { get; set; } = new();

    private string _dataType;
    private string _prevColumnsStr;
    private string _prevValue;
    private List<TColumnItemValue> _value = new();

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
            InternalValue = Value.ToList();

            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} [Value] Invoke Format() in OnParameterSet()");
            isChanged = true;
        }

        if (_prevColumnsStr is null)
        {
            // TODO: how to watch list
            // var columnsStr = JsonSerializer.Serialize(Columns);
            string columnsStr = string.Empty;

            if (Columns is List<TColumnItem> cascadeColumns)
            {
                columnsStr = JsonSerializer.Serialize(cascadeColumns.Select(ItemValue));
            }
            else if (Columns is List<List<TColumnItem>> listColumns)
            {
                var firstColumn = listColumns.First();
                columnsStr = JsonSerializer.Serialize(firstColumn);
            }

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

            if (InternalValue.Count > i)
            {
                var val = InternalValue[i];
                var itemIndex = column.FindIndex(c => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(c), val));
                if (itemIndex > 0)
                    index = itemIndex;
            }

            FormattedColumns.Add(new MobilePickerColumn<TColumnItem>(column, index));
        }

        InternalValue = FormattedColumns.Select(c => ItemValue(c.Values.ElementAtOrDefault(c.Index))).ToList();
    }

    private class Cursor
    {
        public List<TColumnItem> Children { get; set; }

        public TColumnItemValue? Value { get; set; }

        public int Index { get; set; }
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

        InternalValue = FormattedColumns.Select(c => ItemValue(c.Values.ElementAtOrDefault(c.Index))).ToList();
    }

    private async Task HandleOnChange(int columnIndex, TColumnItemValue value)
    {
        InternalValue[columnIndex] = value;

        if (ValueChanged.HasDelegate)
        {
            _prevValue = JsonSerializer.Serialize(InternalValue);
            await ValueChanged.InvokeAsync(InternalValue.ToList());
        }

        Format();

        var items = FormattedColumns.Select(c => c.Values[c.Index]).ToList();

        if (OnSelect.HasDelegate)
        {
            _ = OnSelect.InvokeAsync(items);
        }
    }
}
