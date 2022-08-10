namespace BlazorComponent;

public class MobilePickerColumn<TItem>
{
    public List<TItem> Values { get; set; }

    public int? DefaultIndex { get; set; }

    public string ClassName { get; set; }

    public MobilePickerColumn()
    {
    }

    public MobilePickerColumn(List<TItem> values)
    {
        Values = values;
    }

    public MobilePickerColumn(List<TItem> values, int? defaultIndex): this(values)
    {
        DefaultIndex = defaultIndex;
    }

    public MobilePickerColumn(List<TItem> values, int? defaultIndex, string className): this(values, defaultIndex)
    {
        ClassName = className;
    }
}
