namespace BlazorComponent
{
    public interface IRangeSlider<TValue> : ISlider<IList<TValue>, TValue>
    {
        ElementReference SecondThumbElement { set; }

        Task HandleOnSecondFocusAsync(FocusEventArgs args);

        Task HandleOnSecondBlurAsync(FocusEventArgs args);
    }
}
