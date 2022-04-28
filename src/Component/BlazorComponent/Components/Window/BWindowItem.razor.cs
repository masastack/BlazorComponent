namespace BlazorComponent
{
    public partial class BWindowItem : BGroupItem<ItemGroupBase>
    {
        protected BWindowItem() : base(GroupType.Window)
        {
        }

        protected virtual string ComputedTransition { get; }

        protected virtual Task HandleOnBefore(ElementReference el)
        {
            return Task.CompletedTask;
        }
        
        protected virtual Task HandleOnAfter(ElementReference el)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleOnEnter(ElementReference el)
        {
            return Task.CompletedTask;
        }
    }
}
