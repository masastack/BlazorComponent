namespace BlazorComponent
{
    public partial class BDatePickerHeaderBtn<TDatePickerHeader>
        where TDatePickerHeader : IDatePickerHeader
    {
        [Parameter]
        public int Change { get; set; }

        public bool RTL => Component.RTL;

        public string PrevIcon => Component.PrevIcon;

        public string NextIcon => Component.NextIcon;
    }
}
