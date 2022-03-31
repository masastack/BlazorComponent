namespace BlazorComponent
{
    public static class CascaderAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyCascaderDefault<TItem, TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BSelectList<,,,>), typeof(BCascaderList<TItem, TValue, ICascader<TItem, TValue>>));
        }
    }
}
