namespace BlazorComponent
{
    public partial class BTextFieldLabel<TValue>
    {
        public bool ShowLabel => Component.ShowLabel;

        public BLabel LabelReference
        {
            set
            {
                Component.LabelReference = value;
            }
        }
    }
}
