namespace BlazorComponent
{
    public static class DataIteratorAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDataIteratorDefault<TItem>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDataIteratorDefaultSlot<,>), typeof(BDataIteratorDefaultSlot<TItem, IDataIterator<TItem>>))
                .Apply(typeof(BDataIteratorItems<,>), typeof(BDataIteratorItems<TItem, IDataIterator<TItem>>))
                .Apply(typeof(BDataIteratorEmpty<,>), typeof(BDataIteratorEmpty<TItem, IDataIterator<TItem>>))
                .Apply(typeof(BDataIteratorEmptyWrapper<,>), typeof(BDataIteratorEmptyWrapper<TItem, IDataIterator<TItem>>))
                .Apply(typeof(BDataIteratorFooter<,>), typeof(BDataIteratorFooter<TItem, IDataIterator<TItem>>));
        }
    }
}
