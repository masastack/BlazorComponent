namespace BlazorComponent
{
    public partial class BPickerActions<TPicker> where TPicker : IPicker
    {
        public RenderFragment? ActionsContent => Component.ActionsContent;
    }
}
