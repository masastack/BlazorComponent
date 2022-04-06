namespace BlazorComponent
{
    public partial class BSelectMenu<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        public IList<TItem> ComputedItems => Component.ComputedItems;

        public object Menu
        {
            set
            {
                Component.Menu = value;
            }
        }
    }
}
