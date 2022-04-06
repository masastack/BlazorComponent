namespace BlazorComponent
{
    public partial class BSliderSteps<TValue, TInput> where TInput : ISlider<TValue>
    {
        public double Step => Component.Step;

        public bool ShowTicks => Component.ShowTicks;

        public double NumTicks => Component.NumTicks;

        public bool Vertical => Component.Vertical;

        public List<string> TickLabels => Component.TickLabels;
    }
}
