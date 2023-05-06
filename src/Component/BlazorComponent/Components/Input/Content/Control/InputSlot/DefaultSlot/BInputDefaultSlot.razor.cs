namespace BlazorComponent
{
    public partial class BInputDefaultSlot<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        public bool HasLabel => Component.HasLabel;

        public RenderFragment? ComponentChildContent => Component.ChildContent;

        public string Label => Component.Label;

        public RenderFragment? LabelContent => Component.LabelContent;
    }
}
