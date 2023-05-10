namespace BlazorComponent
{
    public partial class BTimePickerClockValues<TTimePickerClock> where TTimePickerClock : ITimePickerClock
    {
        public int Min => Component.Min;

        public int Max => Component.Max;

        public int Step => Component.Step;

        public Func<int, string>? Format => Component.Format;
    }
}
