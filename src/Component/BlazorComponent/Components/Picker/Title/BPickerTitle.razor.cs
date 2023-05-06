namespace BlazorComponent
{
    public partial class BPickerTitle<TPicker> where TPicker : IPicker
    {
        public RenderFragment? TitleContent => Component.TitleContent;
    }
}
