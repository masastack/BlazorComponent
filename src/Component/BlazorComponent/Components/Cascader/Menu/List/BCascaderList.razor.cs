namespace BlazorComponent;

public partial class BCascaderList<TItem, TValue, TInput> where TInput : ICascader<TItem, TValue>
{
    protected IList<TItem> ComputedItems => Component.ComputedItems;
}