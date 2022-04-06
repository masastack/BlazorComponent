namespace BlazorComponent
{
    public partial class BAutocompleteSelections<TItem, TItemValue, TValue, TInput> where TInput : IAutocomplete<TItem, TItemValue, TValue>
    {
        protected bool Multiple => Component.Multiple;

        protected bool HasSlot => Component.HasSlot;
    }
}
