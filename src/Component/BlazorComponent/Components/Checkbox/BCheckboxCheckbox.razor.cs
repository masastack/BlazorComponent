namespace BlazorComponent
{
    public partial class BCheckboxCheckbox<TInput, TValue> where TInput: ICheckbox<TValue>
    {
        public string ComputedIcon => Component.ComputedIcon;
    }
}
