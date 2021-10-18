namespace BlazorComponent
{
    public static class SimpleTableAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplySimpleTableDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BSimpleTableWrapper<>), typeof(BSimpleTableWrapper<ISimpleTable>));
        }
    }
}
