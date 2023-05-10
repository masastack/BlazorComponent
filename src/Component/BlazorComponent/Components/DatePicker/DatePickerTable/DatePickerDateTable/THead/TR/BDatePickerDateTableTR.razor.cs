namespace BlazorComponent
{
    public partial class BDatePickerDateTableTR<TDatePickerDateTable> where TDatePickerDateTable : IDatePickerDateTable
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
