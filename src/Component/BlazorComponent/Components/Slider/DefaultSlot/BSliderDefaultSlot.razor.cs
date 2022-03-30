namespace BlazorComponent
{
    public partial class BSliderDefaultSlot<TValue, TInput> where TInput : ISlider<TValue>
    {
        public bool InverseLabel => Component.InverseLabel;
    }
}
