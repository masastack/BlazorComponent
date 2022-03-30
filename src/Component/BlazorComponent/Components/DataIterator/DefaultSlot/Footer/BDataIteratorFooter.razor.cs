namespace BlazorComponent
{
    public partial class BDataIteratorFooter<TItem, TComponent> where TComponent : IDataIterator<TItem>
    {
        public bool HideDefaultFooter => Component.HideDefaultFooter;
    }
}
