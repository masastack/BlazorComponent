namespace BlazorComponent
{
    public partial class BSliderDefaultSlot<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public bool InverseLabel => Component.InverseLabel;
    }
}
