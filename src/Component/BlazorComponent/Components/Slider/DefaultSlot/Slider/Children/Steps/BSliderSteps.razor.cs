namespace BlazorComponent
{
    public partial class BSliderSteps<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public double Step => (double)(dynamic)Component.Step;

        public bool ShowTicks => Component.ShowTicks;

        public double NumTicks => Component.NumTicks;

        public bool Vertical => Component.Vertical;

        public List<string> TickLabels => Component.TickLabels;
    }
}
