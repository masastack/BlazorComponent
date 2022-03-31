namespace BlazorComponent
{
    public static class AutocompleteAbstractProvider
    {
        public static ComponentAbstractProvider ApplyAutocompleteDefault<TItem, TItemValue, TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BSelectSelections<,,,>), typeof(BAutocompleteSelections<TItem, TItemValue, TValue, IAutocomplete<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectSelections<,,,>), typeof(BSelectSelections<TItem, TItemValue, TValue, IAutocomplete<TItem, TItemValue, TValue>>), name: "base");
        }
    }
}
