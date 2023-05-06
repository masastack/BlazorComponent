namespace BlazorComponent
{
    public partial class BTimePickerTitleAmPm<TTimePickerTitle> where TTimePickerTitle : ITimePickerTitle
    {
        public bool AmPmReadonly => Component.AmPmReadonly;

        public TimePeriod Period => Component.Period;

        public EventCallback<MouseEventArgs> OnAmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnAmClickAsync);

        public EventCallback<MouseEventArgs> OnPmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnPmClickAsync);

        public string AmText => Component.AmText;

        public  string PmText => Component.PmText;
    }
}
