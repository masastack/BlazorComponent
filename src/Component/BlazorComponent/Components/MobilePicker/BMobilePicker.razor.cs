namespace BlazorComponent;

public partial class BMobilePicker<TColumnItem, TColumnItemValue> : BDomComponentBase
{
    [Parameter, EditorRequired] public List<List<TColumnItem>> Columns { get; set; }

    [Parameter] public Func<TColumnItem, string> ColumnItemText { get; set; }

    [Parameter] public Func<TColumnItem, TColumnItemValue> ColumnItemValue { get; set; }

    [Parameter] public StringNumber ItemHeight { get; set; } = 44;

    [Parameter] public int SwipeDuration { get; set; } = 1000;

    [Parameter] public StringNumber VisibleItemCount { get; set; } = 6;

    [Parameter] public string Title { get; set; }

    [Parameter] public string CancelText { get; set; }

    [Parameter] public string OkText { get; set; }

    [Parameter] public EventCallback<(List<TColumnItemValue> value, List<int> index)> OnOk { get; set; }

    [Parameter] public EventCallback<(List<TColumnItemValue> value, List<int> index)> OnChange { get; set; }

    [Parameter] public EventCallback OnCancel { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // TODO: how to watch Columns
        FormattedColumns = new List<List<TColumnItem>>(Columns);
    }

    protected readonly List<BMobilePickerColumn<TColumnItem, TColumnItemValue>> Children = new();

    protected List<List<TColumnItem>> FormattedColumns { get; set; } = new();

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

    protected async Task HandleOnOk()
    {
        foreach (var child in Children)
        {
            await child.StopMomentum();
        }

        if (OnOk.HasDelegate)
        {
            var items = GetItems();
            var values = items.Select(ColumnItemValue).ToList();
            var indexes = GetIndexes();

            await OnChange.InvokeAsync((values, indexes));
        }
    }

    protected async Task HandleOnCancel()
    {
        if (OnCancel.HasDelegate)
        {
            await OnCancel.InvokeAsync();
        }
    }

    protected async Task HandleOnChange(int index)
    {
        if (OnChange.HasDelegate)
        {
            var items = GetItems();
            var values = items.Select(ColumnItemValue).ToList();
            var indexes = GetIndexes();

            await OnChange.InvokeAsync((values, indexes));
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

    public void Register(BMobilePickerColumn<TColumnItem, TColumnItemValue> column)
    {
        Children.Add(column);
    }
}
