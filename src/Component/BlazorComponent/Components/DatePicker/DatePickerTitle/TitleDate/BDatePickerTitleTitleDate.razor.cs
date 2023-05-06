namespace BlazorComponent
{
    public partial class BDatePickerTitleTitleDate<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public EventCallback<MouseEventArgs> OnTitleDateBtnClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnTitleDateBtnClickAsync);
    }
}
