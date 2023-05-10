namespace BlazorComponent
{
    public partial class BInputLabel<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        public bool HasLabel => Component.HasLabel;

        public string? Label => Component.Label;

        public RenderFragment? LabelContent => Component.LabelContent;
    }
}
