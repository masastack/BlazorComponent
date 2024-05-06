namespace BlazorComponent
{
    public partial class BWindowItem : BGroupItem<ItemGroupBase>, IWindowItem
    {
        protected BWindowItem() : base(GroupType.Window, bootable: true)
        {
        }

        /// <summary>
        /// just to refresh the component.
        /// </summary>
        [CascadingParameter(Name = "WindowValue")]
        public string? WindowValue { get; set; }

        [Parameter]
        public bool Eager { get; set; }
        
        /// <summary>
        /// Internal use
        /// </summary>
        public virtual string Tag { get; set; }

        protected override bool IsEager => Eager;

        protected virtual string? ComputedTransition { get; }

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
