namespace BlazorComponent
{
    public partial class BTimePickerClockAmPm<TTimePicker> where TTimePicker : ITimePicker
    {
        public EventCallback<MouseEventArgs> OnAmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnAmClickAsync);

        public EventCallback<MouseEventArgs> OnPmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnPmClickAsync);

        public string AmText => Component.AmText;
        
        public string PmText => Component.PmText;
    }
}
