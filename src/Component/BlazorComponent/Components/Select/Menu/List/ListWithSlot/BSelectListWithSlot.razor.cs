namespace BlazorComponent
{
    public partial class BSelectListWithSlot<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        protected RenderFragment NoDataContent => Component.NoDataContent;

        protected RenderFragment PrependItemContent => Component.PrependItemContent;

        protected RenderFragment AppendItemContent => Component.AppendItemContent;
    }
}
