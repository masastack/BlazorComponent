namespace BlazorComponent
{
    public partial class BSliderThumbLabel<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        [Parameter]
        public int Index { get; set; }

        public RenderFragment ComputedThumbLabelContent => Component.ComputedThumbLabelContent(Index);

        public bool ShowThumbLabelContainer => Component.ShowThumbLabelContainer;
    }
}
