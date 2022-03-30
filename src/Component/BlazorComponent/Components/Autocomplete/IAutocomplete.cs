namespace BlazorComponent
{
    public interface IAutocomplete<TItem, TItemValue, TValue> : ISelect<TItem, TItemValue, TValue>
    {
        bool HasSlot { get; }
    }
}
