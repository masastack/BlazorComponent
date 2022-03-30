namespace BlazorComponent
{
    public partial class BTimePickerPickerBody<TTimePicker> where TTimePicker : ITimePicker
    {
        public SelectingTimes Selecting => Component.Selecting;

        public bool AmPmInTitle => Component.AmPmInTitle;

        public bool IsAmPm => Component.IsAmPm;
    }
}
