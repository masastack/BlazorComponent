namespace BlazorComponent
{
    public partial class BDataIteratorDefaultSlot<TItem, TDataIterator> where TDataIterator : IDataIterator<TItem>
    {
        public RenderFragment? HeaderContent => Component.HeaderContent;

        public RenderFragment? FooterContent => Component.FooterContent;

        public ElementReference ElementReference
        {
            set
            {
                Component.Ref = value;
            }
        }
    }
}
